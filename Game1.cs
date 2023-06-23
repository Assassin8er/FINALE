using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace FinalProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        MouseState mousestate;
        KeyboardState keyboardState;

        Texture2D gameover ,controls, intro ,asteroidTexture, flame1Texture, flame2Texture, shipTexture, backroundTexture, shotTexture, spaceTexture, rocket1Texture, rocket2Texture;
        Texture2D boom1, boom2, boom3, boom4, boom5, boom6, boom7, introScreen, controlsScreen, Space, gameoverScreen;
        Rectangle asteroidRect, shipRect, shotRect, rocketRect, flame1Rect, flame2Rect, introRect;
        SoundEffect Kaboom, Bam, Pew, Launch;
        SoundEffectInstance kaboomInstance, pewInstance, bamInstance, launchInstance;
        Random random = new Random();
        //Lists
        List<Asteroid> meteorList = new List<Asteroid>();
        List<Bullet> bulletList = new List<Bullet>();


        Screen screen;
        enum Screen
        {
            intro,
            controls,
            Space,
            gameover,
        }
        int shipSpeed;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screen = Screen.intro;
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1200; // Sets the width of the window
            _graphics.PreferredBackBufferHeight = 800; // Sets the height of the window

            shipRect = new Rectangle(70, 350, 120, 130);
            shipSpeed = 6;
            flame1Rect = new Rectangle(54, 406, 40, 16);
            introRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            _graphics.ApplyChanges(); // Applies the new dimensions
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Loading Content
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //User
            shipTexture = Content.Load<Texture2D>("Ship");//User ship
            flame1Texture = Content.Load<Texture2D>("Flame1");//flame sprite 1
            flame2Texture = Content.Load<Texture2D>("Flame2");//flame sprite 2
            //Ammo
            shotTexture = Content.Load<Texture2D>("Bullet");
            rocket1Texture = Content.Load<Texture2D>("rocket1");//sprite 2
            rocket2Texture = Content.Load<Texture2D>("rocket2");//launch sprite 1
            //User Explosion sprite
            boom1 = Content.Load<Texture2D>("Explosion1");//sprite 1
            boom2 = Content.Load<Texture2D>("Explosion2");//sprite 2
            boom3 = Content.Load<Texture2D>("Explosion3");//sprite 3
            boom4 = Content.Load<Texture2D>("Explosion4");//sprite 4
            boom5 = Content.Load<Texture2D>("Explosion5");//sprite 5
            boom6 = Content.Load<Texture2D>("Explosion6");//sprite 6
            boom7 = Content.Load<Texture2D>("Explosion7");//sprite 7
            //asteroid
            asteroidTexture = Content.Load<Texture2D>("Asteroid");
            //Screens
            introScreen = Content.Load<Texture2D>("start_Page");//Intro Screen
            controlsScreen = Content.Load<Texture2D>("Controls_"); //Controls Screen
            spaceTexture = Content.Load<Texture2D>("Space0");//Main Game Screen
            gameoverScreen = Content.Load<Texture2D>("game_over");//Ending Screen
            //Sounds
            Kaboom = Content.Load<SoundEffect>("Kaboom");//User ship explosion sound
            Pew = Content.Load<SoundEffect>("Pew");//Shooting sound effect
            Bam = Content.Load<SoundEffect>("Bam");//asteroid exploding sound
            Launch = Content.Load<SoundEffect>("RocketLaunch");//Launching rocket sound

        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            //KEYS
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.intro)
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    screen = Screen.controls;
                }
            }
            else if (screen == Screen.controls)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.Space;
                }
            }
            else if (screen == Screen.Space)
            {
                //Controls
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    shipRect.Y -= shipSpeed;
                    flame1Rect.Y -= shipSpeed;
                }
                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    shipRect.Y += shipSpeed;
                    flame1Rect.Y += shipSpeed;
                }
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    bulletList.Add(new Bullet(shotTexture, new Rectangle(shipRect.Right, shipRect.Y + shipRect.Height / 2, 5, 5), 10));

                }


                //Ship cant go off screen
                if (shipRect.Y > 800 - shipRect.Height)
                {
                    shipRect.Y = 800 - shipRect.Height;
                }

                if (shipRect.Y < 0)
                {
                    shipRect.Y = 0;
                }

                //Bullets
                foreach (Bullet bullet in bulletList)
                {
                    bullet.Update();
                }

                //Removing Bullets when off screen
                for (int i = bulletList.Count - 1; i >= 0; i--)
                {
                    Bullet bullet = bulletList[i];

                    if (bullet.GetX > 1200)
                    {
                        bulletList.RemoveAt(i);
                        break;
                    }
                }
                //Flame cant go off screen
                if (flame1Rect.Y > 742 - flame1Rect.Height)
                {
                    flame1Rect.Y = 742 - flame1Rect.Height;
                }

                if (flame1Rect.Y < 57)
                {
                    flame1Rect.Y = 57;
                }
                //Asteroids
                foreach (Asteroid asteroid in meteorList)
                {
                    asteroid.Update();
                }
                void SpawnAsteroid()
                {
                    int yPos = random.Next(57, 742 - asteroidTexture.Height);
                    Rectangle newAsteroidRect = new Rectangle(1200, yPos, asteroidTexture.Width, asteroidTexture.Height);
                    Asteroid newAsteroid = new Asteroid(asteroidTexture, newAsteroidRect, 1);
                    meteorList.Add(newAsteroid);
                }
                if (random.Next(100) < 5) // Probability of asteroid spawn (e.g., 5%)
                {
                    SpawnAsteroid();
                }
                for (int i = 0; i < meteorList.Count; i++)
                {
                    meteorList[i].Update();

                    // Check for collision with bullets
                    for (int j = bulletList.Count - 1; j >= 0; j--)
                    {
                        Bullet bullet = bulletList[j];
                        if (meteorList[i].GetX <= bullet.GetX + bullet.GetWidth && meteorList[i].GetX + meteorList[i].GetWidth >= bullet.GetX &&
                            meteorList[i].GetY <= bullet.GetY + bullet.GetHeight && meteorList[i].GetY + meteorList[i].GetHeight >= bullet.GetY)
                        {

                            bulletList.RemoveAt(j);
                            meteorList[i].Health -= 1;
                        }
                    }

                    // Check for collision with spaceship
                    if (meteorList[i].GetX <= shipRect.X + shipRect.Width && meteorList[i].GetX + meteorList[i].GetWidth >= shipRect.X &&
                        meteorList[i].GetY <= shipRect.Y + shipRect.Height && meteorList[i].GetY + meteorList[i].GetHeight >= shipRect.Y)
                    {
                        screen = Screen.gameover;
                    }

                    // Remove asteroids when they go off-screen
                    if (meteorList[i].GetX + meteorList[i].GetWidth < 0)
                    {
                        meteorList.RemoveAt(i);
                        i--;
                    }
                    // Remove asterooid wth zero health
                    else if (meteorList[i].Health <= 0)
                    {
                        meteorList.RemoveAt(i);
                        i--;
                    }
                }
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screen.intro)
            {
                _spriteBatch.Draw(introScreen, introRect, Color.White);
            }
            else if (screen == Screen.controls)
            {
                _spriteBatch.Draw(controlsScreen, introRect, Color.White);
            }
            else if (screen == Screen.Space)
            {
                _spriteBatch.Draw(spaceTexture, new Rectangle(0, 0, 1200, 800), Color.White);
                _spriteBatch.Draw(shipTexture, shipRect, Color.White);
                _spriteBatch.Draw(flame1Texture, flame1Rect, Color.White);
                foreach (Bullet bullet in bulletList)
                {
                    bullet.DrawBullet(_spriteBatch);
                }
                foreach (Asteroid asteroid in meteorList)
                {
                    asteroid.DrawAsteroid(_spriteBatch);
                }
            }
            else if (screen == Screen.gameover)
            {
                _spriteBatch.Draw(gameoverScreen, introRect, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}