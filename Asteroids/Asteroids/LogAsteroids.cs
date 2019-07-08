using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    public class LogAsteroids
    {
        static public void LogToConsole(string s)
        {
            Console.WriteLine(s);
        }

        static public void LogToFile(string s)
        {
            File.AppendAllText("asteroids.log", s+="\n");
        }
    }
}
