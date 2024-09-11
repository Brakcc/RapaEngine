using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Components;
using Rapa.RapaGame.RapaduraEngine.Components.Colliders;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;
using Rapa.RapaGame.RapaduraEngine.Mathematics;
using Rapa.RapaGame.RapaduraEngine.Physics.CollisionPhysics;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame.RapaduraEngine.Entities;

public abstract class Entity
{
    #region properties

    public Scene SceneRef { get; set; }
    
    public ComponentList Components { get; init; }
    
    public float X
    {
        get => Position.X;
        set => Position.X = value;
    }
    
    public float Y
    {
        get => Position.Y;
        set => Position.Y = value;
    }
    
    public int Layer { get; init; }

    public Collider Collider
    {
        get => _collider;
        set => _collider = value;
    }

    public virtual int Width
    {
        get => Collider?.Width ?? _width;
        set => _width = value;
    }


    public virtual int Height
    {
        get => Collider?.Height ?? _height;
        set => _height = value;
    }

    public float Top
    {
        get
        {
            if (Collider is null)
                return Position.Y;

            return Position.Y + Collider.Top;
        }
        set
        {
            if (Collider is null)
            {
                Position.Y = value;
                return;
            }

            Position.Y = value - Collider.Top;
        }
    }

    public float Bottom
    {
        get
        {
            if (Collider is null)
                return Position.Y + Height;

            return Position.Y + Collider.Bottom;
        }
        set
        {
            if (Collider is null)
            {
                Position.Y = value - Height;
                return;
            }

            Position.Y = value - Collider.Bottom;
        }
    }
    
    public float Left
    {
        get
        {
            if (Collider is null)
                return Position.X;

            return Position.X + Collider.Left;
        }
        set
        {
            if (Collider is null)
            {
                Position.X = value;
                return;
            }

            Position.X = value - Collider.Left;
        }
    }
    
    public float Right
    {
        get
        {
            if (Collider is null)
                return Position.X + Width;

            return Position.X + Collider.Right;
        }
        set
        {
            if (Collider is null)
            {
                Position.X = value - Width;
                return;
            }

            Position.X = value - Collider.Right;
        }
    }

    public float CenterX
    {
        get
        {
            if (Collider is null)
                return Position.X + (float)Width / 2;

            return Position.X + Collider.CenterX;
        }
        set
        {
            if (Collider is null)
            {
                Position.X = value - (float)Width / 2;
                return;
            }

            Position.X = value - Collider.CenterX;
        }
    }
    
    public float CenterY
    {
        get
        {
            if (Collider is null)
                return Position.Y + (float)Height / 2;

            return Position.Y + Collider.CenterY;
        }
        set
        {
            if (Collider is null)
            {
                Position.Y = value - (float)Height / 2;
                return;
            }

            Position.Y = value - Collider.CenterY;
        }
    }

    public Vector2 TopLeft
    {
        get => new(Left, Top);
        set
        {
            Left = value.X;
            Top = value.Y;
        }
    }
    
    public Vector2 TopCenter
    {
        get => new(CenterX, Top);
        set
        {
            CenterX = value.X;
            Top = value.Y;
        }
    }
    
    public Vector2 TopRight
    {
        get => new(Right, Top);
        set
        {
            Right = value.X;
            Top = value.Y;
        }
    }
    
    public Vector2 CenterLeft
    {
        get => new(Left, CenterY);
        set
        {
            Left = value.X;
            CenterY = value.Y;
        }
    }
    
    public Vector2 Center
    {
        get => new(CenterX, CenterY);
        set
        {
            CenterX = value.X;
            CenterY = value.Y;
        }
    }
    
    public Vector2 CenterRight
    {
        get => new(Right, CenterY);
        set
        {
            Right = value.X;
            CenterY = value.Y;
        }
    }
    
    public Vector2 BottomLeft
    {
        get => new(Left, Bottom);
        set
        {
            Left = value.X;
            Bottom = value.Y;
        }
    }
    
    public Vector2 BottomCenter
    {
        get => new(CenterX, Bottom);
        set
        {
            CenterX = value.X;
            Bottom = value.Y;
        }
    }
    
    public Vector2 BottomRight
    {
        get => new(Right, Bottom);
        set
        {
            Right = value.X;
            Bottom = value.Y;
        }
    }
    
    #endregion

    #region constructors

    protected Entity(int width = 0, int height = 0, bool debugMode = false)
    {
        _width = width;
        _height = height;

        _debugMode = debugMode;
        
        //lien avec EntityPool à faire pour tracker l'entity
    }

    ~Entity()
    {
        Console.WriteLine("entity disposed");
    }

    #endregion
    
    #region methodes

    public void RemoveSelf()
    {
        SceneRef?.EntityPool.RemoveEntity(this);
    }
    
    public virtual void Init()
    {
        Components?.InitList();
    }
    
    public virtual void Update(GameTime gameTime)
    {
        Components?.Update(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Components?.Draw(spriteBatch);
        
        if (!_debugMode)
            return;

        if (Collider != null)
        {
            Collider.Draw(Color.Firebrick);
            return;
        }

        Drawer.DrawHollowRect(X, Y, Width, Height, Color.LawnGreen);
    }

    public virtual void End()
    {
        Components?.EndList();
    }
    
    public void AddComponent(Component comp)
    {
        Components.AddComponent(comp);
    }
    
    public void RemoveComponent(Component comp)
    {
        Components.RemoveComponent(comp);
    }

    #region collisions

    private bool IsColliding(Entity e) => CollideCalc.CheckCollision(this, e);

    protected bool IsCollidingAt(Entity e, Vector2 at) => CollideCalc.CheckCollisionAt(this, e, at);

    protected bool IsCollidingAt<T>(Vector2 at) where T : Entity
    {
        var others = SceneRef.CollisionsTracker.Colliders;
        return IsCollidingAt(others, at);
    }

    private bool IsCollidingAt(IEnumerable<Entity> col, Vector2 at) => CollideCalc.CheckCollisionAt(this, col, at);

    public bool IsCollidingAll(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            if (IsColliding(e))
                return true;
        }
        return false;
    }

    protected T IsCollidingFirst<T>(IEnumerable<Entity> entities) where T : Entity => CollideCalc.GetEntityCollided(this, entities) as T;

    private T IsCollidingFirstAt<T>(IEnumerable<Entity> entities, Vector2 at) where T : Entity => CollideCalc.GetEntityCollidedAt(this, entities, at) as T;

    protected Solid IsCollidingFirstAt<T>(Vector2 at) where T : Entity
    {
        var others = SceneRef.CollisionsTracker.Colliders;
        return IsCollidingFirstAt<Solid>(others, at);
    }

    public bool CollideAllAction<T>(List<Entity> entities, Action<T> collision) where T : Entity
    {
        var res = false;
        foreach (var e in entities)
        {
            if (!IsColliding(e))
                continue;
            
            res = true;
            collision(e as T);
        }
        
        return res;
    }
    

    #endregion
    
    #endregion
    
    #region fields

    public Vector2 Position;
    
    private Collider _collider;

    private int _width;

    private int _height;

    public bool collidable;

    private readonly bool _debugMode;

    #endregion
}