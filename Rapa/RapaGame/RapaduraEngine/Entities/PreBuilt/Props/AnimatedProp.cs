using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Components;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.Mathematics;

namespace Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Props;

public sealed class AnimatedProp : Entity
{
    #region Accessors

    public float Layer { get; init; }
    
    #endregion
    
    #region Constructor
    
    public AnimatedProp(Dictionary<string, Animation> animations, float width = 0, float height = 0, bool debugMode = false) : base(width, height, debugMode)
    {
        Components = new ComponentList(this, new List<Component>
        {
            new AnimatedSprite(this, animations, Layer)
        });
        //à retirer evidemment c'est pour le debug
        _canMove = true;
    }

    #endregion

    #region Methodes

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Space) && _canMove)
        {
            _canMove = false;
            Position += new Vector2(0, 1);
        }

        if (Keyboard.GetState().IsKeyUp(Keys.Space) && !_canMove)
        {
            _canMove = true;
        }
        
        base.Update(gameTime);
    }
    
    #endregion
    
    #region fields
              
    //toujours rien :D
              
    private bool _canMove;
                  
    #endregion
}