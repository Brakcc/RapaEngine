namespace Rapa.RapaGame.RapaduraEngine.SceneManagement;

public abstract class Scene
{
    #region methodes
    
    public virtual void Begin()
    {
        //Generation et appel des Entités et interComponents
    }
    
    public virtual void BeforeRender()
    {
        //render prio
    }
    public virtual void Render()
    {
        //rendergraph de tt les entités et leurs components
    }

    public virtual void AfterRender()
    {
        //render secondaire
    }

    public virtual void End()
    {
        //fin de scene et décharge de datas (save ?)
    }

    public virtual void LoseFocus()
    {
        //get lost idiot
    }

    public virtual void GainFocus()
    {
        //gg
    }

    public virtual void HandleGraphicsCreate()
    {
        //bah faut créer les graphs dans une scene :/
    }

    public virtual void HandleGraphicsReset()
    {
        //au cas ou faut reset, histoire de mettre tt le monde d'accord
    }
    
    #endregion
}