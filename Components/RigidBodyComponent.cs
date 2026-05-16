namespace Physics_Game;

public class RigidBodyComponent : Component
{
    public float Mass;
    public float Restitution;
    public float Friction;
    public bool IsStatic;

    public float InverseMass => IsStatic || Mass <= 0f ? 0f : 1f / Mass;

    public RigidBodyComponent( float mass = 1f, float restitution = 0.2f, float friction = 0.3f, bool isStatic = false )
    {
        Mass = mass;
        Restitution = restitution;
        Friction = friction;
        IsStatic = isStatic;
    }

    public override void PrintDebug()
    {
        Console.WriteLine($"  RigidBody  mass:{Mass:F1} inv:{InverseMass:F3} rest:{Restitution:F2} fric:{Friction:F2} static:{IsStatic}");
    }
}
