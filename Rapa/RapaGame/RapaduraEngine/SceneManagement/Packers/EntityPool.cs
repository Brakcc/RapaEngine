using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement.Packers;

public class EntityPool
{
    #region properties

    public Dictionary<Type, List<Entity>> Entities { get; }
    
    public Scene SceneRef { get; set; }

    public int Count => _entities.Count;

    public Entity this[int id]
    {
        get
        {
            if (id < 0 || id >= _entities.Count)
                throw new IndexOutOfRangeException();
                
            return _entities[id];
        }
    }

    #endregion
    
    #region constructor

    protected EntityPool()
    {
        Entities = new Dictionary<Type, List<Entity>>();
        _entities = new List<Entity>();
        _toAdd = new List<Entity>();
        _toRemove = new List<Entity>();
        _shuffled = true;
    }

    protected EntityPool(List<Entity> entities)
    {
        Entities = new Dictionary<Type, List<Entity>>();
        _entities = entities;
        _toAdd = new List<Entity>();
        _toRemove = new List<Entity>();
        _shuffled = true;
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
            ent.SceneRef = SceneRef;
            var t = ent.GetType();
            
            if (!Entities.ContainsKey(t))
                Entities.Add(t, new List<Entity>());

            if (t == typeof(Solid) || t == typeof(Tile) || t == typeof(MovableSolid))
            {
                Entities[typeof(Solid)].Add(ent);
                continue;
            }
            
            Entities[t].Add(ent);
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
            _shuffled = true;
        }
        if (_toRemove.Count > 0)
        {
            foreach (var rem in _toRemove)
            {
                _entities.Remove(rem);
            }
            _toRemove.Clear();
        }

        if (!_shuffled)
            return;
        
        _shuffled = false;
        _entities.Sort(CompareLayer);
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

    public bool TryGetFirstEntity<T>(out T entity) where T : Entity
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

    public bool TryGetAllEntities<T>(out List<T> entities) where T : Entity
    {
        entities = new List<T>();
        var hasEnt = false;
        foreach (var ent in _entities)
        {
            if (ent is not T entTest)
                continue;
            
            entities.Add(entTest);
            hasEnt = true;
        }
        return hasEnt;
    }
    
    #endregion
    
    #region fields

    private bool _shuffled;
    
    protected readonly List<Entity> _entities;

    private readonly List<Entity> _toAdd;

    private readonly List<Entity> _toRemove;

    private readonly Comparison<Entity> CompareLayer = (a, b) => MathF.Sign(b.Layer - a.Layer);

    #endregion
}