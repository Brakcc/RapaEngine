using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Props;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;
using Rapa.RapaGame.RapaduraEngine.InputSettings;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;
using static Rapa.RapaGame.Rapadura;
using static Rapa.RapaGame.RapaduraEngine.CoreEngine;

namespace Rapa.RapaGame.GameContent.Scenes;

public sealed class MenuState : State
{
    #region fields

    private readonly List<Button> _components;
    private readonly List<NormalProp> _hollows;
    private readonly SolidSprite _solid;
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    #endregion
    
    #region constructor
    
    public MenuState(Rapadura game, GraphicsDevice graphicsDevice, ContentManager content, SpriteBatch spriteBatch, GraphicsDeviceManager graphManager) : base(game, graphicsDevice, content, spriteBatch)
    {
        var buttonTexture = _content.Load<Texture2D>("ArtContent/Buttons/TestButton");
        var buttonFont = _content.Load<SpriteFont>("ArtContent/Fonts/fontTest");

        var newGameButton = new Button(buttonTexture, buttonFont)
        {
            //PenColor = Color.DarkRed,
            Position = new Vector2(CurrentScreenWidth / 2f - buttonTexture.Width / 2f,
                CurrentScreenHeight / 2f - buttonTexture.Height / 2f - 24),
            Text = "New Game"
        };
        newGameButton.Click += NewGame;

        var loadGameButton = new Button(buttonTexture, buttonFont)
        {
            //PenColor = Color.DarkRed,
            Position = new Vector2(CurrentScreenWidth / 2f - buttonTexture.Width / 2f,
                CurrentScreenHeight / 2f - buttonTexture.Height / 2f),
            Text = "Load Game"
        };
        loadGameButton.Click += LoadGame;
        loadGameButton.Click += OnFullScreen;

        var quitGameButton = new Button(buttonTexture, buttonFont)
        {
            //PenColor = Color.DarkRed,
            Position = new Vector2(CurrentScreenWidth / 2f - buttonTexture.Width / 2f,
                CurrentScreenHeight / 2f - buttonTexture.Height / 2f + 24),
            Text = "quit"
        };
        quitGameButton.Click += QuitGame;

        var fullScreenButton = new Button(buttonTexture, buttonFont)
        {
            //PenColor = Color.DarkRed,
            Position = new Vector2(CurrentScreenWidth / 2f - buttonTexture.Width / 2f,
                CurrentScreenHeight / 2f - buttonTexture.Height / 2f + 48),
            Text = "fullScreen"
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
            {"back", new Animation(_content.Load<Texture2D>("ArtContent/Tiles/TestCrystile"), 1, 1f, 0.5f) }
        };

        var testText = _content.Load<Texture2D>("ArtContent/Tiles/TestCrystile");
        var testText2 = _content.Load<Texture2D>("ArtContent/Tiles/SandTiles/TestSandTile1");

        _hollows = new List<NormalProp>
        {
            new(testText) {Position = new Vector2(CurrentScreenWidth / 2f, CurrentScreenHeight / 2f), Layer = 3},
            new(testText) {Position = new Vector2(CurrentScreenWidth / 2f - 8, CurrentScreenHeight / 2f)},
            new(testText) {Position = new Vector2(CurrentScreenWidth / 2f - 16, CurrentScreenHeight / 2f)},
            new(testText) {Position = new Vector2(CurrentScreenWidth / 2f + 8, CurrentScreenHeight / 2f)},
            new(testText) {Position = new Vector2(CurrentScreenWidth / 2f + 16, CurrentScreenHeight / 2f)},
            new(testText) {Position = new Vector2(CurrentScreenWidth / 2f + 16, CurrentScreenHeight / 2f - 8)}
        };
        
        _hollows[1].AddComponent(new BaseSprite(_hollows[1], testText2, 1));
        
        _solid = new SolidSprite(anims)
        {
            Position = new Vector2(CurrentScreenWidth / 2f - 8, CurrentScreenHeight / 2f)
        };
        
        _graphicsDeviceManager = graphManager;
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

        foreach (var hollow in _hollows)
        {
            hollow.Draw(spriteBatch);
        }
        
        foreach (var component in _components) 
        {
            component.Draw(spriteBatch);
        }
        
        spriteBatch.End();
    }
    public override void Update(GameTime gameTime)
    {
        foreach (var hollow in _hollows)
        {
            hollow.Update(gameTime);
        }
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
    
    #endregion
}