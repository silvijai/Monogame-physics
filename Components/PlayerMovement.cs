using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Physics_Game;

public class PlayerMovement : Component
{
    private float _speed = 200f;
    private float _jumpForce = -600f;

    public override void Update(double deltaTime)
    {
        var kb = Keyboard.GetState();
        float dt = (float)deltaTime;

        if (kb.IsKeyDown(Keys.Right)) Entity.Position.X += _speed * dt;
        if (kb.IsKeyDown(Keys.Left))  Entity.Position.X -= _speed * dt;

        if (kb.IsKeyDown(Keys.Space))
        {
            var gravity = Entity.GetComponent<GravityComponent>();
            if (gravity != null /* && gravity.IsGrounded*/)
                Entity.Velocity.Y = _jumpForce;
        }
    }
}
