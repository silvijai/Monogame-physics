using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Physics_Game;

public abstract class Component
{
    public Entity Entity { get; set; } 
    public bool Enabled = true;
    public bool DrawDebug = true;
    public bool PrintDebug = false;

    public virtual void Update(double deltaTime) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
    public virtual void DebugDraw(SpriteBatch spriteBatch) { }
    public virtual void DebugPrint() { }
}
