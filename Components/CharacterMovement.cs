using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Box2D.NET.Bindings;

namespace Physics_Game;

public class CharacterMovement : Component
{
    private float _moveSpeed = 5f;
    private float _jumpImpulse = 8f;
    private bool _isGrounded = false;

    public override void Update(double deltaTime)
    {
        var rb = Entity.GetComponent<RigidBodyComponent>();
        if (rb == null) return;

        var kb  = Keyboard.GetState();
        var vel = rb.LinearVelocity;

        if (kb.IsKeyDown(Keys.Right) || kb.IsKeyDown(Keys.D)) vel.x =  _moveSpeed;
        else if (kb.IsKeyDown(Keys.Left)  || kb.IsKeyDown(Keys.A)) vel.x = -_moveSpeed;
        else vel.x = 0f;

        rb.LinearVelocity = vel;

        if ((kb.IsKeyDown(Keys.Space) || kb.IsKeyDown(Keys.W)) && rb.IsGrounded)
            rb.LinearVelocity = new B2.Vec2 { x = vel.x, y = -_jumpImpulse };
    }
}
