using System.Collections.Generic;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;

namespace Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;

public class MovableSolid : Solid
{
    public MovableSolid(Dictionary<string, Animation> animations) : base(animations)
    {
    }
}