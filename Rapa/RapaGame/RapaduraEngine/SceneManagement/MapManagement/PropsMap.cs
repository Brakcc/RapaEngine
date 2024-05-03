using System;
using System.Collections.Generic;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement.MapManagement;

public sealed class PropsMap<T> where T : Entity
{
    #region properties

    public List<T> Entities { get; }

    public Entity this[int id]
    {
        get
        {
            if (id < 0 || id >= Entities.Count)
            {
                throw new IndexOutOfRangeException("id out of range of the map");
            }

            return Entities[id];
        }
    }

    #endregion

    #region constructor

    public PropsMap()
    {
        Entities = GenerateMap();
    }

    #endregion

    #region methodes

    private List<T> GenerateMap()
    {
        return new List<T>();
    }

    #endregion
}