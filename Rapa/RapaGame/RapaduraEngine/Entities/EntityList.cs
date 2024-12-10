using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame.RapaduraEngine.Entities;

public sealed class EntityList : IEnumerable<Entity>
{
    #region properties

    public Scene SceneRef { get; }

    #endregion
    
    #region constructors

    public EntityList(Scene scene)
    {
        SceneRef = scene;
        _entities = new List<Entity>();
        _toAdd = new List<Entity>();
        _toRemove = new List<Entity>();
        _current = new HashSet<Entity>();
        _adding = new HashSet<Entity>();
        _removing = new HashSet<Entity>();
        _sorted = false;
    }

    #endregion
    
    #region methods

    public void UpdateList()
    {
        if (_adding.Count > 0)
        {
            for (var i = 0; i < _adding.Count; i++)
            {
                var e = _toAdd[i];
                
                if (!_current.Add(e))
                    continue;
                
                _entities.Add(e);
                
                if (SceneRef is null)
                    continue;
                
                SceneRef.Tags.EntityAdded(e);
                //SceneRef.EntityTracker.EntityAdded(e);
                e.Added(SceneRef);
                e.Init();
            }
            
            _adding.Clear();
            
            _sorted = false;
        }

        if (_removing.Count > 0)
        {
            for (var i = 0; i < _removing.Count; i++)
            {
                var e = _toRemove[i];

                if (!_current.Remove(e))
                    continue;
                
                _entities.Remove(e);
                
                if (SceneRef is null)
                    continue;
                
                SceneRef.Tags.EntityRemoved(e);
                //SceneRef.EntityTracker.EntityRemoved(e);
                e.Removed();
            }
            
            _toRemove.Clear();
            _removing.Clear();
        }

        if (_sorted)
            return;
        
        _entities.Sort(LayerComparison);
        _sorted = true;
    }
    
    public void Update()
    {
        foreach (var e in _entities)
        {
            e.Update();
        }
    }
    
    /// <summary>
    /// Render the full list
    /// </summary>
    /// <param name="spriteBatch">First call of the spriteBatch parameter at this place</param>
    public void Render(SpriteBatch spriteBatch)
    {
        foreach (var e in _entities)
        {
            e.Render(spriteBatch);
        }
    }

    public void RenderOnlyWithTag(SpriteBatch spriteBatch, uint tags)
    {
        foreach (var e in _entities)
        {
            if (!e.TagCheck(tags))
                continue;
            
            e.Render(spriteBatch);
        }
    }
    
    public void RenderOnlyWithFullTag(SpriteBatch spriteBatch, uint tags)
    {
        foreach (var e in _entities)
        {
            if (!e.TagFullCheck(tags))
                continue;
            
            e.Render(spriteBatch);
        }
    }

    public void RenderExceptWithTag(SpriteBatch spriteBatch, uint tags)
    {
        foreach (var e in _entities)
        {
            if (e.TagCheck(tags)) //TODO tag check func i, entity class  
                continue;
            
            e.Render(spriteBatch);
        }
    }

    public void Add(Entity entity)
    {
        if (!_adding.Add(entity) && !_current.Contains(entity))
            return;
        
        _toAdd.Add(entity);
        _sorted = false;
    }

    public void Remove(Entity entity)
    {
        if (!_removing.Add(entity) && _current.Contains(entity))
            return;
        
        _toRemove.Add(entity);
    }
    
    public IEnumerator<Entity> GetEnumerator() => _entities.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    #endregion
    
    #region fields
    
    private readonly List<Entity> _entities;
    
    private readonly List<Entity> _toAdd;
    
    private readonly List<Entity> _toRemove;

    private readonly HashSet<Entity> _current;
    
    private readonly HashSet<Entity> _adding;
    
    private readonly HashSet<Entity> _removing;

    private bool _sorted;
    
    public static readonly Comparison<Entity> LayerComparison = (a, b) => Math.Sign(b.Layer - a.Layer);

    #endregion
}