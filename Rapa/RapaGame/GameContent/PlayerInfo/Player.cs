using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Actors;
using Rapa.RapaGame.RapaduraEngine.InputSettings;

namespace Rapa.RapaGame.GameContent.PlayerInfo;

public sealed class Player : Actor
{
    #region fields
    
    public bool hasDied = false;
    public Inputs input;
    public float jumpForce = 10f;
    public float speed;
    
    #endregion
    
    #region constructor
    
    public Player(Texture2D texture, int width = 0, int height = 0, bool debugMode = false) : base(texture, width, height, debugMode)
    {
    }
    
    public Player(Dictionary<string, Animation> animations, int width = 0, int height = 0, bool debugMode = false) : base(animations, width, height, debugMode)
    {
    }
    
    #endregion

    #region methodes

    public override void Update(GameTime gameTime)
    {
        _actualSpeed = (Keyboard.GetState().IsKeyDown(input.Up) || Keyboard.GetState().IsKeyDown(input.Down))
                       && (Keyboard.GetState().IsKeyDown(input.Left) || Keyboard.GetState().IsKeyDown(input.Right))
            ? speed / (float)Math.Sqrt(2)
            : speed;
        
        /*if (Keyboard.GetState().IsKeyDown(input.Up))
            MoveY(-_actualSpeed);
        if (Keyboard.GetState().IsKeyDown(input.Down))
            MoveY(_actualSpeed);*/
        if (Keyboard.GetState().IsKeyDown(input.Left))
            MoveX(-_actualSpeed);
        if (Keyboard.GetState().IsKeyDown(input.Right))
            MoveX(_actualSpeed);
        
        Position += velocity;
        base.Update(gameTime);

        if (!isGrounded)
            MoveY(speed);

        if (_jumpCoolDownCounter < JumpCoolDow)
        {
            _jumpCoolDownCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        
        if (Keyboard.GetState().IsKeyDown(input.Jump) && isGrounded && _jumpCoolDownCounter >= JumpCoolDow)
        {
            MoveY(-speed * 25);
            _jumpCoolDownCounter = 0;
        }
    }
    
    #endregion

    #region fields

    private float _actualSpeed;
    
    //private float yRemainder;
    
    private float _jumpCoolDownCounter = 0;

    private const float JumpCoolDow = 1;

    #endregion
}