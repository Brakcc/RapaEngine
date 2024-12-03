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
        for (var i = 0; i < _entities.Length; i++)
        {
            _entities[i] = new List<Entity>();
        }
    }
    
    #endregion
    
    #region fields
    
    private List<Entity>[] _entities;
    
    #endregion
}