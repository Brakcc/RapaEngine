using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Components.Colliders;

public abstract class Collider : Component
{
    protected Collider(Entity entityRef) : base(entityRef)
    {
    }
}