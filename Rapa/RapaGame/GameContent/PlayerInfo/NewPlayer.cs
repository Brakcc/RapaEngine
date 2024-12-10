using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Props;
using Rapa.RapaGame.RapaduraEngine.InputSettings;

namespace Rapa.RapaGame.GameContent.PlayerInfo;

public class NewPlayer : NormalProp
{
    private readonly Inputs _input;
    private Vector2 _velocity;

    public NewPlayer(Texture2D texture/*, Inputs input*/) : base(texture)
    {
        //_input = input;
    }

    public override void Update()
    {
        Move();
        _velocity = Vector2.Zero;
        position += _velocity;

        base.Update();
    }


    private void Move()
    {
        /*if (_input == null)
        {
            throw new Exception("Please assign a value to 'Input'");
        }
        if (Keyboard.GetState().IsKeyDown(_input.Left))
        {
            _velocity.X -= 3;
        }
        if (Keyboard.GetState().IsKeyDown(_input.Right))
        {
            _velocity.X += -3;
        }
        if (Keyboard.GetState().IsKeyDown(_input.Up))
        {
            _velocity.Y -= -3;
        }
        if (Keyboard.GetState().IsKeyDown(_input.Down))
        {
            _velocity.Y += 3;
        }*/
    }
}