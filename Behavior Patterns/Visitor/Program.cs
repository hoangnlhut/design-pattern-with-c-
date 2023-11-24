using System.Xml.Linq;

namespace Visitor
{
    //#region code from refactoring guru
    //    // The Component interface declares an `accept` method that should take the
    //    // base visitor interface as an argument.
    //    public interface IComponent
    //    {
    //        void Accept(IVisitor visitor);
    //    }

    //    // Each Concrete Component must implement the `Accept` method in such a way
    //    // that it calls the visitor's method corresponding to the component's
    //    // class.
    //    public class ConcreteComponentA : IComponent
    //    {
    //        // Note that we're calling `VisitConcreteComponentA`, which matches the
    //        // current class name. This way we let the visitor know the class of the
    //        // component it works with.
    //        public void Accept(IVisitor visitor)
    //        {
    //            visitor.Visit(this);
    //        }

    //        // Concrete Components may have special methods that don't exist in
    //        // their base class or interface. The Visitor is still able to use these
    //        // methods since it's aware of the component's concrete class.
    //        public string ExclusiveMethodOfConcreteComponentA()
    //        {
    //            return "A";
    //        }
    //    }

    //    public class ConcreteComponentB : IComponent
    //    {
    //        // Same here: VisitConcreteComponentB => ConcreteComponentB
    //        public void Accept(IVisitor visitor)
    //        {
    //            visitor.Visit(this);
    //        }

    //        public string SpecialMethodOfConcreteComponentB()
    //        {
    //            return "B";
    //        }
    //    }

    //    // The Visitor Interface declares a set of visiting methods that correspond
    //    // to component classes. The signature of a visiting method allows the
    //    // visitor to identify the exact class of the component that it's dealing
    //    // with.
    //    public interface IVisitor
    //    {
    //        void Visit(ConcreteComponentA element);

    //        void Visit(ConcreteComponentB element);
    //    }

    //    // Concrete Visitors implement several versions of the same algorithm, which
    //    // can work with all concrete component classes.
    //    //
    //    // You can experience the biggest benefit of the Visitor pattern when using
    //    // it with a complex object structure, such as a Composite tree. In this
    //    // case, it might be helpful to store some intermediate state of the
    //    // algorithm while executing visitor's methods over various objects of the
    //    // structure.
    //    class ConcreteVisitor1 : IVisitor
    //    {
    //        public void Visit(ConcreteComponentA element)
    //        {
    //            Console.WriteLine(element.ExclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor1");
    //        }

    //        public void Visit(ConcreteComponentB element)
    //        {
    //            Console.WriteLine(element.SpecialMethodOfConcreteComponentB() + " + ConcreteVisitor1");
    //        }
    //    }

    //    class ConcreteVisitor2 : IVisitor
    //    {
    //        public void Visit(ConcreteComponentA element)
    //        {
    //            Console.WriteLine(element.ExclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor2");
    //        }

    //        public void Visit(ConcreteComponentB element)
    //        {
    //            Console.WriteLine(element.SpecialMethodOfConcreteComponentB() + " + ConcreteVisitor2");
    //        }
    //    }

    //    public class Client
    //    {
    //        // The client code can run visitor operations over any set of elements
    //        // without figuring out their concrete classes. The accept operation
    //        // directs a call to the appropriate operation in the visitor object.
    //        public static void ClientCode(List<IComponent> components, IVisitor visitor)
    //        {
    //            foreach (var component in components)
    //            {
    //                component.Accept(visitor);
    //            }
    //        }
    //    }

    //    class Program
    //    {
    //        static void Main(string[] args)
    //        {
    //            List<IComponent> components = new List<IComponent>
    //            {
    //                new ConcreteComponentA(),
    //                new ConcreteComponentB()
    //            };

    //            Console.WriteLine("The client code works with all visitors via the base Visitor interface:");
    //            var visitor1 = new ConcreteVisitor1();
    //            Client.ClientCode(components, visitor1);

    //            Console.WriteLine();

    //            Console.WriteLine("It allows the same client code to work with different types of visitors:");
    //            var visitor2 = new ConcreteVisitor2();
    //            Client.ClientCode(components, visitor2);
    //        }
    //    }
    //#endregion

    #region demo in pluralsight
    
    /// <summary>
    /// Interface element
    /// </summary>
    public interface IElement
    {
        void Accept(IVisitor visitor);
    }

    /// <summary>
    /// Concreate element Employee
    /// </summary>
    public class Employee : IElement
    {
        public decimal YearsEmployed { get; private set; }
        public decimal Discount { get; set; }
        public string Name { get; private set; }
        public Employee(string name, decimal yearsEmployed)
        {
            Name = name;
            YearsEmployed = yearsEmployed;
        }
        public void Accept(IVisitor visitor)
        {
            //visitor.VisitEmployee(this);
            visitor.Visit(this);
            Console.WriteLine($"Visited {nameof(Employee)} {Name}, discount given: {Discount}");
        }
    }

    /// <summary>
    /// Concreate element Customer
    /// </summary>
    public class Customer : IElement
    {
        public decimal AmountOrdered { get; private set; }
        public decimal Discount { get; set; }
        public string Name { get; private set; }

        public Customer(string name, decimal amountOrdered)
        {
            Name = name;
            AmountOrdered = amountOrdered;
        }

        public void Accept(IVisitor visitor)
        {
            //visitor.VisitCustomer(this);
            visitor.Visit(this);
            Console.WriteLine($"Visited {nameof(Customer)} {Name}, discount given: {Discount}");
        }
    }

    /// <summary>
    /// Interface Visitor
    /// </summary>
    public interface IVisitor
    {
        //void VisitCustomer(Customer customer);
        //void VisitEmployee(Employee employee);

        //alternative
        void Visit(IElement element);
    }

    /// <summary>
    /// Concreate Visitor of Discount
    /// </summary>
    public class DiscountVisitor : IVisitor
    {
        public decimal TotalDiscountGiven { get; set; }

        public void Visit(IElement element)
        {
            if (element is Customer)
            {
                VisitCustomer((Customer)element);
            }
            else if(element is Employee)
            {
                VisitEmployee((Employee)element);
            }
        }

        private void VisitCustomer(Customer customer)
        {
            var discount = customer.AmountOrdered / 10;
            customer.Discount = discount;
            TotalDiscountGiven += discount;
        }

        private void VisitEmployee(Employee employee)
        {
            var discount = employee.YearsEmployed < 10 ? 100 : 200;
            employee.Discount = discount;
            TotalDiscountGiven += discount;
        }
    }

    public class Container
    {
        public List<Employee> Employees { get; set; } = new();
        public List<Customer> Customers { get; set; } = new();
        public void Accept(IVisitor visitor)

        {
            foreach (Employee employee in Employees)
            {
                employee.Accept(visitor);
            }
            foreach (Customer customer in Customers)
            {
                customer.Accept(visitor);
            }
        }
    }

    public  class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------Visitor Pattern----------");

            //create container & add concrete elements
            var container = new Container();
            container.Employees = new List<Employee>()
            {
                new Employee("hoang", 15),
                new Employee("trang",20),
                new Employee("huy", 5)
            };
            container.Customers = new List<Customer>()
            {
                new Customer("chau", 200),
                new Customer("doan", 600),
                new Customer("phat", 900),
            };

            // create visitor
            var discountVisitor = new DiscountVisitor();

            // pass it through
            container.Accept(discountVisitor);

            // write out gathered amount
            Console.WriteLine($"Total Discount: {discountVisitor.TotalDiscountGiven}");
        }
    }
    #endregion

}