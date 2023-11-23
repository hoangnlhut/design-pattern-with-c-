namespace Memeto_Snapshot_
{
    #region code from refactoring guru
    // The Originator holds some important state that may change over time. It
    // also defines a method for saving the state inside a memento and another
    // method for restoring the state from it.
    //public class Originator
    //{
    //    // For the sake of simplicity, the originator's state is stored inside a
    //    // single variable.
    //    private string _state;

    //    public Originator(string state)
    //    {
    //        this._state = state;
    //        Console.WriteLine("Originator: My initial state is: " + state);
    //    }

    //    // The Originator's business logic may affect its internal state.
    //    // Therefore, the client should backup the state before launching
    //    // methods of the business logic via the save() method.
    //    public void DoSomething()
    //    {
    //        Console.WriteLine("Originator: I'm doing something important.");
    //        this._state = this.GenerateRandomString(30);
    //        Console.WriteLine($"Originator: and my state has changed to: {_state}");
    //    }

    //    private string GenerateRandomString(int length = 10)
    //    {
    //        string allowedSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    //        string result = string.Empty;

    //        while (length > 0)
    //        {
    //            result += allowedSymbols[new Random().Next(0, allowedSymbols.Length)];

    //            Thread.Sleep(12);

    //            length--;
    //        }

    //        return result;
    //    }

    //    // Saves the current state inside a memento.
    //    public IMemento Save()
    //    {
    //        return new ConcreteMemento(this._state);
    //    }

    //    // Restores the Originator's state from a memento object.
    //    public void Restore(IMemento memento)
    //    {
    //        if (!(memento is ConcreteMemento))
    //        {
    //            throw new Exception("Unknown memento class " + memento.ToString());
    //        }

    //        this._state = memento.GetState();
    //        Console.Write($"Originator: My state has changed to: {_state}");
    //    }
    //}

    //// The Memento interface provides a way to retrieve the memento's metadata,
    //// such as creation date or name. However, it doesn't expose the
    //// Originator's state.
    //public interface IMemento
    //{
    //    string GetName();

    //    string GetState();

    //    DateTime GetDate();
    //}

    //// The Concrete Memento contains the infrastructure for storing the
    //// Originator's state.
    //class ConcreteMemento : IMemento
    //{
    //    private string _state;

    //    private DateTime _date;

    //    public ConcreteMemento(string state)
    //    {
    //        this._state = state;
    //        this._date = DateTime.Now;
    //    }

    //    // The Originator uses this method when restoring its state.
    //    public string GetState()
    //    {
    //        return this._state;
    //    }

    //    // The rest of the methods are used by the Caretaker to display
    //    // metadata.
    //    public string GetName()
    //    {
    //        return $"{this._date} / {this._state}...";
    //    }

    //    public DateTime GetDate()
    //    {
    //        return this._date;
    //    }
    //}

    //// The Caretaker doesn't depend on the Concrete Memento class. Therefore, it
    //// doesn't have access to the originator's state, stored inside the memento.
    //// It works with all mementos via the base Memento interface.
    //class Caretaker
    //{
    //    private List<IMemento> _mementos = new List<IMemento>();

    //    private Originator _originator = null;

    //    public Caretaker(Originator originator)
    //    {
    //        this._originator = originator;
    //    }

    //    public void Backup()
    //    {
    //        Console.WriteLine("\nCaretaker: Saving Originator's state...");
    //        this._mementos.Add(this._originator.Save());
    //    }

    //    public void Undo()
    //    {
    //        if (this._mementos.Count == 0)
    //        {
    //            return;
    //        }

    //        var memento = this._mementos.Last();
    //        this._mementos.Remove(memento);

    //        Console.WriteLine("Caretaker: Restoring state to: " + memento.GetName());

    //        try
    //        {
    //            this._originator.Restore(memento);
    //        }
    //        catch (Exception)
    //        {
    //            this.Undo();
    //        }
    //    }

    //    public void ShowHistory()
    //    {
    //        Console.WriteLine("Caretaker: Here's the list of mementos:");

    //        foreach (var memento in this._mementos)
    //        {
    //            Console.WriteLine(memento.GetName());
    //        }
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        // Client code.
    //        Originator originator = new Originator("Super-duper-super-puper-super.");
    //        Caretaker caretaker = new Caretaker(originator);

    //        caretaker.Backup();
    //        originator.DoSomething();

    //        caretaker.Backup();
    //        originator.DoSomething();

    //        caretaker.Backup();
    //        originator.DoSomething();

    //        Console.WriteLine();
    //        caretaker.ShowHistory();

    //        Console.WriteLine("\nClient: Now, let's rollback!\n");
    //        caretaker.Undo();

    //        Console.WriteLine("\n\nClient: Once more!\n");
    //        caretaker.Undo();

    //        Console.WriteLine();
    //    }
    //}

    #endregion

    #region Command + Memento can be used together
    public class Employee
    {
        public int Id { get; set; }
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
        void AddEmployee(int idManager, Employee employee);
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
                    foreach (Employee employee in manager.Employees)
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
    /// Memento
    /// </summary>
    public class AddEmployeeToManagerListMemento
    {
        public int ManagerId { get; private set; }
        public Employee? Employee { get; private set; }
        public AddEmployeeToManagerListMemento(int managerId , Employee? employee)
        {
            ManagerId = managerId;
            Employee = employee;
        }
    }

    /// <summary>
    /// Concreate Command & Originator
    /// </summary>
    public class AddEmployeeToManagerList : ICommand
    {
        private readonly IEmployeeManagerRepository _iemployRepository;
        private int _idManager;
        private Employee _employee;

        public AddEmployeeToManagerList(IEmployeeManagerRepository iemployRepository, int idManager, Employee? employee)
        {
            _iemployRepository = iemployRepository;
            _idManager = idManager;
            _employee = employee;
        }

        public AddEmployeeToManagerListMemento CreateMemento()
        {
            return new AddEmployeeToManagerListMemento(_idManager, _employee);
        }

        public void RestoreMemento(AddEmployeeToManagerListMemento restore)
        {
            _idManager = restore.ManagerId;
            _employee = restore.Employee;
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
    /// Invoker & CareTaker
    /// </summary>
    public class CommandManger
    {
        private readonly Stack<AddEmployeeToManagerListMemento> _mementos = new Stack<AddEmployeeToManagerListMemento>();
        private AddEmployeeToManagerList? _command;
       
        public void Invoke(ICommand command)
        {
            string message;

            if(_command == null)
            {
                _command = (AddEmployeeToManagerList)command;
            }

            if (command.CanExecute(out message))
            {
                command.Execute();
                _mementos.Push(((AddEmployeeToManagerList)command).CreateMemento());
            }
            else
            {
                Console.WriteLine($"Can't not excute with message {message}");
            }
        }

        public void Undo()
        {
            if (_mementos.Any())
            {
                _command.RestoreMemento(_mementos.Pop());
                _command.Undo();
            }
        }

        public void UndoAll()
        {
            while (_mementos.Any())
            {
                _command.RestoreMemento(_mementos.Pop());
                _command.Undo();
            }
        }
        public void ShowHistory()
        {
            Console.WriteLine("Show history of memento");
            if(_mementos.Count > 0)
            {
                foreach (var item in _mementos)
                {
                    Console.WriteLine($"{item.ManagerId}: has employee :{item.Employee.Id} - {item.Employee.Name}");
                }
            }
            else
            {
                Console.WriteLine("There is no data in history of caretaker");
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
            manager.ShowHistory();
            Console.WriteLine("--------------------------");

            manager.Invoke(new AddEmployeeToManagerList(repository, 1, employee2));
            repository.WriteDataStore();
            manager.ShowHistory();
            Console.WriteLine("--------------------------");

            manager.Undo();
            repository.WriteDataStore();
            manager.ShowHistory();
            Console.WriteLine("--------------------------");
            manager.Undo();
            repository.WriteDataStore();
            manager.ShowHistory();

            //Console.WriteLine("--------------------------");
            //manager.UndoAll();
            //repository.WriteDataStore();
            //manager.ShowHistory();
        }
    }

    #endregion
}