using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.InputSettings;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;
using static Rapa.RapaGame.Game1;

namespace Rapa.RapaGame.GameContent.Scenes;

public sealed class MenuState : State
{
    #region fields

    private readonly List<Button> _components;
    private readonly HollowSprite _hollow;
    private readonly GraphicsDeviceManager _graphicsDeviceManager;
    private readonly Canvas _canvas;
    private bool _isFullScreened;

    #endregion
    
    #region constructor
    
    public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, SpriteBatch spriteBatch, GraphicsDeviceManager graphManager) : base(game, graphicsDevice, content, spriteBatch)
    {
        var buttonTexture = _content.Load<Texture2D>("ArtContent/Buttons/Buttons");
        var buttonFont = _content.Load<SpriteFont>("ArtContent/Fonts/fontTest");

        var newGameButton = new Button(buttonTexture, buttonFont)
        {
            penColor = Color.DarkRed,
            position = new Vector2(CurrentScreenWidth / 2f - buttonTexture.Width / 2f,
                CurrentScreenHeight / 2f - buttonTexture.Height / 2f - 100),
            text = "New Game"
        };
        newGameButton.Click += NewGame;

        var loadGameButton = new Button(buttonTexture, buttonFont)
        {
            penColor = Color.DarkRed,
            position = new Vector2(CurrentScreenWidth / 2f - buttonTexture.Width / 2f,
                CurrentScreenHeight / 2f - buttonTexture.Height / 2f),
            text = "Load Game"
        };
        loadGameButton.Click += LoadGame;
        loadGameButton.Click += OnFullScreen;

        var quitGameButton = new Button(buttonTexture, buttonFont)
        {
            penColor = Color.DarkRed,
            position = new Vector2(CurrentScreenWidth / 2f - buttonTexture.Width / 2f,
                CurrentScreenHeight / 2f - buttonTexture.Height / 2f + 100),
            text = "quit"
        };
        quitGameButton.Click += QuitGame;

        var fullScreenButton = new Button(buttonTexture, buttonFont)
        {
            penColor = Color.DarkRed,
            position = new Vector2(CurrentScreenWidth / 2f - buttonTexture.Width / 2f,
                CurrentScreenHeight / 2f - buttonTexture.Height / 2f + 200),
            text = "fullScreen"
        };
        fullScreenButton.Click += OnFullScreen;
        
        _components = new List<Button>
        {
            newGameButton,
            loadGameButton,
            quitGameButton,
            fullScreenButton
        };
        
        var anims = new Dictionary<string, Animation>
        {
            {"back", new Animation(_content.Load<Texture2D>("ArtContent/TextTileMap"), 1, 1f, 0.5f) }
        };
        
        _hollow = new HollowSprite(anims)
        {
            Position = new Vector2(CurrentScreenWidth / 2f, CurrentScreenHeight / 2f)
        };
        
        _graphicsDeviceManager = graphManager;

        _canvas = new Canvas(graphicsDevice, CurrentScreenHeight, CurrentScreenWidth);

        _isFullScreened = false;
        
        SetResolution(CurrentScreenHeight, CurrentScreenWidth);
    }
    
    #endregion

    #region methodes

    private void NewGame(object sender, EventArgs e)
    {
        _game.ChangeState(new GameState(_game, _graphicsDevice, _content, _spriteBatch, _graphicsDeviceManager));
    }
    private static void LoadGame(object sender, EventArgs e)
    {
        Console.WriteLine("Load Game");
    }
    private void QuitGame(object sender, EventArgs e)
    {
        _game.Exit();
    }

    private void OnFullScreen(object sender, EventArgs e)
    {
        SetFullScreen();
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        _canvas.OnActivate();

        spriteBatch.Begin();

        _hollow.Draw(spriteBatch);
        
        foreach (var component in _components) 
        {
            component.Draw(spriteBatch);
        }
        spriteBatch.End();
        
        _canvas.Draw(spriteBatch);
    }
    public override void Update(GameTime gameTime) 
    {
        _hollow.Update(gameTime);
        
        foreach (var component in _components)
        {
            component.Update(gameTime);
        }
    }
    public override void PostUpdate(GameTime gameTime)
    {
        //not used now
    }

    public override void OnInit()
    {
        _canvas.OnActivate();
    }

    private void SetResolution(int height, int width)
    {
        _graphicsDeviceManager.PreferredBackBufferHeight = height;
        _graphicsDeviceManager.PreferredBackBufferWidth = width;
        _graphicsDeviceManager.ApplyChanges();
        _canvas.SetDestRect();
    }

    private void SetFullScreen()
    {
        if (!_isFullScreened)
        {
            _graphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphicsDeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _game.Window.IsBorderless = true;
            _graphicsDeviceManager.ApplyChanges();
            _canvas.SetDestRect();
            _isFullScreened = true;

            CurrentScreenWidth = _graphicsDeviceManager.PreferredBackBufferWidth;
            CurrentScreenHeight = _graphicsDeviceManager.PreferredBackBufferHeight;
        }
        else
        {
            _graphicsDeviceManager.PreferredBackBufferHeight = TargetScreenHeight;
            _graphicsDeviceManager.PreferredBackBufferWidth = TargetScreenWidth;
            _game.Window.IsBorderless = false;
            _graphicsDeviceManager.ApplyChanges();
            _canvas.SetDestRect();
            _isFullScreened = false;
            
            CurrentScreenWidth = _graphicsDeviceManager.PreferredBackBufferWidth;
            CurrentScreenHeight = _graphicsDeviceManager.PreferredBackBufferHeight;
        }
    }
    
    #endregion
}