using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Components.Colliders.ColliderTypes;

public class BoxTrigger : Collider
{
    public BoxTrigger(Entity entityRef, int width, int height) : base(entityRef, width, height)
    {
    }
}