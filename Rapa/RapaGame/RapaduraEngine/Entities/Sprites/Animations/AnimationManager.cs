using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rapa.RapaGame.RapaduraEngine.Entities.Sprites.Animations;

public class AnimationManager
{
    private Animation animation;

    private float timer;

    public Vector2 position { get; set; }

    public AnimationManager(Animation anim)
    {
        animation = anim;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(animation._texture,
            position,
            new Rectangle(animation._currentFrame * animation._frameWidth,
                0,
                animation._frameWidth,
                animation._frameHeight),
            Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, animation._layer);
    }

    public void Play(Animation anim) 
    {
        if (animation == anim)
        {
            return;
        }
        animation = anim;
        animation._currentFrame = 0;
        timer = 0;
    }
    public void Stop() 
    {
        timer = 0;
        animation._currentFrame = 0;
    }

    public void Update(GameTime gameTime) 
    {
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (timer > animation._frameSpeed) 
        {
            timer = 0;
            animation._currentFrame++;
                
            if (animation._currentFrame >= animation._frameCount) 
            { 
                animation._currentFrame = 0;
            }
        }
    }
}