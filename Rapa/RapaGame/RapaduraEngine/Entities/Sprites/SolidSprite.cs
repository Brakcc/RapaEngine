using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites.Animations;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Rapa.RapaGame.RapaduraEngine.Entities.Sprites;

public class SolidSprite : AbstractEntity
{
    #region fields
    protected AnimationManager animationManager;
    protected Dictionary<string, Animation> animation;
    protected Vector2 pos;

    #endregion

    #region inits
    public Vector2 position
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

    public Rectangle rectangle => new((int)position.X, (int)position.Y, animation.Values.First()._frameWidth, animation.Values.First()._frameHeight);

    public SolidSprite(Dictionary<string, Animation> animations)
    {
        animation = animations;
        animationManager = new AnimationManager(animation.First().Value);
    }
    #endregion

    #region methodes
    public virtual void Update(GameTime gameTime, List<SolidSprite> sprite)
    {
        Animate();
    }

    public virtual void Animate()
    {

    }

    public virtual void Draw(SpriteBatch spriteBatch)
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
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {

    }
    #endregion

    #region Collision
    protected bool IsTouchingLeft(SolidSprite sprite)
    {
        return rectangle.Right + velocity.X > sprite.rectangle.Left &&
               rectangle.Left < sprite.rectangle.Left &&
               rectangle.Bottom > sprite.rectangle.Top &&
               rectangle.Top < sprite.rectangle.Bottom;
    }
    protected bool IsTouchingRight(SolidSprite sprite)
    {
        return rectangle.Left + velocity.X < sprite.rectangle.Right &&
               rectangle.Right > sprite.rectangle.Right &&
               rectangle.Bottom > sprite.rectangle.Top &&
               rectangle.Top < sprite.rectangle.Bottom;
    }
    protected bool IsTouchingTop(SolidSprite sprite)
    {
        return rectangle.Bottom + velocity.X > sprite.rectangle.Top &&
               rectangle.Top < sprite.rectangle.Top &&
               rectangle.Right > sprite.rectangle.Left &&
               rectangle.Left < sprite.rectangle.Right;
    }
    protected bool IsTouchingBottom(SolidSprite sprite)
    {
        return rectangle.Top + velocity.X < sprite.rectangle.Bottom &&
               rectangle.Bottom > sprite.rectangle.Bottom &&
               rectangle.Right > sprite.rectangle.Left &&
               rectangle.Left < sprite.rectangle.Right;
    }
    #endregion
        
        
}