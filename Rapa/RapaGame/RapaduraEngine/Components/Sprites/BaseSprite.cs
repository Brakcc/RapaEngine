using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Components.Sprites;

public sealed class BaseSprite : Component
{
    #region constructor

    public BaseSprite(Entity entityRef, Texture2D texture) : base(entityRef)
    {
        _texture = texture;

        if (entityRef.Width == 0)
            entityRef.Width = texture.Width;

        if (entityRef.Height == 0)
            entityRef.Height = texture.Height;
    }

    #endregion

    #region methodes

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, EntityRef.Position, null, Color.White);
    }

    #endregion
    
    #region fields

    private readonly Texture2D _texture;
    
    #endregion
}