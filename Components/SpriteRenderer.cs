using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Physics_Game;

public class SpriteRenderer : Component
{
    public Texture2D Texture;
    public Color Tint;

    public SpriteRenderer(Texture2D texture, Color tint)
    {
        Texture = texture;
        Tint = tint;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            new Rectangle(
                (int)Entity.Position.X,
                (int)Entity.Position.Y,
                (int)Entity.Size.X,
                (int)Entity.Size.Y),
            Tint
        );
    }
}
