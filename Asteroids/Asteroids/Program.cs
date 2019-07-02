﻿using System;
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
            form.WindowState = FormWindowState.Normal;
            form.Show();
            Game.Init(form);
            Application.Run(form);
        }
    }
}
