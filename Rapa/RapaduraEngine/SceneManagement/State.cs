using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Rapa.RapaduraEngine.SceneManagement
{
    public abstract class State
    {
        #region fields
        
        protected ContentManager _content;
        protected GraphicsDevice _graphicsDevice;
        protected SpriteBatch _spriteBatch;
        protected Game1 _game;
        
        #endregion

        #region methodes
        
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        protected State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, SpriteBatch spriteBatch)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
            _spriteBatch = spriteBatch;
        }

        public abstract void Update(GameTime gameTime);
        
        #endregion
    }
}
