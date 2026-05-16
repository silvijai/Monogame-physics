using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public interface IShape
{
    // Give me all the edge normals (axes to test in SAT)
    // IEnumerable<Vector2> GetAxes(Vector2 position, float rotation);

    // Project yourself onto an axis and return the shadow interval
    // (float Min, float Max) Project(Vector2 position, float rotation, Vector2 axis);

    void DebugDraw(SpriteBatch sb, Texture2D pixel, Vector2 position, float rotation, Color color);
}
