using System;
using System.IO;
using System.Reflection;
using System.Runtime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame.RapaduraEngine;

public class Engine : Game
{
	public static Engine Instance { get; private set; }

	public static GraphicsDeviceManager Graphics { get; private set; }

	public static int Width { get; private set; }

	public static int Height { get; private set; }

	public static int ViewWidth { get; private set; }

	public static int ViewHeight { get; private set; }

	public static int ViewPadding
	{
		get => viewPadding;
		set
		{
			viewPadding = value;
			Instance.UpdateView();
		}
	}

	public static float DeltaTime { get; private set; }

	public static float RawDeltaTime { get; private set; }

	public static ulong FrameCounter { get; private set; }

	public static string ContentDirectory => Path.Combine(AssemblyDirectory, Instance.Content.RootDirectory);
	
	public static Random Random { get; set; }

	protected Engine(int width, int height, int windowWidth, int windowHeight, string windowTitle, bool fullscreen, bool vsync)
	{
		Instance = this;
		Window.Title = windowTitle;
		Title = windowTitle;
		Width = width;
		Height = height;
		ClearColor = Color.Black;
		Random = new Random();
		InactiveSleepTime = new TimeSpan(0L);
		Graphics = new GraphicsDeviceManager(this);
		Graphics.DeviceReset += OnGraphicsReset;
		Graphics.DeviceCreated += OnGraphicsCreate;
		Graphics.SynchronizeWithVerticalRetrace = vsync;
		Graphics.PreferMultiSampling = false;
		Graphics.GraphicsProfile = GraphicsProfile.HiDef;
		Graphics.PreferredBackBufferFormat = SurfaceFormat.Color;
		Graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
		Window.AllowUserResizing = true;
		Window.ClientSizeChanged += OnClientSizeChanged;
		if (fullscreen)
		{
			Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
			Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			Graphics.IsFullScreen = true;
		}
		else
		{
			Graphics.PreferredBackBufferWidth = windowWidth;
			Graphics.PreferredBackBufferHeight = windowHeight;
			Graphics.IsFullScreen = false;
		}

		Content.RootDirectory = "Content";
		IsMouseVisible = false;
		ExitOnEscapeKeypress = true;
		GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
	}

	#region AddOns stab

	private void OnClientSizeChanged(object sender, EventArgs e)
	{
		if (Window.ClientBounds is not { Width: > 0, Height: > 0 } || resizing)
			return;
		
		resizing = true;
		Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
		Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
		UpdateView();
		resizing = false;
	}

	protected virtual void OnGraphicsReset(object sender, EventArgs e)
	{
		UpdateView();
		scene?.HandleGraphicsReset();

		if (nextScene != null && nextScene != scene)
		{
			nextScene.HandleGraphicsReset();
		}
	}

	protected virtual void OnGraphicsCreate(object sender, EventArgs e)
	{
		UpdateView();
		scene?.HandleGraphicsCreate();

		if (nextScene != null && nextScene != scene)
		{
			nextScene.HandleGraphicsCreate();
		}
	}

	protected override void OnActivated(object sender, EventArgs args)
	{
		base.OnActivated(sender, args);
		scene?.GainFocus();
	}

	protected override void OnDeactivated(object sender, EventArgs args)
	{
		base.OnDeactivated(sender, args);
		scene?.LoseFocus();
	}

	#endregion
	
	protected override void Update(GameTime gameTime)
	{
		RawDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
		DeltaTime = RawDeltaTime * TimeRate * TimeRateB;
		FrameCounter += 1UL;
		if (ExitOnEscapeKeypress && Keyboard.GetState().IsKeyDown(Keys.Escape))
		{
			Exit();
			return;
		}

		if (OverloadGameLoop != null)
		{
			OverloadGameLoop();
			base.Update(gameTime);
			return;
		}

		if (scene != nextScene)
		{
			var from = scene;
			from?.End();

			scene = nextScene;
			OnSceneTransition(from, nextScene);
			
			scene?.Begin();
		}

		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		RenderCore();
		base.Draw(gameTime);

		fpsCounter++;
		counterElapsed += gameTime.ElapsedGameTime;

		if (counterElapsed < TimeSpan.FromSeconds(1.0))
			return;

		FPS = fpsCounter;
		fpsCounter = 0;
		counterElapsed -= TimeSpan.FromSeconds(1.0);
	}

	protected virtual void RenderCore()
	{
		scene?.BeforeRender();

		GraphicsDevice.SetRenderTarget(null);
		GraphicsDevice.Viewport = Viewport;
		GraphicsDevice.Clear(ClearColor);
		
		if (scene == null)
			return;
		
		scene.Render();
		scene.AfterRender();
	}

	public void RunGame()
	{
		try
		{
			Run();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}
	}

	protected virtual void OnSceneTransition(Scene from, Scene to)
	{
		GC.Collect();
		GC.WaitForPendingFinalizers();
		TimeRate = 1f;
	}
	
	public static Scene Scene
	{
		get => Instance.scene;
		set => Instance.nextScene = value;
	}
	
	public static Viewport Viewport { get; private set; }

	public static void SetWindowed(int width, int height)
	{
		if (width > 0 && height > 0)
		{
			resizing = true;
			Graphics.PreferredBackBufferWidth = width;
			Graphics.PreferredBackBufferHeight = height;
			Graphics.IsFullScreen = false;
			Graphics.ApplyChanges();
			Console.WriteLine(string.Concat("WINDOW-", width, "x", height));
			resizing = false;
		}
	}

	public static void SetFullscreen()
	{
		resizing = true;
		Graphics.PreferredBackBufferWidth = Graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
		Graphics.PreferredBackBufferHeight = Graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
		Graphics.IsFullScreen = true;
		Graphics.ApplyChanges();
		resizing = false;
	}

	private void UpdateView()
	{
		var num = (float)GraphicsDevice.PresentationParameters.BackBufferWidth;
		var num2 = (float)GraphicsDevice.PresentationParameters.BackBufferHeight;
		if (num / Width > num2 / Height)
		{
			ViewWidth = (int)(num2 / Height * Width);
			ViewHeight = (int)num2;
		}
		else
		{
			ViewWidth = (int)num;
			ViewHeight = (int)(num / Width * Height);
		}

		var num3 = ViewHeight / (float)ViewWidth;
		ViewWidth -= ViewPadding * 2;
		ViewHeight -= (int)(num3 * ViewPadding * 2f);
		ScreenMatrix = Matrix.CreateScale(ViewWidth / (float)Width);
		Viewport = new Viewport
		{
			X = (int)(num / 2f - ViewWidth / 2f),
			Y = (int)(num2 / 2f - ViewHeight / 2f),
			Width = ViewWidth,
			Height = ViewHeight,
			MinDepth = 0f,
			MaxDepth = 1f
		};
	}

	public string Title;

	public Version Version;

	public static Action OverloadGameLoop;

	private static int viewPadding;

	private static bool resizing;

	public static float TimeRate = 1f;

	public static float TimeRateB = 1f;

	public static float FreezeTimer;

	public static int FPS;

	private TimeSpan counterElapsed = TimeSpan.Zero;

	private int fpsCounter;

	private static readonly string AssemblyDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

	public static Color ClearColor;

	public static bool ExitOnEscapeKeypress;

	private Scene scene;

	private Scene nextScene;

	public static Matrix ScreenMatrix;
}