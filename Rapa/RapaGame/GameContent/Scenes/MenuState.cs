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

public class MenuState : State
{
    private List<Button> _components;
    private HollowSprite hollow;

    public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, SpriteBatch spriteBatch) : base(game, graphicsDevice, content, spriteBatch)
    {
        var buttonTexture = _content.Load<Texture2D>("ArtContent/Buttons/Buttons");
        var buttonFont = _content.Load<SpriteFont>("ArtContent/Fonts/fontTest");

        var newGameButton = new Button(buttonTexture, buttonFont)
        {
            position = new Vector2(screenWidth / 2 - buttonTexture.Width / 2, screenHeight / 2 - buttonTexture.Height / 2 - 100),
            text = "New Game"
        };
        newGameButton.Click += NewGame;

        var loadGameButton = new Button(buttonTexture, buttonFont)
        {
            position = new Vector2(screenWidth / 2 - buttonTexture.Width / 2, screenHeight / 2 - buttonTexture.Height / 2),
            text = "Load Game",
        };
        loadGameButton.Click += LoadGame;

        var quitGameButton = new Button(buttonTexture, buttonFont)
        {
            position = new Vector2(screenWidth / 2 - buttonTexture.Width / 2, screenHeight / 2 - buttonTexture.Height / 2 + 100),
            text = "quit",
        };
        quitGameButton.Click += QuitGame;

        _components = new List<Button>
        {
            newGameButton,
            loadGameButton,
            quitGameButton
        };
        var anims = new Dictionary<string, Animation>
        {
            {"back", new Animation(_content.Load<Texture2D>("ArtContent/BackGrounds/BackGround1"), 1, 1f, 0f) },
        };
        hollow = new HollowSprite(anims);
    }

    #region methodes

    private void NewGame(object sender, EventArgs e)
    {
        _game.ChangeState(new GameState(_game, _graphicsDevice, _content, _spriteBatch));
    }
    private void LoadGame(object sender, EventArgs e)
    {
        Console.WriteLine("Load Game");
    }
    private void QuitGame(object sender, EventArgs e)
    {
        _game.Exit();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        hollow.Draw(gameTime, spriteBatch);
        spriteBatch.End();

        spriteBatch.Begin();

        foreach (var component in _components) 
        {
            component.Draw(gameTime, spriteBatch);
        }
        spriteBatch.End();
    }
    public override void Update(GameTime gameTime) 
    {
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