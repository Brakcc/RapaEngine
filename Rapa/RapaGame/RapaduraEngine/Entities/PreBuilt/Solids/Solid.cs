using System;
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

    public Solid(Texture2D texture, float width = 0, float height = 0, bool debugMode = false) : base(width, height, debugMode)
    {
        Components = new ComponentList(this, new List<Component>
        {
            new BaseSprite(this, texture)
        });
        Collider = new BoxCollider(this, 8, 8, X, Y);
        collidable = true;
    }
    
    public Solid(Dictionary<string, Animation> animations, float width = 0, float height = 0, bool debugMode = false) : base(width, height, debugMode)
    {
        Components = new ComponentList(this, new List<Component>
        {
            new AnimatedSprite(this, animations)
        });
        Collider = new BoxCollider(this, 8, 8, X, Y);
        collidable = true;
    }
    
    #endregion

    #region methodes

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            Components.GetComponent<BaseSprite>().SetVisible(false);

        if (!Keyboard.GetState().IsKeyDown(Keys.P))
            return;

        if (Components.TryGetComponent<AnimatedSprite>(out var test))
        {
            test.SetVisible(false);
            return;
        }
        
        Console.WriteLine("Nope");
        
        base.Update(gameTime);
    }
    
    #endregion
    
    #region fields

    public Vector2 velocity;

    #endregion
}