using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using nkast.Aether.Physics2D.Dynamics;

namespace Physics_Game;

public class Player : Entity
{
    public int Health = 100;
    private RigidBodyComponent _rb;

    public Player(Vector2 position, SceneManager scene, Texture2D pixel) : base(scene.NextId(), "Player")
    {
        _rb = this.AddComponent(new RigidBodyComponent(scene.World, position, false));
        _rb.Body.FixedRotation = true;
        
        this.AddComponent(new TransformComponent(position, 0f));
        this.AddComponent(new ShapeComponent(scene.World, _rb.Body,
            new[] { new Vector2(-20,-30), new Vector2(20,-30), new Vector2(20,30), new Vector2(-20,30) },
            pixel, Color.Pink));
        this.AddComponent(new CharacterMovement());

        scene.RegisterEntity(this);
    }
}
