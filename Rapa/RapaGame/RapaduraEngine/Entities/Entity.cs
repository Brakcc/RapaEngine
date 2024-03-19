using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Components;
using Rapa.RapaGame.RapaduraEngine.Components.Colliders;
using Rapa.RapaGame.RapaduraEngine.Physics.CollisionPhysics;

namespace Rapa.RapaGame.RapaduraEngine.Entities;

public abstract class Entity
{
    #region properties

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

    public Collider Collider
    {
        get => _collider;
        set => _collider = value;
    }

    public float Width => Collider?.Width ?? _width;

    public float Height => Collider?.Height ?? _height;

    public float Top
    {
        get
        {
            if (Collider == null)
                return Position.Y;

            return Position.Y + Collider.Top;
        }
        set
        {
            if (Collider == null)
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
            if (Collider == null)
                return Position.Y;

            return Position.Y + Collider.Bottom;
        }
        set
        {
            if (Collider == null)
            {
                Position.Y = value;
                return;
            }

            Position.Y = value - Collider.Bottom;
        }
    }
    
    public float Left
    {
        get
        {
            if (Collider == null)
                return Position.X;

            return Position.X + Collider.Left;
        }
        set
        {
            if (Collider == null)
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
            if (Collider == null)
                return Position.X;

            return Position.X + Collider.Right;
        }
        set
        {
            if (Collider == null)
            {
                Position.X = value;
                return;
            }

            Position.X = value - Collider.Right;
        }
    }

    public float CenterX
    {
        get
        {
            if (Collider == null)
                return Position.X;

            return Position.X + Collider.CenterX;
        }
        set
        {
            if (Collider == null)
            {
                Position.X = value;
                return;
            }

            Position.X = value - Collider.CenterX;
        }
    }
    
    public float CenterY
    {
        get
        {
            if (Collider == null)
                return Position.Y;

            return Position.Y + Collider.CenterY;
        }
        set
        {
            if (Collider == null)
            {
                Position.Y = value;
                return;
            }

            Position.Y = value - Collider.CenterY;
        }
    }

    public Vector2 TopLeft
    {
        get => new Vector2(Left, Top);
        set
        {
            Left = value.X;
            Top = value.Y;
        }
    }
    
    public Vector2 TopCenter
    {
        get => new Vector2(CenterX, Top);
        set
        {
            CenterX = value.X;
            Top = value.Y;
        }
    }
    
    public Vector2 TopRight
    {
        get => new Vector2(Right, Top);
        set
        {
            Right = value.X;
            Top = value.Y;
        }
    }
    
    public Vector2 CenterLeft
    {
        get => new Vector2(Left, CenterY);
        set
        {
            Left = value.X;
            CenterY = value.Y;
        }
    }
    
    public Vector2 Center
    {
        get => new Vector2(CenterX, CenterY);
        set
        {
            CenterX = value.X;
            CenterY = value.Y;
        }
    }
    
    public Vector2 CenterRight
    {
        get => new Vector2(Right, CenterY);
        set
        {
            Right = value.X;
            CenterY = value.Y;
        }
    }
    
    public Vector2 BottomLeft
    {
        get => new Vector2(Left, Bottom);
        set
        {
            Left = value.X;
            Bottom = value.Y;
        }
    }
    
    public Vector2 BottomCenter
    {
        get => new Vector2(CenterX, Bottom);
        set
        {
            CenterX = value.X;
            Bottom = value.Y;
        }
    }
    
    public Vector2 BottomRight
    {
        get => new Vector2(Right, Bottom);
        set
        {
            Right = value.X;
            Bottom = value.Y;
        }
    }
    
    #endregion

    #region constructors

    protected Entity()
    {
        //lien avec EntityPool à faire pour tracker l'entity
        Start();
    }

    protected Entity(float width, float height)
    {
        _width = width;
        _height = height;
    }

    #endregion
    
    #region methodes

    private void Start() => Init();
    protected virtual void Init()
    {
        Components?.InitList();
    }
    
    public virtual void Update(GameTime gameTime)
    {
        Components.Update(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Components.Draw(spriteBatch);
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

    public bool IsColliding(Entity e) => CollideCalc.CheckCollision(this, e);

    #endregion
    
    #endregion
    
    #region fields

    public Vector2 Position;

    private readonly float _width;

    private readonly float _height;
    
    private Collider _collider;

    public bool collidable;

    #endregion
}