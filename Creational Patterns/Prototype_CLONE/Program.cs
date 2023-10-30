using System.Net.Sockets;

namespace Prototype_CLONE
{
    internal class Program
    {
        public abstract class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public DateTime Birth { get; set; }
            public Address Address { get; set; }

            public abstract Person Clone(bool isDeepCopy);
        }

        public class Address
        {
            public string Home;
            //public string AtWork { get; set; }

            public Address(string home)
            {
                Home = home;
            }
        }

        public class Employee : Person
        {
            public override Person Clone(bool isDeepCopy)
            {
                if (isDeepCopy)
                {
                    Employee employee = new Employee();
                    employee.Name = this.Name;
                    employee.Age = this.Age;
                    employee.Birth = this.Birth;
                    employee.Address = new Address(this.Address.Home);
                    return employee;
                }
                return (Employee)this.MemberwiseClone();
            }
        }

        //public class Manager : Person
        //{
        //    public override Person Clone(bool isDeepCopy)
        //    {
        //        if (isDeepCopy)
        //        {
        //            Manager manager = new Manager();
        //            manager.Age = this.Age;
        //            manager.Birth = this.Birth;
        //            manager.Address = new Address(this.Address.Home);
        //            return manager;
        //        }

        //        return (Manager)this.MemberwiseClone();
        //    }
        //}

        static void Main(string[] args)
        {
            Employee e1 = new Employee();
            e1.Name = "Hoang";
            e1.Age = 10;
            e1.Birth = DateTime.Now;
            e1.Address = new Address("41 tho nhuom");

            Employee e2 = (Employee)e1.Clone(false);
            Employee e3 = (Employee)e1.Clone(true);

            Console.WriteLine("Before Change");
            Console.WriteLine("Employee 1:");
            DisplayEmployee(e1);
            Console.WriteLine("Employee 2:");
            DisplayEmployee(e2);
            Console.WriteLine("Employee 3:");
            DisplayEmployee(e3);

            Console.WriteLine("After Change");
            e1.Name = "Trang";
            e1.Age = 20;
            e1.Address.Home = "Phu tho cam khe";
            Console.WriteLine("Employee 1:");
            DisplayEmployee(e1);
            Console.WriteLine("Employee 2:");
            DisplayEmployee(e2);
            Console.WriteLine("Employee 3:");
            DisplayEmployee(e3);

        }

        private static void DisplayEmployee(Employee e)
        {
            Console.WriteLine($"Name: {e.Name}, age: {e.Age}, Birthday: {e.Birth} and Address: {e.Address.Home}");
        }
    }

    
}