using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    abstract class BaseEmployee : IComparable<BaseEmployee>
    {
        public string Name { get; set; }
        public double Salary { get; set; }
        public abstract double AverageMonthSalary();

        public int CompareTo(BaseEmployee other)
        {
            bool z = AverageMonthSalary() > other.AverageMonthSalary();
            if (z) return 1;
            else return -1;
        }
    }
}
