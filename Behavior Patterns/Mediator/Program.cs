using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ObjectiveC;

namespace Mediator
{
    #region code from refactoring.guru
    //// The Mediator interface declares a method used by components to notify the
    //// mediator about various events. The Mediator may react to these events and
    //// pass the execution to other components.
    //public interface IMediator
    //{
    //    void Notify(object sender, string ev);
    //}

    //// Concrete Mediators implement cooperative behavior by coordinating several
    //// components.
    //class ConcreteMediator : IMediator
    //{
    //    private Component1 _component1;

    //    private Component2 _component2;

    //    public ConcreteMediator(Component1 component1, Component2 component2)
    //    {
    //        this._component1 = component1;
    //        this._component1.SetMediator(this);
    //        this._component2 = component2;
    //        this._component2.SetMediator(this);
    //    }

    //    public void Notify(object sender, string ev)
    //    {
    //        if (ev == "A")
    //        {
    //            Console.WriteLine("Mediator reacts on A and triggers following operations:");
    //            this._component2.DoC();
    //        }
    //        if (ev == "D")
    //        {
    //            Console.WriteLine("Mediator reacts on D and triggers following operations:");
    //            this._component1.DoB();
    //            this._component2.DoC();
    //        }
    //    }
    //}

    //// The Base Component provides the basic functionality of storing a
    //// mediator's instance inside component objects.
    //class BaseComponent
    //{
    //    protected IMediator _mediator;

    //    public BaseComponent(IMediator mediator = null)
    //    {
    //        this._mediator = mediator;
    //    }

    //    public void SetMediator(IMediator mediator)
    //    {
    //        this._mediator = mediator;
    //    }
    //}

    //// Concrete Components implement various functionality. They don't depend on
    //// other components. They also don't depend on any concrete mediator
    //// classes.
    //class Component1 : BaseComponent
    //{
    //    public void DoA()
    //    {
    //        Console.WriteLine("Component 1 does A.");

    //        this._mediator.Notify(this, "A");
    //    }

    //    public void DoB()
    //    {
    //        Console.WriteLine("Component 1 does B.");

    //        this._mediator.Notify(this, "B");
    //    }
    //}

    //class Component2 : BaseComponent
    //{
    //    public void DoC()
    //    {
    //        Console.WriteLine("Component 2 does C.");

    //        this._mediator.Notify(this, "C");
    //    }

    //    public void DoD()
    //    {
    //        Console.WriteLine("Component 2 does D.");

    //        this._mediator.Notify(this, "D");
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        // The client code.
    //        Component1 component1 = new Component1();
    //        Component2 component2 = new Component2();
    //        new ConcreteMediator(component1, component2);

    //        Console.WriteLine("Client triggers operation A.");
    //        component1.DoA();

    //        Console.WriteLine();

    //        Console.WriteLine("Client triggers operation D.");
    //        component2.DoD();
    //    }
    //}
    #endregion

    #region Code from ChatGPT
    //abstract class Mediator
    //{
    //    public abstract void Send(string message, Colleague colleague);
    //}

    //class ConcreteMediator : Mediator
    //{
    //    private ConcreteColleagueA colleagueA;
    //    private ConcreteColleagueB colleagueB;

    //    public ConcreteColleagueA ColleagueA
    //    {
    //        set { colleagueA = value; }
    //    }

    //    public ConcreteColleagueB ColleagueB
    //    {
    //        set { colleagueB = value; }
    //    }

    //    public override void Send(string message, Colleague colleague)
    //    {
    //        if (colleague == colleagueA)
    //        {
    //            colleagueB.Receive(message);
    //        }
    //        else
    //        {
    //            colleagueA.Receive(message);
    //        }
    //    }
    //}

    //abstract class Colleague
    //{
    //    protected Mediator mediator;

    //    public Colleague(Mediator mediator)
    //    {
    //        this.mediator = mediator;
    //    }

    //    public abstract void Send(string message);
    //    public abstract void Receive(string message);
    //}

    //class ConcreteColleagueA : Colleague
    //{
    //    public ConcreteColleagueA(Mediator mediator) : base(mediator) { }

    //    public override void Send(string message)
    //    {
    //        mediator.Send(message, this);
    //    }

    //    public override void Receive(string message)
    //    {
    //        Console.WriteLine("ConcreteColleagueA received: " + message);
    //    }
    //}

