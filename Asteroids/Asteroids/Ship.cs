using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class Ship : BaseObject
    {
        Image image;
        private int _energy = 100;
        private int _count = 0;
        public int Energy => _energy;
        public int Count => _count;

        public static event Message MessageDie;
        public void EnergyLow(int n)
        {
            _energy -= n;
        }
        public void EnergyUp(int n)
        {
            _energy += n;
            if (_energy > 100) _energy = 100;
        }
        public void Counter()
        {
            _count++;
        }

        public Ship(Point pos, Point dir, Size size, Image image) : base(pos, dir, size)
        {
            this.image = image;
        }

        public override Point Pos { get => pos; set => throw new NotImplementedException(); }
        public override Size Size { get; set; }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(image, pos.X, pos.Y, size.Width, size.Height);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
        public void Up()
        {
            if (pos.Y > 0) pos.Y = pos.Y - dir.Y;
        }
        public void Down()
        {
            if (pos.Y < Game.Height) pos.Y = pos.Y + dir.Y;
        }
        public void Die()
        {
            MessageDie?.Invoke();
        }

    }
}
