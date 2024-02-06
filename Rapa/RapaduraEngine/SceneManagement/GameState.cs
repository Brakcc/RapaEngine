using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaduraEngine.GameContent.CameraManagement;
using Rapa.RapaduraEngine.GameContent.Entity.Sprites;
using Rapa.RapaduraEngine.GameContent.Entity.Sprites.Animation;
using Rapa.RapaduraEngine.GameContent.Entity.Sprites.Player;
using Rapa.RapaduraEngine.InputSettings;

namespace Rapa.RapaduraEngine.SceneManagement
{
    public class GameState : State
    {
  
        private List<HollowSrpite> hollowSprites;
        private List<Parallaxe> parallaxes;

        private float timer;

        private Player player;
        private Player playerplayerplayer;
        private Camera camera;

        //private TmxMap map;
        private SolidSprite text;
        private List<Rectangle> colliders;
        //private TileMapManager tileMap;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, SpriteBatch spriteBatch) : base(game, graphicsDevice, content, spriteBatch)
        {
            Restart();
        }

        private void Restart()
        {
            //InitMap
            //map = new("ArtContent\\TestTileMap");
            //text = _content.Load<Texture2D>("ArtContent/TestTileSet/" + map.Tilesets[0].Name);
            //var tileWidth = map.Tilesets[0].TileWidth;
            //var tileHeight = map.Tilesets[0].TileHeight;
            //var tileSetTileWidth = text.Width / tileWidth;
            //tileMap = new TileMapManager(map, text, tileSetTileWidth, tileWidth, tileHeight);
                
            colliders = new List<Rectangle>();
            
            //Inits des anims des Components
            var anims = new Dictionary<string, Animation>
            {
                { "Idle", new Animation(_content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f, 0.8f) },
                { "WalkRight", new Animation(_content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f, 0.8f) },
                { "WalkLeft" , new Animation(_content.Load<Texture2D>("ArtContent/Chara/TestChara"), 5, 0.2f, 0.8f)}
            };
            var back = new Dictionary<string, Animation>()
            {
                {"back", new Animation(_content.Load<Texture2D>("ArtContent/BackGrounds/BackGround1"), 1, 1f, 0f) },
            };

            camera = new Camera();

            //Appel du Perso
            player = new Player(anims)
            {
                position = new Vector2(0, 0),
                input = new Inputs
                {
                    Up = Keys.Z,
                    Down= Keys.S,
                    Left = Keys.Q,
                    Right = Keys.D,
                    Jump = Keys.Space,
                    Special = Keys.CapsLock
                },
                speed = 10f,
            };

            /*playerplayerplayer = new Player(anims)
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
            };*/

            text = new SolidSprite(anims)
            {
                position = new Vector2(50, 5),
            };


            parallaxes = new List<Parallaxe>()
            {
                new Parallaxe(_content.Load<Texture2D>("ArtContent/BackGrounds/BackGround1"), player, 8f)
                {
                    layer = 0f,
                },
                new Parallaxe(_content.Load<Texture2D>("ArtContent/TextTileMap"), player, 5f)
                {
                    layer = 0.5f,
                },
            };

            hollowSprites = new List<HollowSrpite>()
            {
                new HollowSrpite(back)
                {
                    position = new Vector2(-100, -100),
                },
                new HollowSrpite(back)
                {
                    position = new Vector2(-500, -500),
                },
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);
            //player.Draw(spriteBatch);
            foreach (var para in parallaxes)
            {
                //para.Draw(gameTime, spriteBatch);
            }
            //tileMap.Draw(spriteBatch);
            
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, transformMatrix: camera.Transform);
            
            player.Draw(spriteBatch);

            foreach(var hollow in hollowSprites)
            {
                hollow.Draw(spriteBatch);
            }

            text.Draw(spriteBatch);

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

            foreach (var hollow in hollowSprites)
            {
                hollow.Update(gameTime);
            }

            player.Update(gameTime, new List<SolidSprite> { text });

            text.Update(gameTime, null);

            foreach (var hollow in hollowSprites)
            {
                hollow.Update(gameTime, null);
            }

            foreach (var para in parallaxes)
            {
                //para.Update(gameTime);
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
}
