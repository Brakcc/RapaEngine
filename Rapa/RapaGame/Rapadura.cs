using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.GameContent.Scenes;
using Rapa.RapaGame.RapaduraEngine;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame;

public class Rapadura : Engine
{
    private SpriteBatch spriteBatch;
    private State currentState;
    private State nextState;

    public const int RenderScreenWidth = 320;
    public const int RenderScreenHeight = 180;
    
    public static int CurrentScreenWidth { get; set; }
    public static int CurrentScreenHeight { get; set; }
    
    private static readonly Color color = Color.Wheat;

    private readonly bool hasStarted;

    public Rapadura() : base(10, 10, 10, 10, "Rapadura", false, true)
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        hasStarted = true;
        
        Graphics.PreferredBackBufferWidth = RenderScreenWidth;
        Graphics.PreferredBackBufferHeight = RenderScreenHeight;
        Graphics.ApplyChanges();
    }

    public static void OnRun()
    {
        Instance.RunGame();
    }
    
    protected override void Initialize()
    {
        CurrentScreenHeight = Graphics.PreferredBackBufferHeight;
        CurrentScreenWidth = Graphics.PreferredBackBufferWidth;
        
        Graphics.ApplyChanges();

        LoadContent();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        AllStates();
    }

    private void AllStates()
    {
        currentState = new MenuState(this, Graphics.GraphicsDevice, Content, spriteBatch, Graphics);
    }
    
    public void ChangeState(State state)
    {
        nextState = state;
    }

    protected override void Update(GameTime gameTime)
    {
        //état du jeu
        if (nextState != null)
        {
            currentState = nextState;
            nextState = null;
        }

        currentState.Update(gameTime);
        currentState.PostUpdate(gameTime);

        //verif du lacement du exe
        if (!hasStarted)
        {
            return;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(color);

        currentState.Draw(spriteBatch);

        base.Draw(gameTime);
    }
}