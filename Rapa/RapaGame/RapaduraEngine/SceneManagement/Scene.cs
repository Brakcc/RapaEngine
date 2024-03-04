namespace Rapa.RapaGame.RapaduraEngine.SceneManagement;

public abstract class Scene
{
    #region methodes
    
    public void Begin()
    {
        //Generation et appel des Entités et interComponents
    }

    public void Render()
    {
        //rendergraph de tt les entités et leurs components
    }

    public void AfterRender()
    {
        //render secondaire
    }

    public void BeforeRender()
    {
        //render prio
    }

    public void End()
    {
        //fin de scene et décharge de datas (save ?)
    }

    public void LoseFocus()
    {
        //get lost idiot
    }

    public void GainFocus()
    {
        //gg
    }

    public void HandleGraphicsCreate()
    {
        //bah faut créer les graphs dans une scene :/
    }

    public void HandleGraphicsReset()
    {
        //au cas ou faut reset, histoire de mettre tt le monde d'accord
    }
    
    #endregion
}