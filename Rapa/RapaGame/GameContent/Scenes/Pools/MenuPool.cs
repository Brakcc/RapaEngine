using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.GameContent.PlayerInfo;
using Rapa.RapaGame.RapaduraEngine;
using Rapa.RapaGame.RapaduraEngine.CameraManagement;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.Entities;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Props;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;
using Rapa.RapaGame.RapaduraEngine.InputSettings;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;
using Rapa.RapaGame.RapaduraEngine.SceneManagement.Packers;

namespace Rapa.RapaGame.GameContent.Scenes.Pools;

public class MenuPool : EntityPool
{
    private static List<Entity> Full()
    {
        var list = new List<Entity>();
        
        //Inits des anims des Components
        var anims = new Dictionary<string, Animation>
        {
            { "Idle", new Animation(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f) },
            { "WalkRight", new Animation(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f) },
            { "WalkLeft" , new Animation(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f) }
        };
        var back = new Dictionary<string, Animation>
        {
            {"back", new Animation(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/BackGrounds/UnitTileTest"), 1, 1f) }
        };
        var front = new Dictionary<string, Animation>
        {
            {"front", new Animation(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/BackGrounds/UnitTileTest"), 1, 1f) }
        };

        var testText = CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/TestCrystile");
        
        var sands = new[]
        {
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTiles/TestSandTile1"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTiles/TestSandTile2"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTiles/TestSandTile3"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTiles/TestSandTile4"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTiles/TestSandTile5"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTiles/TestSandTile6")
        };

        var star = new Dictionary<string, Animation>
        {
            {"idle", new Animation(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/BackGrounds/BProps/StarA"), 4, 0.35f)}
        };

        var mock = CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/MockUps/MockUpDesert");
        
        //Appel du Perso
        var player = new Player(testText)
        {
            Position = new Vector2(0, 36),
            input = new Inputs
            {
                Up = Keys.Z,
                Down= Keys.S,
                Left = Keys.Q,
                Right = Keys.D,
                Jump = Keys.Space,
                Special = Keys.CapsLock
            },
            speed = 1f,
            Layer = -3f
        };

        var focus = new Empty { Position = new Vector2(320f / 2, 180f / 2) };
        var camera = new Camera(focus);

        var _normalSprite = new List<NormalProp>
        {
            new(mock) {Position = Vector2.Zero, Layer = -2},
            new(sands[0], 8, 8, debugMode:true) {Position = Vector2.Zero, Layer = -1},
            new(sands[1], 8, 8, debugMode:true) {Position = new Vector2(8, 0), Layer = -1},
            new(sands[2], 8, 8, debugMode:true) {Position = new Vector2(16, 0), Layer = -1},
            new(sands[3], 8, 8, debugMode:true) {Position = new Vector2(24, 0), Layer = -1},
            new(sands[4], 8, 8, debugMode:true) {Position = new Vector2(32, 0), Layer = -1},
            new(sands[5], 8, 8, debugMode:true) {Position = new Vector2(40, 0), Layer = -1}
        };

        var parallaxes = new List<Parallaxe>
        {
            new(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/BackGrounds/BackGround1"), player, 8f) { layer = 0f }, 
            new(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/BackGrounds/TextTileMap"), player, 5f) { layer = 0.5f },
        };

        var _hollowSprites = new List<AnimatedProp>
        {
            new(back, 8, 8, debugMode:true) { Position = new Vector2(50, 0), Layer = -1 }, 
            new(back, 8, 8, debugMode:true) { Position = new Vector2(60, 0), Layer = -1 }
        };

        var _solidSprites = new List<Solid>
        {
            new(testText, debugMode:true) { Position = new Vector2(0, 8), Layer = 1 },
            new(testText, debugMode:true) { Position = new Vector2(24, 8), Layer = 1 },
            new(testText, debugMode:true) { Position = new Vector2(48, 8), Layer = 1 },
            new(testText, debugMode:true) { Position = new Vector2(72, 8), Layer = 1 },
            new(testText, debugMode:true) { Position = new Vector2(96, 8), Layer = 1 },
            new(testText, debugMode:true) { Position = new Vector2(120, 8), Layer = 1 }
        };

        var stars = new List<AnimatedProp>
        {
            new(star, 11, 11) { Position = new Vector2(100, 100), Layer = -3f},
            new(star, 11, 11) { Position = new Vector2(180, 75), Layer = -3f },
            new(star, 11, 11) { Position = new Vector2(220, 55), Layer = -3f }
        };
        
        

        foreach (var s in _normalSprite)
        {
            list.Add(s);
        }
        foreach (var s in _solidSprites)
        {
            list.Add(s);
        }
        foreach (var s in _hollowSprites)
        {
            list.Add(s);
        }
        foreach (var s in stars)
        {
            list.Add(s);
        }
        
        
        list.Add(camera);
        list.Add(focus);
        list.Add(player);
        
        return list;
    }

    public MenuPool(Scene entityRef) : base(entityRef)
    {
        var l = Full();
        foreach (var e in l)
        {
            AddEntity(e);
        }
    }

    public MenuPool(Scene entityRef, List<Entity> entities) : base(entityRef, entities)
    {
    }
}