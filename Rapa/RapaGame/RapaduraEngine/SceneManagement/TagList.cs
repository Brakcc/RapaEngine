using System;
using System.Collections.Generic;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement;

public class TagList
{
    #region properties

    public List<Entity> this[int id]
    {
        get
        {
            if (id < 0 || id >= _entities.Length)
                throw new IndexOutOfRangeException();

            return _entities[id];
        }
    }
    
    #endregion
    
    #region constructors

    public TagList()
    {
        _entities = new List<Entity>[Tag32.TotalTags];
        _unsorted = new bool[Tag32.TotalTags];
        
        for (var i = 0; i < _entities.Length; i++)
        {
            _entities[i] = new List<Entity>();
        }
    }
    
    #endregion
    
    #region methodes

    public void UpdateLists()
    {
        if (!_anyUnsorted)
            return;
        for (var i = 0; i < _entities.Length; i++)
        {
            if (!_unsorted[i])
                continue;
            
            _entities[i].Sort(EntityList.LayerComparison);
            _unsorted[i] = false;
        }
        
        _anyUnsorted = false;
    }

    public void MarkUnsorted(int i)
    {
        _anyUnsorted = true;
        _unsorted[i] = true;
    }
    
    public void EntityAdded(Entity e)
    {
        for (var i = 0; i < 32; i++)
        {
            if (!e.TagCheck(1U << i))
                continue;
            
            this[i].Add(e);
            _unsorted[i] = true;
            _anyUnsorted = true;
        }
    }

    public void EntityRemoved(Entity e)
    {
        for (var i = 0; i < 32; i++)
        {
            if (!e.TagCheck(1U << i))
                continue;
            
            this[i].Remove(e);
        }
    }
    
    #endregion
    
    #region fields
    
    private readonly List<Entity>[] _entities;

    private readonly bool[] _unsorted;
    
    private bool _anyUnsorted;

    #endregion
}