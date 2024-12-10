using System;
using System.Collections.Generic;
using Rapa.RapaGame.RapaduraEngine.Entities;
using Rapa.RapaGame.RapaduraEngine.Rendering;
using Rapa.RapaGame.RapaduraEngine.SceneManagement.Packers;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement;

public abstract class Scene
{
    #region properties
    
    public EntityList Entities { get; private set; }
    
    public CollisionsTracker CollisionsTracker { get; }
    
    public RendererList Renderers { get; }
    
    public TagList Tags { get; }

    public List<Entity> this[Tag32 tag] => Tags[tag.Id];

    #endregion

    #region constructor

    protected Scene()
    {
        CollisionsTracker = new CollisionsTracker();
        Entities = new EntityList(this);
        Renderers = new RendererList(this);
        Tags = new TagList();
    }

    #endregion
    
    #region methodes
    
    public virtual void Begin()
    {
        foreach (var e in Entities)
        {
            e.Begin(this);
        }
    }

    public virtual void BeforeUpdate()
    {
        if (!isPaused)
            totalTime += CoreEngine.DeltaTime;
        
        rawTotalTime += CoreEngine.RawDeltaTime;
        
        Tags.UpdateLists();
        Entities.UpdateList();
        Renderers.UpdateList();
    }

    public virtual void Update()
    {
        if (isPaused)
            return;
        
        Entities.Update();
        Renderers.Update();
    }

    public virtual void LateUpdate()
    {
        if (OnEndOfFrame is null)
            return;
        
        OnEndOfFrame.Invoke();
        OnEndOfFrame = null;
    }
    
    public virtual void BeforeRender()
    {
        Renderers.BeforeRender();
    }
    
    public virtual void Render()
    {
        Renderers.Render();
    }

    public virtual void AfterRender()
    {
        Renderers.AfterRender();
    }

    public virtual void End()
    {
        foreach (var e in Entities)
        {
            e.End(this);
        }
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
    
    #region fields

    public event Action OnEndOfFrame;
    
    public bool isPaused;

    public float rawTotalTime;
    
    public float totalTime;

    #endregion
}