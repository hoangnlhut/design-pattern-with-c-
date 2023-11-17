namespace State
{
    #region code from refactoring guru
        //// The Context defines the interface of interest to clients. It also
        //// maintains a reference to an instance of a State subclass, which
        //// represents the current state of the Context.
        //class Context
        //{
        //    // A reference to the current state of the Context.
        //    private State _state = null;

        //    public Context(State state)
        //    {
        //        this.TransitionTo(state);
        //    }

        //    // The Context allows changing the State object at runtime.
        //    public void TransitionTo(State state)
        //    {
        //        Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
        //        this._state = state;
        //        this._state.SetContext(this);
        //    }

        //    // The Context delegates part of its behavior to the current State
        //    // object.
        //    public void Request1()
        //    {
        //        this._state.Handle1();
        //    }

        //    public void Request2()
        //    {
        //        this._state.Handle2();
        //    }
        //}

        //// The base State class declares methods that all Concrete State should
        //// implement and also provides a backreference to the Context object,
        //// associated with the State. This backreference can be used by States to
        //// transition the Context to aher State.
        //abstract class State
        //{
        //    protected Context _context;

        //    public void SetContext(Context context)
        //    {
        //        this._context = context;
        //    }

        //    public abstract void Handle1();

        //    public abstract void Handle2();
        //}

        //// Concrete States implement various behaviors, associated with a state of
        //// the Context.
        //class ConcreteStateA : State
        //{
        //    public override void Handle1()
        //    {
        //        Console.WriteLine("ConcreteStateA handles request1.");
        //        Console.WriteLine("ConcreteStateA wants to change the state of the context.");
        //        this._context.TransitionTo(new ConcreteStateB());
        //    }

        //    public override void Handle2()
        //    {
        //        Console.WriteLine("ConcreteStateA handles request2.");
        //    }
        //}

        //class ConcreteStateB : State
        //{
        //    public override void Handle1()
        //    {
        //        Console.Write("ConcreteStateB handles request1.");
        //    }

        //    public override void Handle2()
        //    {
        //        Console.WriteLine("ConcreteStateB handles request2.");
        //        Console.WriteLine("ConcreteStateB wants to change the state of the context.");
        //        this._context.TransitionTo(new ConcreteStateA());
        //    }
        //}

        //class Program
        //{
        //    static void Main(string[] args)
        //    {
        //        // The client code.
        //        var context = new Context(new ConcreteStateA());
        //        context.Request1();
        //        context.Request2();
        //    }
        //}
    #endregion

    #region: state transition in buying products in e-commerce from Created --> Cancelled
    //                                                                |
    //                                                               Paid --> Deliverd -> Finished
    public class Context
    {
        public State _state = null;
        public Context(State state) 
        {
            SetState(state);
        }

        public void SetState(State state)
        {
            _state = state;
            _state.SetContext(this);
            Console.WriteLine($"Set new state {_state.GetType().Name}");
        }

        public void Cancel()
        {
            var res = _state.Cancel();
            res = string.IsNullOrEmpty(res) ? "Successful!" : "Error message: " + res;
            Console.WriteLine($"Cancelled the order: {res}");

        }

        public void Paid()
        {
            var res = _state.Paid();
            res = string.IsNullOrEmpty(res) ? "Successful!" : "Error message: " + res;
            Console.WriteLine($"Paid the order: {res}");
        }

        public void Deliverd()
        {
            var res = _state.Deliverd();
            res = string.IsNullOrEmpty(res) ? "Successful!" : "Error message: " + res;
            Console.WriteLine($"Deliverd the order: {res}");
        }

        public void Finish()
        {
            var res = _state.Finish();
            res = string.IsNullOrEmpty(res) ? "Successful!" : "Error message: " + res;
            Console.WriteLine($"Finished the order: {res}");
        }
    }

    public abstract class State
    {
        public Context _ctx;
        public void SetContext(Context context)
        {
            _ctx = context;
        }

        public abstract string Cancel();
        public abstract string Paid();  
        public abstract string Deliverd();
        public abstract string Finish();

    }

    public class CreatedState : State
    {
        public override string Cancel()
        {
            Console.WriteLine("The customer don't want to buy this product. He cancelled order");
            _ctx.SetState(new CanceledState());
            return string.Empty;
        }

        public override string Deliverd()
        {
            return "Can't Deliverd with Order State is Created";
        }

        public override string Finish()
        {
            return "Can't Finish with Order State is created";
        }

        public override string Paid()
        {
            Console.WriteLine("The customer paid the order");
            _ctx.SetState(new PaidState());
            return string.Empty;
        }
    }

    public class CanceledState : State
    {
        public override string Cancel()
        {
            return "Can't Cancel with  State is Canceled";
        }

        public override string Deliverd()
        {
            return "Can't Deliverd with  State is Canceled";
        }

        public override string Finish()
        {
            return "Can't Finish with  State is Canceled";
        }

        public override string Paid()
        {
            return "Can't Paid with  State is Canceled";
        }
    }

    public class PaidState : State
    {
        public override string Cancel()
        {
            return "Can't Paid with  State is Paid";
        }

        public override string Deliverd()
        {
            Console.WriteLine("Products were shipped to customer");
            _ctx.SetState(new DeliverdState());
            return string.Empty;
        }

        public override string Finish()
        {
            return "Can't Paid with  State is Paid";
        }

        public override string Paid()
        {
            return "Can't Paid with  State is Paid";
        }
    }

    public class DeliverdState : State
    {
        public override string Cancel()
        {
            return "Can't Cancel with  State is Deliverd";
        }

        public override string Deliverd()
        {
            return "Can't Deliverd with  State is Deliverd";
        }

        public override string Finish()
        {
            Console.WriteLine("Finish order");
            _ctx.SetState(new FinishedState());
            return string.Empty;
        }

        public override string Paid()
        {
            return "Can't Paid with  State is Deliverd";
        }
    }

    public class FinishedState : State
    {
        public override string Cancel()
        {
            return "Can't Cancel with  State is Finished";
        }

        public override string Deliverd()
        {
            return "Can't Deliverd with  State is Finished";
        }

        public override string Finish()
        {
            return "Can't Finish with  State is Finished";
        }

        public override string Paid()
        {
            return "Can't Paid with  State is Finished";
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            Context context = new Context(new CreatedState());
            context.Paid();
            context.Deliverd();
            context.Finish();

            Console.WriteLine("---------------------------------");
            Context context2 = new Context(new CreatedState());
            context2.Cancel();
            context2.Deliverd();
        }
    }
    #endregion

}