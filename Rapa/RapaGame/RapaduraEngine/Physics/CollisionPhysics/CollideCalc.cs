using Microsoft.Xna.Framework;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Physics.CollisionPhysics;

public static class CollideCalc
{
    public static bool CheckCollision(Entity a, Entity b)
        => a.Collider != null && b.Collider != null && a != b && b.collidable && a.Collider.Collide(b);

    public static bool RectPointCollision(Rectangle rect, Vector2 point)
        => point.X >= rect.X && point.Y >= rect.Y && point.X < rect.X + rect.Width && point.Y < rect.Y + rect.Height;
    
    public static bool RectPointCollision(float rectX, float rectY, float rectWidth, float rectHeight, Vector2 point)
        => point.X >= rectX && point.Y >= rectY && point.X < rectX + rectWidth && point.Y < rectY + rectHeight;
}