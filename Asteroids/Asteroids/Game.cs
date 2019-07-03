using System;
using System.Collections.Generic;
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
        static Random rand = new Random();

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
            objs = new BaseObject[100];
            int countStars = 90;
            int countAsteroids = 7;
            int countPlanets = 3;

            for (int i = 0; i < countStars; i++)
            {
                int x = rand.Next(0, Game.Width);
                int y = rand.Next(0, Game.Height);
                Image image = Image.FromFile(@"Pictures\star.png");
                objs[i] = new Star
                    (
                        new Point(x, y),
                        Star.Dir(x, y, 3),
                        new Size(5, 5), 
                        image
                    );
            }

            for (int i = countStars; i < countStars + countAsteroids; i++)
            {
                int x = rand.Next(0, Game.Width);
                int y = rand.Next(0, Game.Height);
                Image image = Image.FromFile(@"Pictures\asteroid.png");
                objs[i] = new Asteroid
                    (
                        new Point(x, y),
                        Asteroid.Dir(x, y, 7),
                        new Size(5, 5),
                        image
                    );
            }

            for (int i = countStars + countAsteroids; i < countStars + countAsteroids + countPlanets; i++)
            {
                int x = rand.Next(0, Game.Width);
                int y = rand.Next(0, Game.Height);
                Image image = Image.FromFile(@"Pictures\planet.png");
                objs[i] = new Planet
                    (
                        new Point(x, y),
                        BaseObject.Dir(x, y, 2),
                        new Size(100, 100),
                        image
                    );
            }
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
