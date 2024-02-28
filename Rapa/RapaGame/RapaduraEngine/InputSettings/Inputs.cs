using Microsoft.Xna.Framework.Input;

namespace Rapa.RapaGame.RapaduraEngine.InputSettings;

public class Inputs
{
    public Keys Up { get; init; }
    public Keys Down { get; init; }
    public Keys Left { get; init; }
    public Keys Right { get; init; }
    public Keys Jump { get; set; }
    public Keys Special { get; set; }
}