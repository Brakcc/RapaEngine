using Rapa.RapaGame.RapaduraEngine.Entities;

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
    
    public BoxCollider(Entity entityRef, int width, int height, float xPos = 0, float yPos = 0) : base(entityRef, xPos, yPos)
    {
        _width = width;
        _height = height;
    }
    
    #endregion

    #region methodes

    public override void Collide()
    {
    }

    #endregion

    #region fields

    private float _width;

    private float _height;

    #endregion
}