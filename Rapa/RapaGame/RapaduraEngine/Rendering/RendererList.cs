using System.Collections.Generic;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame.RapaduraEngine.Rendering;

public sealed class RendererList
{
    #region constructor

    public RendererList(Scene scene)
    {
        _scene = scene;
        _renderers = new List<Renderer>();
        _adding = new List<Renderer>();
        _removing = new List<Renderer>();
    }
    
    #endregion
    
    #region methodes

    public void UpdateList()
    {
        if (_adding.Count > 0)
        {
            foreach (var r in _adding)
            {
                _renderers.Add(r);
            }
        }
        _adding.Clear();

        if (_removing.Count > 0)
        {
            foreach (var r in _removing)
            {
                _renderers.Remove(r);
            }
        }
        _removing.Clear();
    }

    public void AddRenderer(Renderer renderer) => _adding.Add(renderer);
    
    public void RemoveRenderer(Renderer renderer) => _removing.Add(renderer);
    
    public void Update()
    {
        foreach (var r in _renderers)
        {
            r.Update();
        }
    }
    
    public void BeforeRender()
    {
        foreach (var r in _renderers)
        {
            if (!r.IsVisible)
                continue;
            
            r.BerforeRender();
        }
    }
    
    public void Render()
    {
        foreach (var r in _renderers)
        {
            if (!r.IsVisible)
                continue;
            
            r.Render();
        }
    }

    public void AfterRender()
    {
        foreach (var r in _renderers)
        {
            if (!r.IsVisible)
                continue;
            
            r.AfterRender();
        }
    }

    public void MoveToFront(Renderer renderer)
    {
        _renderers.Remove(renderer);
        _renderers.Add(renderer);
    }
    
    #endregion
    
    #region fields
    
    private Scene _scene;
    
    private readonly List<Renderer> _renderers;

    private readonly List<Renderer> _adding;
    
    private readonly List<Renderer> _removing;

    #endregion
}