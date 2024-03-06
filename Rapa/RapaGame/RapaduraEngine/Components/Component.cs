using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Components;

public abstract class Component
{
    #region properties

    private Entity EntityRef { get; init; }

    #endregion

    #region constructor

    protected Component(Entity entityRef)
    {
        EntityRef = entityRef;
    }
    
    #endregion
    
    #region methodes
    
    public virtual void Update(GameTime gameTime) {}

    public virtual void Draw(SpriteBatch spriteBatch) {}

    public void RemoveSelf()
    {
        EntityRef?.RemoveComponent(this);
    }
    
    #endregion
}