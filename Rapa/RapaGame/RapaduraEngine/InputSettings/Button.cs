using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.InputSettings;

public class Button : Entity
{
    #region properties

    public Color PenColor { get; init; }
    
    public Vector2 FontSize { get; init; }

    private Rectangle Rectangle => new((int)position.X, (int)position.Y, _texture.Width, _texture.Height);

    public string Text { get; init; }
    
    #endregion

    #region constructor
    
    public Button(Texture2D text, SpriteFont fo)
    {
        _texture = text;
        _font = fo;
    }

    #endregion
    
    #region methodes
    
    public override void Render(SpriteBatch spriteBatch)
    {
        var color = Color.White;
        
        if (_isHoverIng)
        {
            color = Color.MonoGameOrange;
        }

        spriteBatch.Draw(_texture, Rectangle, color);

        if (string.IsNullOrEmpty(Text))
            return;
        
        var x = Rectangle.X + Rectangle.Width / 2f - _font.MeasureString(Text).X / 2;
        var y = Rectangle.Y + Rectangle.Height / 2f - _font.MeasureString(Text).Y / 2;

        spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
    }

    public override void Update()
    {
        _previousMouse = _currentMouse;
        _currentMouse = Mouse.GetState();

        var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

        _isHoverIng = false;

        if (!mouseRectangle.Intersects(Rectangle))
            return;
        
        _isHoverIng = true;

        if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
        {
            Click?.Invoke(this, EventArgs.Empty);
        }
    }
    
    #endregion
    
    #region fields
    
    public event EventHandler Click;
    
    private readonly Texture2D _texture;
    private readonly SpriteFont _font;
                  
    private MouseState _currentMouse;
    private MouseState _previousMouse;
              
    private bool _isHoverIng;
                  
    #endregion
}