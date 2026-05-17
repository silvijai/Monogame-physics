using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Physics_Game;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SceneManager _sceneManager;
    private InputManager _input;

    private Texture2D _whitePixel;   

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _sceneManager = new SceneManager(GraphicsDevice);
        _input = new InputManager();
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _whitePixel = new Texture2D(GraphicsDevice, 1, 1);
        _whitePixel.SetData(new[] { Color.White });
        _sceneManager.LoadContent(new SpriteBatch(GraphicsDevice), _whitePixel);

        var world = _sceneManager.WorldId;

        // Triangle
        var triangle = new Entity(_sceneManager.NextId(), "Triangle");
        var triBody = triangle.AddComponent(new RigidBodyComponent(world, new Vector2(380, 300), false));
        triangle.AddComponent(new TransformComponent(new Vector2(380, 300)));
        triangle.AddComponent(new ShapeComponent(world, triBody.BodyId,
            new[] { new Vector2(0,-60), new Vector2(50,40), new Vector2(-50,40) },
            _whitePixel, Color.Cyan));
        _sceneManager.RegisterEntity(triangle);

        // Box
        var box = new Entity(_sceneManager.NextId(), "Box");
        var boxBody = box.AddComponent(new RigidBodyComponent(world, new Vector2(400, 100), false));
        box.AddComponent(new TransformComponent(new Vector2(400, 100)));
        box.AddComponent(new ShapeComponent(world, boxBody.BodyId,
            new[] { new Vector2(-30,-30), new Vector2(30,-30), new Vector2(30,30), new Vector2(-30,30) },
            _whitePixel, Color.Yellow));
        _sceneManager.RegisterEntity(box);

        // Ground left
        var ground = new Entity(_sceneManager.NextId(), "Ground");
        var gBody = ground.AddComponent(new RigidBodyComponent(world, new Vector2(40, 615), true, 0.1f));
        ground.AddComponent(new TransformComponent(new Vector2(40, 615)));
        ground.AddComponent(new ShapeComponent(world, gBody.BodyId,
            new[] { new Vector2(-600,-15), new Vector2(600,-15), new Vector2(600,15), new Vector2(-600,15) },
            _whitePixel, Color.Green));
        _sceneManager.RegisterEntity(ground);

        // Ground right
        var ground2 = new Entity(_sceneManager.NextId(), "Ground2");
        var g2Body = ground2.AddComponent(new RigidBodyComponent(world, new Vector2(1240, 615), true, -0.1f));
        ground2.AddComponent(new TransformComponent(new Vector2(1240, 615)));
        ground2.AddComponent(new ShapeComponent(world, g2Body.BodyId,
            new[] { new Vector2(-600,-15), new Vector2(600,-15), new Vector2(600,15), new Vector2(-600,15) },
            _whitePixel, Color.Green));
        _sceneManager.RegisterEntity(ground2); 

        var player = new Player(new Vector2(30, 200), _sceneManager, _whitePixel);
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

        _sceneManager.Update(gameTime.ElapsedGameTime.TotalSeconds); 

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _sceneManager.Draw(gameTime.ElapsedGameTime.TotalSeconds);

        base.Draw(gameTime);
    }
}
