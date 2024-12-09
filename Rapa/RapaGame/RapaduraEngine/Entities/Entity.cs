﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.GameContent;
using Rapa.RapaGame.RapaduraEngine.Components;
using Rapa.RapaGame.RapaduraEngine.Components.Colliders;
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

            if (SceneRef is not null)
            {
                const uint o = 1U;

                for (var i = 0; i < Tag32.TotalTags; i++)
                {
                    var flag = (value & (o << i)) != 0;

                    if ((_tag & (o << i)) == 0 && flag)
                    {
                        SceneRef.Tags[i].Add(this);
                        SceneRef.Tags.MarkUnsorted(i);
                    }
                    else if ((_tag & (o << i)) != 0 && !flag)
                    {
                        SceneRef.Tags[i].Remove(this);
                    }
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
        Tag = GameTags.Default;
        //lien avec EntityPool à faire pour tracker l'entity
    }

    ~Entity()
    {
        Console.WriteLine("entity disposed");
    }

    #endregion
    
    #region methodes
    
    public void Added(Scene scene) => SceneRef = scene;
    
    public void Removed()
    {
        SceneRef = null;
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

    public virtual void Begin(Scene scene)
    {
    }
    
    public virtual void End(Scene scene)
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

    public void AddTag(uint t) => Tag |= t;

    public void RemoveTag(uint t) => Tag &= ~t;
    
    public bool TagFullCheck(uint t) => (_tag & t) == _tag;
    
    public bool TagCheck(uint t) => (_tag & t) != 0;
    
    #region collisions

    private bool IsColliding(Entity e) => CollideCalc.CheckCollision(this, e);

    #region Coliding At
    
    protected bool IsCollidingAt(Entity e, Vector2 at) => CollideCalc.CheckCollisionAt(this, e, at);

    protected bool IsCollidingAt<T>(Vector2 at) where T : Entity
    {
        var others = SceneRef.CollisionsTracker.Colliders;
        return IsCollidingAt(at, others);
    }

    protected bool IsCollidingAt<T>(Vector2 at, Tag32 tag) where T : Entity
    {
        return IsCollidingAt(at, SceneRef[tag]);
    }

    protected bool IsCollidingAt<T>(Vector2 at, params Tag32[] tags) where T : Entity
    {
        foreach (var t in tags)
        {
            if (IsCollidingAt(at, SceneRef[t]))
                return true;
        }

        return false;
    }
    
    protected bool IsCollidingAt<T>(Vector2 at, uint tags) where T : Entity
    {
        const uint o = 1U;
        
        for (var i = 0; i < Tag32.TotalTags; i++)
        {
            if ((tags & (o << i)) == 0)
                continue;
            
            if (IsCollidingAt(at, SceneRef.Tags[i]))
                return true;
        }

        return false;
    }
    
    private bool IsCollidingAt(Vector2 at, IEnumerable<Entity> col) => CollideCalc.CheckCollisionAt(this, col, at);

    #endregion
    
    #region Colliding First At
    
    protected T IsCollidingFirst<T>(IEnumerable<Entity> entities) where T : Entity => CollideCalc.GetEntityCollided(this, entities) as T;

    private T IsCollidingFirstAt<T>(Vector2 at, IEnumerable<Entity> entities) where T : Entity => CollideCalc.GetEntityCollidedAt(this, entities, at) as T;

    protected T IsCollidingFirstAt<T>(Vector2 at) where T : Entity
    {
        var others = SceneRef.CollisionsTracker.Colliders;
        return IsCollidingFirstAt<T>(at, others);
    }
    
    protected T IsCollidingFirstAt<T>(Vector2 at, Tag32 tag) where T : Entity
    {
        return IsCollidingFirstAt<T>(at, SceneRef[tag]);
    }
    
    protected T IsCollidingFirstAt<T>(Vector2 at, params Tag32[] tags) where T : Entity
    {
        foreach (var t in tags)
        {
            var e = IsCollidingFirstAt<T>(at, SceneRef[t]);
            
            if (e is not null)
                return e;
        }

        return null;
    }

    protected T IsCollidingFirstAt<T>(Vector2 at, uint tags) where T : Entity
    {
        const uint o = 1U;

        for (var i = 0; i < Tag32.TotalTags; i++)
        {
            if ((tags & (o << i)) == 0)
                continue;
            
            var e = IsCollidingFirstAt<T>(at, SceneRef.Tags[i]);

            if (e is not null)
                return e;
        }

        return null;
    }

    #endregion

    public bool IsCollidingAll(List<Entity> entities)
    {
        foreach (var e in entities)
        {
            if (IsColliding(e))
                return true;
        }
        return false;
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