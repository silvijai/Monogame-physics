using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using nkast.Aether.Physics2D.Dynamics;
using System.Collections.Generic;

namespace Physics_Game;

public class SceneManager
{
    private SpriteBatch _spriteBatch;
    private Texture2D _pixel;
    private List<Entity> _entities = new List<Entity>();

    public World World { get; private set; }

    public const float PixelsToMeters = 1f / 64f;
    public const float MetersToPixels = 64f;

    public SceneManager(GraphicsDevice graphicsDevice)
    {
        World = new World(new Vector2(0f, 9.8f));
    }

    public void RegisterEntity(Entity entity) => _entities.Add(entity);
    public void UnregisterEntity(Entity entity) => _entities.Remove(entity);

    public void LoadContent(SpriteBatch spriteBatch, Texture2D pixel)
    {
        _spriteBatch = spriteBatch;
        _pixel = pixel;
    }

    public void Update(double deltaTime)
    {
        World.Step((float)deltaTime);

        foreach (var entity in _entities)
        {
            entity.GetComponent<RigidBodyComponent>()?.SyncTransform();
            entity.DebugPrint();
        }
    }

    public void Draw(double deltaTime)
    {
        _spriteBatch.Begin();
        foreach (var entity in _entities)
        {
            entity.Draw(_spriteBatch);
            entity.DebugDraw(_spriteBatch);
        }
        _spriteBatch.End();
    }
}
