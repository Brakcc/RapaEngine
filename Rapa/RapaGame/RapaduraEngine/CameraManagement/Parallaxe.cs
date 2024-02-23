using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.GameContent.PlayerInfo;
using Rapa.RapaGame.RapaduraEngine.Entities.Sprites;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Rapa.RapaGame.RapaduraEngine.CameraManagement;

public class Parallaxe : AbstractEntity
{
    #region fields
    private bool _constantSpeed;
    private float _layer;
    private float _scrollingSpeed;
    private List<NormalSprite> _sprites;
    private readonly Player _player;
    private float _speed;
    public float layer
    {
        get { return _layer; }
        set
        {
            _layer = value;
            foreach (var sprite in _sprites)
            {
                sprite.layer = _layer;
            }
        }
    }
        
    public Parallaxe(Texture2D texture, Player player, float scrollingSpeed, bool constantSpeed = false)
        : this(new List<Texture2D> { texture, texture }, player, scrollingSpeed, constantSpeed)
    {

    }

    public Parallaxe(List<Texture2D> textures, Player player, float scrolingSpeed, bool constantSpeed = false)
    {
        _player = player;
        _sprites = new List<NormalSprite>();

        for (int i = 0; i < textures.Count; i++) 
        {
            var texture = textures[i];
            _sprites.Add(new NormalSprite(texture)
            {
                position = new Vector2(-i * texture.Width - Math.Min(i, i + 1), Game1.ScreenHeight - texture.Height),
            });
        }

        _scrollingSpeed = scrolingSpeed;
        _constantSpeed = constantSpeed;
    }

    public override void Update(GameTime gameTime)
    {
        ApplySpeed(gameTime);
        CheckPosition();
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (var sprite in _sprites)
        {
            sprite.Draw(gameTime, spriteBatch);
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
            sprite.position.X -= _speed;
        }
    }

    private void CheckPosition()
    {
        for (int i = 0; i < _sprites.Count;i++)
        {
            var sprite = _sprites[i];
            if (sprite.rectangle.Right <= 0 && _speed > 0)
            {
                var index = i - 1;
                if (index < 0)
                {
                    index = _sprites.Count - 1;
                }
                sprite.position.X = _sprites[index].rectangle.Right - (_speed * 2);
            }
        }
    }
    #endregion
}