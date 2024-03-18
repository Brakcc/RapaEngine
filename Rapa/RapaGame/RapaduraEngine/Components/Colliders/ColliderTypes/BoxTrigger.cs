using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Components.Colliders.ColliderTypes;

public class BoxTrigger : Collider
{
    #region properties

    public override float Width { get; set; }
    
    public override float Height { get; set; }
    
    public override float Top { get; protected set; }
    
    public override float Bottom { get; protected set; }
    
    public override float Right { get; protected set; }
    
    public override float Left { get; protected set; }

    #endregion
    
    #region constructor
    
    public BoxTrigger(Entity entityRef, int width, int height, float xPos = 0, float yPos = 0) : base(entityRef, xPos, yPos)
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