using Microsoft.Xna.Framework;
using Rapa.RapaGame.GameContent.PlayerInfo;

namespace Rapa.RapaGame.RapaduraEngine.CameraManagement;

public class Camera
{
    public Matrix Transform { get; private set; }

    public void Follow (Player target)
    {
        var offset = Matrix.CreateTranslation(
            Game1.ScreenWidth / 2,
            Game1.ScreenHeight / 2,
            0);

        var pos = Matrix.CreateTranslation(
            -target.Position.X + target.rectangle.Width / 2,
            -target.Position.Y + target.rectangle.Height / 2,
            0);

        Transform = pos * offset;
    }
}