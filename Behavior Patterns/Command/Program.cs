using System.Linq;

namespace Command
{
    #region 1. sample code from refactoring guru site
    //public interface ICommand
    //{
    //    void Execute();
    //}

    //public class SimpleCommand : ICommand
    //{
    //    private readonly string _payload = string.Empty;

    //    public SimpleCommand(string payload)
    //    {
    //        _payload = payload;
    //    }
    //    public void Execute() 
    //    {
    //        Console.WriteLine($"Action with Simple Command: {_payload}");   
    //    }
    //}

    //public class ComplexCommand :ICommand
    //{
    //    private readonly Receiver _receiver;
    //    private readonly string _a;
    //    private readonly string _b;

    //    public ComplexCommand(Receiver receiver, string a, string b)
    //    {
    //        _receiver = receiver;
    //        _a = a;
    //        _b = b;
    //    }

    //    public void Execute()
    //    {
    //        Console.WriteLine("Execute some commands");
    //        Console.WriteLine($"Executing {_a}.....");
    //        Console.WriteLine($"Executing another command: {_b}.....");
    //    }
    //}

    //public class Receiver
    //{
    //    public void DoSomething(string payload1)
    //    {
    //        Console.WriteLine($"Do {payload1} in DoSomething method");
    //    }

    //    public void DoSomethingOther(string payload2)
    //    {
    //        Console.WriteLine($"Do {payload2} in DoSomething method");
    //    }
    //}

    //public class Invoker
    //{
    //    private ICommand? _start;
    //    private ICommand? _finish;

    //    public void OnStart(ICommand start)
    //    {
    //        _start = start;
    //    }

    //    public void OnFinish(ICommand finish)
    //    {
    //        _finish = finish;
    //    }

    //    public void DoSomeImportantJob()
    //    {
    //        Console.WriteLine("Do you want to do some command before excute ?");
    //        if(_start is ICommand)
    //        {
    //            _start.Execute();
    //        }

    //        Console.WriteLine("Do something job......");
    //        Console.WriteLine("Do you want to do some command after finish job?");

    //        if( _finish is ICommand)
    //        {
    //            _finish.Execute();
    //        }
    //    }
    //}

    //public class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Invoker invoker = new Invoker();
    //        invoker.OnStart(new SimpleCommand("Say Hi!"));
    //        invoker.OnFinish(new ComplexCommand(new Receiver(), "Send Email", "Run to Send Report"));

    //        invoker.DoSomeImportantJob();
    //    }

    //}
    #endregion

    #region 2. Code from Pluralsight 
    public class Employee
    {
        public int Id{ get; set; }
        public string Name { get; set; }

        public Employee(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Manager : Employee
    {
        public List<Employee> Employees = new List<Employee>();
        public Manager(int id, string name) : base(id, name)
        {
        }
    }

    /// <summary>
    /// Receiver Interface
    /// </summary>
    public interface IEmployeeManagerRepository
    {
        void AddEmployee(int idManager,  Employee employee);
        void RemoveEmployee(int idManager, Employee employee);
        bool HasEmployee(int idManager, int idEmployee);
        void WriteDataStore();
    }

    /// <summary>
    /// Receiver (Implementation)
    /// </summary>
    public class EmployeeManagerRepository : IEmployeeManagerRepository
    {
        public List<Manager> listEms = new List<Manager>()
        {
            new Manager(1, "hoang"),
            new Manager(2, "trang")
        };
        public void AddEmployee(int idManager, Employee employee)
        {
            listEms.First(x => x.Id == idManager).Employees.Add(employee);
        }

        public bool HasEmployee(int idManager, int idEmployee)
        {
           return listEms.First(x => x.Id == idManager).Employees.Any(x => x.Id == idEmployee);
        }

        public void RemoveEmployee(int idManager, Employee employee)
        {
            listEms.First(x => x.Id == idManager).Employees.Remove(employee);
        }

        public void WriteDataStore()
        {
           foreach (Manager manager in listEms)
            {
                Console.WriteLine($"Manager: {manager.Id} , Name: {manager.Name}");
                if (manager.Employees.Any())
                {
                    foreach(Employee employee in manager.Employees)
                    {
                        Console.WriteLine($"Information of employee {employee.Id}, Name: {employee.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("No Employee in manager.......");
                }
            }
        }
    }

    /// <summary>
    /// Command
    /// </summary>
    public interface ICommand
    {
        void Execute();
        bool CanExecute(out string message);
        void Undo();
    }
    
    /// <summary>
    /// Concreate Command
    /// </summary>
    public class AddEmployeeToManagerList : ICommand
    {
        private readonly IEmployeeManagerRepository _iemployRepository;
        private readonly int _idManager;
        private readonly Employee _employee;

        public AddEmployeeToManagerList(IEmployeeManagerRepository iemployRepository, int idManager, Employee? employee)
        {
            _iemployRepository = iemployRepository;
            _idManager = idManager;
            _employee = employee;
        }

        public bool CanExecute(out string message)
        {
            Console.WriteLine("Check to CanExecute in AddEmployeeToManagerList");
            if (_employee is null)
            {
                message = "Employee is not found";
                return false;
            }

            if (_iemployRepository.HasEmployee(_idManager, _employee.Id))
            {
                message = "Employee shouldn't be on the manager's list already so It  can't be execute........";
                return false;
            }

            message = string.Empty;
            return true;
        }

        public void Execute()
        {
            Console.WriteLine("Executing in AddEmployeeToManagerList.........");
            _iemployRepository.AddEmployee(_idManager, _employee);
            Console.WriteLine($"Added Employee");
        }

        public void Undo()
        {
            if (_employee is null)
            {
                Console.WriteLine("Employee is not found");
                return;
            }
            _iemployRepository.RemoveEmployee(_idManager, _employee);
        }
    }

    /// <summary>
    /// Invoker
    /// </summary>
    public class CommandManger
    {
        private readonly Stack<ICommand> _commandHistory = new Stack<ICommand>();
        public void Invoke(ICommand command)
        {
            string message;
            if (command.CanExecute(out message))
            {
                command.Execute();
                _commandHistory.Push(command);
            }
            else
            {
                Console.WriteLine($"Can't not excute with message {message}");
            }
        }

        public void Undo()
        {
            if (_commandHistory.Any()) 
            {
                _commandHistory.Pop()?.Undo();
            }
        }

        public void UndoAll()
        {
            while (_commandHistory.Any())
            {
                _commandHistory.Pop()?.Undo();
            }
        }
    }

    /// <summary>
    /// Client
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            CommandManger manager = new CommandManger();
            IEmployeeManagerRepository repository = new EmployeeManagerRepository();
            Employee employee1 = new Employee(1, "Huy Can");
            Employee employee2 = new Employee(1, "Huy Can Loi");
            manager.Invoke(new AddEmployeeToManagerList(repository, 2, employee1));
            repository.WriteDataStore();
            Console.WriteLine("--------------------------");
            manager.Invoke(new AddEmployeeToManagerList(repository, 1, employee2));
            repository.WriteDataStore();
            Console.WriteLine("--------------------------");

            //manager.Undo();
            //repository.WriteDataStore();
            //Console.WriteLine("--------------------------");
            //manager.Undo();
            //repository.WriteDataStore();

            Console.WriteLine("--------------------------");
            manager.UndoAll();
            repository.WriteDataStore();
        }
    }
   
    #endregion
}