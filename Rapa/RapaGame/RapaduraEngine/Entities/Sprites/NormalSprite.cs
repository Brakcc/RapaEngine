using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rapa.RapaGame.RapaduraEngine.Entities.Sprites;

public class NormalSprite : Entity
{
    #region accessors

    public float Layer { get; set; }

    public Rectangle Rect => new ((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);

    #endregion
    
    #region constructor
    
    public NormalSprite(Texture2D texture)
    {
        _texture = texture;
    }
    
    #endregion

    #region methodes
    
    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, Position, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, Layer);
        base.Draw(spriteBatch);
    }
    
    #endregion
    
    #region fields
    
    private readonly Texture2D _texture;
    
    #endregion
}