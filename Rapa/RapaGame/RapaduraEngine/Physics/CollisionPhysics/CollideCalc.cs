using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Physics.CollisionPhysics;

public static class CollideCalc
{
    public static bool CheckCollision(Entity a, Entity b)
        => a.Collider != null && b.Collider != null && a != b && b.collidable && a.Collider.Collide(b);

    public static bool CheckCollision(Entity a, IEnumerable<Entity> b)
    {
        foreach (var e in b)
        {
            if (CheckCollision(a, e))
                return true;
        }
        return false;
    }

    public static Entity GetEntityCollided(Entity ent, IEnumerable<Entity> entities)
    {
        foreach (var e in entities)
        {
            if (CheckCollision(ent, e))
                return e;
        }
        return null;
    }

    public static Entity GetEntityCollidedAt(Entity a, IEnumerable<Entity> b, Vector2 at)
    {
        var tempPos = a.Position;
        a.Position = at;
        var tempE = GetEntityCollided(a, b);
        a.Position = tempPos;
        return tempE;
    }
    
    public static bool CheckCollisionAt(Entity a, Entity b, Vector2 at)
    {
        var tempPos = a.Position;
        a.Position = at;
        var res = CheckCollision(a, b);
        a.Position = tempPos;
        return res;
    }
    
    public static bool CheckCollisionAt(Entity a, IEnumerable<Entity> b, Vector2 at)
    {
        var tempPos = a.Position;
        a.Position = at;
        var res = CheckCollision(a, b);
        a.Position = tempPos;
        return res;
    }
    
    public static bool RectPointCollision(Rectangle rect, Vector2 point)
        => point.X >= rect.X && point.Y >= rect.Y && point.X < rect.X + rect.Width && point.Y < rect.Y + rect.Height;
    
    public static bool RectPointCollision(float rectX, float rectY, float rectWidth, float rectHeight, Vector2 point)
        => point.X >= rectX && point.Y >= rectY && point.X < rectX + rectWidth && point.Y < rectY + rectHeight;
}