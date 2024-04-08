using System.Collections.Generic;
using Rapa.RapaGame.RapaduraEngine.Components;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;

namespace Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Props;

public sealed class AnimatedProp : Entity
{
    #region Accessors

    //no
    
    #endregion
    
    #region Constructor
    
    public AnimatedProp(Dictionary<string, Animation> animations, float width = 0, float height = 0, bool debugMode = false) : base(width, height, debugMode)
    {
        Components = new ComponentList(this, new List<Component>
        {
            new AnimatedSprite(this, animations)
        });
        //à retirer evidemment c'est pour le debug
        _canMove = true;
    }

    #endregion

    #region Methodes

    //lourd
    
    #endregion
    
    #region fields
              
    //toujours rien :D
              
    private bool _canMove;
                  
    #endregion
}