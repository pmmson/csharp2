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

        static Asteroid[] asteroids;
        static Star[] stars;
        static Planet[] planets;
        static Bullet bullet;
        
        static public int Init(Form form)
        {
            // графическое устройство для вывода графики
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics(); // создаем поверхность для рисования и связываем его с формой
            try
            {
                Width = form.ClientSize.Width;
                Height = form.ClientSize.Height;
                if (Width <= 120 || Width > Screen.PrimaryScreen.WorkingArea.Width * 0.99 || Height <= 0 || Height > Screen.PrimaryScreen.WorkingArea.Height * 0.95) throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentOutOfRangeException)
            {
                return -1;
            }
            //  связываем буфер в памяти с графическим объектом для того чтобы рисовать в буфере
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
            return 0;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        static public void Load()
        {
            stars= new Star[100];
            asteroids = new Asteroid[10];
            planets = new Planet[3];
            Image rocket = Image.FromFile(@"Pictures\rocket.png");
            bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(10, 10), rocket);

            for (int i = 0; i < stars.Length; i++)
            {
                int x = rand.Next(0, Game.Width);
                int y = rand.Next(0, Game.Height);
                Image image = Image.FromFile(@"Pictures\star.png");
                stars[i] = new Star
                    (
                        new Point(x, y),
                        Star.Dir(x, y, 3),
                        new Size(20, 20), 
                        image
                    );
            }

            for (int i = 0; i < asteroids.Length; i++)
            {
                int x = rand.Next(0, Game.Width);
                int y = rand.Next(0, Game.Height);
                Image image = Image.FromFile(@"Pictures\asteroid.png");
                asteroids[i] = new Asteroid
                    (
                        new Point(x, y),
                        Asteroid.Dir(x, y, 7),
                        new Size(50, 50),
                        image
                    );
            }

            for (int i = 0; i < planets.Length; i++)
            {
                int x = rand.Next(0, Game.Width);
                int y = rand.Next(0, Game.Height);
                Image image = Image.FromFile(@"Pictures\planet.png");
                planets[i] = new Planet
                    (
                        new Point(x, y),
                        BaseObject.Dir(x, y, 2),
                        new Size(70, 70),
                        image
                    );
            }
        }

        static public void Draw()
        {
            buffer.Graphics.Clear(Color.Black);

            foreach (Star obj in stars)
            {
                obj.Draw();
            }
            foreach (Asteroid obj in asteroids)
            {
                obj.Draw();
            }
            foreach (Planet obj in planets)
            {
                obj.Draw();
            }
            bullet.Draw();
            buffer.Render();
        }

        static public void Update()
        {
            foreach(Star obj in stars)
            {
                obj.Update();
            }
            foreach (Asteroid obj in asteroids)
            {
                obj.Update();
            }
                foreach (Planet obj in planets)
            {
                obj.Update();
            }
            bullet.Update();

            foreach (Asteroid obj in asteroids)
            {
                if (obj.Collision(bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    obj.Pos = new Point(rand.Next(0, Game.Width), rand.Next(0, Game.Height));
                    bullet.Pos = new Point(0, rand.Next(0, Game.Height));
                }
            }
        }
    }
}
