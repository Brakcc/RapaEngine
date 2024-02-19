using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.GameContent.Scenes;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch spriteBatch;

    private State currentState;
    private State nextState;
    public void ChangeState(State state)
    {
        nextState = state;
    }

    //private TiledMap tileMap;
    //private TiledMapRenderer tileMapRenderer;

    public static Random random;

    public static int screenWidth;
    public static int screenHeight;
    private static Color color = Color.Wheat;

    private bool hasStarted;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        random = new Random();

        hasStarted = true;

        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        screenHeight = _graphics.PreferredBackBufferHeight;
        screenWidth = _graphics.PreferredBackBufferWidth;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        AllStates();
    }

    private void AllStates()
    {
        currentState = new MenuState(this, _graphics.GraphicsDevice, Content, spriteBatch);
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

        currentState.Draw(gameTime, spriteBatch);

        base.Draw(gameTime);
    }
}