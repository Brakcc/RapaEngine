using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement.MapManagement;

public class TileMap : IEnumerable<Entity>
{
    #region properties

    public Entity this[int id]
    {
        get
        {
            if (id < 0 || id >= _tiles.Count)
            {
                throw new IndexOutOfRangeException("Id out of range of the map");
            }

            return _tiles[id];
        }
    }

    #endregion
    
    #region constructor
        
    public TileMap(int tileWidth, int tileHeight, int tileOffset, string mapPath, string tileKey)
    {
        _tileWidth = tileWidth;
        _tileHeight = tileHeight;
        _tileOffset = tileOffset;
        _mapPath = mapPath;
        _tileKey = tileKey;
        _tiles = new List<Entity>();

        GenerateMap();
    }
    
    #endregion

    #region methodes

    private void GenerateMap()
    {
        var mapData = TileMapGenerator.GetMapMatrix(_mapPath);
        
        for (var i = 0; i <= mapData.Length; i++)
        {
            for (var j = 0; j <= mapData[i].Length; j++)
            {
                if (mapData[i][j] != 0)
                {
                    AddEntity(new Empty());
                }
            }
        }
    }

    protected virtual void AddEntity<T>(T ent) where T : Entity
    {
        _tiles.Add(ent);
    }
    
    public IEnumerator<Entity> GetEnumerator() => _tiles.GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion

    #region fields

    private List<Entity> _tiles;
    
    private int _tileOffset;
    
    private int _tileWidth;
    
    private int _tileHeight;

    private string _mapPath;

    private string _tileKey;

    #endregion


}