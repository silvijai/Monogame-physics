using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

public class PolygonShape : IShape
{
    public readonly Vector2[] LocalVertices;

    public PolygonShape(Vector2[] vertices) { LocalVertices = vertices; }

    public Vector2[] WorldVertices(Vector2 pos, float rot)
    {
        float cos = MathF.Cos(rot), sin = MathF.Sin(rot);
        var w = new Vector2[LocalVertices.Length];
        for (int i = 0; i < LocalVertices.Length; i++)
        {
            var v = LocalVertices[i];
            w[i] = new Vector2(
                v.X * cos - v.Y * sin + pos.X,
                v.X * sin + v.Y * cos + pos.Y
            );
        }
        return w;
    }

    public void DebugDraw(SpriteBatch sb, Texture2D pixel, Vector2 pos, float rot, Color color)
    {
        var verts = WorldVertices(pos, rot);
        for (int i = 0; i < verts.Length; i++)
            DrawLine(sb, pixel, verts[i], verts[(i+1) % verts.Length], color);
    }

    private static void DrawLine(SpriteBatch sb, Texture2D px, Vector2 a, Vector2 b, Color c)
    {
        var d = b - a;
        sb.Draw(px, a, null, c, MathF.Atan2(d.Y, d.X),
            Vector2.Zero, new Vector2(d.Length(), 1f), SpriteEffects.None, 0f);
    }
}
