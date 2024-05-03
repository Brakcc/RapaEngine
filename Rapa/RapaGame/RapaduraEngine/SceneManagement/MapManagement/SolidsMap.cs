using System;
using System.Collections.Generic;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement.MapManagement;

public sealed class SolidsMap<T> where T : Entity
{
    #region properties

    public List<T> Solids { get; }

    public Entity this[int id]
    {
        get
        {
            if (id < 0 || id >= Solids.Count)
            {
                throw new IndexOutOfRangeException("id out of range of the map");
            }

            return Solids[id];
        }
    }

    #endregion

    #region constructor

    public SolidsMap()
    {
        Solids = GenerateMap();
    }

    #endregion

    #region methodes

    private List<T> GenerateMap()
    {
        return new List<T>();
    }

    #endregion
}