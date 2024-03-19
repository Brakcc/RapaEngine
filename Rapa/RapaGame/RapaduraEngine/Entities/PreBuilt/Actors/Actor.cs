using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;

namespace Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Actors;

public class Actor : Solid
{
    #region constructor
    
    public Actor(Texture2D texture, float width = 0, float height = 0, bool debugMode = false) : base(texture, width, height, debugMode)
    {
    }

    public Actor(Dictionary<string, Animation> animations, float width = 0, float height = 0, bool debugMode = false) : base(animations, width, height, debugMode)
    {
    }
    
    #endregion
}