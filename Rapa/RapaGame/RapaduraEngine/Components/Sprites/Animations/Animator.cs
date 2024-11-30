using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;

public class Animator
{
    #region constructor
    
    public Animator(Animation anim, Entity entityRef)
    {
        _animation = anim;
        _entityRef = entityRef;
        _canLoop = true;
    }

    #endregion
    
    #region methodes
    
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_animation._texture,
            _entityRef.Position,
            new Rectangle(_animation._currentFrame * _animation._frameWidth,
                0,
                _animation._frameWidth,
                _animation._frameHeight),
            Color.White);
    }

    public void Play(Animation anim) 
    {
        if (_animation == anim)
        {
            return;
        }
        _animation = anim;
        _animation._currentFrame = 0;
        _timer = 0;
    }
    public void Stop() 
    {
        _timer = 0;
        _animation._currentFrame = 0;
    }

    public void Update()
    {
        if (!_canLoop)
            return;
        
        _timer += CoreEngine.DeltaTime;
        if (!(_timer > _animation._frameSpeed))
            return;
        
        _timer = 0;
        _animation._currentFrame++;

        if (_animation._currentFrame < _animation._frameCount)
            return;
        
        _animation._currentFrame = 0;

        if (!_animation._isLooping)
            _canLoop = false;
    }
    
    #endregion
    
    #region fields
                  
    private Animation _animation;
    
    private float _timer;
    
    private readonly Entity _entityRef;

    private bool _canLoop;

    #endregion
}