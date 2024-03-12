using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.Components.Sprites;

public sealed class AnimatedSprite : Component
{
    #region constructor
    
    public AnimatedSprite(Entity entityRef, Dictionary<string, Animation> anims, float layer = 0f) : base(entityRef)
    {
        _animations = anims;
        _layer = layer;
        _animator = new Animator(_animations.First().Value, EntityRef);
    }
    
    #endregion

    #region methodes

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _animator?.Draw(spriteBatch);
    }

    public void SwitchAnimation(string animName)
    {
        _animator.Play(_animations[animName]);
    }
    
    #endregion

    #region fields

    private readonly Animator _animator;
    
    private readonly Dictionary<string, Animation> _animations;
    
    private readonly float _layer;

    #endregion
}