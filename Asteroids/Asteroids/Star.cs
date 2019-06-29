using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    class Star : BaseObject
    {
        Pen color;
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size) // обращение к конструктору базового объекта
        {
            color = Pens.Aqua;
        }

        public Star(Point pos, Point dir, Size size, Pen color) : this(pos, dir, size) // : base(pos, dir, size)
        {
            this.color = color;
        }
        public override void Draw()
        {
            Game.buffer.Graphics.DrawLine(color, pos.X, pos.Y, pos.X + size.Width, pos.Y + size.Width);
            Game.buffer.Graphics.DrawLine(color, pos.X + size.Width, pos.Y, pos.X, pos.Y + size.Width);
        }
    }
}
