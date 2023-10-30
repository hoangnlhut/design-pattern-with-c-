namespace Singleton
{
    public class Program
    {
        #region Native Singleton
        //public sealed class Singleton
        //{
        //    private Singleton() { }

        //    private static Singleton _instance;

        //    public static Singleton GetInstance()
        //    {
        //        if(_instance == null) _instance = new Singleton();
        //        return _instance;
        //    }
        //}
        #endregion

        #region Thread-safe Singleton
        //public class Singleton
        //{
        //    private Singleton() { }
        //    private static Singleton _instance;

        //    private static readonly object _lock = new object();

        //    public static Singleton GetInstance(string value)
        //    {
        //        if (_instance == null)
        //        {
        //            lock (_lock)
        //            {
        //                if (_instance == null)
        //                {
        //                    _instance = new Singleton();
        //                    _instance.Value = value;
        //                }
        //            }
        //        }
        //        return _instance;
        //    }

        //    public string Value { get; set; }
        //}


        #endregion

        #region Using Lazy<T> for Thread-safe Singleton
        public class Singleton
        {
            protected Singleton() { }
            private static readonly Lazy<Singleton> _lazySingleton = new Lazy<Singleton>(() => new Singleton());

            public static Singleton GetInstance()
            { return _lazySingleton.Value; }
        }
        #endregion

        static void Main(string[] args)
        {
            #region Native Singleton
            // The client code.
            Singleton s1 = Singleton.GetInstance();
            Singleton s2 = Singleton.GetInstance();

            if (s1 == s2)
            {
                Console.WriteLine("Singleton works, both variables contain the same instance.");
            }
            else
            {
                Console.WriteLine("Singleton failed, variables contain different instances.");
            }
            #endregion


            #region Thread Safe Singleton
           // Console.WriteLine(
           //    "{0}\n{1}\n\n{2}\n",
           //    "If you see the same value, then singleton was reused (yay!)",
           //    "If you see different values, then 2 singletons were created (booo!!)",
           //    "RESULT:"
           //);

           // Thread process1 = new Thread(() =>
           // {
           //     TestSingleton("FOO");
           // });
           // Thread process2 = new Thread(() =>
           // {
           //     TestSingleton("BAR");
           // });

           // process1.Start();
           // process2.Start();

           // process1.Join();
           // process2.Join();
            

        }

        //private static void TestSingleton(string value)
        //{
        //    Singleton singleton = Singleton.GetInstance(value);
        //    Console.WriteLine(singleton.Value);
        //}
        #endregion
    }
}