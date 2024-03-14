using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Rapa.RapaGame.RapaduraEngine.Mathematics;

public static class Calculus
{
    #region methodes
    
    public static T[] Array<T>(params T[] array) => array;
    
    public static T RandomPick<T>(params T[] pool) => pool[Rand.Next(pool.Length)];
    
    public static T RandomPick<T>(List<T> pool) => pool[Rand.Next(pool.Count)];
    
    public static float Angle(Vector2 from, Vector2 to) => (float)Math.Atan2(to.Y - from.Y, to.X - from.X);

    public static void Max(params int[] values)
    {
        var max = values[0];
        foreach (var v in values)
        {
            if (v > max)
                max = v;
        }
    }
    
    public static void Max(params float[] values)
    {
        var max = values[0];
        foreach (var v in values)
        {
            if (v > max)
                max = v;
        }
    }
    
    public static void Min(params float[] values)
    {
        var max = values[0];
        foreach (var v in values)
        {
            if (v > max)
                max = v;
        }
    }
    
    public static void Min(params int[] values)
    {
        var min = values[0];
        foreach (var v in values)
        {
            if (v < min)
                min = v;
        }
    }

    public static float ToDeg(float radVal) => (float)(radVal / Math.PI * 180);
    
    public static float ToRad(float degVal) => (float)(degVal / 180 * Math.PI);
    
    #endregion

    #region fields

    private static readonly Random Rand = new();

    #endregion
}