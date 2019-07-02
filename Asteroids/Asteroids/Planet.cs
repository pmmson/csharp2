using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Planet : BaseObject
    {
        Image image;
        static Random rand = new Random();

        public Planet(Point pos, Point dir, Size size, Image image) : base (pos, dir, size)
        {
            this.image = image;
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(image, pos);
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;
            
            if (pos.X < 0) pos.X = Game.Width;
            else if (pos.X > Game.Width) pos.X = 0;
            else if (pos.Y < 0) pos.Y = Game.Height;
            else if (pos.Y > Game.Height) pos.Y = 0;

            BaseObject.Dir(pos.X, pos.Y, rand.Next(1, 4));
        }
    }
}
