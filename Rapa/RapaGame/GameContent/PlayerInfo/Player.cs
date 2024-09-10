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
    
    public Player(Texture2D texture, float width = 0, float height = 0, bool debugMode = false) : base(texture, width, height, debugMode)
    {
    }
    
    public Player(Dictionary<string, Animation> animations, float width = 0, float height = 0, bool debugMode = false) : base(animations, width, height, debugMode)
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
        
        if (Keyboard.GetState().IsKeyDown(input.Up))
            MoveY(-_actualSpeed);
        if (Keyboard.GetState().IsKeyDown(input.Down))
            MoveY(_actualSpeed);
        if (Keyboard.GetState().IsKeyDown(input.Left))
            MoveX(-_actualSpeed);
        if (Keyboard.GetState().IsKeyDown(input.Right))
            MoveX(_actualSpeed);
        
        Position += velocity;
        base.Update(gameTime);
    }
    
    #endregion

    #region fields

    private float _actualSpeed;

    //private float yRemainder;

    #endregion
}