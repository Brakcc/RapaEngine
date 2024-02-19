using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rapa.RapaGame.RapaduraEngine.InputSettings;

public class Button : AbstractEntity
{
    #region fields
    private MouseState currentMouse;
    private MouseState previousMouse;

    private SpriteFont font;

    private bool isHoverIng;

    private Texture2D texture;
    #endregion

    #region properties
    public event EventHandler Click;

    public bool clicked { get; private set; }

    public Color penColor { get; set; }

    public Vector2 position { get; set; }

    public Rectangle rectangle
    { 
        get
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
    }

    public string text { get; set; }
    #endregion

    #region methodes
    public Button(Texture2D text, SpriteFont fo)
    {
        texture = text;
        font = fo;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        var color = Color.White;

        if (isHoverIng)
        {
            color = Color.Cyan;
        }

        spriteBatch.Draw(texture, rectangle, color);

        if (!string.IsNullOrEmpty(text))
        {
            var x = (rectangle.X + (rectangle.Width / 2)) - (font.MeasureString(text).X / 2);
            var y = (rectangle.Y + (rectangle.Height / 2)) - (font.MeasureString(text).Y / 2);

            spriteBatch.DrawString(font, text, new Vector2(x, y), penColor);
        }
    }

    public override void Update(GameTime gameTime)
    {
        previousMouse = currentMouse;
        currentMouse = Mouse.GetState();

        var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

        isHoverIng = false;

        if (mouseRectangle.Intersects(rectangle))
        {
            isHoverIng = true;

            if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
            {
                Click?.Invoke(this, new EventArgs());
            }
        }
    }
    #endregion
}