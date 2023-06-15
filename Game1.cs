using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FinalProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        KeyboardState keyboardState;

        Texture2D asteroidTexture, flame1Texture, flame2Texture, shipTexture, backroundTexture, shotTexture, spaceTexture;
        Rectangle asteroidRect, shipRect, shotRect, rocketRect, flame1Rect, flame2Rect;
        SoundEffect Kaboom, Bam, Pew, Launch;
        SoundEffectInstance kaboomInstance, pewInstance, bamInstance, launchInstance;

        List<Bullet> bulletList = new List<Bullet>();
        int shipSpeed;
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

            shipRect = new Rectangle(70, 350, 120, 130);
            shipSpeed = 6;



            _graphics.ApplyChanges(); // Applies the new dimensions
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            shipTexture = Content.Load<Texture2D>("Ship");
            flame1Texture = Content.Load<Texture2D>("Flame1");
            flame2Texture = Content.Load<Texture2D>("Flame2");
            shotTexture = Content.Load<Texture2D>("bullet");
            spaceTexture = Content.Load<Texture2D>("Space0");

            Kaboom = Content.Load<SoundEffect>("Kaboom");
            Pew = Content.Load<SoundEffect>("Pew");
            Bam = Content.Load<SoundEffect>("Bam");
            Launch = Content.Load<SoundEffect>("RocketLaunch");


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                shipRect.Y -= shipSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                shipRect.Y += shipSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                bulletList.Add( new Bullet(shotTexture, new Rectangle(shipRect.Right, shipRect.Y +shipRect.Height/2, 5, 5), 10));
            }

            if (shipRect.Y > 800 - shipRect.Height)
            {
                shipRect.Y = 800- shipRect.Height;
            }

            if (shipRect.Y < 0)
            {
                shipRect.Y = 0;
            }
            foreach (Bullet bullet in bulletList)
            {
                bullet.Update();
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(spaceTexture, new Rectangle(0, 0, 1200, 800), Color.White);
            _spriteBatch.Draw(shipTexture, shipRect, Color.White);
            foreach(Bullet bullet in bulletList)
            {
                bullet.DrawBullet(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}