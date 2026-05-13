using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        Entity player = new Player(new Vector2(100, 300), _whitePixel, _sceneManager);

        var platform = new Entity(new Vector2(0, 650));
        platform.Size = new Vector2(1280, 20);
        platform.AddComponent(new SpriteRenderer(_whitePixel, Color.DarkGray));
        var platformCollider = platform.AddComponent(new Collider("solid"));
        _sceneManager.RegisterEntity(platform);
        _sceneManager.CollisionManager.Register(platformCollider);
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
        _sceneManager.Update(gameTime.ElapsedGameTime.TotalSeconds);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Green);

        _sceneManager.Draw(gameTime.ElapsedGameTime.TotalSeconds); 

        base.Draw(gameTime);
    }
}
