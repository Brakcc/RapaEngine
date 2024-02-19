using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites.Animations;

namespace Rapa.RapaGame.RapaduraEngine.Entities.Sprites;

public class HollowSprite : AbstractEntity
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
            {
                animationManager.position = pos;
            }   
        }
    }
    public Vector2 velocity;

    public bool isRemoved = false;

    public Rectangle rectangle => new((int)position.X, (int)position.Y, animation.Values.First()._frameWidth, animation.Values.First()._frameHeight);

    public HollowSprite(Dictionary<string, Animation> animations)
    {
        animation = animations;
        animationManager = new AnimationManager(animation.First().Value);
    }
    #endregion

    #region Methodes
    public virtual void Update(GameTime gameTime, List<HollowSprite> sprite) 
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
}