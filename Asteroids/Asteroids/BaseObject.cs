using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    public delegate void Message();
    abstract class BaseObject : ICollision
    {
        protected Point pos; // позиция объектов
        protected Point dir; // направление движения объектов
        protected Size size; // размеры объектов

        public abstract Point Pos { get; set; }
        public abstract Size Size { get; set; }


        protected BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            if (size.Width > 0 && size.Height > 0)
            {
                this.size.Width = size.Width;
                this.size.Height = size.Height;
            }
            else throw new GameObjectException();
        }

        public abstract void Draw(); // метод прорисовки объектов
       
        public abstract void Update(); // метод измениния положения объектов

        public Rectangle Rect => new Rectangle(Pos, Size);

        public bool Collision(ICollision obj)
        {
            return obj.Rect.IntersectsWith(this.Rect);
        }
    }
}
