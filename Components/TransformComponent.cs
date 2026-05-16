using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Physics_Game;

public class TransformComponent : Component
{
    public Vector2 Position;
    public float Rotation;
    public Vector2 Scale; 

    public TransformComponent(Vector2 position, Vector2 scale, float rotation = 0f)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }

    // defualt constructor, without scale defined
    public TransformComponent(Vector2 position, float rotation = 0f)
    {
        Position = position;
        Rotation = rotation;
        Scale = new Vector2(1f, 1f);
    }

    // TODO add a DrawDebug() with the ability to display rotation visually

    public override void DebugPrint()
        => Console.WriteLine($"  Transform  pos:({Position.X:F1}, {Position.Y:F1})  rot:{MathHelper.ToDegrees(Rotation):F1}° sca:({Scale.X:F1}, {Scale.Y:F1})");
}
