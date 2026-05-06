using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Topic_4___Tracking_Time_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;

        Texture2D bombTexture;
        Texture2D boomTexture;
       

        Rectangle bombRect;
        Rectangle boomRect;
       

        SpriteFont bombText;

        bool exploded;

        SoundEffect explosion;
        SoundEffectInstance explosionInstance;

        float seconds;

        MouseState mouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0,0,800,500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            bombRect = new Rectangle(50, 50, 700, 400);
            boomRect = new Rectangle(50, 50, 700, 400);
            exploded = false;
            seconds = 0f;
            seconds = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            bombTexture = Content.Load<Texture2D>("bomb");
            boomTexture = Content.Load<Texture2D>("boom"); 
            bombText = Content.Load<SpriteFont>("BombFont");
            explosion = Content.Load<SoundEffect>("explosion");
            explosionInstance = explosion.CreateInstance();

        }

        protected override void Update(GameTime gameTime)
        {
            this.Window.Title = mouseState.Position.ToString();

            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
                seconds = 0f;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (!exploded)
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (seconds >= 15)
            {
                explosionInstance.Play();
                seconds = 0f;
                exploded = true;
            }
                
            if (exploded && explosionInstance.State == SoundState.Stopped)
                Exit();
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Azure);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (!exploded)
            {
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
                _spriteBatch.DrawString(bombText, seconds.ToString("00:0"), new Vector2(270, 200), Color.Black);
            }
            else
            {
                _spriteBatch.Draw(boomTexture, boomRect, Color.White);
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
