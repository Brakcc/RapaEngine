using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement;

public abstract class State
{
    #region fields
        
    protected readonly ContentManager _content;
    protected readonly GraphicsDevice _graphicsDevice;
    protected readonly SpriteBatch _spriteBatch;
    protected readonly Game1 _game;
        
    #endregion

    #region methodes
        
    public abstract void Draw(SpriteBatch spriteBatch);

    public abstract void PostUpdate(GameTime gameTime);
    
    public virtual void OnInit() {}

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