using static Behavior_Strategy.Program;

namespace Behavior_Strategy
{
    public class Program
    {
        public interface ITransfortStrategy
        {
            void Move();
        }

        public class Car : ITransfortStrategy
        {
            public void Move()
            {
                Console.WriteLine("Moving by Car");
            }
        }

        public class Bicycle : ITransfortStrategy
        {
            public void Move()
            {
                Console.WriteLine("Moving by Bicycle");
            }
        }


        public class Plane : ITransfortStrategy
        {
            public void Move()
            {
                Console.WriteLine("Moving by Plane");
            }
        }


        public class Navigator
        {
            private ITransfortStrategy _transfort;
            public Navigator()
            {
            }

            public void SetNavigator(ITransfortStrategy transport)
            {
                _transfort = transport;
            }

            public void Moving()
            {
                _transfort.Move();
            }
        }


        static void Main(string[] args)
        {
            Navigator navigator = new Navigator();

            Console.WriteLine("Client choose Car transportation");
            navigator.SetNavigator(new Car());
            navigator.Moving();

            Console.WriteLine("Client choose Bicycle transportation");
            navigator.SetNavigator(new Bicycle());
            navigator.Moving();


            Console.WriteLine("Client choose Plane transportation");
            navigator.SetNavigator(new Plane());
            navigator.Moving();

        }
    }
}