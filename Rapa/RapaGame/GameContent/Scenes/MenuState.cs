using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.InputSettings;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;
using static Rapa.RapaGame.Rapadura;
using static Rapa.RapaGame.RapaduraEngine.CoreEngine;

namespace Rapa.RapaGame.GameContent.Scenes;

public sealed class MenuState : State
{
    #region fields

    private readonly List<Button> _components;
    private readonly HollowSprite _hollow;
    private readonly SolidSprite _solid;
    private readonly GraphicsDeviceManager _graphicsDeviceManager;
    private readonly Canvas _canvas;

    #endregion
    
    #region constructor
    
    public MenuState(Rapadura game, GraphicsDevice graphicsDevice, ContentManager content, SpriteBatch spriteBatch, GraphicsDeviceManager graphManager) : base(game, graphicsDevice, content, spriteBatch)
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
            //loadGameButton,
            quitGameButton,
            fullScreenButton
        };
        
        var anims = new Dictionary<string, Animation>
        {
            {"back", new Animation(_content.Load<Texture2D>("ArtContent/BackGrounds/UnitTileTest"), 1, 1f, 0.5f) }
        };
        
        _hollow = new HollowSprite(anims)
        {
            Position = new Vector2(CurrentScreenWidth / 2f, CurrentScreenHeight / 2f)
        };
        
        _solid = new SolidSprite(anims)
        {
            Position = new Vector2(CurrentScreenWidth / 2f - 16, CurrentScreenHeight / 2f)
        };
        
        _graphicsDeviceManager = graphManager;

        _canvas = new Canvas(graphicsDevice, CurrentScreenHeight, CurrentScreenWidth);
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
        //_game.Exit();
        SetWindowed(500, 350);
    }

    private static void OnFullScreen(object sender, EventArgs e)
    {
        SetFullScreen();
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _hollow.Draw(spriteBatch);
        _solid.Draw(spriteBatch);
        
        foreach (var component in _components) 
        {
            component.Draw(spriteBatch);
        }
        spriteBatch.End();
    }
    public override void Update(GameTime gameTime)
    {
        _hollow.Update(gameTime);
        _solid.Update(gameTime);
        
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
    
    #endregion
}