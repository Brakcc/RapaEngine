using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.InputSettings;

namespace Rapa.RapaGame.GameContent.Player;

public class Player : SolidSprite
{
    public bool hasDied = false;
    public Inputs input;
    public float jumpForce = 10f;
    public float speed;

    public Player(Dictionary<string, Animation> animations) : base(animations)
    {
    }

    public override void Update(GameTime gameTime, List<SolidSprite> sprites)
    {
        Move();
        if (sprites != null)
        {
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                {
                    continue;
                }
                if (velocity.X > 0 && IsTouchingLeft(sprite) || velocity.X < 0 && IsTouchingRight(sprite))
                {
                    velocity.X = 0;
                }
                if (velocity.Y > 0 && this.IsTouchingTop(sprite) || velocity.Y < 0 && IsTouchingBottom(sprite))
                {
                    velocity.Y = 0;
                }
            }
        }

        Animate();
        animationManager.Update(gameTime);

        position += velocity;
        //pos.X = Math.Clamp(position.X, 0, Game1.screenWidth - rectangle.Width);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    public void Move()
    {
        if (Keyboard.GetState().IsKeyDown(input.Left))
        {
            velocity.X = -speed;
        }
        if (Keyboard.GetState().IsKeyDown(input.Right))
        {
            velocity.X = +speed;
        }
        if (Keyboard.GetState().IsKeyDown(input.Up))
        {
            velocity.Y = -speed;
        }
        if (Keyboard.GetState().IsKeyDown(input.Down))
        {
            velocity.Y = +speed;
        }
        if (Keyboard.GetState().IsKeyUp(input.Left) && Keyboard.GetState().IsKeyUp(input.Right) && Keyboard.GetState().IsKeyUp(input.Up) && Keyboard.GetState().IsKeyUp(input.Down))
        {
            velocity = Vector2.Zero;
        }
    }

    public override void Animate()
    {
        if (velocity.X == 0 && velocity.Y == 0)
        {
            animationManager.Play(animation["Idle"]);
        }
        if (velocity.X > 0)
        {
            animationManager.Play(animation["WalkRight"]);
        }
        else if (velocity.X < 0)
        {
            animationManager.Play(animation["WalkLeft"]);
        }
        base.Animate();
    }
}