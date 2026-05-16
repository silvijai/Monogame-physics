using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using nkast.Aether.Physics2D.Dynamics;
using System;

namespace Physics_Game;

public class CharacterMovement : Component
{
    private float _moveSpeed   = 5f;
    private float _jumpImpulse = 8f;
    private bool  _isGrounded  = false;

    public override void Update(double deltaTime)
    {
        var rb = Entity.GetComponent<RigidBodyComponent>();
        if (rb == null) { return; }

        var kb = Keyboard.GetState();
        var vel = rb.Body.LinearVelocity;

        if (kb.IsKeyDown(Keys.Right) || kb.IsKeyDown(Keys.D)) vel.X =  _moveSpeed;
        else if (kb.IsKeyDown(Keys.Left) || kb.IsKeyDown(Keys.A)) vel.X = -_moveSpeed;
        else vel.X = 0f;

        rb.Body.LinearVelocity = vel;

        _isGrounded = CheckGrounded(rb);

        if ((kb.IsKeyDown(Keys.Space) || kb.IsKeyDown(Keys.W)) && _isGrounded)
        {
            rb.Body.LinearVelocity = new Vector2(vel.X, -_jumpImpulse);
        }
    }

    private bool CheckGrounded(RigidBodyComponent rb)
    {
        bool grounded = false;
        var start = rb.Body.Position;
        var end = start + new Vector2(0f, 0.55f);

        rb.Body.World.RayCast((fixture, point, normal, fraction) =>
        {
            if (fixture.Body == rb.Body) return -1f;
            grounded = true;
            return 0f;
        }, start, end);

        return grounded;
    }
}
