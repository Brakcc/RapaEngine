using Rapa.RapaGame.GameContent.Scenes;
using Rapa.RapaGame.GameContent.Scenes.Pools;
using Rapa.RapaGame.RapaduraEngine;

namespace Rapa.RapaGame;

public class Rapadura : CoreEngine
{
    #region Accessors
    
    public static int CurrentScreenWidth { get; private set; }
    public static int CurrentScreenHeight { get; private set; }

    private bool HasStarted { get; }
    
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
        Scene = new MenuScene(new MenuPool());
        HasStarted = true;
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
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
    }

    #endregion
    
    #region fields

    private const int RenderScreenWidth = 320;
    private const int RenderScreenHeight = 180;

    private const int WindowStartScreenWidth = 720;
    private const int WindowStartScreenHeight = 480;

    #endregion
}