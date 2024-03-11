using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Components;

public class ComponentList
{
    #region properties
    
    public Entity EntityRef { get; private set; }
    
    #endregion
    
    #region constructor

    public ComponentList(Entity entityRef)
    {
        EntityRef = entityRef;
        _components = new List<Component>();
    }

    public ComponentList(Entity entityRef, List<Component> components)
    {
        EntityRef = entityRef;
        _components = components;
    }
    
    #endregion
    
    #region methodes

    public void Update(GameTime gameTime)
    {
        if (_components == null)
            return;
        
        foreach (var comp in _components)
        {
            if (!comp.Active)
                continue;
            
            comp.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (_components == null)
            return;
        
        foreach (var comp in _components)
        {
            if (!comp.Visible)
                continue;
            
            comp.Draw(spriteBatch);
        }
    }

    public void AddComponent(Component component)
    {
        _components.Add(component);
    }

    public void RemoveComponent(Component component)
    {
        if (!_components.Contains(component))
            return;

        _components.Remove(component);
    }
    
    public T GetComponent<T>() where T : Component
    {
        foreach (var comp in _components)
        {
            if (comp is T component)
                return component;
        }
        throw new Exception($"No Component of Type {typeof(T)} was found");
    }

    public bool TryGetComponent<T>(out T component) where T : Component
    {
        foreach (var comp in _components)
        {
            if (comp is not T compTest)
                continue;
            
            component = compTest;
            return true;
        }
        component = null;
        return false;
    }
    
    #endregion
    
    #region fields

    private readonly List<Component> _components;

    #endregion
}