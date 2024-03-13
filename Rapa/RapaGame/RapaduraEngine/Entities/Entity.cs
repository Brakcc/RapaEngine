using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.Components;

namespace Rapa.RapaGame.RapaduraEngine.Entities;

public abstract class Entity
{
    #region properties

    public Vector2 Position { get; set; }

    protected ComponentList Components { get; init; }
    
    #endregion

    #region constructors

    protected Entity()
    {
        //lien avec EntityPool à faire pour tracker l'entity
        Start();
    }

    #endregion
    
    #region methodes

    private void Start() => Init();
    protected virtual void Init()
    {
        Components?.InitList();
    }
    
    public virtual void Update(GameTime gameTime)
    {
        Components.Update(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Components.Draw(spriteBatch);
    }

    public virtual void End()
    {
        Components?.EndList();
    }
    
    public void AddComponent(Component comp)
    {
        Components.AddComponent(comp);
    }
    
    public void RemoveComponent(Component comp)
    {
        Components.RemoveComponent(comp);
    }
    
    #endregion
}