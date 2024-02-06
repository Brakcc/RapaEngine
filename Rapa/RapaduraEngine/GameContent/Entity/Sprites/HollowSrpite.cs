using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaduraEngine.GameContent.Entity.Sprites.Animation;

namespace Rapa.RapaduraEngine.GameContent.Entity.Sprites
{
    public class HollowSrpite : RapaduraEngine.Entity
    {
        #region fields
        protected AnimationManager animationManager;
        protected Dictionary<string, Animation.Animation> animation;
        protected Vector2 pos;
        #endregion

        #region inits
        public Vector2 position
        {
            get { return pos; }
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

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, animation.Values.First()._frameWidth, animation.Values.First()._frameHeight);
            }
        }

        public HollowSrpite(Dictionary<string, Animation.Animation> animations)
        {
            animation = animations;
            animationManager = new AnimationManager(animation.First().Value);
        }
        #endregion

        #region Methodes
        public virtual void Update(GameTime gameTime, List<HollowSrpite> sprite) 
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
}
