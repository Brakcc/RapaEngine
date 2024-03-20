using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Components;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites;

namespace Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Props;

public class NormalProp : Entity
{
    #region properties

    public float Layer { get; init; }

    #endregion
    
    #region constructor
    
    public NormalProp(Texture2D texture, float width = 0, float height = 0, bool debugMode = false) : base(width, height, debugMode)
    {
        Components = new ComponentList(this, new List<Component>
        {
            new BaseSprite(this, texture, Layer)
        });
        collidable = false;
    }

    #endregion

    #region methodes
    
    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            Components.GetComponent<BaseSprite>().SetVisible(false);

        if (!Keyboard.GetState().IsKeyDown(Keys.P))
            return;

        if (Components.TryGetComponent<AnimatedSprite>(out var test))
        {
            test.SetVisible(false);
            return;
        }
        
        Console.WriteLine("Nope");
        
        base.Update(gameTime);
    }

    #endregion
    
    #region fields
    
    //Zero :D
    
    #endregion
}