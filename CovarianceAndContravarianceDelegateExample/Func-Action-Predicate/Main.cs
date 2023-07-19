using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovarianceAndContravarianceDelegateExample.Func_Action_Predicate
{
    public static class FuncActionPredicate
    {
        delegate TResult Func2<out TResult>();
        delegate TResult Func2<in T1, out TResult>(T1 arg);
        delegate TResult Func2<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
        delegate TResult Func2<in T1, in T2, in T3, out TResult>(T1 arg1, T2 arg2, T3 arg3);

        public static void Example()
        {
            MathClass mathClass = new MathClass();

            Func<int, int, int> calc = (a, b) => a + b;
            int result = calc(1, 2);
            Console.WriteLine($"Result: { result }");

            Func<float, float, int, float> calc2 = (arg1, arg2, arg3) => (arg1 + arg2) * arg3;
            float d = 2.3f, e = 4.56f;
            int f = 5;
            float result2 = calc2(d, e, f);
            Console.WriteLine($"Result 2: {result2}");

            Func<decimal, decimal, decimal> calculateTotalAnnualSalary = (annualSalary, bonusPercentage) => annualSalary + (annualSalary * (bonusPercentage / 100));
            Console.WriteLine($"Total annual Salary: {calculateTotalAnnualSalary(60000, 2)}");

            Action<int, string, string, decimal, char, bool> employeeInfo = (id, firstName, lastName, salary, sex, isManager) => Console.WriteLine($"ID: {id}{Environment.NewLine}First Name: {firstName}{Environment.NewLine}Last Name: { lastName}{Environment.NewLine}Salary: {salary}{Environment.NewLine}SEX: {sex}{Environment.NewLine}Manager: {isManager}");
            employeeInfo(1, "Jhon", "Doe", 60000m, 'm', true);

            List<Employee> employees = new List<Employee>()
            {
                new Employee { Id = 1, FirstName = "Jhon", LastName = "Doe", AnnualSalary = calculateTotalAnnualSalary(60000, 10), Gender = 'M', IsManager = true},
                new Employee { Id = 1, FirstName = "Peter", LastName = "PArker", AnnualSalary = calculateTotalAnnualSalary(20000, 5), Gender = 'M', IsManager = false },
                new Employee { Id = 1, FirstName = "Frank", LastName = "Castle", AnnualSalary =  calculateTotalAnnualSalary(3000, 60), Gender = 'M', IsManager = false },
                new Employee { Id = 1, FirstName = "Mary", LastName = "Jane", AnnualSalary =  calculateTotalAnnualSalary(70000, 15), Gender = 'F', IsManager = true }
            };

            var employesFiltered = employees.Where(e => e.IsManager).ToList();
            
            foreach(Employee employee in employesFiltered )
            {
                employeeInfo(employee.Id, employee.FirstName, employee.LastName, employee.AnnualSalary, employee.Gender, employee.IsManager);
            }

            Console.ReadKey();
        }

        static List<Employee> FilterEmployees(List<Employee> employees, Predicate<Employee> predicate)
        {
            List<Employee> employeesFiltered = new List<Employee>();

            foreach(Employee employee in employees)
            {
                if(predicate(employee))
                {
                    employeesFiltered.Add(employee);
                }
            }

            return employeesFiltered;
        }

    }

    public static class Extensions
    {
        public static List<Employee> FilterEmployees(this List<Employee> employees, Predicate<Employee> predicate)
        {
            List<Employee> employeesFiltered = new List<Employee>();

            foreach (Employee employee in employees)
            {
                if (predicate(employee))
                {
                    employeesFiltered.Add(employee);
                }
            }

            return employeesFiltered;
        }
    }
    public class MathClass
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public char Gender { get; set; }
        public bool IsManager { get; set; }
    }
}
