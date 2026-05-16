namespace Physics_Game;

using Microsoft.Xna.Framework;

public class TransformComponent : Component
{
    // Read-only to everything except RigidBodyComponent.SyncTransform()
    // Do NOT set these manually — they are synced from Box2D every frame
    public Vector2 Position { get; internal set; }
    public float Rotation   { get; internal set; }
    public Vector2 Scale    { get; set; } = Vector2.One;  // Box2D has no scale, so this is yours to own

    // Convenience — if you want to set initial position before first sync
    public TransformComponent(Vector2 position, float rotation = 0f)
    {
        Position = position;
        Rotation = rotation;
    }
}
