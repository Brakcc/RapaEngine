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
    #region accessors

    public float Layer { get; init; }

    #endregion
    
    #region constructor
    
    public NormalProp(Texture2D texture)
    {
        Components = new ComponentList(this, new List<Component>
        {
            new BaseSprite(this, texture, Layer)
        });
    }
    
    #endregion

    #region methodes
    
    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.D))
            Components.GetComponent<BaseSprite>().SetVisible(false);

        if (!Keyboard.GetState().IsKeyDown(Keys.P))
            return;

        if (Components.TryGetComponent<AnimatedSprite>(out var test))
        {
            test.SetVisible(false);
            return;
        }
        
        Console.WriteLine("Nope");
    }
    
    #endregion
    
    #region fields
    
    //Zero :D
    
    #endregion
}