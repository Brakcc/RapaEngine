using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Rapa.RapaGame.RapaduraEngine.Entities;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement.MapManagement;

public sealed class TileMap<T> where T : Entity
{
    #region properties
    
    public List<T> Tiles { get; }

    public Entity this[int id]
    {
        get
        {
            if (id < 0 || id >= Tiles.Count)
            {
                throw new IndexOutOfRangeException("Id out of range of the map");
            }
            return Tiles[id];
        }
    }

    #endregion
    
    #region constructor
        
    public TileMap(int tileWidth, int tileHeight, int layer, Vector2 tileOffset, string mapPath, string tilePath, string tileKey, bool debugMod = false)
    {
        _tileWidth = tileWidth;
        _tileHeight = tileHeight;
        _layer = layer;
        _tileOffset = tileOffset;
        _mapPath = mapPath;
        _tilePath = tilePath;
        _tileKey = tileKey;
        _debugMod = debugMod;
        Tiles = GenerateMap();
    }
    
    #endregion
    
    #region methodes

    private List<T> GenerateMap()
    {
        var mapData = TileMapParser.GetMapMatrix(_mapPath);
        var tempMap = new List<T>();
        
        for (var i = 0; i < mapData.Length; i++)
        {
            for (var j = 0; j < mapData[i].Length; j++)
            {
                if (mapData[i][j] == 0)
                    continue;
                
                var pos = new Vector2(_tileWidth * j + _tileOffset.X, _tileHeight * i + _tileOffset.Y);

                if (typeof(T) == typeof(Tile))
                    tempMap.Add(GetTile(mapData[i][j], pos) as T);

                if (typeof(T) == typeof(Solid))
                    tempMap.Add(GetSolid(mapData[i][j], pos) as T);
            }
        }

        return tempMap;
    }

    private Tile GetTile(int id, Vector2 pos)
    {
        var tempEnt = new Tile(TileMapParser.TextureLoader(_tilePath, _tileKey, id), _tileWidth, _tileHeight, debugMode:_debugMod)
        {
            Position = pos,
            Layer = _layer
        };
        return tempEnt;
    }

    private Solid GetSolid(int id, Vector2 pos)
    {
        var tempEnt = new Solid(TileMapParser.TextureLoader(_tilePath, _tileKey, id), _tileWidth, _tileHeight, debugMode:_debugMod)
        {
            Position = pos,
            Layer = _layer
        };
        return tempEnt;
    }

    #endregion

    #region fields

    private readonly Vector2 _tileOffset;
    
    private readonly int _tileWidth;
    
    private readonly int _tileHeight;

    private readonly int _layer;

    private readonly string _mapPath;

    private readonly string _tilePath;
    
    private readonly string _tileKey;

    private readonly bool _debugMod;

    #endregion
}