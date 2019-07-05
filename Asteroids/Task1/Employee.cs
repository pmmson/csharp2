using System;
using System.Collections;

namespace Task1
{
    class Employee : IEnumerator, IEnumerable
    {
        BaseEmployee[] employees; // класс содержащий массив сотрудников

        public Employee(int n)
        {
            employees = new BaseEmployee[n];
            Random rand = new Random();
            for (int i = 0; i < employees.Length; i++)
            {
                employees[i] = new HourlySalary($"Name_{i + 100}", rand.Next(1000, 5000));
            }
        }

        int i = -1; // первоначальный индекс
        public object Current
        {
            get
            {
                return employees[i];
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (i == employees.Length - 1)
            {
                Reset();
                return false;
            }
            i++;
            return true;
        }

        public void Reset()
        {
            i = -1;
        }
    }
}
