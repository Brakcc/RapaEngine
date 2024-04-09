using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Components;
using Rapa.RapaGame.RapaduraEngine.Components.Colliders.ColliderTypes;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;

namespace Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;

public class Solid : Entity
{
    #region properties
    
    //yeaaaa nooooooo

    #endregion
    
    #region constructor

    public Solid(Texture2D texture, float width = 8, float height = 8, bool debugMode = false) : base(width, height, debugMode)
    {
        Components = new ComponentList(this, new List<Component>
        {
            new BaseSprite(this, texture)
        });
        Collider = new BoxCollider(this, width, height, X, Y);
        collidable = true;
        _wasPressed = false;
    }
    
    public Solid(Dictionary<string, Animation> animations, float width = 8, float height = 8, bool debugMode = false) : base(width, height, debugMode)
    {
        Components = new ComponentList(this, new List<Component>
        {
            new AnimatedSprite(this, animations)
        });
        Collider = new BoxCollider(this, width, height, X, Y);
        collidable = true;
        _wasPressed = false;
    }
    
    #endregion

    #region methodes

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !_wasPressed)
        {
            collidable = !collidable;
            _wasPressed = true;
        }

        if (Keyboard.GetState().IsKeyUp(Keys.Enter))
            _wasPressed = false;
        
        base.Update(gameTime);
    }
    
    #endregion
    
    #region fields

    public Vector2 velocity;

    private bool _wasPressed;

    #endregion
}