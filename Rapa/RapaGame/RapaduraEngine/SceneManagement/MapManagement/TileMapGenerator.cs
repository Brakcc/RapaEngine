using System;
using System.IO;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement.MapManagement;

public static class TileMapGenerator
{
    public static T[][] GetMapMatrix<T>(string matrixFilePath) where T : struct
    {
        var path = AppContext.BaseDirectory + "Content/NewTestMap.txt";
        Console.WriteLine(File.Exists(path));
        var test = CoreEngine.Instance.Content.RootDirectory + "NewTestMap.txt";
        Console.WriteLine(File.Exists(test));
        foreach (var l in File.ReadAllLines(path))
        {
            Console.WriteLine(l);
        }
        return null;
    }
}