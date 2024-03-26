using System;
using System.IO;
using System.Reflection;
using System.Runtime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Mathematics;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame.RapaduraEngine;

public class CoreEngine : Game
{
	#region accessors
	
	public static CoreEngine Instance { get; private set; }

	protected static GraphicsDeviceManager Graphics { get; private set; }

	protected static SpriteBatch SpriteBatch { get; private set; }

	private static int BaseRenderWidth { get; set; }

	private static int BaseRenderHeight { get; set; }

	private static int ViewWidth { get; set; }

	private static int ViewHeight { get; set; }

	protected RenderTarget2D RenderScreen { get; set; }

	protected Rectangle RenderRect => _renderRect;

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

	private static float RawDeltaTime { get; set; }

	private static ulong FrameCounter { get; set; }

	public static string ContentDirectory => Path.Combine(AssemblyDirectory, Instance.Content.RootDirectory);
	
	public static Scene Scene
	{
		get => Instance.scene;
		set => Instance.nextScene = value;
	}

	private static Viewport Viewport { get; set; }
	
	public static Random Random { get; private set; }
	
	#endregion

	#region constructor
	
	protected CoreEngine(int renderWidth, int renderHeight, int windowWidth, int windowHeight, string windowTitle, bool fullscreen, bool vsync)
	{
		Instance = this;
		BaseRenderWidth = renderWidth;
		BaseRenderHeight = renderHeight;
		ClearColor = new Color(3, 2, 6, 255);
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
		Window.AllowAltF4 = true;  
		title = windowTitle;
		if (fullscreen)
		{
			Graphics.PreferredBackBufferWidth = renderWidth;
			Graphics.PreferredBackBufferHeight = renderHeight;
			Graphics.ApplyChanges();
			Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
			Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			Graphics.ApplyChanges();
			Graphics.IsFullScreen = true;
		}
		else
		{
			Graphics.PreferredBackBufferWidth = renderWidth;
			Graphics.PreferredBackBufferHeight = renderHeight;
			SetWindowed(windowWidth, windowHeight);
			Graphics.IsFullScreen = false;
		}
		RenderScreen = new RenderTarget2D(GraphicsDevice, renderWidth, renderHeight);
		SpriteBatch = new SpriteBatch(GraphicsDevice);
		Content.RootDirectory = "Content";
		IsMouseVisible = true;
		ExitOnEscapeKeypress = false;
		GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
		Drawer.InitDrawer(SpriteBatch);
	}

	#endregion
	
	#region methodes
	
	#region AddOns stab

	private void OnClientSizeChanged(object sender, EventArgs e)
	{
		if (Window.ClientBounds is not { Width: > 0, Height: > 0 } || resizing)
			return;
		
		resizing = true;
		
		GetRectTarget();
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

	protected override void Initialize()
	{
		Window.Title = title;
		GetRectTarget();
		base.Initialize();
	}

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
		
		if (scene != null)
		{
			scene.BeforeUpdate();
			scene.Update(gameTime);
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
		GraphicsDevice.Viewport = Viewport;
		
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
		if (scene == null)
     			return;
		
		GraphicsDevice.SetRenderTarget(RenderScreen);
		//GraphicsDevice.Viewport = Viewport;
		GraphicsDevice.Clear(ClearColor);
		scene.BeforeRender();
		
		SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix:ScreenMatrix);
		scene.Draw(SpriteBatch);
		scene.AfterRender();
		SpriteBatch.End();
		
		GraphicsDevice.SetRenderTarget(null);
		GraphicsDevice.Clear(Color.Black);
		
		SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
		SpriteBatch.Draw(RenderScreen, RenderRect, Color.White);
		SpriteBatch.End();
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

	public static void SetWindowed(int width, int height)
	{
		if (width <= 0 || height <= 0)
			return;
		
		Graphics.PreferredBackBufferWidth = width;
		Graphics.PreferredBackBufferHeight = height;
		Graphics.IsFullScreen = false;
		Graphics.ApplyChanges();
	}

	public static void SetFullScreen()
	{
		Graphics.PreferredBackBufferWidth = Graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
		Graphics.PreferredBackBufferHeight = Graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
		Graphics.IsFullScreen = true;
		Graphics.ApplyChanges();
	}

	private void GetRectTarget()
	{
		var size = Graphics.GraphicsDevice.Viewport.Bounds.Size;

		var scaleX = (float)size.X / RenderScreen.Width;
		var scaleY = (float)size.Y / RenderScreen.Height;
		var scale = Math.Min(scaleX, scaleY);

		_renderRect.Width = (int)(RenderScreen.Width * scale);
		_renderRect.Height = (int)(RenderScreen.Height * scale);

		_renderRect.X = (size.X - _renderRect.Width) / 2;
		_renderRect.Y = (size.Y - _renderRect.Height) / 2;
	}
	
	private void UpdateView()
	{
		var num = (float)GraphicsDevice.PresentationParameters.BackBufferWidth;
		var num2 = (float)GraphicsDevice.PresentationParameters.BackBufferHeight;
		if (num / BaseRenderWidth > num2 / BaseRenderHeight)
		{
			ViewWidth = (int)(num2 / BaseRenderHeight * BaseRenderWidth);
			ViewHeight = (int)num2;
		}
		else
		{
			ViewWidth = (int)num;
			ViewHeight = (int)(num / BaseRenderWidth * BaseRenderHeight);
		}

		var num3 = ViewHeight / (float)ViewWidth;
		ViewWidth -= ViewPadding * 2;
		ViewHeight -= (int)(num3 * ViewPadding * 2f);
		ScreenMatrix = Matrix.CreateScale(ViewWidth / (float)BaseRenderWidth);
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
	
	#endregion

	#region fields

	private readonly string title;

	public Version Version;

	public static Action OverloadGameLoop;

	private static int viewPadding;

	private static bool resizing;

	private static float TimeRate = 1f;

	private const float TimeRateB = 1f;

	public static float FreezeTimer;

	public static int FPS;

	private TimeSpan counterElapsed = TimeSpan.Zero;

	private int fpsCounter;

	private static readonly string AssemblyDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);

	private static Color ClearColor;

	private static bool ExitOnEscapeKeypress;

	private Scene scene;

	private Scene nextScene;

	public static Matrix ScreenMatrix;

	private Rectangle _renderRect;
	
	#endregion
}