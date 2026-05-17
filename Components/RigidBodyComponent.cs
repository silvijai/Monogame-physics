using Microsoft.Xna.Framework;
using Box2D.NET.Bindings;
using System;

namespace Physics_Game;

public class RigidBodyComponent : Component
{
    public B2.BodyId BodyId { get; private set; }
    public B2.WorldId WorldId { get; private set; }
    
    public unsafe RigidBodyComponent(B2.WorldId worldId, Vector2 pixelPosition, bool isStatic, float rotation = 0f)
    {
        WorldId = worldId;
        var bodyDef = B2.DefaultBodyDef();
        bodyDef.type = isStatic ? B2.staticBody : B2.dynamicBody;
        bodyDef.position = new B2.Vec2
        {
            x = pixelPosition.X * SceneManager.PixelsToMeters,
            y = pixelPosition.Y * SceneManager.PixelsToMeters
        };
        bodyDef.rotation = new B2.Rot
        {
            s = MathF.Sin(rotation),
            c = MathF.Cos(rotation)
        };
        BodyId = B2.CreateBody(worldId, &bodyDef);
        B2.BodyEnableContactEvents(BodyId, true);
    }

    public override unsafe void Update(double deltaTime)
    {
        int capacity = B2.BodyGetContactCapacity(BodyId);
        if (capacity == 0) { GroundContactCount = 0; return; }

        var contacts = stackalloc B2.ContactData[capacity];
        int count = B2.BodyGetContactData(BodyId, contacts, capacity);

        GroundContactCount = 0;
        for (int i = 0; i < count; i++)
            if (contacts[i].manifold.pointCount > 0)
                GroundContactCount++;
    }

    public void SetFixedRotation(bool value)
    {
        B2.BodySetFixedRotation(BodyId, value);
    }

    public B2.Vec2 LinearVelocity
    {
        get => B2.BodyGetLinearVelocity(BodyId);
        set => B2.BodySetLinearVelocity(BodyId, value);
    }

    public void SyncTransform()
    {
        var t = Entity.GetComponent<TransformComponent>();
        if (t == null) return;
        var pos = B2.BodyGetPosition(BodyId);
        var rot = B2.BodyGetRotation(BodyId);
        t.Position = new Vector2(pos.x * SceneManager.MetersToPixels, pos.y * SceneManager.MetersToPixels);
        t.Rotation = MathF.Atan2(rot.s, rot.c);
    }

    public int GroundContactCount { get; private set; } = 0;
    public bool IsGrounded => GroundContactCount > 0;

    public void OnGroundContactBegin() => GroundContactCount++;
    public void OnGroundContactEnd()   => GroundContactCount = Math.Max(0, GroundContactCount - 1);
}
