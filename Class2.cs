using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Asteroid
    {

        Texture2D _texture;
        Rectangle _rect;
        float _speed;
        int _health;
        public Asteroid(Texture2D texture, Rectangle rect, int speed)
        {
            _texture = texture;
            _rect = rect;
            _speed = speed;
            _health = 12;
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }


        public int GetX
        {
            get { return _rect.X; }
        }
        public int GetY
        {
            get { return _rect.Y; }
        }
        public int GetWidth
        {
            get { return _rect.Width; }
        }
        public int GetHeight
        {
            get { return _rect.Height; }
        }
        public void Update()
        {
            //Move bullet right
            _rect.X -= (int)_speed;
        }
        public void DrawAsteroid(SpriteBatch sprite)
        {
            sprite.Draw(_texture, _rect, Color.White);
        }

    }
}
