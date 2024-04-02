using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.CameraManagement;

public class Camera : Entity
{
    #region properties
    
    public Matrix Transform { get; private set; }
    
    #endregion

    #region constructor

    public Camera()
    {
        X = 320f;
        Y = 180f;
    }

    #endregion
    
    #region methodes

    public override void Update(GameTime gameTime)
    {
        SetCam(X, Y);
        
        if (Keyboard.GetState().IsKeyDown(Keys.C))
            EasedTraveling(new Vector2(640, 180), 0.25f);
        
        if (Keyboard.GetState().IsKeyDown(Keys.E))
            X += 6;
        if (Keyboard.GetState().IsKeyDown(Keys.A))
            X -= 6;
        
        if (Keyboard.GetState().IsKeyDown(Keys.Y))
            Shake(Position, 0.05f);
    }

    private void SetCam (float x, float y)
    {
        var offset = Matrix.CreateTranslation(
            Rapadura.CurrentScreenWidth / 2f,
            Rapadura.CurrentScreenHeight / 2f,
            0);

        var pos = Matrix.CreateTranslation(-x / 2, -y / 2, 0);
        
        Transform = offset * pos;

        //à refaire d'urgence
        CoreEngine.ScreenMatrix = Transform;
    }

    private void EasedTraveling(Vector2 focusPoint, float speed)
    {
        Position += (focusPoint - Position) * speed;
    }

    private void Shake(Vector2 focusPoint, float speed)
    {
        var coef = focusPoint - Position;
        if (coef.Length() <= 1)
        {
            coef = Vector2.One;
            speed = 1;
        }
        Position += coef * speed;
    }
    
    #endregion
}