using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Mathematics;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement.MapManagement;

public static class TileMapGenerator
{
    public static IEnumerable<int[]> GetMapMatrix(string matrixFilePath)
    {
        var path = AppContext.BaseDirectory + matrixFilePath;
        var matLines = File.ReadAllLines(path);
        var mat = new int[matLines.Length][];
        for (var i = 0; i < matLines.Length; i++)
        {
            var tempMat = Calculus.UnpackDatas(matLines[i]);
            mat[i] = Calculus.DivideDatas(tempMat);
        }
        return mat;
    }

    public static Texture2D TextureLoader(string path, string key, int id)
        => CoreEngine.Instance.Content.Load<Texture2D>(path + key + id);
}