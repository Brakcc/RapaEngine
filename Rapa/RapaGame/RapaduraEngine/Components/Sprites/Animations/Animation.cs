using Microsoft.Xna.Framework.Graphics;

namespace Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;

public class Animation
{
    public int _currentFrame { get; set; }
    public int _frameCount { get; }
    public int _frameHeight => _texture.Height;
    public int _frameWidth => _texture.Width / _frameCount;
    public float _frameSpeed { get; }
    public bool _isLooping { get; set; }
    public Texture2D _texture { get; }
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