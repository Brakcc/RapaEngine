using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Components;
using Rapa.RapaGame.RapaduraEngine.Components.Colliders.ColliderTypes;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;

namespace Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Actors;

public class Actor : Entity
{
    #region properties

    public Vector2 Velocity => velocity;

    #endregion
    
    #region constructor

    protected Actor(Texture2D texture, float width = 0, float height = 0, bool debugMode = false) : base(width, height, debugMode)
    {
        Components = new ComponentList(this, new List<Component>
        {
            new BaseSprite(this, texture)
        });
        Collider = new BoxCollider(this, 8, 8, X, Y);
        collidable = true;
    }

    protected Actor(Dictionary<string, Animation> animations, float width = 0, float height = 0, bool debugMode = false) : base(width, height, debugMode)
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
        base.Update(gameTime);
        if (IsGrounded())
            Console.WriteLine("grounded");
    }

    public bool IsGrounded(int checkLength = 1) => IsCollidingAt<Solid>(Position + Vector2.UnitY * checkLength);

    #endregion

    #region fields

    protected Vector2 velocity;

    #endregion
}