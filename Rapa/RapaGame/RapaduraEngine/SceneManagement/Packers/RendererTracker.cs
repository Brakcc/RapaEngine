using System.Collections.Generic;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement.Packers;

public class RendererTracker
{
    #region properties

    public List<Entity> Renderers { get; } = new();

    #endregion
}