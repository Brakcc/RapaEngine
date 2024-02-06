using Microsoft.Xna.Framework;
using Rapa.RapaduraEngine.GameContent.Entity.Sprites.Player;

namespace Rapa.RapaduraEngine.GameContent.CameraManagement
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow (Player target)
        {
            var offset = Matrix.CreateTranslation(
                    Game1.screenWidth / 2,
                    Game1.screenHeight / 2,
                    0);

            var pos = Matrix.CreateTranslation(
                -target.position.X + target.rectangle.Width / 2,
                -target.position.Y + target.rectangle.Height / 2,
                0);

            Transform = pos * offset;
        }
    }
}
