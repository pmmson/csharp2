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
        
        public Bullet(Point pos, Point dir, Size size, Image image) : base(pos, dir, size)
        {
            this.image = image;
        }

        public override Point Pos { get => pos; set => pos = value; }
        public override Size Size { get => size; set => size = value; }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(image, pos);
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
        }
    }
}
