using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.InputSettings;

public class Button : Entity
{
    #region fields
    
    private MouseState _currentMouse;
    private MouseState _previousMouse;

    private readonly SpriteFont _font;

    private bool _isHoverIng;

    private readonly Texture2D _texture;
    
    #endregion

    #region properties
    
    public event EventHandler Click;

    public Color PenColor { get; init; }

    public Vector2 Position { get; init; }
    
    public Vector2 FontSize { get; init; }

    private Rectangle rectangle => new((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);

    public string text { get; init; }
    
    #endregion

    #region methodes
    
    public Button(Texture2D text, SpriteFont fo)
    {
        _texture = text;
        _font = fo;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var color = Color.White;

        if (_isHoverIng)
        {
            color = Color.MonoGameOrange;
        }

        spriteBatch.Draw(_texture, rectangle, color);

        if (string.IsNullOrEmpty(text))
            return;
        
        var x = rectangle.X + rectangle.Width / 2f - _font.MeasureString(text).X / 2;
        var y = rectangle.Y + rectangle.Height / 2f - _font.MeasureString(text).Y / 2;

        spriteBatch.DrawString(_font, text, new Vector2(x, y), PenColor);
    }

    public override void Update(GameTime gameTime)
    {
        _previousMouse = _currentMouse;
        _currentMouse = Mouse.GetState();

        var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

        _isHoverIng = false;

        if (!mouseRectangle.Intersects(rectangle))
            return;
        
        _isHoverIng = true;

        if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
        {
            Click?.Invoke(this, EventArgs.Empty);
        }
    }
    
    #endregion
}