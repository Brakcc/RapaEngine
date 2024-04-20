using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Components;

public abstract class Component
{
    #region properties

    protected Entity EntityRef { get; init; }
    
    public bool Active { get; private set; }
    
    public bool Visible { get; private set; }

    #endregion

    #region constructor

    protected Component(Entity entityRef)
    {
        EntityRef = entityRef;
        Active = true;
        Visible = true;
    }

    protected Component(Entity entityRef, bool active, bool visible)
    {
        EntityRef = entityRef;
        Active = active;
        Visible = visible;
    }
    
    #endregion
    
    #region methodes
    
    public virtual void Init() {}

    public abstract void Update(GameTime gameTime);

    public abstract void Draw(SpriteBatch spriteBatch);

    public virtual void End() {}
    
    public void SetActive(bool active) => Active = active;
    
    public void SetVisible(bool visible) => Visible = visible;
    
    public void RemoveSelf() => EntityRef?.RemoveComponent(this);
    
    
    #endregion
}