using Microsoft.Xna.Framework;

namespace Physics_Game;

public class GravityComponent : Component
{
    public Vector2 Force;

    public GravityComponent(Vector2 force)
    {
        Force = force;
    }

    // Default constructor, for if force vector isn't defined
    public GravityComponent(float strength = 400f)
    {
        Force = new Vector2(0, strength);
    }

    public override void Update(double deltaTime)
    {
        var velocity = Entity.GetComponent<VelocityComponent>();
        if (velocity == null) return;

        velocity.Linear += Force * (float)deltaTime;
    }
}
