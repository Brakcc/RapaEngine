using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame.RapaduraEngine.Rendering;

public abstract class Renderer
{
    #region methodes
    
    public abstract void Update(Scene scene);

    public abstract void BeforeRender(Scene scene);
    
    public abstract void Render(Scene scene);
    
    public abstract void AfterRender(Scene scene);
    
    #endregion
    
    #region fields

    public bool IsVisible = true;

    #endregion
}