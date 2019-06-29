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
        static BaseObject[] objs; // описание нашего массива объектов
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
            // Draw(); // for test
            Load();
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        static public void Load()
        {
            objs = new BaseObject[30+1];
            for(int i = 0; i < objs.Length / 2; i++)
            {
                objs[i] = new BaseObject(new Point(300, i * 20), new Point(15 - i, 15 - i), new Size(20, 20));
            }
            for (int i = objs.Length / 2; i < objs.Length; i++)
            {
                objs[i] = new Star(new Point(300, i * 20), new Point(15 - i, 15 - i), new Size(20, 20), Pens.Red);
            }
            Image image = Image.FromFile(@"Pictures\planet.png");
            objs[30] = new Planet(new Point(500, 500), new Point(-3, 0), new Size(100, 100), image);
        }

        static public void Draw()
        {
            // проверяем вывод графики for test
            buffer.Graphics.Clear(Color.Black);
            //buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            foreach (BaseObject obj in objs)
            {
                //if (obj is Star) (obj as Star).Draw();
                //if (obj is BaseObject) (obj as BaseObject).Draw();
                obj.Draw();
            }
            buffer.Render();
        }

        static public void Update()
        {
            foreach(BaseObject obj in objs)
            {
                obj.Update();
            }
        }
    }
}
