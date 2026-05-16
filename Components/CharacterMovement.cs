using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Box2D.NET.Bindings;

namespace Physics_Game;

public class CharacterMovement : Component
{
    private float _moveSpeed   = 5f;
    private float _jumpImpulse = 8f;
    private bool  _isGrounded  = false;

    public override void Update(double deltaTime)
    {
        var rb = Entity.GetComponent<RigidBodyComponent>();
        if (rb == null) return;

        var kb  = Keyboard.GetState();
        var vel = rb.LinearVelocity;

        if      (kb.IsKeyDown(Keys.Right) || kb.IsKeyDown(Keys.D)) vel.x =  _moveSpeed;
        else if (kb.IsKeyDown(Keys.Left)  || kb.IsKeyDown(Keys.A)) vel.x = -_moveSpeed;
        else    vel.x = 0f;

        rb.LinearVelocity = vel;

        _isGrounded = CheckGrounded(rb);

        if ((kb.IsKeyDown(Keys.Space) || kb.IsKeyDown(Keys.W)) && _isGrounded)
            rb.LinearVelocity = new B2.Vec2 { x = vel.x, y = -_jumpImpulse };
    }

    private unsafe bool CheckGrounded(RigidBodyComponent rb)
    {
        var pos         = B2.BodyGetPosition(rb.BodyId);
        var translation = new B2.Vec2 { x = 0f, y = 0.55f };
        var filter      = B2.DefaultQueryFilter();

        var result = B2.WorldCastRayClosest(rb.WorldId, pos, translation, filter);

        if (result.hit)
        {
            var hitBody = B2.ShapeGetBody(result.shapeId);
            if (!hitBody.Equals(rb.BodyId))
                return true;
        }

        return false;
    }
}
