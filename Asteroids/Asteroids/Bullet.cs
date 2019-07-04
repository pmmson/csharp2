using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Bullet : BaseObject
    {
        Image image;
        static Random rand = new Random();
        
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        public Bullet(Point pos, Point dir, Size size, Image image) : base(pos, dir, size)
        {
            this.image = image;
        }

        public override Point Pos { get => pos; set => pos = value; }
        public override Size Size { get => size; set => size = value; }

        public override void Draw()
        {
            //Game.buffer.Graphics.DrawRectangle(Pens.OrangeRed, pos.X, pos.Y, size.Width, size.Height);
            Game.buffer.Graphics.DrawImage(image, pos);
        }

        public override void Update()
        {
            pos.X = pos.X + 30;
            if (pos.X > Game.Width)
            {
                pos.X = 0;
                pos.Y = rand.Next(0, Game.Height);
            }
        }
    }
}
