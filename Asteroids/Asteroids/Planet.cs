﻿using System;
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
        public Planet(Point pos, Point dir, Size size, Image image) : base (pos, dir, size)
        {
            this.image = image;
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(image, pos);
        }
    }
}
