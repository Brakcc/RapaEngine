using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;
using Rapa.RapaGame.RapaduraEngine.SceneManagement.Packers;

namespace Rapa.RapaGame.GameContent.Scenes;

public class MenuScene : Scene
{
    #region constructor
    
    public MenuScene(EntityPool entityPool) : base(entityPool)
    {
        _canRemove = true;
        EntityPool.SceneRef = this;
    }
    
    #endregion

    #region methodes

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.E) && _canRemove)
        {
            //EntityPool.RemoveEntity(EntityPool[^1]);
            _canRemove = false;
        }
        base.Update(gameTime);
    }

    #endregion

    #region fields

    private bool _canRemove;

    #endregion
}