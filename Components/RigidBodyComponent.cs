using Microsoft.Xna.Framework;
using nkast.Aether.Physics2D.Dynamics;

namespace Physics_Game;

public class RigidBodyComponent : Component
{
    public Body Body { get; private set; }

    public RigidBodyComponent(World world, Vector2 pixelPosition, bool isStatic)
    {
        Body = world.CreateBody(
            pixelPosition * SceneManager.PixelsToMeters,
            0f,
            isStatic ? BodyType.Static : BodyType.Dynamic
        );
    }

    public void SyncTransform()
    {
        var t = Entity.GetComponent<TransformComponent>();
        if (t == null) return;
        t.Position = Body.Position * SceneManager.MetersToPixels;
        t.Rotation = Body.Rotation;
    }
}
