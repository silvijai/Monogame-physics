using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Box2D.NET.Bindings;
using System.Collections.Generic;

namespace Physics_Game;

public class SceneManager
{
    private SpriteBatch _spriteBatch;
    private Texture2D _pixel;
    private List<Entity> _entities = new List<Entity>();

    public B2.WorldId WorldId { get; private set; }

    private int _nextId = 0;
    public int NextId() => _nextId++;

    public const float PixelsToMeters = 1f / 64f;
    public const float MetersToPixels = 64f;

    public unsafe SceneManager(GraphicsDevice graphicsDevice)
    {
        var worldDef = B2.DefaultWorldDef();
        worldDef.gravity = new B2.Vec2 { x = 0f, y = 9.8f };
        WorldId = B2.CreateWorld(&worldDef);
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
        foreach (var entity in _entities)
        {
            entity.Update(deltaTime);
            entity.DebugPrint();
        }

        B2.WorldStep(WorldId, (float)deltaTime, 4);

        foreach (var entity in _entities)
            entity.GetComponent<RigidBodyComponent>()?.SyncTransform();
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

    public void Dispose()
    {
        B2.DestroyWorld(WorldId);
    }
}

