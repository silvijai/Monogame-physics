using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Physics_Game;

// https://github.com/MonoGame/MonoGame.Samples/blob/3.8.0/Platformer2D/Platformer2D.Core/Game/Enemy.cs

public class Player : Entity
{
    public int Health = 100;

    public Player(Vector2 position) : base(position)
    {
        // components are added in Game1, not here
    }
}
