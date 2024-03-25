using Microsoft.Xna.Framework;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.CameraManagement;

public class Camera : Entity
{
    #region properties
    
    public Entity FocusPoint { get; init; }
    
    public Matrix Transform { get; private set; }
    
    #endregion

    #region constructor

    public Camera(Entity focus)
    {
        FocusPoint = focus;
    }

    #endregion
    
    #region methodes

    public override void Update(GameTime gameTime)
    {
        Follow(FocusPoint);
    }

    public void Follow (Entity target)
    {
        var offset = Matrix.CreateTranslation(
            Rapadura.CurrentScreenWidth / 2f,
            Rapadura.CurrentScreenHeight / 2f,
            0);

        var pos = Matrix.CreateTranslation(
            -target.X + target.Width / 2,
            -target.Y + target.Height / 2,
            0);
        
        Transform = offset * pos;

        //à refaire d'urgence
        CoreEngine.ScreenMatrix = Transform;
    }
    
    #endregion
}