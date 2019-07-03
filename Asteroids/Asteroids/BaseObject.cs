using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class BaseObject
    {
        protected Point pos;
        protected Point dir;
        protected Size size;
        static Random rand = new Random();
        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }

        public virtual void Draw()
        {
            Game.buffer.Graphics.DrawEllipse(Pens.Wheat, pos.X, pos.Y, size.Width, size.Height);
        }

        public virtual void Update()
        {
            pos.X += dir.X;
            pos.Y += dir.Y;
            if (pos.X < 0) dir.X = -dir.X;
            if (pos.X > Game.Width) dir.X = -dir.X;
            if (pos.Y < 0) dir.Y = -dir.Y;
            if (pos.Y > Game.Height) dir.Y = -dir.Y;
        }

        public static Point Dir(int x, int y, int speed)
        {
            if (x > Game.Width / 2 && y > Game.Height / 2) { x = rand.Next(1, speed + 1); y = rand.Next(1, speed + 1); }
            else if (x > Game.Width / 2 && y < Game.Height / 2) { x = rand.Next(1, speed + 1); y = rand.Next(-speed, 1); }
            else if (x < Game.Width / 2 && y > Game.Height / 2) { x = rand.Next(-speed, 1); y = rand.Next(1, speed + 1); }
            else if (x < Game.Width / 2 && y < Game.Height / 2) { x = rand.Next(-speed, 1); y = rand.Next(-speed, 1); }

            return new Point(x, y);
        }
    }
}
