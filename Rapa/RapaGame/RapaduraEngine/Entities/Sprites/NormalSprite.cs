using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rapa.RapaGame.RapaduraEngine.Entities.Sprites;

public class NormalSprite : AbstractEntity
{
    #region fields, Inits et constructeur
    protected float _layer { get; set; }
    protected Texture2D _texture;
    public Vector2 position;

    public float layer
    {
        get { return _layer; } 
        set { _layer = value; }
    }

    public Rectangle rectangle
    {
        get
        {
            return new ((int)position.X, (int)position.Y, _texture.Width, _texture.Height);
        }
    }

    public NormalSprite(Texture2D texture)
    {
        _texture = texture;
    }
    #endregion

    #region methodes
    public override void Update(GameTime gameTime)
    {
            
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, position, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, layer);
    }
    #endregion
}