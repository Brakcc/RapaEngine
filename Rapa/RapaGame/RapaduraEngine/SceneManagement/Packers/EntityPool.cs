using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement.Packers;

public abstract class EntityPool
{
    #region properties

    public Dictionary<Type, List<Entity>> Entities { get; }
    
    public Scene SceneRef { get; set; }

    public int Count => entities.Count;

    public Entity this[int id]
    {
        get
        {
            if (id < 0 || id >= entities.Count)
                throw new IndexOutOfRangeException();
                
            return entities[id];
        }
    }

    #endregion
    
    #region constructor

    protected EntityPool()
    {
        Entities = new Dictionary<Type, List<Entity>>();
        entities = new List<Entity>();
        _toAdd = new HashSet<Entity>();
        _toRemove = new HashSet<Entity>();
        _shuffled = true;
    }

    protected EntityPool(List<Entity> entities)
    {
        Entities = new Dictionary<Type, List<Entity>>();
        this.entities = entities;
        _toAdd = new HashSet<Entity>();
        _toRemove = new HashSet<Entity>();
        _shuffled = true;
    }
    
    #endregion
    
    #region methodes

    public void InitList()
    {
        if (entities == null)
            return;
        
        foreach (var ent in entities)
        {
            ent.Init();
            ent.SceneRef = SceneRef;
            var t = ent.GetType();
            
            if (!Entities.ContainsKey(t))
                Entities.Add(t, new List<Entity>());
            
            if (ent.Collidable)
                SceneRef.CollisionsTracker.Colliders.Add(ent);
            
            //Refaire les systeme de RendererList
            
            Entities[t].Add(ent);
        }
    }
    
    public void Update(GameTime gameTime)
    {
        if (entities == null)
            return;
        
        foreach (var ent in entities)
        {
            ent.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (entities == null)
            return;
        
        foreach (var ent in entities)
        {
            ent.Draw(spriteBatch);
        }
    }

    public void EndList()
    {
        if (entities == null)
            return;
        
        foreach (var ent in entities)
        {
            ent.End();
        }
    }

    public void AddEntity(Entity entity)
    {
        if (entities.Contains(entity)) 
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
        if (!entities.Contains(entity))
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
                entities.Add(add);
            }
            _toAdd.Clear();
            _shuffled = true;
        }
        if (_toRemove.Count > 0)
        {
            foreach (var rem in _toRemove)
            {
                entities.Remove(rem);
            }
            _toRemove.Clear();
        }

        if (!_shuffled)
            return;
        
        _shuffled = false;
        entities.Sort(_compareLayer);
    }
    
    public T GetFirstEntity<T>() where T : Entity
    {
        foreach (var ent in entities)
        {
            if (ent is T entity)
                return entity;
        }
        throw new Exception($"No Component of Type {typeof(T)} was found");
        //throw is a bit violent
    }

    public List<T> GetAllEntities<T>() where T : Entity
    {
        var list = new List<T>();
        
        foreach (var ent in entities)
        {
            if (ent is T entity)
                list.Add(entity);
        }

        return list;
    }

    public bool TryGetFirstEntity<T>(out T entity) where T : Entity
    {
        foreach (var ent in entities)
        {
            if (ent is not T entTest)
                continue;
            
            entity = entTest;
            return true;
        }
        entity = null;
        return false;
    }

    public bool TryGetAllEntities<T>(out List<T> ents) where T : Entity
    {
        ents = new List<T>();
        var hasEnt = false;
        foreach (var ent in entities)
        {
            if (ent is not T entTest)
                continue;
            
            ents.Add(entTest);
            hasEnt = true;
        }
        return hasEnt;
    }
    
    #endregion
    
    #region fields

    private bool _shuffled;
    
    protected readonly List<Entity> entities;

    private readonly HashSet<Entity> _toAdd;

    private readonly HashSet<Entity> _toRemove;

    private readonly Comparison<Entity> _compareLayer = (a, b) => MathF.Sign(b.Layer - a.Layer);

    #endregion
}