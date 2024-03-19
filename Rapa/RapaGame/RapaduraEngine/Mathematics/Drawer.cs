using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rapa.RapaGame.RapaduraEngine.Mathematics;

public static class Drawer
{
    #region methodes

    public static void InitDrawer(SpriteBatch spriteBatch)
    {
        SpriteBatch = spriteBatch;
        DefaultFont = CoreEngine.Instance.Content.Load<SpriteFont>("ArtContent/Fonts/fontTest");
        Pixel = CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/DefaultPixel");
        Particule = CoreEngine.Instance.Content.Load<Texture2D>("ArtContent/DefaultPixel");

        var test = new object[] {1, 'e', "T", true, Pixel};
        Console.WriteLine(Calculus.RandomPick(test));
    }

    public static void DrawHollowRect(float x ,float y, float width, float height, Color color)
    {
        var rect = new Rectangle((int)x, (int)y, (int)width, 1);
        
        SpriteBatch.Draw(Pixel, rect, PixelRect, color);

        rect.Y += (int)height - 1;
        
        SpriteBatch.Draw(Pixel, rect, PixelRect, color);

        rect.Y -= (int)height - 1;
        rect.Width = 1;
        rect.Height = (int)height;
        
        SpriteBatch.Draw(Pixel, rect, PixelRect, color);

        rect.X += (int)width - 1;
        
        SpriteBatch.Draw(Pixel, rect, PixelRect, color);
    }
    public static void DrawParticule(Vector2 pos)
    {
        SpriteBatch.Draw(Particule, pos, Color.Red);
    }
    
    public static void DrawPixel(Vector2 pos)
    {
        SpriteBatch.Draw(Particule, pos, Color.Red);
    }
    
    #endregion

    #region fields

    private static SpriteBatch SpriteBatch;

    private static SpriteFont DefaultFont;

    private static Texture2D Pixel;

    private static Rectangle PixelRect = new Rectangle(1, 1, 1, 1);

    private static Texture2D Particule;

    #endregion
}