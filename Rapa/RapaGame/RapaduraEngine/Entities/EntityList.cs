using System;
using System.Collections;
using System.Collections.Generic;
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
        
    }
    
    public void Render()
    {
        
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