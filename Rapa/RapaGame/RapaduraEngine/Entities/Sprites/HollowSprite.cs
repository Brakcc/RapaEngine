using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites.Animations;

namespace Rapa.RapaGame.RapaduraEngine.Entities.Sprites;

public sealed class HollowSprite : Entity
{
    #region fields

    private readonly AnimationManager animationManager;
    private readonly Dictionary<string, Animation> animation;
    private readonly Vector2 pos;
        
    #endregion

    #region inits
    public Vector2 Position
    {
        get => pos;
        init
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

    public Rectangle rectangle => new((int)Position.X, (int)Position.Y, animation.Values.First()._frameWidth, animation.Values.First()._frameHeight);

    public HollowSprite(Dictionary<string, Animation> animations)
    {
        animation = animations;
        animationManager = new AnimationManager(animation.First().Value);
    }
    #endregion

    #region Methodes

    private static void Animate() 
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
        Animate();
    }
    
    #endregion
}