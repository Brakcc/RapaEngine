using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.Entities;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Props;
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
        Tiles = new List<T>();

        GenerateMap();
    }
    
    #endregion
    
    #region methodes

    private void GenerateMap()
    {
        var mapData = TileMapGenerator.GetMapMatrix(_mapPath);
        
        for (var i = 0; i < mapData.Length; i++)
        {
            for (var j = 0; j < mapData[i].Length; j++)
            {
                if (mapData[i][j] == 0)
                    continue;
                
                var pos = new Vector2(_tileWidth * j + _tileOffset.X, _tileHeight * i + _tileOffset.Y);

                if (typeof(T) == typeof(Tile))
                    AddTile(mapData[i][j], pos);

                if (typeof(T) == typeof(Solid))
                    AddSolid(mapData[i][j], pos);
                
                else if (typeof(T) == typeof(NormalProp))
                    AddProp(mapData[i][j], pos);
                
                else if (typeof(T) == typeof(AnimatedProp))
                    AddAnimProp(mapData[i][j], pos);
            }
        }
    }

    private void AddTile(int id, Vector2 pos)
    {
        var tempEnt = new Tile(TileMapGenerator.TextureLoader(_tilePath, _tileKey, id), _tileWidth, _tileHeight, debugMode:_debugMod)
        {
            Position = pos,
            Layer = _layer
        };
        Tiles.Add(tempEnt as T);
    }

    private void AddSolid(int id, Vector2 pos)
    {
        var tempEnt = new Solid(TileMapGenerator.TextureLoader(_tilePath, _tileKey, id), _tileWidth, _tileHeight, debugMode:_debugMod)
        {
            Position = pos,
            Layer = _layer
        };
        Tiles.Add(tempEnt as T);
    }

    private void AddProp(int id, Vector2 pos)
    {
        var tempEnt = new NormalProp(TileMapGenerator.TextureLoader(_tilePath, _tileKey, id), _tileWidth, _tileHeight, debugMode:_debugMod)
        {
            Position = pos,
            Layer = _layer
        };
        Tiles.Add(tempEnt as T);
    }

    private void AddAnimProp(int id, Vector2 pos)
    {
        var anims = new Dictionary<string, Animation>
        {
            {
                "idle", new Animation(TileMapGenerator.TextureLoader(_tilePath, _tileKey, id), 1, 1)
            }
        };
        
        var tempEnt = new AnimatedProp(anims, _tileWidth, _tileHeight, debugMode:_debugMod)
        {
            Position = pos,
            Layer = _layer
        };
        Tiles.Add(tempEnt as T);
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