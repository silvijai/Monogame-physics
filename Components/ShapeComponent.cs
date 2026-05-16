using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using nkast.Aether.Physics2D.Dynamics;
using nkast.Aether.Physics2D.Common;
using System;

namespace Physics_Game;

public class ShapeComponent : Component
{
    private readonly Vector2[] _localVerts;
    private readonly Texture2D _pixel;
    public Color Color { get; }

    public ShapeComponent(World world, Body body, Vector2[] localVerts,
                          Texture2D pixel, Color color,
                          float density = 1f, float friction = 0.3f, float restitution = 0.1f)
    {
        _localVerts = localVerts;
        _pixel = pixel;
        Color = color;

        var verts = new Vertices();
        foreach (var v in localVerts)
            verts.Add(v * SceneManager.PixelsToMeters);

        var fixture = body.CreatePolygon(verts, density);
        fixture.Friction = friction;
        fixture.Restitution = restitution;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        var t = Entity.GetComponent<TransformComponent>();
        if (t == null) return;

        // Transform each vertex by position + rotation, then draw edges
        var worldVerts = new Vector2[_localVerts.Length];
        float cos = MathF.Cos(t.Rotation);
        float sin = MathF.Sin(t.Rotation);

        for (int i = 0; i < _localVerts.Length; i++)
        {
            var v = _localVerts[i];
            worldVerts[i] = new Vector2(
                t.Position.X + v.X * cos - v.Y * sin,
                t.Position.Y + v.X * sin + v.Y * cos
            );
        }

        // Draw edges
        for (int i = 0; i < worldVerts.Length; i++)
        {
            var a = worldVerts[i];
            var b = worldVerts[(i + 1) % worldVerts.Length];
            DrawLine(spriteBatch, a, b, Color);
        }
    }

    private void DrawLine(SpriteBatch sb, Vector2 a, Vector2 b, Color color)
    {
        var diff = b - a;
        float length = diff.Length();
        float angle = MathF.Atan2(diff.Y, diff.X);
        sb.Draw(_pixel, a, null, color, angle, Vector2.Zero, new Vector2(length, 1f), SpriteEffects.None, 0f);
    }
}
