using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rapa.RapaGame.RapaduraEngine.Entities;

public abstract class Entity
{
    public abstract void Update(GameTime gameTime);

    public abstract void Draw(SpriteBatch spriteBatch);
}