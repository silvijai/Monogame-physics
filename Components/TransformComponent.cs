using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Physics_Game;

public class TransformComponent : Component
{
    public Vector2 Position;
    public float Rotation;

    public TransformComponent(Vector2 position, float rotation = 0f)
    {
        Position = position;
        Rotation = rotation;
    }

    // TODO add a DrawDebug() with the ability to display rotation visually

    public override void DebugPrint()
        => Console.WriteLine($"  Transform  pos:({Position.X:F1}, {Position.Y:F1})  rot:{MathHelper.ToDegrees(Rotation):F1}°");
}
