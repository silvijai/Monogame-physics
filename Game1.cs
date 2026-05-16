using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Physics_Game;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SceneManager _sceneManager = new SceneManager();

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
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _sceneManager.LoadContent(new SpriteBatch(GraphicsDevice));

        _whitePixel = new Texture2D(GraphicsDevice, 1, 1);
        _whitePixel.SetData(new[] { Color.White });

        // debug triangle
        var triangle = new Entity(1, "Triangle");

        triangle.AddComponent(new TransformComponent(new Vector2(400, 300)));
        triangle.AddComponent(new VelocityComponent());
        triangle.AddComponent(new ShapeComponent(
            new PolygonShape(new[]
            {
                new Vector2(  0, -60),
                new Vector2( 50,  40),
                new Vector2(-50,  40),
            }),
            _whitePixel,
            Color.Cyan
        ));

        triangle.Debug = true;

        _sceneManager.RegisterEntity(triangle);

        // physics box
        var box = new Entity(2, "Box");  // use a different id than the triangle
        var tbox = box.AddComponent(new TransformComponent(new Vector2(400, 100), new Vector2(2, 2)));
        box.AddComponent(new VelocityComponent());
        box.AddComponent(new GravityComponent());       // updates velocity first
        box.AddComponent(new RigidBodyComponent());     // then integrates into position
        box.AddComponent(new ShapeComponent(
            new PolygonShape(new[]
            {
                new Vector2(-30, -30),
                new Vector2( 30, -30),
                new Vector2( 30,  30),
                new Vector2(-30,  30),
            }),
            _whitePixel,
            Color.Yellow
        ));

        box.Debug = true;
        tbox.PrintDebug = true;

        _sceneManager.RegisterEntity(box);
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
