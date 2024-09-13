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
        //sprites
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

        //Empty and cam
        var focus = new Empty { Position = new Vector2(320f, 180f) };
        var camera = new Camera(focus);
        
        //player
        var player = new Player(testText, debugMode:false)
        {
            Position = new Vector2(0, 0),
            input = new Inputs
            {
                Up = Keys.Z,
                Down = Keys.S,
                Left = Keys.Q,
                Right = Keys.D,
                Jump = Keys.Space,
                Special = Keys.CapsLock
            },
            speed = 0.45f,
            Layer = -3
        };

        //BackGrounds
        var normalSprite = new List<NormalProp>
        {
            new(mock, 320, 180) { Position = Vector2.Zero, Layer = 2 },
            new(mock) {Position = new Vector2(320, 0), Layer = 2},
            new(mock) {Position = new Vector2(640, 0), Layer = 2}
        };
        
        var stars = new List<AnimatedProp>
        {
            new(star, 11, 11) { Position = new Vector2(100, 100), Layer = -3 },
            new(star, 11, 11) { Position = new Vector2(180, 75), Layer = -3 },
            new(star, 11, 11) { Position = new Vector2(220, 55), Layer = -3 }
        };

        //Maps
        var map = new TileMap<Tile>(8, 8, 0, Vector2.Zero, "Content/NewTestMap.rapa", 
            "ArtContent/Tiles/SandTilesV2/", "ST");

        var map2 = new TileMap<Tile>(8, 8, 0, new Vector2(320, 0), "Content/NewTestMap.rapa",
            "ArtContent/Tiles/SandTilesV2/", "ST");
        
        var map3 = new TileMap<Tile>(8, 8, 0, new Vector2(640, 0), "Content/NewTestMap.rapa",
            "ArtContent/Tiles/SandTilesV2/", "ST");
        
        //Entity adding
        foreach (var s in normalSprite)
            entities.Add(s);
        
        foreach (var s in stars)
            entities.Add(s);

        foreach (var t in map.Tiles)
            entities.Add(t);

        foreach (var t in map2.Tiles)
            entities.Add(t);
        
        foreach (var t in map3.Tiles)
            entities.Add(t);
        
        entities.Add(camera);
        entities.Add(focus);
        entities.Add(player);
    }
}