namespace Physics_Game;

using Microsoft.Xna.Framework;

public class TransformComponent : Component
{
    public Vector2 Position { get; internal set; }
    public float Rotation   { get; internal set; }
    public Vector2 Scale    { get; set; } = Vector2.One;

    public TransformComponent(Vector2 position, float rotation = 0f)
    {
        Position = position;
        Rotation = rotation;
    }
}
