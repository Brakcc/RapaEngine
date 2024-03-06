using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Components;

namespace Rapa.RapaGame.RapaduraEngine.Entities;

public abstract class Entity
{
    #region properties

    public Vector2 Position { get; set; }

    private List<Component> Components { get; init; }
    
    #endregion

    #region constructors

    protected Entity() {}
    
    protected Entity(List<Component> components = null)
    {
        Components = components;
    }

    #endregion
    
    #region methodes

    public virtual void Update(GameTime gameTime)
    {
        if (Components.Count == 0)
            return;

        foreach (var comps in Components)
        {
            comps.Update(gameTime);
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (Components.Count == 0)
            return;

        foreach (var comps in Components)
        {
            comps.Draw(spriteBatch);
        }
    }

    public void AddComponent(Component comp)
    {
        Components.Add(comp);
    }
    
    public void RemoveComponent(Component comp)
    {
        Components.Remove(comp);
    }
    
    #endregion
}