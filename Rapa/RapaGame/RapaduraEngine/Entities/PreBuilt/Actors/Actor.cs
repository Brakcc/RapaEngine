using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Components;
using Rapa.RapaGame.RapaduraEngine.Components.Colliders.ColliderTypes;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;
using Rapa.RapaGame.RapaduraEngine.Mathematics;
using Rapa.RapaGame.RapaduraEngine.Physics.CollisionPhysics;
using Rapa.RapaGame.RapaduraEngine.SceneManagement.Packers;

namespace Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Actors;

[ColTracked]
public class Actor : Entity
{
    #region properties

    public Vector2 Velocity => velocity;

    public Vector2 MovedPos => Position + velocity;

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

        trapRes = OnTrap;
    }

    protected Actor(Dictionary<string, Animation> animations, float width = 0, float height = 0, bool debugMode = false) : base(width, height, debugMode)
    {
        Components = new ComponentList(this, new List<Component>
        {
            new AnimatedSprite(this, animations)
        });
        Collider = new BoxCollider(this, 8, 8, X, Y);
        collidable = true;

        trapRes = OnTrap;
    }
    
    #endregion

    #region methodes

    public override void Init()
    {
        keyboardState = new KeyboardState();
    }
    
    protected virtual void OnTrap(CollisionDatas datas)
    {
        if (!TryEscapeTrap(datas))
            RemoveSelf();
    }

    protected virtual bool TryEscapeTrap(CollisionDatas datas)
    {
        return true;
    }

    public override void Update(GameTime gameTime)
    {
        isGrounded = IsGrounded();
        
        base.Update(gameTime);
        if (isGrounded)
        {
            //Console.WriteLine("grounded");
        }

        keyboardState = Keyboard.GetState();
        if (keyboardState[Keys.A] == KeyState.Down)
            Console.WriteLine("line");
    }
    
    public virtual bool IsRiding(Solid solid) => IsCollidingAt(solid, Position + Vector2.UnitY);

    private bool IsGrounded(int checkLength = 1) => IsCollidingAt<Solid>(Position + Vector2.UnitY * checkLength);
    
    public bool IsGroundedAt(int checkLength, Vector2 at)
    {
        var tempPos = Position;
        Position = at;
        var res = IsGrounded(checkLength);
        Position = tempPos;
        return res;
    }
    
    private bool MoveXAbsolute(int moveAmount, Collision onCollide = null, Solid pusher = null)
    {
        var targetPos = Position + Vector2.UnitX * moveAmount;
        var dir = Math.Sign(moveAmount);
        var distTraveled = 0;

        while (moveAmount != 0)
        {
            var solid = IsCollidingFirstAt<Solid>(Position + Vector2.UnitX * dir);
            if (solid != null)
            {
                velocity.X = 0;
                onCollide?.Invoke(new CollisionDatas
                {
                    dir = Vector2.UnitX * dir,
                    moved = Vector2.UnitX * distTraveled,
                    targetPos = targetPos,
                    hit = solid,
                    pusher = pusher
                });
                return true;
            }

            distTraveled += dir;
            moveAmount -= dir;
            X += dir;
        }

        return false;
    }
    
    private bool MoveYAbsolute(int moveAmount, Collision onCollide = null, Solid pusher = null)
    {
        var targetPos = Position + Vector2.UnitY * moveAmount;
        var dir = Math.Sign(moveAmount);
        var distTraveled = 0;

        while (moveAmount != 0)
        {
            var solid = IsCollidingFirstAt<Solid>(Position + Vector2.UnitY * dir);
            if (solid != null)
            {
                velocity.Y = 0;
                onCollide?.Invoke(new CollisionDatas
                {
                    dir = Vector2.UnitY * dir,
                    moved = Vector2.UnitY * distTraveled,
                    targetPos = targetPos,
                    hit = solid,
                    pusher = pusher
                });
                return true;
            }

            distTraveled += dir;
            moveAmount -= dir;
            Y += dir;
        }

        return false;
    }
    
    protected bool MoveX(float moveAmount, Collision onCollide = null, Solid pusher = null)
    {
        velocity.X += moveAmount;
        var round = (int)Math.Round(velocity.X + moveAmount);
        if (round == 0)
            return false;
        
        velocity.X -= moveAmount;
        return MoveXAbsolute(round, onCollide, pusher);
    }
    
    protected bool MoveY(float moveAmount, Collision onCollide = null, Solid pusher = null)
    {
        velocity.Y += moveAmount;
        var round = (int)Math.Round(velocity.Y + moveAmount);
        if (round == 0)
            return false;
        
        velocity.Y -= moveAmount;
        return MoveYAbsolute(round, onCollide, pusher);
    }
    
    protected void MoveTowardsX(float target, float maxAmount, Collision onCollide)
    {
        var x = Calculus.CompareVals(X, target, maxAmount);
        MoveToX(x, onCollide);
    }
    
    protected void MoveTowardsY(float target, float maxAmount, Collision onCollide)
    {
        var y = Calculus.CompareVals(X, target, maxAmount);
        MoveToY(y, onCollide);
    }
    
    protected void MoveToX(float toX, Collision onCollide = null)
    {
        MoveX(toX - MovedPos.X, onCollide);
    }
    
    protected void MoveToY(float toY, Collision onCollide = null)
    {
        MoveX(toY - MovedPos.Y, onCollide);
    }

    #endregion

    #region fields

    public Collision trapRes;
    
    protected Vector2 velocity;

    protected bool isGrounded;

    private KeyboardState keyboardState;

    #endregion
}