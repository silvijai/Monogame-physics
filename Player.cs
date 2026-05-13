using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Physics_Game;

public class Player : Entity
{
    public int Health = 100;
    
    public Player(Vector2 position, Texture2D texture, SceneManager scene) : base(position)
    {
        Size = new Vector2(32, 48);

        AddComponent(new GravityComponent());
        AddComponent(new PlayerMovement()); 
        AddComponent(new SpriteRenderer(texture, Color.CornflowerBlue));
        var collider = AddComponent(new Collider("player"));

        scene.RegisterEntity(this);
        scene.CollisionManager.Register(collider);
    }
}
