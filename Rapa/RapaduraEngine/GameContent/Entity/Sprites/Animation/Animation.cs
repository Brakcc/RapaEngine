using Microsoft.Xna.Framework.Graphics;

namespace Rapa.RapaduraEngine.GameContent.Entity.Sprites.Animation
{
    public class Animation
    {
        public int _currentFrame { get; set; }
        public int _frameCount { get; set; }
        public int _frameHeight { get { return _texture.Height; } }
        public int _frameWidth { get { return _texture.Width / _frameCount; } }
        public float _frameSpeed { get; set; }
        public bool _isLooping { get; set; }
        public Texture2D _texture { get; private set; }
        public float _layer { get; private set; }

        public Animation(Texture2D texture, int frameCount, float frameSpeed, float layer)
        {
            _texture = texture;
            _frameCount = frameCount;
            _isLooping = true;
            _frameSpeed = frameSpeed;
            _layer = layer;
        }
    }
}
