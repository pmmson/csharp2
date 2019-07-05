using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class FixSalary : BaseEmployee
    {
        public FixSalary(string name, double salary)
        {
            Name = name;
            Salary = salary;
        }
        public override double AverageMonthSalary()
        {
            return Salary;
        }
        public override string ToString()
        {
            return $"worker: {Name}, salary(F): {AverageMonthSalary()}";
        }
    }
}
