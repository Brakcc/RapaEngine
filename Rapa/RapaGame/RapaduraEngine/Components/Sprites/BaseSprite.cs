using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Components.Sprites;

public class BaseSprite : Component
{
    #region constructor
    
    public BaseSprite(Entity entityRef) : base(entityRef)
    {
        _texture = null;
    }

    public BaseSprite(Entity entityRef, Texture2D texture) : base(entityRef)
    {
        _texture = texture;
    }
    #endregion

    #region fields

    private Texture2D _texture;

    #endregion
}