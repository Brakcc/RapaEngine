using Microsoft.Xna.Framework;
using Rapa.RapaGame.GameContent.PlayerInfo;
using Rapa.RapaGame.RapaduraEngine.Entities;

namespace Rapa.RapaGame.RapaduraEngine.CameraManagement;

public class Camera : Entity
{
    #region properties
    
    public Player PlayerRef { get; init; }
    
    public Matrix Transform { get; private set; }

    #endregion

    #region constructor

    public Camera(Player playerRef)
    {
        PlayerRef = playerRef;
    }

    #endregion
    
    #region methodes

    public override void Update(GameTime gameTime)
    {
        Follow(PlayerRef);
    }

    public void Follow (Player target)
    {
        var offset = Matrix.CreateTranslation(
            Rapadura.CurrentScreenWidth / 2f,
            Rapadura.CurrentScreenHeight / 2f,
            0);

        var pos = Matrix.CreateTranslation(
            -target.X + target.Width / 2,
            -target.Y + target.Height / 2,
            0);
        
        Transform = pos * offset;

        //à refaire d'urgence
        CoreEngine.ScreenMatrix = Transform;
    }
    
    #endregion
}