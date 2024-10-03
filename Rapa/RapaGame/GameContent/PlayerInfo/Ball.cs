using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Actors;

namespace Rapa.RapaGame.GameContent.PlayerInfo;

public class Ball : Actor
{
    #region constructors
    
    public Ball(Texture2D texture, int width = 0, int height = 0, bool debugMode = false) : base(texture, width, height, debugMode)
    {
    }

    public Ball(Dictionary<string, Animation> animations, int width = 0, int height = 0, bool debugMode = false) : base(animations, width, height, debugMode)
    {
    }
    
    #endregion
    
    #region methodes

    public override void Init()
    {
        base.Init();
        _initPos = Position;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        _elapsedTime += CoreEngine.DeltaTime;
        
        if (_initYSpeed < 0.01f)
            return;
        
        var newPos = new Vector2(InitXSpeed * _elapsedTime + _initPos.X , 0.5f * _g * _elapsedTime * _elapsedTime - _initYSpeed * _elapsedTime + _initPos.Y);
        
        Position = newPos;

        if (!isGrounded)
            return;
        
        _elapsedTime = 0;
        _initYSpeed *= 0.90f;
        _initPos = Position;
    }
    
    #endregion
    
    #region fields

    private float _g = 50f;
    
    private float _elapsedTime = 0f;
    
    private Vector2 _initPos;

    private const float InitXSpeed = 5;

    private float _initYSpeed = 65;

    #endregion
}