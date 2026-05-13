using Microsoft.Xna.Framework;

namespace Physics_Game;

public class GravityComponent : Component
{
    public float Force = 800f;

    public override void Update(double deltaTime)
    {
        float dt = (float)deltaTime;
        Entity.Velocity.Y += Force * dt; 
    }
}
