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
    
    //yes

    #endregion
    
    #region constructor
    
    public NormalProp(Texture2D texture, float width = 0, float height = 0, bool debugMode = false) : base(width, height, debugMode)
    {
        Components = new ComponentList(this, new List<Component>
        {
            new BaseSprite(this, texture)
        });
        collidable = false;
    }

    #endregion

    #region methodes
    
    //lourd

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.P))
        {
            TopLeft = Vector2.Zero;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.O))
        {
            BottomRight = new Vector2(0, 180);
        }
        
        base.Update(gameTime);
    }

    #endregion
    
    #region fields
    
    //Zero :D
    
    #endregion
}