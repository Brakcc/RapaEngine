using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;

namespace Rapa.RapaGame.RapaduraEngine.Entities.Sprites;

public sealed class HollowSprite : Entity
{
    #region Accessors

    public bool isRemoved = false;

    public Rectangle rectangle => new((int)Position.X, (int)Position.Y, animation.Values.First()._frameWidth, animation.Values.First()._frameHeight);

    #endregion
    
    #region Constructor
    
    public HollowSprite(Dictionary<string, Animation> animations)
    {
        animation = animations;
        animationManager = new AnimationManager(animation.First().Value, this);

        //_canMove = true;
    }

    public HollowSprite(Texture2D texture)
    {
        
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

        /*if (Keyboard.GetState().IsKeyDown(Keys.Space) && _canMove)
        {
            _canMove = false;
            Position += new Vector2(0, 1);
        }

        if (Keyboard.GetState().IsKeyUp(Keys.Space) && !_canMove)
        {
            _canMove = true;
        }*/
    }
    
    #endregion
    
    #region fields
              
    private readonly AnimationManager animationManager;
    private readonly Dictionary<string, Animation> animation;
    public Vector2 velocity;
              
    //private bool _canMove;
                  
    #endregion
}