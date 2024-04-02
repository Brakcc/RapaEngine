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
using Rapa.RapaGame.RapaduraEngine.SceneManagement.Packers;

namespace Rapa.RapaGame.GameContent.Scenes.Pools;

public class MenuPool : EntityPool
{
    public MenuPool()
    {
        var testText = CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/TestCrystile");

        var solidSands = new[]
        {
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST2"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST3"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST4"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST5"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST6"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST7"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST8"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST9"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST10"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST11"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST12"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST13"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST14"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST15"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST16"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST18"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST19"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST20"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST21"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST22"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST23"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST24"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST25"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST26"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST27"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST28"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST29"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST30"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST31"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST32"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST33"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST34"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST35"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST36"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST37"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST38"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST39"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST40"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST41"),
            CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/Tiles/SandTilesV2/ST42")
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
            speed = 0.35f,
            Layer = -3f
        };

        var focus = new Empty { Position = new Vector2(320f / 2, 180f / 2) };
        var camera = new Camera();

        var _normalSprite = new List<NormalProp>
        {
            new(mock, 320, 180) { Position = Vector2.Zero, Layer = 2 }
        };

        var _solidSprites = new List<Solid>
        {
            new(testText, debugMode:true) { Position = new Vector2(0, 32), Layer = 1 },
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

        foreach (var s in _normalSprite)
            _entities.Add(s);

        foreach (var s in _solidSprites)
            _entities.Add(s);
        
        foreach (var s in stars)
            _entities.Add(s);

        _entities.Add(camera);
        _entities.Add(focus);
        _entities.Add(player);
    }
}