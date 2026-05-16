using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Physics_Game;

public class SceneManager
{
    private SpriteBatch _spriteBatch;
    // public CollisionManager CollisionManager = new CollisionManager();

    private List<Entity> _entities = new List<Entity>();

    public void RegisterEntity(Entity entity) => _entities.Add(entity);
    public void UnregisterEntity(Entity entity) => _entities.Remove(entity);

    public void LoadContent(SpriteBatch spriteBatch)
    {
        _spriteBatch = spriteBatch;

    }

    public void Update(double deltaTime)
    {
        for (int i = 0; i < _entities.Count; i++)
        {
            _entities[i].Update(deltaTime);
            _entities[i].DebugPrint();
        }

        // CollisionManager.Update();
    }

    public void Draw(double deltaTime)
    {
        _spriteBatch.Begin();

        for (int i = 0; i < _entities.Count; i++)
        {
            _entities[i].Draw(_spriteBatch);
            _entities[i].DebugDraw(_spriteBatch);
        }

        _spriteBatch.End();        
    }
}
