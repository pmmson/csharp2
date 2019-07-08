using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class HourlySalary : BaseEmployee
    {
        public HourlySalary(string name, double salary)
        {
            Name = name;
            Salary = salary / (20.8 * 8);
        }
        public override double AverageMonthSalary()
        {
            return 20.8 * 8 * Salary;
        }
        public override string ToString()
        {
            return $"worker: {Name}, salary(H): {AverageMonthSalary()}";
        }
    }
}
