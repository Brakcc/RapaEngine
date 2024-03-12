using System.Collections.Generic;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;

namespace Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Solids;

public class Tile : Solid
{
    public Tile(Dictionary<string, Animation> animations) : base(animations)
    {
    }
}