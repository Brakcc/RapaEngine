using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.InputSettings;

namespace Rapa.RapaGame.GameContent.PlayerInfo;

public sealed class Player : SolidSprite
{
    #region fields
    
    public bool hasDied = false;
    public Inputs input;
    public float jumpForce = 10f;
    public float speed;
    private Vector2 directionalSpeedX;
    private Vector2 directionalSpeedY;
    
    #endregion
    
    #region constructor
    
    public Player(Dictionary<string, Animation> animations) : base(animations)
    {
    }
    
    #endregion

    #region methodes
    
    public void Update(GameTime gameTime, List<SolidSprite> sprites)
    {
        if (sprites != null)
        {
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                if (/*velocity.X > 0 && */IsTouchingLeft(sprite))
                    directionalSpeedX.Y = 0;
                else
                    directionalSpeedX.Y = speed;
                
                if (/*velocity.X < 0 && */IsTouchingRight(sprite))
                    directionalSpeedX.X = 0;
                else
                    directionalSpeedX.X = speed;

                if ( /*velocity.Y > 0 && */IsTouchingTop(sprite))
                {
                    Console.WriteLine(1);
                    directionalSpeedY.X = 0;
                }
                else
                    directionalSpeedY.X = speed;

                if ( /*velocity.Y < 0 && */IsTouchingBottom(sprite))
                {
                    Console.WriteLine(2);
                    directionalSpeedY.Y = 0;
                }
                else
                    directionalSpeedY.Y = speed;
            }
        }
        
        MoveX(directionalSpeedX);
        MoveY(directionalSpeedY);
        
        Console.WriteLine(velocity);
        Animate();
        animationManager.Update(gameTime);

        Position += velocity;
    }

    private void MoveX(Vector2 dirSpeed)
    {
        if (Keyboard.GetState().IsKeyDown(input.Left))
            velocity.X = -dirSpeed.Y;
        
        else if (Keyboard.GetState().IsKeyDown(input.Right))
            velocity.X = +dirSpeed.X;
        
        else if (Keyboard.GetState().IsKeyUp(input.Left) && Keyboard.GetState().IsKeyUp(input.Right))
            velocity.X = 0;
    }

    private void MoveY(Vector2 dirSpeed)
    {
        if (Keyboard.GetState().IsKeyDown(input.Up))
            velocity.Y = -dirSpeed.X;
                
        else if (Keyboard.GetState().IsKeyDown(input.Down))
            velocity.Y = +dirSpeed.Y;
        
        else if (Keyboard.GetState().IsKeyUp(input.Up) && Keyboard.GetState().IsKeyUp(input.Down))
            velocity.Y = 0;
    }

    protected override void Animate()
    {
        switch (velocity.X)
        {
            case 0 when velocity.Y == 0:
                animationManager.Play(animation["Idle"]);
                break;
            case > 0:
                animationManager.Play(animation["WalkRight"]);
                break;
            case < 0:
                animationManager.Play(animation["WalkLeft"]);
                break;
        }

        base.Animate();
    }
    
    #endregion
}