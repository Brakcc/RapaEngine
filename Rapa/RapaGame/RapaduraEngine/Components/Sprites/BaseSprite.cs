using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Components.Sprites;

public sealed class BaseSprite : Component
{
    #region constructor
    
    public BaseSprite(Entity entityRef, Texture2D texture = null, float layer = 0f) : base(entityRef)
    {
        _texture = texture;
        _layer = layer;
    }
    
    #endregion

    #region methodes

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, EntityRef.Position, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None , _layer);
        base.Draw(spriteBatch);
    }

    #endregion
    
    #region fields

    private readonly Texture2D _texture;

    private readonly float _layer;

    #endregion
}