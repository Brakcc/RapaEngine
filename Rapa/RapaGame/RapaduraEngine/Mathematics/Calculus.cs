using System;
using Microsoft.Xna.Framework;

namespace Rapa.RapaGame.RapaduraEngine.Mathematics;

public static class Calculus
{
    #region methodes

    public static float Angle(Vector2 from, Vector2 to) => (float)Math.Atan2(to.Y - from.Y, to.X - from.X);

    public static T RandomPick<T>(params T[] pool) => pool[Rand.Next(pool.Length)];
        
    #endregion

    #region fields

    private static readonly Random Rand = new();

    #endregion
}