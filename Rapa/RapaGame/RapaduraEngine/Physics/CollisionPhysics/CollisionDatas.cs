using Microsoft.Xna.Framework;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;

namespace Rapa.RapaGame.RapaduraEngine.Physics.CollisionPhysics;

public struct CollisionDatas
{
    public Vector2 dir;

    public Vector2 moved;

    public Vector2 targetPos;

    public Solid hit;

    public Solid pusher;
}