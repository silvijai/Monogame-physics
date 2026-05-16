using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Physics_Game;

public class VelocityComponent : Component
{
    public Vector2 Linear;
    public float Angular;

    public VelocityComponent(Vector2 linear = default, float angular = 0f)
    {
        Linear  = linear;
        Angular = angular;
    }

    // TODO add a DrawDebug() with the ability to display velocity visually

    public override void DebugPrint()
    {
        Console.WriteLine($"  Velocity   linear:({Linear.X:F1}, {Linear.Y:F1})  angular:{Angular:F2}");
    }
}
