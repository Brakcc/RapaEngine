using Microsoft.Xna.Framework;
using Rapa.RapaGame.GameContent.PlayerInfo;

namespace Rapa.RapaGame.RapaduraEngine.CameraManagement;

public class Camera
{
    public Matrix Transform { get; private set; }

    public void Follow (Player target)
    {
        var offset = Matrix.CreateTranslation(
            Rapadura.CurrentScreenWidth / 2f,
            Rapadura.CurrentScreenHeight / 2f,
            0);

        var pos = Matrix.CreateTranslation(
            -target.Position.X + target.Width / 2,
            -target.Position.Y + target.Height / 2,
            0);

        Transform = pos * offset;
    }
}