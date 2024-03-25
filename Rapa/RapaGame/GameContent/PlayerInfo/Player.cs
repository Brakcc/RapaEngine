using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;
using Rapa.RapaGame.RapaduraEngine.InputSettings;

namespace Rapa.RapaGame.GameContent.PlayerInfo;

public sealed class Player : Solid
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
    
    public Player(Texture2D texture) : base(texture)
    {
    }
    
    #endregion

    #region methodes
    
    public override void Update(GameTime gameTime)
    {
        directionalSpeedX.Y = speed;
            
        directionalSpeedX.X = speed;

        directionalSpeedY.X = speed;

        directionalSpeedY.Y = speed;
    
        MoveX(directionalSpeedX);
        MoveY(directionalSpeedY);
        
        //Console.WriteLine(velocity);
        //Animate();

        Position += velocity;
        
        base.Update(gameTime);
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

    /*protected override void Animate()
    {
        switch (velocity.X)
        {
            case 0 when velocity.Y == 0:
                Animator.Play(animation["Idle"]);
                break;
            case > 0:
                Animator.Play(animation["WalkRight"]);
                break;
            case < 0:
                Animator.Play(animation["WalkLeft"]);
                break;
        }

        base.Animate();
    }*/
    
    #endregion
}