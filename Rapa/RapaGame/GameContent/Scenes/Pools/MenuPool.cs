﻿using System.Collections.Generic;
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
using Rapa.RapaGame.RapaduraEngine.SceneManagement.Packers;

namespace Rapa.RapaGame.GameContent.Scenes.Pools;

public class MenuPool : EntityPool
{
    public MenuPool()
    {
        //Inits des anims des Components
        var anims = new Dictionary<string, Animation>
        {
            {
                "Idle",
                new Animation(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f)
            },
            {
                "WalkRight",
                new Animation(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f)
            },
            {
                "WalkLeft",
                new Animation(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f)
            }
        };
        var back = new Dictionary<string, Animation>
        {
            {
                "back",
                new Animation(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/BackGrounds/UnitTileTest"), 1, 1f)
            }
        };
        var front = new Dictionary<string, Animation>
        {
            {
                "front",
                new Animation(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/BackGrounds/UnitTileTest"), 1, 1f)
            }
        };

        var testText = CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/TestCrystile");

        var solidSands = new[]
        {
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST2"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST3"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST4"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST5"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST6"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST7")
        };

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
            Position = new Vector2(0, 46),
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
            Layer = -3f
        };

        var focus = new Empty { Position = new Vector2(320f / 2, 180f / 2) };
        var camera = new Camera();

        var _normalSprite = new List<NormalProp>
        {
            new(mock) { Position = Vector2.Zero, Layer = 2 },
            new(solidSands[0], 8, 8, debugMode: true) { Position = Vector2.Zero, Layer = -1 },
            new(solidSands[1], 8, 8, debugMode: true) { Position = new Vector2(8, 0), Layer = -1 },
            new(solidSands[2], 8, 8, debugMode: true) { Position = new Vector2(16, 0), Layer = -1 },
            new(solidSands[3], 8, 8, debugMode: true) { Position = new Vector2(24, 0), Layer = -1 },
            new(solidSands[4], 8, 8, debugMode: true) { Position = new Vector2(32, 0), Layer = -1 },
            new(solidSands[5], 8, 8, debugMode: true) { Position = new Vector2(40, 0), Layer = -1 }
        };

        var parallaxes = new List<Parallaxe>
        {
            //new(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/BackGrounds/BackGround1"), player, 8f) { layer = 0f }, 
            //new(CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/BackGrounds/TextTileMap"), player, 5f) { layer = 0.5f },
        };

        var _hollowSprites = new List<AnimatedProp>
        {
            //new(back, 8, 8, debugMode: true) { Position = new Vector2(50, 0), Layer = -1 },
            //new(back, 8, 8, debugMode: true) { Position = new Vector2(60, 0), Layer = -1 }
        };

        var _solidSprites = new List<Solid>
        {
            new(testText/*, debugMode: true*/) { Position = new Vector2(0, 32), Layer = 1 },
            new(testText/*, debugMode: true*/) { Position = new Vector2(24, 32), Layer = 1 },
            new(testText/*, debugMode: true*/) { Position = new Vector2(48, 32), Layer = 1 },
            new(testText/*, debugMode: true*/) { Position = new Vector2(72, 32), Layer = -3 },
            new(testText/*, debugMode: true*/) { Position = new Vector2(96, 32), Layer = 1 },
            new(testText/*, debugMode: true*/) { Position = new Vector2(120, 32), Layer = 1 }
        };

        var stars = new List<AnimatedProp>
        {
            new(star, 11, 11) { Position = new Vector2(100, 100), Layer = -3f },
            new(star, 11, 11) { Position = new Vector2(180, 75), Layer = -3f },
            new(star, 11, 11) { Position = new Vector2(220, 55), Layer = -3f }
        };

        foreach (var s in _normalSprite)
        {
            _entities.Add(s);
        }

        foreach (var s in _solidSprites)
        {
            _entities.Add(s);
        }

        foreach (var s in _hollowSprites)
        {
            //_entities.Add(s);
        }

        foreach (var s in stars)
        {
            _entities.Add(s);
        }

        _entities.Add(camera);
        _entities.Add(focus);
        _entities.Add(player);
    }
}