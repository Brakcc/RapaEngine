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
        CurrentScreenHeight = RenderScreenHeight;
        CurrentScreenWidth = RenderScreenWidth;
        
        Graphics.ApplyChanges();
        AllStates();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
    }

    private void AllStates()
    {
        currentState = new MenuState(this, Graphics.GraphicsDevice, Content, SpriteBatch, Graphics);
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
        currentState.Draw(SpriteBatch);
        
        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        SpriteBatch.Draw(RenderScreen, RenderRect, Color.White);
        SpriteBatch.End();
    }
    
    #endregion
    
    #region fields
    
    private State currentState;
    private State nextState;

    private const int RenderScreenWidth = 320;
    private const int RenderScreenHeight = 180;

    private const int WindowStartScreenWidth = 720;
    private const int WindowStartScreenHeight = 480;

    #endregion
}