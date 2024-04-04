using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Xna.Framework;

namespace Rapa.RapaGame.RapaduraEngine.Mathematics;

public static class Calculus
{
    #region methodes
    
    public static T[] Array<T>(params T[] array) => array;

    public static float CompareVals(float val, float target, float maxVal)
    {
        return val <= target ? Math.Min(val + maxVal, target) : Math.Max(val - maxVal, target);
    }
    
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

    public static int[] DivideDatas(string dataString, char sep = ',')
    {
        var dataList = dataString.Split(sep);
        var newList = new int[dataList.Length];
        for (var i = 0; i < dataList.Length; i++)
        {
            newList[i] = int.Parse(dataList[i], CultureInfo.InvariantCulture);
        }
        return newList;
    }

    public static string UnpackDatas(string dataString, char packTop = '[', char packBottom = ']')
    {
        if (dataString.StartsWith(packTop) && dataString.EndsWith(packBottom))
            dataString = dataString.Substring(1, dataString.Length - 2);

        return dataString;
    }
    
    #endregion

    #region fields

    private static readonly Random Rand = new();

    #endregion
}