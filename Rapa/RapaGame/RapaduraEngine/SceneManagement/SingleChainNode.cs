#nullable enable
namespace Rapa.RapaGame.RapaduraEngine.SceneManagement;

public class SingleChainNode<T>
{
    #region properties
    
    public T Value { get; init; }
    
    public SingleChainedList<T>? Parent { get; set; }
    
    public SingleChainNode<T>? Next { get; set; }

    #endregion
    
    #region constructors
    
    public SingleChainNode(T value, SingleChainedList<T> parent)
    {
        Value = value;
        Parent = parent;
    }
    
    #endregion

    #region methodes

    public void OnEmpty()
    {
        Parent = null;
        Next = null;
    }

    #endregion
}