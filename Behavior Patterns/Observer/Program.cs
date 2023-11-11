namespace Observer
{
    #region Code from refactoring guru
    ///// <summary>
    ///// Interface of subcriber
    ///// </summary>
    //    public interface IObserver
    //    {
    //        // Receive update from subject
    //        void Update(ISubject subject);
    //    }

    ///// <summary>
    ///// Interface of Publisher
    ///// </summary>
    //    public interface ISubject
    //    {
    //        // Attach an observer to the subject.
    //        void Attach(IObserver observer);

    //        // Detach an observer from the subject.
    //        void Detach(IObserver observer);

    //        // Notify all observers about an event.
    //        void Notify();
    //    }

    ///// <summary>
    ///// Concreate Publisher
    ///// </summary>
    //    // The Subject owns some important state and notifies observers when the
    //    // state changes.
    //    public class Subject : ISubject
    //    {
    //        // For the sake of simplicity, the Subject's state, essential to all
    //        // subscribers, is stored in this variable.
    //        public int State { get; set; } = -0;

    //        // List of subscribers. In real life, the list of subscribers can be
    //        // stored more comprehensively (categorized by event type, etc.).
    //        private List<IObserver> _observers = new List<IObserver>();

    //        // The subscription management methods.
    //        public void Attach(IObserver observer)
    //        {
    //            Console.WriteLine("Subject: Attached an observer.");
    //            this._observers.Add(observer);
    //        }

    //        public void Detach(IObserver observer)
    //        {
    //            this._observers.Remove(observer);
    //            Console.WriteLine("Subject: Detached an observer.");
    //        }

    //        // Trigger an update in each subscriber.
    //        public void Notify()
    //        {
    //            Console.WriteLine("Subject: Notifying observers...");

    //            foreach (var observer in _observers)
    //            {
    //                observer.Update(this);
    //            }
    //        }

    //        // Usually, the subscription logic is only a fraction of what a Subject
    //        // can really do. Subjects commonly hold some important business logic,
    //        // that triggers a notification method whenever something important is
    //        // about to happen (or after it).
    //        public void SomeBusinessLogic()
    //        {
    //            Console.WriteLine("\nSubject: I'm doing something important.");
    //            this.State = new Random().Next(0, 10);

    //            Thread.Sleep(15);

    //            Console.WriteLine("Subject: My state has just changed to: " + this.State);
    //            this.Notify();
    //        }
    //    }

    //    // Concrete Observers react to the updates issued by the Subject they had
    //    // been attached to.
    //    class ConcreteObserverA : IObserver
    //    {
    //        public void Update(ISubject subject)
    //        {
    //            if ((subject as Subject).State < 3)
    //            {
    //                Console.WriteLine("ConcreteObserverA: Reacted to the event.");
    //            }
    //        }
    //    }

    //    class ConcreteObserverB : IObserver
    //    {
    //        public void Update(ISubject subject)
    //        {
    //            if ((subject as Subject).State == 0 || (subject as Subject).State >= 2)
    //            {
    //                Console.WriteLine("ConcreteObserverB: Reacted to the event.");
    //            }
    //        }
    //    }

    //    class Program
    //    {
    //        static void Main(string[] args)
    //        {
    //            // The client code.
    //            var subject = new Subject();
    //            var observerA = new ConcreteObserverA();
    //            subject.Attach(observerA);

    //            var observerB = new ConcreteObserverB();
    //            subject.Attach(observerB);

    //            subject.SomeBusinessLogic();
    //            subject.SomeBusinessLogic();

    //            subject.Detach(observerB);

    //            subject.SomeBusinessLogic();
    //        }
    //    }
    #endregion

    #region Tự lấy ví dụ về công ty báo cho khách hàng khi có sản phẩm mới theo trạng thái quy định 1 là điện thoại, 2 là máy tính bảng, 3 là ipod
    /// <summary>
    /// Interface subcriber
    /// </summary>
    public interface ICustomerSubcriber
    {
        void update(ICompanyPublisher iCompanyPublisher);
    }

    /// <summary>
    /// interface of publisher
    /// </summary>
    public interface ICompanyPublisher
    {
        void AddSubcriber(ICustomerSubcriber subcriber);
        void RemoveSubcriber(ICustomerSubcriber subcriber);
        void Notify();
    }

    /// <summary>
    /// Concreate Publisher - Apple
    /// </summary>
    public class AppleCompany : ICompanyPublisher
    {
        //1 là điện thoại, 2 là máy tính bảng, 3 là ipod
        public int State { get; set; } = 0;
        private List<ICustomerSubcriber> listCustomer = new List<ICustomerSubcriber>();

        public void AddSubcriber(ICustomerSubcriber subcriber)
        {
            Console.WriteLine($"Add new subcriber.....");
            listCustomer.Add(subcriber);
        }

        public void Notify()
        {
            Console.WriteLine("Notifying to all subcriber.....");
            foreach(var subcriber in listCustomer)
            {
                subcriber.update(this);
            }
        }

        public void RemoveSubcriber(ICustomerSubcriber subcriber)
        {
            Console.WriteLine($"Remove subcriber.....");
            listCustomer.Remove(subcriber);
        }

        public void LaunchingNewProduct()
        {
            Console.WriteLine("Apple is planning to launch new product");
            State = new Random().Next(1, 3);

            Console.WriteLine($"New state of product:{State}");
            Notify();
        }
    }

    /// <summary>
    /// Concreate Subcriber of Hoang
    /// </summary>
    /// 
    public class CustomerHoang : ICustomerSubcriber
    {
        public void update(ICompanyPublisher iCompanyPublisher)
        {
            if((iCompanyPublisher as AppleCompany).State == 1)
            {
                Console.WriteLine("Sắp có điện thoại iphone rồi Hoàng ơi");
            }
        }
    }

    public class CustomerTrang : ICustomerSubcriber
    {
        public void update(ICompanyPublisher iCompanyPublisher)
        {
            if ((iCompanyPublisher as AppleCompany).State > 1)
            {
                Console.WriteLine("Sắp có ipad / ipod rồi Trang ơi");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            AppleCompany apple = new();
            CustomerHoang customerHoang= new CustomerHoang();
            CustomerTrang trang= new CustomerTrang();

            apple.AddSubcriber(customerHoang);
            apple.AddSubcriber(trang);

            apple.LaunchingNewProduct();
            apple.LaunchingNewProduct();

            apple.RemoveSubcriber(customerHoang);

            apple.LaunchingNewProduct();
        }
    }
    #endregion

}