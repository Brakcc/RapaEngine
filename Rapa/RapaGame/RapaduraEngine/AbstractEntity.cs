using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rapa.RapaGame.RapaduraEngine;

public abstract class AbstractEntity
{
    public abstract void Update(GameTime gameTime);

    public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}