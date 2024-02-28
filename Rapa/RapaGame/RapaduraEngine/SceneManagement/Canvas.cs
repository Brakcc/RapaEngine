using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement;

public class Canvas
{
    private readonly RenderTarget2D _target;
    private readonly GraphicsDevice _graphDevice;
    private Rectangle _rect;

    public Canvas(GraphicsDevice graphDevice, int height, int width)
    {
        _graphDevice = graphDevice;
        _target = new RenderTarget2D(_graphDevice, width, height);
    }

    public void SetDestRect()
    {
        var screenSize = _graphDevice.PresentationParameters.Bounds;

        var scaleX = (float)screenSize.Width / _target.Width;
        var scaleY = (float)screenSize.Height / _target.Height;
        var scale = Math.Min(scaleX, scaleY);

        var newWidth = (int)(_target.Width * scale);
        var newHeight = (int)(_target.Height * scale);

        var posX = (screenSize.Width - newWidth) / 2;
        var posY = (screenSize.Height - newHeight) / 2;

        _rect = new Rectangle(posX, posY, newWidth, newHeight);
    }

    public void OnActivate()
    {
        _graphDevice.SetRenderTarget(_target);
        _graphDevice.Clear(Color.Wheat);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _graphDevice.SetRenderTarget(null);
        _graphDevice.Clear(Color.Black);
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        spriteBatch.Draw(_target, _rect, Color.White);
        spriteBatch.End();
    }
}