using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.GameContent.PlayerInfo;
using Rapa.RapaGame.RapaduraEngine;
using Rapa.RapaGame.RapaduraEngine.CameraManagement;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Props;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;
using Rapa.RapaGame.RapaduraEngine.InputSettings;
using Rapa.RapaGame.RapaduraEngine.SceneManagement.MapManagement;
using Rapa.RapaGame.RapaduraEngine.SceneManagement.Packers;

namespace Rapa.RapaGame.GameContent.Scenes.Pools;

public class MenuPool : EntityPool
{
    public MenuPool()
    {
        var testText = CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/TestCrystile");

        var star = new Dictionary<string, Animation>
        {
            {
                "idle",
                new Animation(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/BackGrounds/BProps/StarA"), 4,
                    0.35f)
            }
        };

        var mock = CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/MockUps/MockUpDesert");

        //Appel du Perso
        var player = new Player(testText, debugMode:true)
        {
            Position = new Vector2(8, 146),
            input = new Inputs
            {
                Up = Keys.Z,
                Down = Keys.S,
                Left = Keys.Q,
                Right = Keys.D,
                Jump = Keys.Space,
                Special = Keys.CapsLock
            },
            speed = 0.35f,
            Layer = -3f
        };

        var focus = new Empty { Position = new Vector2(320f, 180f) };
        var camera = new Camera(focus);

        var _normalSprite = new List<NormalProp>
        {
            new(mock, 320, 180) { Position = Vector2.Zero, Layer = 2 }
        };

        var _solidSprites = new List<Solid>
        {
            new(testText) { Position = new Vector2(0, 32), Layer = 1 },
            new(testText) { Position = new Vector2(24, 32), Layer = 1 },
            new(testText) { Position = new Vector2(48, 32), Layer = 1 },
            new(testText) { Position = new Vector2(72, 32), Layer = 1 },
            new(testText) { Position = new Vector2(96, 32), Layer = 1 },
            new(testText) { Position = new Vector2(120, 32), Layer = 1 }
        };

        var stars = new List<AnimatedProp>
        {
            new(star, 11, 11) { Position = new Vector2(100, 100), Layer = -3f },
            new(star, 11, 11) { Position = new Vector2(180, 75), Layer = -3f },
            new(star, 11, 11) { Position = new Vector2(220, 55), Layer = -3f }
        };

        var map = new TileMap<Tile>(8, 8, 0, Vector2.Zero, "Content/NewTestMap.txt", "ArtContent/Tiles/SandTilesV2/", "ST");

        var map2 = new TileMap<Tile>(8, 8, 0, new Vector2(320, 0), "Content/NewTestMap.txt",
            "ArtContent/Tiles/SandTilesV2/", "ST");
        
        var map3 = new TileMap<Tile>(8, 8, 0, new Vector2(640, 0), "Content/NewTestMap.txt",
            "ArtContent/Tiles/SandTilesV2/", "ST");
        
        
        foreach (var s in _normalSprite)
            _entities.Add(s);

        foreach (var s in _solidSprites)
            _entities.Add(s);
        
        foreach (var s in stars)
            _entities.Add(s);

        foreach (var t in map.Tiles)
            _entities.Add(t);

        foreach (var t in map2.Tiles)
            _entities.Add(t);
        
        foreach (var t in map3.Tiles)
            _entities.Add(t);
        
        _entities.Add(camera);
        _entities.Add(focus);
        _entities.Add(player);
    }
}