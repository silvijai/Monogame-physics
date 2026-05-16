using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Physics_Game;

public class PolygonShape : IShape
{
    public readonly Vector2[] LocalVertices;

    public PolygonShape(Vector2[] vertices) { LocalVertices = vertices; }

    public Vector2[] WorldVertices(TransformComponent t)
    {
        float cos = MathF.Cos(t.Rotation), sin = MathF.Sin(t.Rotation);
        var w = new Vector2[LocalVertices.Length];

        for (int i = 0; i < LocalVertices.Length; i++)
        {
            var v = LocalVertices[i];
            var s = new Vector2(v.X * t.Scale.X, v.Y * t.Scale.Y);

            var r = new Vector2(
                s.X * cos - s.Y * sin,
                s.X * sin + s.Y * cos
            );

            w[i] = r + t.Position;
        }

        return w;
    }

    public void DebugDraw(SpriteBatch sb, Texture2D pixel, TransformComponent t, Color color)
    {
        var verts = WorldVertices(t);
        for (int i = 0; i < verts.Length; i++)
            DrawLine(sb, pixel, verts[i], verts[(i + 1) % verts.Length], color);
    }

    private static void DrawLine(SpriteBatch sb, Texture2D px, Vector2 a, Vector2 b, Color c)
    {
        var d = b - a;
        sb.Draw(px, a, null, c, MathF.Atan2(d.Y, d.X),
            Vector2.Zero, new Vector2(d.Length(), 1f), SpriteEffects.None, 0f);
    }
}
