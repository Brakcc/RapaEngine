namespace Rapa.RapaGame.RapaduraEngine.Rendering;

public abstract class Renderer
{
    #region methodes
    
    public abstract void Update();

    public abstract void BerforeRender();
    
    public abstract void Render();
    
    public abstract void AfterRender();
    
    #endregion
    
    #region fields

    public bool IsVisible = true;

    #endregion
}