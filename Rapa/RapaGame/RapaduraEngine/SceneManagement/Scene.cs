﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.SceneManagement.Packers;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement;

public abstract class Scene
{
    #region properties

    public EntityPool EntityPool { get; }
    
    public CollisionsTracker CollisionsTracker { get; }
    
    public RendererTracker RendererTracker { get; }

    #endregion

    #region constructor

    protected Scene(EntityPool entityPool)
    {
        CollisionsTracker = new CollisionsTracker();
        RendererTracker = new RendererTracker();
        EntityPool = entityPool;
        EntityPool.SceneRef = this;
    }

    #endregion
    
    #region methodes
    
    public virtual void Begin()
    {
        //Generation et appel des Entités et interComponents
        
        EntityPool.InitList();
    }

    public virtual void BeforeUpdate()
    {
        EntityPool.UpdateEntList();
    }

    public virtual void Update(GameTime gameTime)
    {
        EntityPool.Update(gameTime);
    }
    
    public virtual void BeforeRender()
    {
        //render prio
    }
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        //rendergraph de tt les entités et leurs components
        
        EntityPool.Draw(spriteBatch);
    }

    public virtual void AfterRender()
    {
        //render secondaire
    }

    public virtual void End()
    {
        //fin de scene et décharge de datas (save ?)
        
        EntityPool.EndList();
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
    
    
    
    #endregion
}