using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class asteroid
    {
        Texture2D _texture;
        Rectangle _rect;
        float _speed;
        public asteroid(Texture2D texture, Rectangle rect, int speed)
        {
            _texture = texture;
            _rect = rect;
            _speed = speed;
        }
        public int GetX
        {
            get { return _rect.X; }
        }
        public void Update()
        {
            //Move bullet right
            _rect.X -= (int)_speed;
        }
        public void ZCollide()
        {


        }







    }
}
