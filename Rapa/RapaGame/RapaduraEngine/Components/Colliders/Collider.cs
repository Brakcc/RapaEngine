using System;
using Microsoft.Xna.Framework;
using Rapa.RapaGame.RapaduraEngine.Components.Colliders.ColliderTypes;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Components.Colliders;

public abstract class Collider
{
    #region properties

    public Entity EntityRef { get; private init; }
    
    public abstract int Width { get; set; }
    
    public abstract int Height { get; set; }
    
    public abstract float Top { get; protected set; }
    
    public abstract float Bottom { get; protected set; }
    
    public abstract float Right { get; protected set; }
    
    public abstract float Left { get; protected set; }

    public float CenterX
    {
        get => Left + (float)Width / 2;
        private set => Left = value - (float)Width / 2;
    }

    public float CenterY
    {
        get => Top + (float)Height / 2;
        private set => Top = value - (float)Height / 2;
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
        get => new(Center.X, Top);
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

    public Vector2 EntityDepPosition => EntityRef?.position ?? position;

    public float EntityDepX
    {
        get
        {
            if (EntityRef != null)
                return position.X + EntityRef.X;

            return position.X;
        }
    }
    
    public float EntityDepY
    {
        get
        {
            if (EntityRef != null)
                return position.Y + EntityRef.Y;

            return position.Y;
        }
    }
    
    public float EntityDepTop
    {
        get
        {
            if (EntityRef != null)
                return Top + EntityRef.Y;

            return Top;
        }
    }
    
    public float EntityDepBottom
    {
        get
        {
            if (EntityRef != null)
                return Bottom + EntityRef.Y;

            return Bottom;
        }
    }
    
    public float EntityDepLeft
    {
        get
        {
            if (EntityRef != null)
                return Left + EntityRef.X;

            return Left;
        }
    }
    
    public float EntityDepRight
    {
        get
        {
            if (EntityRef != null)
                return Right + EntityRef.X;

            return Right;
        }
    }

    public Rectangle Boundaries => new((int)EntityDepX, (int)EntityDepY, (int)Width, (int)Height);
    
    #endregion
    
    #region constructor
    
    protected Collider(Entity entityRef, float xPos = 0, float yPos = 0)
    {
        EntityRef = entityRef;
        position.X = xPos;
        position.Y = yPos;
    }
    
    #endregion

    #region methodes

    public virtual void Draw(Color color, float layer = 0) {}

    public bool Collide(Entity entity) => Collide(entity.Collider);

    private bool Collide(Collider collider) => collider switch
    { 
        BoxCollider boxCollider => Collide(boxCollider),
        BoxTrigger trigger => Collide(trigger),
        _ => throw new Exception("No methode is available to collide on this collider type")
    };

    protected abstract bool Collide(Vector2 point);

    protected abstract bool Collide(Rectangle rect);

    protected abstract bool Collide(BoxCollider box);

    protected abstract bool Collide(BoxTrigger trigger);
    
    //ajouter methodes pour les collision avec cercles
    
    //collisions polygones ?

    #endregion

    #region fields

    protected Vector2 position;

    #endregion
}