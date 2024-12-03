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

    public uint Tag
    {
        get => _tag;
        set
        {
            if (_tag == value)
                return;
            
            if (SceneRef is null)
                return;

            for (var i = 0; i < Tag32.TotalTags; i++)
            {
                var o = 1U << i;
                var flag = (value & o) != 0;

                if ((_tag & o) != 0 != flag)
                {
                    SceneRef.Tags[i].Add(this);
                }
                else
                {
                    SceneRef.Tags[i].Remove(this);
                }
            }
            
            _tag = value;
        }
    }
    
    public float X
    {
        get => position.X;
        set => position.X = value;
    }
    
    public float Y
    {
        get => position.Y;
        set => position.Y = value;
    }
    
    public int Layer { get; init; }

    public bool Collidable
    {
        get => Collider is not null && _collidable;
        set => _collidable = value;
    }
    
    public Collider Collider
    {
        get => _collider;
        set => _collider = value;
    }

    public virtual int Width
    {
        get => Collider?.Width ?? _width;
        set
        {
            if (Collider is null)
                _width = value;

            else
                Collider.Width = value;
        }
    }


    public virtual int Height
    {
        get => Collider?.Height ?? _height;
        set
        {
            if (Collider is null)
                _height = value;

            else
                Collider.Height = value;
        }
    }

    public float Top
    {
        get
        {
            if (Collider is null)
                return position.Y;

            return position.Y + Collider.Top;
        }
        set
        {
            if (Collider is null)
            {
                position.Y = value;
                return;
            }

            position.Y = value - Collider.Top;
        }
    }

    public float Bottom
    {
        get
        {
            if (Collider is null)
                return position.Y + Height;

            return position.Y + Collider.Bottom;
        }
        set
        {
            if (Collider is null)
            {
                position.Y = value - Height;
                return;
            }

            position.Y = value - Collider.Bottom;
        }
    }
    
    public float Left
    {
        get
        {
            if (Collider is null)
                return position.X;

            return position.X + Collider.Left;
        }
        set
        {
            if (Collider is null)
            {
                position.X = value;
                return;
            }

            position.X = value - Collider.Left;
        }
    }
    
    public float Right
    {
        get
        {
            if (Collider is null)
                return position.X + Width;

            return position.X + Collider.Right;
        }
        set
        {
            if (Collider is null)
            {
                position.X = value - Width;
                return;
            }

            position.X = value - Collider.Right;
        }
    }

    public float CenterX
    {
        get
        {
            if (Collider is null)
                return position.X + (float)Width / 2;

            return position.X + Collider.CenterX;
        }
        set
        {
            if (Collider is null)
            {
                position.X = value - (float)Width / 2;
                return;
            }

            position.X = value - Collider.CenterX;
        }
    }
    
    public float CenterY
    {
        get
        {
            if (Collider is null)
                return position.Y + (float)Height / 2;

            return position.Y + Collider.CenterY;
        }
        set
        {
            if (Collider is null)
            {
                position.Y = value - (float)Height / 2;
                return;
            }

            position.Y = value - Collider.CenterY;
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
    
    public virtual void Update()
    {
        Components?.Update();
    }

    public virtual void Render(SpriteBatch spriteBatch)
    {
        Components?.Render(spriteBatch);
        
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

    public void AddTag(uint t)
    {
    }

    public void RemoveTag(uint t)
    {
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
    
    public Vector2 position;

    private uint _tag;
    
    private Collider _collider;

    private int _width;

    private int _height;

    private bool _collidable;

    private readonly bool _debugMode;

    #endregion
}