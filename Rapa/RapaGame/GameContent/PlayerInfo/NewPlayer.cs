using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Props;
using Rapa.RapaGame.RapaduraEngine.InputSettings;

namespace Rapa.RapaGame.GameContent.PlayerInfo;

public class NewPlayer : NormalProp
{
    private readonly Inputs input;
    private Vector2 velocity;

    public NewPlayer(Texture2D texture, Inputs input) : base(texture)
    {
        this.input = input;
    }

    public override void Update(GameTime gameTime)
    {
        Move();
        velocity = Vector2.Zero;
        Position += velocity;

        base.Update(gameTime);
    }


    private void Move()
    {
        if (input == null)
        {
            throw new Exception("Please assign a value to 'Input'");
        }
        if (Keyboard.GetState().IsKeyDown(input.Left))
        {
            velocity.X -= 3;
        }
        if (Keyboard.GetState().IsKeyDown(input.Right))
        {
            velocity.X += -3;
        }
        if (Keyboard.GetState().IsKeyDown(input.Up))
        {
            velocity.Y -= -3;
        }
        if (Keyboard.GetState().IsKeyDown(input.Down))
        {
            velocity.Y += 3;
        }
    }
}