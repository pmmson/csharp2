using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            form.Width = 1200;
            form.Height = 700;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
            int result = Game.Init(form);
            if (result == 0) Application.Run(form);
            else if (result == -1)
            {
                Console.WriteLine("Недопустимый размер экрана для игры");
                form.Close();
            }

            
            Console.ReadKey();
        }
    }
}
