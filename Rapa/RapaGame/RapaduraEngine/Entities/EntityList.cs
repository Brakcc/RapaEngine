using System.Collections.Generic;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame.RapaduraEngine.Entities;

public sealed class EntityList
{
    #region properties

    public Scene SceneRef { get; init; }

    #endregion
    
    #region constructors

    public EntityList(Scene scene)
    {
        SceneRef = scene;
        _entities = new List<Entity>();
    }

    #endregion
    
    #region methods

    public void Init()
    {
        
    }
    
    #endregion
    
    #region fields
    
    private List<Entity> _entities;
    
    
    
    #endregion
}