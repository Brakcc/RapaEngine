using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rapa.RapaGame.RapaduraEngine.Components.Sprites.Animations;
using Rapa.RapaGame.RapaduraEngine.Entities.PreBuilt.Actors;
using Rapa.RapaGame.RapaduraEngine.InputSettings;

namespace Rapa.RapaGame.GameContent.PlayerInfo;

public sealed class Player : Actor
{
    #region fields
    
    public bool hasDied = false;
    public Inputs input;
    public float jumpForce = 10f;
    public float speed;
    
    #endregion
    
    #region constructor
    
    public Player(Texture2D texture, float width = 0, float height = 0, bool debugMode = false) : base(texture, width, height, debugMode)
    {
    }
    
    public Player(Dictionary<string, Animation> animations, float width = 0, float height = 0, bool debugMode = false) : base(animations, width, height, debugMode)
    {
    }
    
    #endregion

    #region methodes

    public override void Init()
    {
        base.Init();
        
        Console.WriteLine(SceneRef);
    }

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(input.Up))
            MoveY(-speed);
        if (Keyboard.GetState().IsKeyDown(input.Down))
            MoveY(speed);
        if (Keyboard.GetState().IsKeyDown(input.Left))
            MoveX(-speed);
        if (Keyboard.GetState().IsKeyDown(input.Right))
            MoveX(speed);

        Position += velocity;
        base.Update(gameTime);
    }
    
    #endregion

    #region fields

    //private float yRemainder;

    #endregion
}