using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rapa.RapaGame.RapaduraEngine.Components;

public abstract class Component
{
    #region methodes
    
    public virtual void Update(GameTime gameTime) {}

    public virtual void Draw(SpriteBatch spriteBatch) {}
    
    #endregion
}