    //class ConcreteColleagueB : Colleague
    //{
    //    public ConcreteColleagueB(Mediator mediator) : base(mediator) { }

    //    public override void Send(string message)
    //    {
    //        mediator.Send(message, this);
    //    }

    //    public override void Receive(string message)
    //    {
    //        Console.WriteLine("ConcreteColleagueB received: " + message);
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        ConcreteMediator mediator = new ConcreteMediator();
    //        ConcreteColleagueA colleagueA = new ConcreteColleagueA(mediator);
    //        ConcreteColleagueB colleagueB = new ConcreteColleagueB(mediator);

    //        mediator.ColleagueA = colleagueA;
    //        mediator.ColleagueB = colleagueB;

    //        colleagueA.Send("Hello, how are you?");
    //        colleagueB.Send("I'm fine, thank you. How about you?");
    //    }
    //}
    #endregion

    #region Chatroom application

    /// <summary>
    /// Mediator Interface
    /// </summary>
    public interface IChatroom
    {
        void Send(string sender, string message);
        void SendFromTo(string from, string to, string message);
        void SendGroup<T>(string from, string message) where T : TeamMember;
    }

    /// <summary>
    /// Mediator Concrete
    /// </summary>
    public class ChatRoom : IChatroom
    {
        private readonly Dictionary<string, TeamMember> listTeamMember = new();
        public void Register(TeamMember teamMember)
        {
            teamMember.SetTeamMember(this);
            if (!listTeamMember.ContainsKey(teamMember.Name))
            {
                listTeamMember.Add(teamMember.Name, teamMember);
            }
        }

        public void Send(string sender, string message)
        {
            foreach (var teamMember in listTeamMember.Values)
            {
                teamMember.Receive(sender,message);
            }
        }

        public void SendFromTo(string from, string to, string message)
        {
            var receiver = listTeamMember[to];
            receiver.Receive(from,message);
        }

        public void SendGroup<T>(string from, string message) where T : TeamMember
        {
            foreach (var teamMember in listTeamMember.Values.OfType<T>())
            {
                teamMember.Receive(from, message);
            }
        }
    }

  
    /// <summary>
    /// Base Component
    /// </summary>
    public abstract class TeamMember 
    {
        private IChatroom? _chatRoom;
        public string Name;
        public TeamMember(string name)
        {
            Name = name;
        }

        public void SetTeamMember(IChatroom member)
        {
            _chatRoom = member;
        }
        public void Send(string message)
        {
            _chatRoom?.Send(Name, message);
        }

        public void SendFromTo(string to, string message)
        {
            _chatRoom?.SendFromTo(Name, to, message);
        }

        public void SendGroup<T>(string message) where T : TeamMember
        {
            _chatRoom?.SendGroup<T>(Name, message);
        }

        public virtual void Receive(string from, string message)
        {
            Console.WriteLine($"Message from {from} to {Name}: {message}");
        }
    }

    /// <summary>
    /// Concrete Component
    /// </summary>
    public class AccountManager : TeamMember
    {
        public AccountManager(string name) : base(name)
        {
        }

        public override void Receive(string from, string message)
        {
            Console.WriteLine($"{nameof(AccountManager)} {Name} received:");
            base.Receive(from, message);
        }
    }

    /// <summary>
    /// Concrete Component
    /// </summary>
    public class Lawyer : TeamMember
    {
        public Lawyer(string name) : base(name)
        {
        }

        public override void Receive(string from, string message)
        {
            Console.WriteLine($"{nameof(Lawyer)} {Name} received:");
            base.Receive(from, message);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Lawyer lawyer = new Lawyer("hoang");
            Lawyer lawyer2 = new Lawyer("trang");
            AccountManager accountManager = new AccountManager("huy");
            AccountManager accountManager2 = new AccountManager("huong");

            ChatRoom chatRoom = new ChatRoom();
            chatRoom.Register(lawyer);
            chatRoom.Register(lawyer2);
            chatRoom.Register(accountManager);
            chatRoom.Register(accountManager2);

            accountManager.Send("Hello I'm account Manager. Nice to meet you!");
            lawyer.Send("Me too. I'm a lawyer. Hi everybody.");

            lawyer.SendFromTo("huy", "Lam bao cao di.");
            lawyer.SendGroup<Lawyer>("Anh em oi di an com di");
            lawyer.SendGroup<AccountManager>("2h chieu hop giao ban noi bo giua 2 phong");


        }
    }
    #endregion
}