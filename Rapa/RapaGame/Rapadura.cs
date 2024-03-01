using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.GameContent.Scenes;
using Rapa.RapaGame.RapaduraEngine;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame;

public class Rapadura : CoreEngine
{
    #region Accessors
    
    public static int CurrentScreenWidth { get; private set; }
    public static int CurrentScreenHeight { get; private set; }

    private bool hasStarted { get; }
    
    #endregion

    #region constructors
    
    public Rapadura() : base(
        RenderScreenWidth,
        RenderScreenHeight,
        WindowStartScreenWidth,
        WindowStartScreenHeight,
        "Rapadura",
        false,
        true)
    {
        hasStarted = true;
    }

    #endregion
    
    #region methodes
    
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
            return;

        base.Update(gameTime);
    }

    protected override void RenderCore()
    {
        GraphicsDevice.Clear(ClearColor);
        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Viewport = Viewport;
        currentState.Draw(spriteBatch);
    }
    
    #endregion
    
    #region fields
    
    private SpriteBatch spriteBatch;
    private State currentState;
    private State nextState;

    private const int RenderScreenWidth = 320;
    private const int RenderScreenHeight = 180;

    private const int WindowStartScreenWidth = 1920;
    private const int WindowStartScreenHeight = 1080;
    
    #endregion
}