using System.Collections.Generic;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement.Packers;

public class CollisionsTracker
{
    #region properties

    public List<Entity> Colliders { get; set; } = new();

    #endregion
}