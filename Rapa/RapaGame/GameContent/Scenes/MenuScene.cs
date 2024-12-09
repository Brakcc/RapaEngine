using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Rendering;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;
using Rapa.RapaGame.RapaduraEngine.SceneManagement.Packers;

namespace Rapa.RapaGame.GameContent.Scenes;

public class MenuScene : Scene
{
    #region constructor
    
    public MenuScene(EntityTracker entityTracker) : base(entityTracker)
    {
        _canRemove = true;
        LoadMenu(entityTracker);
        Renderers.AddRenderer(new GameplayRenderer());
    }
    
    #endregion

    #region methodes

    private void LoadMenu(EntityTracker ep)
    {
        for (var i = 0; i < ep.Count; i++)
        {
            Entities.Add(ep[i]);
        }
    }
    
    public override void Update()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.E) && _canRemove)
        {
            //EntityPool.RemoveEntity(EntityPool[^1]);
            _canRemove = false;
        }
        base.Update();
    }

    #endregion

    #region fields

    private bool _canRemove;

    #endregion
}