using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement.Packers;

public class EntityPool
{
    #region properties
    
    public Scene SceneRef { get; private set; }

    public int Count => _entities.Count;

    public Entity this[int id]
    {
        get
        {
            if (id < 0 || id >= _entities.Count)
                throw new IndexOutOfRangeException();
                
            return _entities [id];
        }
    }

    #endregion
    
    #region constructor

    public EntityPool(Scene entityRef)
    {
        SceneRef = entityRef;
        _entities = new List<Entity>();
        _toAdd = new List<Entity>();
        _toRemove = new List<Entity>();
    }

    public EntityPool(Scene entityRef, List<Entity> entities)
    {
        SceneRef = entityRef;
        _entities = entities;
        _toAdd = new List<Entity>();
        _toRemove = new List<Entity>();
    }
    
    #endregion
    
    #region methodes

    public void InitList()
    {
        if (_entities == null)
            return;
        
        foreach (var ent in _entities)
        {
            ent.Init();
        }
    }
    
    public void Update(GameTime gameTime)
    {
        if (_entities == null)
            return;
        
        foreach (var ent in _entities)
        {
            ent.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (_entities == null)
            return;
        
        foreach (var ent in _entities)
        {
            ent.Draw(spriteBatch);
        }
    }

    public void EndList()
    {
        if (_entities == null)
            return;
        
        foreach (var comp in _entities)
        {
            comp.End();
        }
    }

    public void AddEntity(Entity entity)
    {
        if (_entities.Contains(entity) || _toAdd.Contains(entity))
            return;
        
        _toAdd.Add(entity);
    }

    public void AddEntities(params Entity[] ents)
    {
        foreach (var ent in ents)
        {
            AddEntity(ent);
        }
    }
    
    public void RemoveEntity(Entity entity)
    {
        if (!_entities.Contains(entity) || _toRemove.Contains(entity))
            return;
        
        _toRemove.Add(entity);
    }

    public void RemoveEntities(params Entity[] ents)
    {
        foreach (var ent in ents)
        {
            RemoveEntity(ent);
        }
    }
    
    public void UpdateEntList()
    {
        if (_toAdd.Count > 0)
        {
            foreach (var add in _toAdd)
            {
                _entities.Add(add);
            }
            _toAdd.Clear();
        }
        if (_toRemove.Count > 0)
        {
            foreach (var rem in _toRemove)
            {
                _entities.Remove(rem);
            }
            _toRemove.Clear();
        }
    }
    
    public T GetFirstEntity<T>() where T : Entity
    {
        foreach (var ent in _entities)
        {
            if (ent is T entity)
                return entity;
        }
        throw new Exception($"No Component of Type {typeof(T)} was found");
    }

    public List<T> GetAllEntities<T>() where T : Entity
    {
        var list = new List<T>();
        
        foreach (var ent in _entities)
        {
            if (ent is T entity)
                list.Add(entity);
        }

        return list;
    }

    public bool TryGetEntity<T>(out T entity) where T : Entity
    {
        foreach (var ent in _entities)
        {
            if (ent is not T entTest)
                continue;
            
            entity = entTest;
            return true;
        }
        entity = null;
        return false;
    }
    
    #endregion
    
    #region fields

    private readonly List<Entity> _entities;

    private readonly List<Entity> _toAdd;

    private readonly List<Entity> _toRemove;

    #endregion
}