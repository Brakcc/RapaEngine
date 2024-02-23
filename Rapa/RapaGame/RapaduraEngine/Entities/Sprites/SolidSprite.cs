using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites.Animations;

namespace Rapa.RapaGame.RapaduraEngine.Entities.Sprites;

public class SolidSprite : Entity
{
    #region fields
    
    protected readonly AnimationManager animationManager;
    protected readonly Dictionary<string, Animation> animation;
    private Vector2 pos;

    #endregion

    #region inits
    
    public Vector2 Position
    {
        get => pos;
        set
        {
            pos = value;
            if (animationManager != null)
                animationManager.position = pos;
        }
    }
    public Vector2 velocity;

    public bool isRemoved = false;

    public Rectangle rectangle => new((int)Position.X, (int)Position.Y, animation.Values.First()._frameWidth, animation.Values.First()._frameHeight);

    public SolidSprite(Dictionary<string, Animation> animations)
    {
        animation = animations;
        animationManager = new AnimationManager(animation.First().Value);
    }
    
    #endregion

    #region methodes

    protected virtual void Animate()
    {

    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (animationManager != null)
        {
            animationManager.Draw(spriteBatch);
        }
        else throw new Exception("no animation set up");
    }

    public override void Update(GameTime gameTime)
    {

    }
    
    #endregion

    #region Collision
    
    protected bool IsTouchingLeft(SolidSprite sprite)
        => rectangle.Right + velocity.X > sprite.rectangle.Left &&
           rectangle.Left < sprite.rectangle.Left && 
           rectangle.Bottom > sprite.rectangle.Top &&
           rectangle.Top < sprite.rectangle.Bottom;
    
    protected bool IsTouchingRight(SolidSprite sprite)
       => rectangle.Left + velocity.X < sprite.rectangle.Right &&
          rectangle.Right > sprite.rectangle.Right &&
          rectangle.Bottom > sprite.rectangle.Top &&
          rectangle.Top < sprite.rectangle.Bottom;
    
    protected bool IsTouchingTop(SolidSprite sprite)
        => rectangle.Bottom + velocity.X > sprite.rectangle.Top &&
           rectangle.Top < sprite.rectangle.Top &&
           rectangle.Right > sprite.rectangle.Left &&
           rectangle.Left < sprite.rectangle.Right;
    
    protected bool IsTouchingBottom(SolidSprite sprite) 
        => rectangle.Top + velocity.X < sprite.rectangle.Bottom &&
           rectangle.Bottom > sprite.rectangle.Bottom &&
           rectangle.Right > sprite.rectangle.Left &&
           rectangle.Left < sprite.rectangle.Right;
    
    #endregion
        
        
}