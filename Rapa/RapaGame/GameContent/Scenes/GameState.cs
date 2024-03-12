using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.GameContent.PlayerInfo;
using Rapa.RapaGame.RapaduraEngine.CameraManagement;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Props;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;
using Rapa.RapaGame.RapaduraEngine.InputSettings;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame.GameContent.Scenes;

public class GameState : State
{
    private readonly GraphicsDeviceManager _graphManager;

    private List<NormalProp> _normalSprite;
    
    private List<AnimatedProp> _hollowSprites;
    private List<Solid> _solidSprites;
    private List<Parallaxe> parallaxes;

    private float timer;

    private Player player;
    private Camera camera;

    public GameState(Rapadura game, GraphicsDevice graphicsDevice, ContentManager content, SpriteBatch spriteBatch, GraphicsDeviceManager graphManager) : base(game, graphicsDevice, content, spriteBatch)
    {
        _graphManager = graphManager;
        
        Restart();
    }

    private void Restart()
    {
        //Inits des anims des Components
        var anims = new Dictionary<string, Animation>
        {
            { "Idle", new Animation(_content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f, 0.8f) },
            { "WalkRight", new Animation(_content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f, 0.8f) },
            { "WalkLeft" , new Animation(_content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f, 0.8f) }
        };
        var back = new Dictionary<string, Animation>
        {
            {"back", new Animation(_content.Load<Texture2D>("ArtContent/BackGrounds/UnitTileTest"), 1, 1f, 1f) }
        };
        var front = new Dictionary<string, Animation>
        {
            {"front", new Animation(_content.Load<Texture2D>("ArtContent/BackGrounds/UnitTileTest"), 1, 1f, 1f) }
        };

        var sands = new[]
        {
            _content.Load<Texture2D>("ArtContent/Tiles/SandTiles/TestSandTile1"),
            _content.Load<Texture2D>("ArtContent/Tiles/SandTiles/TestSandTile2"),
            _content.Load<Texture2D>("ArtContent/Tiles/SandTiles/TestSandTile3"),
            _content.Load<Texture2D>("ArtContent/Tiles/SandTiles/TestSandTile4"),
            _content.Load<Texture2D>("ArtContent/Tiles/SandTiles/TestSandTile5"),
            _content.Load<Texture2D>("ArtContent/Tiles/SandTiles/TestSandTile6")
        };

        camera = new Camera();

        //Appel du Perso
        player = new Player(anims)
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
            speed = 10f
        };

        _normalSprite = new List<NormalProp>
        {
            new(sands[0]) {Position = Vector2.Zero},
            new(sands[1]) {Position = new Vector2(8, 0)},
            new(sands[2]) {Position = new Vector2(16, 0)},
            new(sands[3]) {Position = new Vector2(24, 0)},
            new(sands[4]) {Position = new Vector2(32, 0)},
            new(sands[5]) {Position = new Vector2(40, 0)}
        };

        parallaxes = new List<Parallaxe>
        {
            new(_content.Load<Texture2D>("ArtContent/BackGrounds/BackGround1"), player, 8f) { layer = 0f }, 
            new(_content.Load<Texture2D>("ArtContent/BackGrounds/TextTileMap"), player, 5f) { layer = 0.5f },
        };

        _hollowSprites = new List<AnimatedProp>
        {
            new(back) { Position = new Vector2(-86, -100) }, 
            new(back) { Position = new Vector2(-500, -500) }
        };

        _solidSprites = new List<Solid>
        {
            new(front) { Position = new Vector2(0, 0) },
            new(front) { Position = new Vector2(24, 0) },
            new(front) { Position = new Vector2(48, 0) },
            new(front) { Position = new Vector2(72, 0) },
            new(front) { Position = new Vector2(96, 0) },
            new(front) { Position = new Vector2(120, 0) }
        };
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);
        
        foreach (var para in parallaxes)
        {
            //para.Draw(gameTime, spriteBatch);
        }
        
        spriteBatch.End();

        spriteBatch.Begin(SpriteSortMode.BackToFront, transformMatrix: camera.Transform);

        player.Draw(spriteBatch);
        
        foreach (var s in _solidSprites)
        {
            //s.Draw(spriteBatch);
        }
        
        foreach(var hollow in _hollowSprites)
        {
            //hollow.Draw(spriteBatch);
        }

        foreach (var normal in _normalSprite)
        {
            normal.Draw(spriteBatch);
        }

        spriteBatch.End();
    }
    
    public override void PostUpdate(GameTime gameTime)
    {
    }
    
    public override void Update(GameTime gameTime)
    {
        //timer utilisable
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content, _spriteBatch, _graphManager));
        }

        foreach (var hollow in _hollowSprites)
        {
            //hollow.Update(gameTime);
        }

        foreach (var para in parallaxes)
        {
            para.Update(gameTime);
        }
        foreach (var s in _solidSprites)
        {
            //s.Update(gameTime);
        }

        foreach (var normal in _normalSprite)
        {
            //normal.Update(gameTime);
        }
        
        player.Update(gameTime, _solidSprites);
        
        //suivi du joueur avec la cam
        camera.Follow(player);

        if (timer > 0.25f)
        {
            timer = 0f;
        }
    }
}