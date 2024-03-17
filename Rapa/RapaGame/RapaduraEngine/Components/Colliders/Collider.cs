using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Components.Colliders;

public abstract class Collider
{
    #region properties

    public Entity EntityRef { get; private init; }
    
    public int Width { get; private init; }
    
    public int Height { get; private init; }

    #endregion
    
    #region constructor
    
    protected Collider(Entity entityRef, int width, int height)
    {
        EntityRef = entityRef;
        Width = width;
        Height = height;
    }
    
    #endregion

    #region methodes

    public void Collide()
    {
        
    }

    #endregion
}