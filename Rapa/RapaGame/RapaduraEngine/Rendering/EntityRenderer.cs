using Microsoft.Xna.Framework.Graphics;
using Rapa.RapaGame.RapaduraEngine.CameraManagement;
using Rapa.RapaGame.RapaduraEngine.SceneManagement;

namespace Rapa.RapaGame.RapaduraEngine.Rendering;

public sealed class EntityRenderer : Renderer
{
    #region constructors

    public EntityRenderer()
    {
        _blendState = BlendState.AlphaBlend;
        _samplerState = SamplerState.PointClamp;
        _effect = null;
        _camera = new Camera();
    }
    
    #endregion
    
    #region methodes
    
    public override void Update(Scene scene)
    {
    }

    public override void BeforeRender(Scene scene)
    {
    }

    public override void Render(Scene scene)
    {
        CoreEngine.SpriteBatch.Begin(SpriteSortMode.Deferred, _blendState, _samplerState, DepthStencilState.None, RasterizerState.CullNone, _effect);
        scene.Entities.Render(CoreEngine.SpriteBatch);
        CoreEngine.SpriteBatch.End();
    }

    public override void AfterRender(Scene scene)
    {
    }
    
    #endregion
    
    #region fields
    
    private BlendState _blendState;
    
    private SamplerState _samplerState;
    
    private Effect _effect;
    
    private Camera _camera;
    
    #endregion
}