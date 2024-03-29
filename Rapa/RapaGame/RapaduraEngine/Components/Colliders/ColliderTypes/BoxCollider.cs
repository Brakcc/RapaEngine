using Microsoft.Xna.Framework;
using Rapa.RapaGame.RapaduraEngine.Entities;
using Rapa.RapaGame.RapaduraEngine.Mathematics;
using Rapa.RapaGame.RapaduraEngine.Physics.CollisionPhysics;

namespace Rapa.RapaGame.RapaduraEngine.Components.Colliders.ColliderTypes;

public class BoxCollider : Collider
{
    #region properties
    
    public override float Width
    {
        get => _width;
        set => _width = value;
    }

    public override float Height
    {
        get => _height;
        set => _height = value;
    }

    public override float Top
    {
        get => position.Y;
        protected set => position.Y = value;
    }

    public override float Bottom
    {
        get => position.Y + Height;
        protected set => position.Y = value - Height;
    }

    public override float Right
    {
        get => position.X + Width;
        protected set => position.X = value - Width;
    }

    public override float Left
    {
        get => position.X;
        protected set => position.X = value;
    }

    #endregion
    
    #region constructor
    
    public BoxCollider(Entity entityRef, float width, float height, float xPos = 0, float yPos = 0) : base(entityRef, xPos, yPos)
    {
        _width = width;
        _height = height;
    }
    
    #endregion

    #region methodes

    public override void Draw(Color color, float layer = 0)
    {
        Drawer.DrawHollowRect(Boundaries, Color.Red, layer);
    }

    protected override bool Collide(Vector2 point)
        => CollideCalc.RectPointCollision(EntityDepX, EntityDepY, Width, Height, point);

    protected override bool Collide(Rectangle rect)
        => EntityDepRight > rect.Left &&
           EntityDepBottom > rect.Top &&
           EntityDepLeft < rect.Right &&
           EntityDepTop < rect.Bottom;

    protected override bool Collide(BoxCollider box)
        => EntityDepLeft < box.EntityDepRight &&
           EntityDepRight > box.EntityDepLeft &&
           EntityDepBottom > box.EntityDepTop &&
           EntityDepTop < box.EntityDepBottom;

    protected override bool Collide(BoxTrigger trigger)
        => EntityDepLeft < trigger.EntityDepRight &&
           EntityDepRight > trigger.EntityDepLeft &&
           EntityDepBottom > trigger.EntityDepTop &&
           EntityDepTop < trigger.EntityDepBottom;

    #endregion

    #region fields

    private float _width;

    private float _height;

    #endregion
}