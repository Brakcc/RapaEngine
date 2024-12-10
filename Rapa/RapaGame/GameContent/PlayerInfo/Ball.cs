using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Actors;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame.GameContent.PlayerInfo;

public class Ball : Actor
{
    #region constructors

    public Ball(Texture2D texture, int width = 0, int height = 0, bool debugMode = false) : base(texture, width, height,
        debugMode)
    {
        Console.WriteLine(Tag32.TotalTags);
    }

    public Ball(Dictionary<string, Animation> animations, int width = 0, int height = 0, bool debugMode = false) : base(
        animations, width, height, debugMode)
    {
    }

    #endregion

    #region methodes

    public override void Init()
    {
        base.Init();
        _initPos = position;

        AddTag(GameTags.Test);
    }

    public override void Update()
    {
        base.Update();

        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            //Console.WriteLine(ConvToUint(Reverse(0b00010000000000000000000000000110)));
            //RemoveTag(GameTags.Test);
            //Console.WriteLine(Convert.ToString(Tag, 2).PadLeft(32, '0'));
        }

        _elapsedTime += CoreEngine.DeltaTime;

        if (_initYSpeed < 0.01f)
            return;

        var newPos = new Vector2(InitXSpeed * _elapsedTime + _initPos.X,
            0.5f * G * _elapsedTime * _elapsedTime - _initYSpeed * _elapsedTime + _initPos.Y);

        position = newPos;

        if (!isGrounded)
            return;

        _elapsedTime = 0;
        _initYSpeed *= 0.90f;
        _initPos = position;
    }
    
    private static uint Complementaire(uint i) => i ^ ~0U;
    
    private static uint DeleteLwb(uint i) => i & ~3U;
    
    private static uint FourFive(uint i) => i | 48U;
    
    private static byte CountOne(uint i)
    {
        byte c = 0;
        
        for (byte j = 0; j < 32; j++)
        {
            if ((i & (uint)Math.Pow(2, j)) == (uint)Math.Pow(2, j))
                c++;
        }

        return c;
    }

    private static bool IsTwoPow(uint i) => CountOne(i) == 1;
    
    private static bool ZeroOneZeroOne(uint i)
    {
        for (byte j = 0; j < 28; j++)
        {
            if ((i & 15U) == 5U)
                return true;

            i >>= 1;
        }

        return false;
    }

    private static bool OneOneOne(uint i)
    {
        var m = 7U;

        for (byte j = 0; j < 29; j++)
        {
            if ((i & m) == m)
                return true;

            m <<= 1;
        }

        return false;
    }

    private static byte DiffBits(uint i, uint j)
    {
        byte c = 0;
        
        for (byte k = 0; k < 32; k++)
        {
            if ((i & (uint)Math.Pow(2, k)) != (j & (uint)Math.Pow(2, k)))
                c++;
        }

        return c;
    }

    private static uint RoundPow(uint i)
    {
        var c = 65536U;
        
        for (sbyte k = 3; k >= -1; k--)
        {
            if (i > c)
                c <<= 1 << (k < 0 ? 0 : k);
            
            else if (i < c)
                c >>= 1 << (k < 0 ? 0 : k);
            
            if ((i & c) == c)
                return i == c ? c : c == 2147483648U ? c : c << 1;
        }

        return 0U;
    }

    private static uint OptiRound(uint i)
    {
        i--;
        i |= i >> 1;
        i |= i >> 2;
        i |= i >> 4;
        i |= i >> 8;
        i |= i >> 16;
        i++;
        return i;
    }

    private static uint Reverse(uint i)
    {
        uint j = 0;

        for (var k = 0; k < 32; k++)
        {
            j <<= 1;
            if ((i & 1) == 1)
            {
                j ^= 1;
            }
            i >>= 1;
        }

        return j;
    }

    private static string ConvToUint(uint i) => Convert.ToString(i, 2).PadLeft(32, '0');
    
    #endregion
    
    #region fields

    private const float G = 50f;

    private float _elapsedTime;
    
    private Vector2 _initPos;

    private const float InitXSpeed = 5;

    private float _initYSpeed = 65;

    #endregion
}