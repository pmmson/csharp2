using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroids
{
    delegate void Logger(string s); // для логирования
    static class Game
    {
        static BufferedGraphicsContext context;
        static public BufferedGraphics buffer;
        static Logger log;
        static Logger logFile;
        static Random rand = new Random();
        private static Timer timer = new Timer();

        // ширина и высота игрового поля
        static public int Width { get; set; }
        static public int Height { get; set; }

        // создаем массивы объектов игры
        static Asteroid[] asteroids;
        static Asteroid[] aids;
        static Star[] stars;
        static Planet[] planets;
        static Bullet bullet;

        //создаем корабль
        static Image shipImage = Image.FromFile(@"Pictures\ship.png");
        static Ship ship = new Ship(new Point(5, 300), new Point(15, 15), new Size(70, 70), shipImage);
        /// <summary>
        /// Инициилизация игры
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        static public int Init(Form form)
        {
            log = LogAsteroids.LogToConsole;
            logFile = LogAsteroids.LogToFile;

            // графическое устройство для вывода графики
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics(); // создаем поверхность для рисования и связываем его с формой
            try
            {
                Width = form.ClientRectangle.Width;
                Height = form.ClientRectangle.Height;
                // логгируем размеры игрового поля
                log($"Создано игровое поле {Width}x{Height}");
                logFile($"Создано игровое поле {Width}x{Height}");
                if (Width <= 120 || Width > Screen.PrimaryScreen.WorkingArea.Width * 0.99 || Height <= 0 || Height > Screen.PrimaryScreen.WorkingArea.Height * 0.95) throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentOutOfRangeException)
            {
                return -1;
            }
            //  связываем буфер в памяти с графическим объектом для того чтобы рисовать в буфере
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            try
            {
                Load();
            }
            catch (GameObjectException)
            {
                return -2;
            }

            timer.Interval = 50;
            timer.Start();
            timer.Tick += Timer_Tick;
            
            form.KeyDown += Form_KeyDown;

            Ship.MessageDie += Finish;

            return 0;
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            Image rocket = Image.FromFile(@"Pictures\rocket.png");
            if (e.KeyCode == Keys.ControlKey)
            {
                bullet = new Bullet(new Point(ship.Pos.X, ship.Pos.Y), new Point(30, 0), new Size(10, 10), rocket);
                log($"Пуск ракеты ({bullet.Pos.X}, {bullet.Pos.Y}) в точке ({ship.Pos.X}, {ship.Pos.Y}) ");
                logFile($"Пуск ракеты ({bullet.Pos.X}, {bullet.Pos.Y}) в точке ({ship.Pos.X}, {ship.Pos.Y}) ");
            }
            if (e.KeyCode == Keys.Up)
            {
                ship.Up();
                // log($"Текущие коррдинаты корабля ({ship.Pos.X}, {ship.Pos.Y})");
            }
            if (e.KeyCode == Keys.Down)
            {
                ship.Down();
                // log($"Текущие коррдинаты корабля ({ship.Pos.X}, {ship.Pos.Y})");
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Load()
        {
            stars = new Star[300];
            asteroids = new Asteroid[10];
            aids = new Asteroid[1];
            planets = new Planet[3];
            
            // создаем звезды
            for (int i = 0; i < stars.Length; i++)
            {
                int x = rand.Next(0, Game.Width);
                int y = rand.Next(0, Game.Height);
                int size = rand.Next(10, 31); // размеры звезд

                Image image = Image.FromFile(@"Pictures\star.png");
                stars[i] = new Star
                    (
                        new Point(x, y),
                        new Point(-1, 0),
                        new Size(size, size),
                        image
                    );
            }
            // создаем астеройды
            for (int i = 0; i < asteroids.Length; i++)
            {
                int x = Game.Width;
                int y = rand.Next(0, Game.Height);
                Image image = Image.FromFile(@"Pictures\asteroid.png");
                asteroids[i] = new Asteroid
                    (
                        new Point(x, y),
                        new Point(rand.Next(-10, 0), rand.Next(-1, 2)),
                        new Size(40, 40),
                        image
                    );
            }
            // создаем аптечки
            for (int i = 0; i < aids.Length; i++)
            {
                int x = Game.Width;
                int y = rand.Next(0, Game.Height);
                Image image = Image.FromFile(@"Pictures\aid.png");
                aids[i] = new Asteroid
                    (
                        new Point(x, y),
                        new Point(rand.Next(-10, 0), rand.Next(-1, 2)),
                        new Size(40, 40),
                        image
                    );
            }
            // создаем планеты
            for (int i = 0; i < planets.Length; i++)
            {
                int x = rand.Next(0, Game.Width);
                int y = rand.Next(0, Game.Height);
                Image image = Image.FromFile(@"Pictures\planet.png");
                planets[i] = new Planet
                    (
                        new Point(x, y),
                        new Point(rand.Next(-1, 2), rand.Next(-1, 2)),
                        new Size(50, 50),
                        image
                    );
            }

        }

        public static void Draw()
        {
            buffer.Graphics.Clear(Color.Black);

            foreach (Star obj in stars)
            {
                obj.Draw();
            }
            foreach (Asteroid obj in asteroids)
            {
                obj?.Draw();
            }
            foreach(Asteroid obj in aids)
            {
                obj?.Draw();
            }
            foreach (Planet obj in planets)
            {
                obj.Draw();
            }

            bullet?.Draw();

            ship?.Draw();

            if (ship != null)
            {
                buffer.Graphics.DrawString("Energy:" + ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
                buffer.Graphics.DrawString("Counter:" + ship.Count, SystemFonts.DefaultFont, Brushes.White, 0, 20);
            }

            buffer.Render();
        }

        public static void Finish()
        {
            timer.Stop();
            buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            buffer.Render();
        }

        public static void Update()
        {
            foreach (Star obj in stars)
            {
                obj.Update();
            }

            if (bullet != null)
            {
                bullet.Update();
                // log($"Текущие коррдинаты ракеты ({bullet.Pos.X}, {bullet.Pos.Y})"); 
                if (bullet.Pos.X >= Width)
                {
                    // логгируем холостой выстрел
                    log($"Ракета ({bullet.Pos.X}, {bullet.Pos.Y}) покинула поле!");
                    logFile($"Ракета ({bullet.Pos.X}, {bullet.Pos.Y}) покинула поле!");
                    bullet = null; // если ракета достигла конца игрового поля - уничтожаем ракету
                }
            }

            for (int i = 0; i < asteroids.Length; i++)
            {
                if (asteroids[i] == null) continue;
                asteroids[i].Update();
                if(bullet != null && bullet.Collision(asteroids[i]))
                {
                    // логгируем попадание ракеты в астеройд
                    log($"Ракета ({bullet.Pos.X}, {bullet.Pos.Y}) сбила астеройд ({asteroids[i].Pos.X}, {asteroids[i].Pos.Y})");
                    logFile($"Ракета ({bullet.Pos.X}, {bullet.Pos.Y}) сбила астеройд ({asteroids[i].Pos.X}, {asteroids[i].Pos.Y})");
                    System.Media.SystemSounds.Hand.Play();
                    ship?.Counter();
                    asteroids[i] = null;
                    bullet = null;
                    continue;
                }
                if (!ship.Collision(asteroids[i])) continue;
                var rnd = new Random();
                int n = rnd.Next(1, 10);
                ship?.EnergyLow(n);
                // логгиурем попадание асетройда в корабль
                log($"Астеройд ({asteroids[i].Pos.X}, {asteroids[i].Pos.Y}) попал в корабль ({ship.Pos.X}, {ship.Pos.Y}) -{n} !");
                logFile($"Астеройд ({asteroids[i].Pos.X}, {asteroids[i].Pos.Y}) попал в корабль ({ship.Pos.X}, {ship.Pos.Y}) -{n} !");
                System.Media.SystemSounds.Asterisk.Play();
                if (ship.Energy <= 0) ship?.Die();
            }

            for (int i = 0; i < aids.Length; i++)
            {
                if (aids[i] == null) continue;
                aids[i].Update();
                if (!ship.Collision(aids[i])) continue;
                var rnd = new Random();
                int n = rnd.Next(1, 10);
                ship?.EnergyUp(n);
                // логгиурем захват аптечки
                log($"Корабль ({ship.Pos.X}, {ship.Pos.Y}) подобрал аптечку ({aids[i].Pos.X}, {aids[i].Pos.Y}) +{n} !");
                logFile($"Корабль ({ship.Pos.X}, {ship.Pos.Y}) подобрал аптечку ({aids[i].Pos.X}, {aids[i].Pos.Y}) +{n} !");
                System.Media.SystemSounds.Asterisk.Play();
            }

            foreach (Planet obj in planets)
            {
                obj.Update();
            }
        }
    }
}
