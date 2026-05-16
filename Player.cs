using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Box2D.NET.Bindings;

namespace Physics_Game;

public class Player : Entity
{
    public int Health = 100;
    public RigidBodyComponent Rb { get; private set; }

    public Player(Vector2 position, SceneManager scene, Texture2D pixel)
        : base(scene.NextId(), "Player")
    {
        Rb = this.AddComponent(new RigidBodyComponent(scene.WorldId, position, false));
        Rb.SetFixedRotation(true);

        this.AddComponent(new TransformComponent(position, 0f));
        this.AddComponent(new ShapeComponent(scene.WorldId, Rb.BodyId,
            new[] { new Vector2(-20,-30), new Vector2(20,-30), new Vector2(20,30), new Vector2(-20,30) },
            pixel, Color.Pink));
        this.AddComponent(new CharacterMovement());

        scene.RegisterEntity(this);
    }
}

