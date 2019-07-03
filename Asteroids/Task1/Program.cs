/*
 * Павленко Михаил
 * 
 * 1.Построить  три  класса  (базовый  и  2  потомка),  описывающих  работников  с  почасовой  оплатой al (один  из  потомков)  и  фиксированной оплатой (второй потомок):
 * a.	Описать в базовом классе абстрактный метод для расчета среднемесячной заработной платы. 
 *      Для «повременщиков» формула для расчета такова: «среднемесячная заработная плата = 20.8 * 8 * почасовая ставка»; для  работников  с  фиксированной  
 *      оплатой: «среднемесячная заработная плата = фиксированная месячная оплата»;
 * b.	Создать на базе абстрактного класса массив сотрудников и заполнить его;
 * c.	* Реализовать интерфейсы для возможности сортировки массива, используя Array.Sort();
 * d.	* Создать класс, содержащий массив сотрудников, и реализовать возможность вывода данных с использованием foreach.
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            BaseEmployee[] workers = new BaseEmployee[15];
            for(int i = 0; i < workers.Length / 3; i++)
            {
                workers[i] = new FixSalary($"Name_{i * 10}", rand.Next(1000, 5000));
            }

            for (int i = workers.Length / 3; i < workers.Length; i++)
            {
                workers[i] = new HourlySalary($"Name_{i * 10}", rand.Next(1000, 5000));
            }

            Array.Sort(workers);

            foreach(var worker in workers)
            {
                Console.WriteLine(worker.ToString());
            }

            Console.ReadKey();
        }
    }
}
