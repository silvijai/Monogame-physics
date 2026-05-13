using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Physics_Game;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _whitePixel;

    private Player _player;

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
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Placeholder 1x1 white pixel for drawing coloured rectangles
        _whitePixel = new Texture2D(GraphicsDevice, 1, 1);
        _whitePixel.SetData(new[] { Color.White });

        // Build player using ECS
        _player = new Player(new Vector2(100, 300));
        _player.Size = new Vector2(32, 48);
        _player.AddComponent(new GravityComponent { GroundY = 650f });
        _player.AddComponent(new SpriteRenderer(_whitePixel, Color.CornflowerBlue));
    }

    protected override void Update(GameTime gameTime)
    {
        float speed = 200f;
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        var kb = Keyboard.GetState();

        if (kb.IsKeyDown(Keys.Escape)) Exit();
        if (kb.IsKeyDown(Keys.Right)) _player.Position.X += speed * dt;
        if (kb.IsKeyDown(Keys.Left))  _player.Position.X -= speed * dt;

        // Jump
        if (kb.IsKeyDown(Keys.Space))
        {
            var gravity = _player.GetComponent<GravityComponent>();
            if (gravity != null && gravity.IsGrounded)
                _player.Velocity.Y = -600f;
        }

        _player.Update(gameTime.ElapsedGameTime.TotalSeconds);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DeepPink);

        _spriteBatch.Begin();
        _player.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
