using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt;

public class Empty : Entity
{
    public override void Update()
    {
        base.Update();
        
        if (Keyboard.GetState().IsKeyDown(Keys.C))
            EasedTraveling(new Vector2(960, 180), 0.25f);

        if (Keyboard.GetState().IsKeyDown(Keys.V))
        {
            EasedTraveling(new Vector2(1600, 180), 0.25f);
            
        }
    }

    private void EasedTraveling(Vector2 focusPoint, float speed)
    {
        position += (focusPoint - position) * speed;
    }
}