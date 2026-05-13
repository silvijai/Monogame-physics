using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Physics_Game;

public class Collider : Component
{
    public string Tag;
    public Action<Collider> OnCollisionEnter;

    // Bounds are always derived from the entity's live position + size
    public Rectangle Bounds => new Rectangle(
        (int)Entity.Position.X,
        (int)Entity.Position.Y,
        (int)(Entity.Size.X * Entity.Scale.X),
        (int)(Entity.Size.Y * Entity.Scale.Y)
    );

    public Collider(string tag = "")
    {
        Tag = tag;
    }

    public bool Intersects(Collider other) => Bounds.Intersects(other.Bounds);

    public Rectangle Overlap(Collider other) => Rectangle.Intersect(Bounds, other.Bounds);

    // Called by CollisionWorld — resolves solid collisions automatically
    public void Resolve(Collider other)
    {
        var overlap = Overlap(other);
        if (overlap.IsEmpty) return;

        // Push out on the smaller axis (minimum displacement)
        if (overlap.Width < overlap.Height)
        {
            Entity.Position.X += Bounds.X < other.Bounds.X ? -overlap.Width : overlap.Width;
        }
        else
        {
            Entity.Position.Y += Bounds.Y < other.Bounds.Y ? -overlap.Height : overlap.Height;
            if (Entity.Velocity.Y > 0) Entity.Velocity.Y = 0; // stop falling on landing
        }
    }

    internal void ReportCollision(Collider other, bool isColliding)
    {
        if (isColliding) OnCollisionEnter?.Invoke(other);
    }
}
