using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt;

public class Empty : Entity
{
    //du lourd mamen

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        if (Keyboard.GetState().IsKeyDown(Keys.C))
            EasedTraveling(new Vector2(960, 180), 0.25f);

        if (Keyboard.GetState().IsKeyDown(Keys.V))
        {
            EasedTraveling(new Vector2(1600, 180), 0.25f);
            
        }
    }

    private void EasedTraveling(Vector2 focusPoint, float speed)
    {
        Position += (focusPoint - Position) * speed;
    }
}