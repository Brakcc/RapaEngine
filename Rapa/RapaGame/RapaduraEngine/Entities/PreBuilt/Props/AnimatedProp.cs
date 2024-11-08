﻿using System.Collections.Generic;
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
    
    public AnimatedProp(Dictionary<string, Animation> animations, int width = 0, int height = 0, bool debugMode = false) : base(width, height, debugMode)
    {
        Components = new ComponentList(this, new List<Component>
        {
            new AnimatedSprite(this, animations)
        });
    }

    #endregion

    #region Methodes

    //lourd

    #endregion
    
    #region fields
              
    //toujours rien :D
                  
    #endregion
}