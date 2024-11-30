using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame.RapaduraEngine.Entities;

public sealed class EntityList : IEnumerable<Entity>
{
    #region properties

    public Scene SceneRef { get; init; }

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

    public void Init()
    {
        
    }

    public void UpdateList()
    {
        
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

    public void RenderOnlyWithTag(SpriteBatch spriteBatch, int tags)
    {
        foreach (var e in _entities)
        {
            //if (pas le bon tag)
            //    continue;
            
            e.Render(spriteBatch);
        }
    }

    public void RenderExceptWithTag(SpriteBatch spriteBatch, int tags)
    {
        foreach (var e in _entities)
        {
            //if (les bons tags)
            //    continue;
            
            e.Render(spriteBatch);
        }
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
    
    private static Comparison<Entity> DepthComparison = (a, b) => Math.Sign(b.Layer - a.Layer);

    #endregion
}