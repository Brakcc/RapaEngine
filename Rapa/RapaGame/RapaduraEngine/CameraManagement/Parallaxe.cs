using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Entities;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Actors;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Props;

namespace Rapa.RapaGame.RapaduraEngine.CameraManagement;

public class Parallaxe : Entity
{
    #region fields
    
    private readonly bool _constantSpeed;
    private float _layer;
    private readonly float _scrollingSpeed;
    private readonly List<NormalProp> _sprites;
    private readonly Actor _player;
    private float _speed;
    public float layer
    {
        get => _layer;
        set
        {
            _layer = value;
            foreach (var sprite in _sprites)
            {
                //sprite.Layer = _layer;
            }
        }
    }
        
    public Parallaxe(Texture2D texture, Actor player, float scrollingSpeed, bool constantSpeed = false)
    {
        _sprites = new List<NormalProp>
        {
            new(texture)
        };
        _player = player;
        _scrollingSpeed = scrollingSpeed;
        _constantSpeed = constantSpeed;
    }

    private Parallaxe(IReadOnlyList<Texture2D> textures, Actor player, float scrollingSpeed, bool constantSpeed = false)
    {
        _player = player;
        _sprites = new List<NormalProp>();

        for (var i = 0; i < textures.Count; i++) 
        {
            var texture = textures[i];
            /*_sprites.Add(new NormalProp(texture)
            {
                Position = new Vector2(-i * texture.Width - Math.Min(i, i + 1), Rapadura.CurrentScreenHeight - texture.Height)
            });*/
        }

        _scrollingSpeed = scrollingSpeed;
        _constantSpeed = constantSpeed;
    }

    public override void Update()
    {
        ApplySpeed();
        //CheckPosition();
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        foreach (var sprite in _sprites)
        {
            sprite.Render(spriteBatch);
        }
    }

    private void ApplySpeed()
    {
        _speed = _scrollingSpeed * CoreEngine.DeltaTime;

        if (!_constantSpeed || _player.Velocity.X > 0)
        {
            _speed *= _player.Velocity.X;
        }

        foreach (var sprite in _sprites)
        {
            sprite.Position = sprite.Position with { X = _speed };
        }
    }

    /*private void CheckPosition()
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
    }*/
    
    #endregion
}