using System.Reflection.Metadata;

namespace ChainOfResponsibilities
{
    #region refactoring guru
    //// The Handler interface declares a method for building the chain of
    //// handlers. It also declares a method for executing a request.
    //public interface IHandler
    //{
    //    IHandler SetNext(IHandler handler);

    //    object Handle(object request);
    //}

    //// The default chaining behavior can be implemented inside a base handler
    //// class.
    //abstract class AbstractHandler : IHandler
    //{
    //    private IHandler _nextHandler;

    //    public IHandler SetNext(IHandler handler)
    //    {
    //        this._nextHandler = handler;

    //        // Returning a handler from here will let us link handlers in a
    //        // convenient way like this:
    //        // monkey.SetNext(squirrel).SetNext(dog);
    //        return handler;
    //    }

    //    public virtual object Handle(object request)
    //    {
    //        if (this._nextHandler != null)
    //        {
    //            return this._nextHandler.Handle(request);
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //}

    //class MonkeyHandler : AbstractHandler
    //{
    //    public override object Handle(object request)
    //    {
    //        if ((request as string) == "Banana")
    //        {
    //            return $"Monkey: I'll eat the {request.ToString()}.\n";
    //        }
    //        else
    //        {
    //            return base.Handle(request);
    //        }
    //    }
    //}

    //class SquirrelHandler : AbstractHandler
    //{
    //    public override object Handle(object request)
    //    {
    //        if (request.ToString() == "Nut")
    //        {
    //            return $"Squirrel: I'll eat the {request.ToString()}.\n";
    //        }
    //        else
    //        {
    //            return base.Handle(request);
    //        }
    //    }
    //}

    //class DogHandler : AbstractHandler
    //{
    //    public override object Handle(object request)
    //    {
    //        if (request.ToString() == "MeatBall")
    //        {
    //            return $"Dog: I'll eat the {request.ToString()}.\n";
    //        }
    //        else
    //        {
    //            return base.Handle(request);
    //        }
    //    }
    //}

    //class Client
    //{
    //    // The client code is usually suited to work with a single handler. In
    //    // most cases, it is not even aware that the handler is part of a chain.
    //    public static void ClientCode(AbstractHandler handler)
    //    {
    //        foreach (var food in new List<string> { "Nut", "Banana", "Cup of coffee" })
    //        {
    //            Console.WriteLine($"Client: Who wants a {food}?");

    //            var result = handler.Handle(food);

    //            if (result != null)
    //            {
    //                Console.Write($"   {result}");
    //            }
    //            else
    //            {
    //                Console.WriteLine($"   {food} was left untouched.");
    //            }
    //        }
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        // The other part of the client code constructs the actual chain.
    //        var monkey = new MonkeyHandler();
    //        var squirrel = new SquirrelHandler();
    //        var dog = new DogHandler();

    //        monkey.SetNext(squirrel).SetNext(dog);

    //        // The client should be able to send a request to any handler, not
    //        // just the first one in the chain.
    //        Console.WriteLine("Chain: Monkey > Squirrel > Dog\n");
    //        Client.ClientCode(monkey);
    //        Console.WriteLine();

    //        Console.WriteLine("Subchain: Squirrel > Dog\n");
    //        Client.ClientCode(squirrel);
    //    }
    //}
    #endregion

    #region example by hoang about filter request with filter : document title , day...., type, 
    public class DocumentRequest
    {
        public string? Title { get; set; }
        // 1: toán, 2, tiếng việt, 3 tiếng anh
        public int Type { get; set; }
        public DateTime PubishedDate { get; set; }
    }


    public interface IHandle
    {
        void Handle(DocumentRequest request);
        IHandle SetNext(IHandle handle);
    }

    public abstract class HandleBase : IHandle
    {
        private IHandle _next;
        public IHandle SetNext(IHandle handle)
        {
            return _next = handle;
        }

        public virtual void Handle(DocumentRequest request)
        {
            if (_next != null)
            {
                _next.Handle(request);
            }
            else
            {
                return;
            }
        }
    }

    public class DocumentTitleHandle : HandleBase
    {
        public override void Handle(DocumentRequest request)
        {
            if (request != null && request.Title == "Sach Van Hoc")
            {
                Console.WriteLine($"Book's title : {request.Title} is valid");
            }
            else
            {
                Console.WriteLine($"Book's title : {request.Title} is INVALID");
            }

            base.Handle(request);
        }
    }

    public class DocumentTypeHandle : HandleBase
    {
        public override void Handle(DocumentRequest request)
        {
            if (request != null && (request.Type == 1 || request.Type == 2))
            {
                Console.WriteLine($"Type : {request.Type} is valid");
            }
            else
            {
                Console.WriteLine($"Type : {request.Type} is INVALID");
            }

            base.Handle(request);
        }
    }

    public class DocumentPublishedDateHandle : HandleBase
    {
        public override void Handle(DocumentRequest request)
        {
            if (request != null && request.PubishedDate >= (new DateTime(1990, 1, 1)))
            {
                Console.WriteLine($"Pushlish date : {request.PubishedDate} is valid");
            }
            else
            {
                Console.WriteLine($"Pushlish date : {request.PubishedDate} is INVALID");
            }

            base.Handle(request);
        }
    }


    public class Client
    {
        public void HandleChain(HandleBase handle, DocumentRequest request)
        {
            try
            {
                handle.Handle(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DocumentRequest documentRequest = new DocumentRequest { Title = "Sach Van Hocaa", Type = 4, PubishedDate = new DateTime(1000, 1, 1) };

            DocumentTitleHandle documentTitleHandle = new DocumentTitleHandle();
            DocumentPublishedDateHandle documentPublishedDateHandle = new DocumentPublishedDateHandle();
            DocumentTypeHandle documentTypeHandle = new DocumentTypeHandle();
            documentTitleHandle.SetNext(documentPublishedDateHandle).SetNext(documentTypeHandle);

            Client client = new Client();
            client.HandleChain(documentTitleHandle, documentRequest);
        }
    }

    #endregion
}