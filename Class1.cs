using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class Bullet
    {

        Texture2D _texture;
        Rectangle _rect;
        float _speed;
        public Bullet(Texture2D texture, Rectangle rect, int speed)
        {
            _texture = texture;
            _rect = rect;
            _speed = speed;

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
            _rect.X += (int)_speed;
        }
        public void Collide()
        {


        }
        public void DrawBullet(SpriteBatch sprite)
        {
            sprite.Draw(_texture, _rect, Color.White);
        }
    }

}