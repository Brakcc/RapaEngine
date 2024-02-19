using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.CameraManagement;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.InputSettings;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame.GameContent.Scenes;

public class GameState : State
{
  
    private List<HollowSprite> _hollowSprites;
    private List<SolidSprite> _solidSprites;
    private List<Parallaxe> parallaxes;

    private float timer;

    private Player.Player player;
    private Player.Player playerplayerplayer;
    private Camera camera;

    private SolidSprite text;
    private List<Rectangle> colliders;

    public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, SpriteBatch spriteBatch) : base(game, graphicsDevice, content, spriteBatch)
    {
        Restart();
    }

    private void Restart()
    {
        colliders = new List<Rectangle>();
            
        //Inits des anims des Components
        var anims = new Dictionary<string, Animation>
        {
            { "Idle", new Animation(_content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f, 0.8f) },
            { "WalkRight", new Animation(_content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f, 0.8f) },
            { "WalkLeft" , new Animation(_content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f, 0.8f)}
        };
        var back = new Dictionary<string, Animation>
        {
            {"back", new Animation(_content.Load<Texture2D>("ArtContent/BackGrounds/TextTileMap"), 1, 1f, 0f) }
        };
        var front = new Dictionary<string, Animation>
        {
            {"front", new Animation(_content.Load<Texture2D>("ArtContent/BackGrounds/UnitTileTest"), 1, 1f, 0.55f)}
        };

        camera = new Camera();

        //Appel du Perso
        player = new Player.Player(anims)
        {
            position = new Vector2(0, 36),
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

        playerplayerplayer = new Player.Player(anims)
        {
            position = new Vector2(100, 0),
            input = new Inputs
            {
                Up = Keys.Up,
                Down = Keys.Down,
                Left = Keys.Left,
                Right = Keys.Right,
                Jump = Keys.Space,
                Special = Keys.CapsLock
            },
            speed = 1f
        };

        /*text = new SolidSprite(anims)
        {
            position = new Vector2(50, 5),
        };*/


        parallaxes = new List<Parallaxe>
        {
            new(_content.Load<Texture2D>("ArtContent/BackGrounds/BackGround1"), player, 8f)
            {
                layer = 0f
            },
            new(_content.Load<Texture2D>("ArtContent/BackGrounds/TextTileMap"), player, 5f)
            {
                layer = 0.5f
            },
        };

        /*_hollowSprites = new List<HollowSprite>
        {
            new(back)
            {
                position = new Vector2(-100, -100),
            },
            new(back)
            {
                position = new Vector2(-500, -500),
            },
        };*/

        _solidSprites = new List<SolidSprite>
        {
            new(front)
            {
                position = new Vector2(0, 0)
            },
            new(front)
            {
                position = new Vector2(24, 0)
            },
            new(front)
            {
                position = new Vector2(48, 0)
            },
            new(front)
            {
                position = new Vector2(72, 0)
            },
            new(front)
            {
                position = new Vector2(96, 0)
            },
            new(front)
            {
                position = new Vector2(120, 0)
            }
        };
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);
        
        foreach (var para in parallaxes)
        {
            para.Draw(gameTime, spriteBatch);
        }
        
        spriteBatch.End();

        spriteBatch.Begin(SpriteSortMode.BackToFront, transformMatrix: camera.Transform);
            
        player.Draw(spriteBatch);
        
        foreach (var s in _solidSprites)
        {
            s.Draw(gameTime, spriteBatch);
        }
        
        /*foreach(var hollow in _hollowSprites)
        {
            hollow.Draw(spriteBatch);
        }*/

        //text.Draw(spriteBatch);

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
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content, _spriteBatch));
        }

        /*foreach (var hollow in _hollowSprites)
        {
            hollow.Update(gameTime);
        }*/

        player.Update(gameTime, new List<SolidSprite> { text });

        //text.Update(gameTime, null);

        foreach (var para in parallaxes)
        {
            para.Update(gameTime);
        }
        foreach (var s in _solidSprites)
        {
            s.Update(gameTime);
        }
        
        //suivi du joueur avec la cam
        camera.Follow(player);

        if (timer > 0.25f)
        {
            timer = 0f;
        }

        //compteurs d'entitées
        /*for (int i = 0; i < solidSprites.Count; i++)
        {
            var sprite = solidSprites[i];

            if (sprite.isRemoved)
            {
                solidSprites.RemoveAt(i);
                i--;
            }
            if (sprite is Player)
            {
                var player = sprite as Player;
                if (player.hasDied)
                {
                    Restart();
                }
            }
        }*/
    }
}