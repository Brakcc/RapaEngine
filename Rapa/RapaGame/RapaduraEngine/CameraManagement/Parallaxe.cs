using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.GameContent.PlayerInfo;
using Rapa.RapaGame.RapaduraEngine.Entities;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Rapa.RapaGame.RapaduraEngine.CameraManagement;

public class Parallaxe : Entity
{
    #region fields
    
    private readonly bool _constantSpeed;
    private float _layer;
    private readonly float _scrollingSpeed;
    private readonly List<NormalSprite> _sprites;
    private readonly SolidSprite _player;
    private float _speed;
    public float layer
    {
        get => _layer;
        set
        {
            _layer = value;
            foreach (var sprite in _sprites)
            {
                sprite.Layer = _layer;
            }
        }
    }
        
    public Parallaxe(Texture2D texture, SolidSprite player, float scrollingSpeed, bool constantSpeed = false)
        : this(new List<Texture2D> { texture, texture }, player, scrollingSpeed, constantSpeed)
    {

    }

    private Parallaxe(IReadOnlyList<Texture2D> textures, SolidSprite player, float scrollingSpeed, bool constantSpeed = false)
    {
        _player = player;
        _sprites = new List<NormalSprite>();

        for (var i = 0; i < textures.Count; i++) 
        {
            var texture = textures[i];
            _sprites.Add(new NormalSprite(texture)
            {
                Position = new Vector2(-i * texture.Width - Math.Min(i, i + 1), Rapadura.CurrentScreenHeight - texture.Height)
            });
        }

        _scrollingSpeed = scrollingSpeed;
        _constantSpeed = constantSpeed;
    }

    public override void Update(GameTime gameTime)
    {
        ApplySpeed(gameTime);
        CheckPosition();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        foreach (var sprite in _sprites)
        {
            sprite.Draw(spriteBatch);
        }
    }

    private void ApplySpeed(GameTime gameTime)
    {
        _speed = (float)(_scrollingSpeed * gameTime.ElapsedGameTime.TotalSeconds);

        if (!_constantSpeed || _player.velocity.X > 0)
        {
            _speed *= _player.velocity.X;
        }

        foreach (var sprite in _sprites)
        {
            sprite.Position = sprite.Position with { X = _speed };
        }
    }

    private void CheckPosition()
    {
        for (var i = 0; i < _sprites.Count;i++)
        {
            var sprite = _sprites[i];
            if (sprite.Rect.Right > 0 || !(_speed > 0))
                continue;
            
            var index = i - 1;
            if (index < 0)
            {
                index = _sprites.Count - 1;
            }

            sprite.Position = sprite.Position with { X = _sprites[index].Rect.Right - _speed * 2 };
        }
    }
    
    #endregion
}