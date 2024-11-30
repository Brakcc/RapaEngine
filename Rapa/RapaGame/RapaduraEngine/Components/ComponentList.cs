using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Components;

public class ComponentList : IEnumerable<Component>
{
    #region properties
    
    public Entity EntityRef { get; private set; }

    public int Count => _components.Count;

    public Component this[int id]
    {
        get
        {
            if (id < 0 || id >= _components.Count)
                throw new IndexOutOfRangeException();

            return _components[id];
        }
    }
    
    public CompLockMode LockMode
    {
        get => _lockMode;
        set
        {
            _lockMode = value;
            SetLockMode(value);
        }
    }

    #endregion
    
    #region constructor

    public ComponentList(Entity entityRef)
    {
        EntityRef = entityRef;
        _components = new List<Component>();
        _toAdd = new HashSet<Component>();
        _toRemove = new HashSet<Component>();
        _lockMode = CompLockMode.Unlocked;
    }

    public ComponentList(Entity entityRef, List<Component> components)
    {
        EntityRef = entityRef;
        _components = components;
        _toAdd = new HashSet<Component>();
        _toRemove = new HashSet<Component>();
        _lockMode = CompLockMode.Unlocked;
    }
    
    #endregion
    
    #region methodes

    public void InitList()
    {
        if (_components == null)
            return;
        
        foreach (var comp in _components)
        {
            comp.Init();
        }
    }
    
    public void Update()
    {
        LockMode = CompLockMode.Locked;
        
        if (_components == null)
            return;
        
        foreach (var comp in _components)
        {
            if (!comp.Active)
                continue;
            
            comp.Update();
        }

        LockMode = CompLockMode.Unlocked;
    }

    public void Render(SpriteBatch spriteBatch)
    {
        LockMode = CompLockMode.Locked;
        
        if (_components == null)
            return;
        
        foreach (var comp in _components)
        {
            if (!comp.Visible)
                continue;
            
            comp.Render(spriteBatch);
        }

        LockMode = CompLockMode.Unlocked;
    }

    public void EndList()
    {
        if (_components == null)
            return;
        
        foreach (var comp in _components)
        {
            comp.End();
        }
    }

    public void AddComponent(Component component)
    {
        if (_components.Contains(component))
            return;
        
        switch (_lockMode)
        {
            case CompLockMode.Unlocked:
                _components.Add(component);
                break;
            case CompLockMode.Locked:
                _toAdd.Add(component);
                break;
            case CompLockMode.DefaultError:
                Console.WriteLine("Cant modify the componentLists in the mode");
                break;
            default:
                throw new Exception("How did u managed to get this error wtf ?");
        }
    }

    public void RemoveComponent(Component component)
    {
        if (!_components.Contains(component))
            return;

        switch (_lockMode)
        {
            case CompLockMode.Unlocked:
                _components.Remove(component);
                break;
            case CompLockMode.Locked:
                _toRemove.Add(component);
                break;
            case CompLockMode.DefaultError:
                Console.WriteLine("Cant modify the componentLists in the mode");
                break;
            default:
                throw new Exception("How did u managed to get this error wtf ?");
        }
    }

    private void SetLockMode(CompLockMode lockMode)
    {
        if (lockMode != CompLockMode.Unlocked)
            return;

        if (_toAdd.Count > 0)
        {
            foreach (var add in _toAdd)
            {
                _components.Add(add);
            }
            _toAdd.Clear();
        }

        if (_toRemove.Count > 0)
        {
            foreach (var remove in _toRemove)
            {
                _components.Remove(remove);
            }
            _toRemove.Clear();
        }
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
    
    public IEnumerator<Component> GetEnumerator() => _components.GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    #endregion
    
    #region fields

    private readonly List<Component> _components;

    private readonly HashSet<Component> _toAdd;

    private readonly HashSet<Component> _toRemove;
    
    private CompLockMode _lockMode;

    #endregion
}