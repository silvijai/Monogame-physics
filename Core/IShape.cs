using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Physics_Game;

public interface IShape
{
    IEnumerable<Vector2> GetAxes(TransformComponent transform);
    (float Min, float Max) Project(TransformComponent transform, Vector2 axis);
    void DebugDraw(SpriteBatch sb, Texture2D pixel, TransformComponent transform, Color color);
}
