using Microsoft.Xna.Framework;

namespace Physics_Game;

public class GravityComponent : Component
{
    public float Force = 800f;
    public float GroundY = 650f;
    public bool IsGrounded = false;

    public override void Update(double deltaTime)
    {
        float dt = (float)deltaTime;
        Entity.Velocity.Y += Force * dt;
        Entity.Position += Entity.Velocity * dt;

        if (Entity.Position.Y >= GroundY)
        {
            Entity.Position.Y = GroundY;
            Entity.Velocity.Y = 0;
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }
}
