using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Asteroid : BaseObject
    {
        Image image;
        static Random rand = new Random();
        public Asteroid(Point pos, Point dir, Size size, Image image) : base(pos, dir, size)
        {
            this.image = image;
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(image, pos);
        }

        public override void Update()
        {
            pos.X += dir.X;
            pos.Y += dir.Y;
            if (pos.X < 0 || pos.X > Game.Width || pos.Y < 0 || pos.Y > Game.Height)
            {
                pos.X = rand.Next(0, Game.Width);
                pos.Y = rand.Next(0, Game.Height);
                BaseObject.Dir(pos.X, pos.Y, rand.Next(1, 7));
            }
        }
    }
}
