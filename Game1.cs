using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D asteroidTexture, flame1Texture, flame2Texture, shipTexture, backroundTexture, shotTexture, spaceTexture;
        Rectangle asteroidRect, shipRect, shotRect, rocketRect, flame1Rect, flame2Rect;
        Vector2 bulletspeed, asteroidSpeed, shipSpeed, rocketSpeed, flame1Speed, flame2Speed;
        SoundEffect Kaboom, Bam, Pew, Launch;
        SoundEffectInstance kaboomInstance, pewInstance, bamInstance, launchInstance;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1200; // Sets the width of the window
            _graphics.PreferredBackBufferHeight = 800; // Sets the height of the window
            _graphics.ApplyChanges(); // Applies the new dimensions
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            shipTexture = Content.Load<Texture2D>("Ship");
            flame1Texture = Content.Load<Texture2D>("Flame1");
            flame2Texture = Content.Load<Texture2D>("Flame2");
            shotTexture = Content.Load<Texture2D>("shot");
            spaceTexture = Content.Load<Texture2D>("Space0");

            Kaboom = Content.Load<SoundEffect>("Kaboom");
            Pew = Content.Load<SoundEffect>("Pew");
            Bam = Content.Load<SoundEffect>("Bam");
            Launch = Content.Load<SoundEffect>("RocketLaunch");


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(spaceTexture, new Rectangle(0, 0, 1200, 800), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}