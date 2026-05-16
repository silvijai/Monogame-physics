using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Physics_Game;

public class ShapeComponent : Component
{
    public IShape Shape;
    public Color DebugColor;
    private Texture2D _pixel;

    public ShapeComponent(IShape shape, Texture2D pixel, Color debugColor)
    {
        Shape      = shape;
        _pixel     = pixel;
        DebugColor = debugColor;
    }
 
    public override void DebugDraw(SpriteBatch spriteBatch)
    {
        var transform = Entity.GetComponent<TransformComponent>();
        if (transform == null) return;

        Shape.DebugDraw(spriteBatch, _pixel, transform, DebugColor);
    }

    public override void DebugPrint()
        => Console.WriteLine($"  Shape      type:{Shape.GetType().Name}  color:{DebugColor}");
}
