using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.GameContent.Scenes;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch spriteBatch;
    private State currentState;
    private State nextState;

    //private TiledMap tileMap;
    //private TiledMapRenderer tileMapRenderer;

    public static Random random;

    public static int TargetScreenWidth { get; private set; }
    public static int TargetScreenHeight { get; private set; }
    
    public static int CurrentScreenWidth { get; set; }
    public static int CurrentScreenHeight { get; set; }
    
    private static readonly Color color = Color.Wheat;

    private readonly bool hasStarted;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        random = new Random();

        hasStarted = true;

        TargetScreenWidth = 320;
        TargetScreenHeight = 180;
        
        _graphics.PreferredBackBufferWidth = TargetScreenWidth;
        _graphics.PreferredBackBufferHeight = TargetScreenHeight;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        CurrentScreenHeight = _graphics.PreferredBackBufferHeight;
        CurrentScreenWidth = _graphics.PreferredBackBufferWidth;
        
        _graphics.ApplyChanges();

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
        currentState = new MenuState(this, _graphics.GraphicsDevice, Content, spriteBatch, _graphics);
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