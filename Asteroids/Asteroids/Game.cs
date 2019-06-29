using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroids
{
    static class Game
    {
        static BufferedGraphicsContext context;
        static public BufferedGraphics buffer;

        // ширина и высота игрового поля
        static public int Width { get; set; }
        static public int Height { get; set; }

        static public void Init(Form form)
        {
            // графическое устройство для вывода графики
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics(); // создаем поверхность для рисования и связываем его с формой
            Width = form.Width;
            Height = form.Height;
            //  связываем буфер в памяти с графическим объектом для того чтобы рисовать в буфере
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Draw();
        }

        static public void Draw()
        {
            // проверяем вывод графики
            buffer.Graphics.Clear(Color.Black);
            buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            buffer.Render();
        }

    }
}
