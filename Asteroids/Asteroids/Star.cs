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
        //Pen color;
        Image image;
        static Random rand = new Random();

        public override Point Pos { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override Size Size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Star(Point pos, Point dir, Size size) : base(pos, dir, size) // обращение к конструктору базового объекта
        {
            //color = Pens.Aqua;
        }

        //public Star(Point pos, Point dir, Size size, Pen color) : this(pos, dir, size) // : base(pos, dir, size)
        //{
        //    this.color = color;
        //}
        public Star(Point pos, Point dir, Size size, Image image) : this(pos, dir, size) // : base(pos, dir, size)
        {
            this.image = image;
        }
        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(image, pos);
            //    Game.buffer.Graphics.DrawLine(color, pos.X, pos.Y, pos.X + size.Width, pos.Y + size.Width);
            //    Game.buffer.Graphics.DrawLine(color, pos.X + size.Width, pos.Y, pos.X, pos.Y + size.Width);
            //    Game.buffer.Graphics.DrawLine(color, pos.X + size.Width / 2, pos.Y - size.Width / 2, pos.X + size.Width / 2, pos.Y + size.Width / 2 + size.Width);
            //    Game.buffer.Graphics.DrawLine(color, pos.X - size.Width / 2, pos.Y + size.Width / 2, pos.X + size.Width / 2 + size.Width, pos.Y + size.Width / 2);
        }

        public override void Update()
        {
            pos.X += dir.X;
            pos.Y += dir.Y;
            if (pos.X < 0 || pos.X > Game.Width || pos.Y < 0 || pos.Y > Game.Height)
            {
                pos.X = rand.Next(0, Game.Width);
                pos.Y = rand.Next(0, Game.Height);
                BaseObject.Dir(pos.X, pos.Y, rand.Next(0, 2));
            }
        }
    }
}
