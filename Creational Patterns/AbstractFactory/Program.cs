using System.Runtime.CompilerServices;
using static AbstractFactory.Program;

namespace AbstractFactory
{
    public  class Program
    {

        public interface IButton
        {
            void Paint();
        }

        public class WinButtion : IButton
        {
            public void Paint()
            {
                Console.WriteLine("Paint of Win Button");
            }
        }

        public class MacButton : IButton
        {
            public void Paint()
            {
                Console.WriteLine("Paint of Mac Button");
            }
        }

        public interface ICheckbox
        {
            void Paint();
        }

        public class WinCheckbox : ICheckbox
        {
            public void Paint()
            {
                Console.WriteLine("Paint of Win Checkbox");
            }
        }

        public class MacCheckbox : ICheckbox
        {
            public void Paint()
            {
                Console.WriteLine("Paint of Mac Checkbox");
            }
        }

        public interface IGuiFactory
        {
            IButton CreateButton();
            ICheckbox CreateCheckbox();
        }

        public class WinFactory : IGuiFactory
        {
            public IButton CreateButton()
            {
                return new WinButtion();
            }

            public ICheckbox CreateCheckbox()
            {
                return new WinCheckbox();
            }
        }

        public class MacFactory : IGuiFactory
        {
            public IButton CreateButton()
            {
                return new MacButton();
            }

            public ICheckbox CreateCheckbox()
            {
                return new MacCheckbox();
            }
        }

        public class Application
        {
            private IButton _button;
            private ICheckbox _checkbox;

            public Application(IGuiFactory factory)
            {
                _button = factory.CreateButton();
                _checkbox = factory.CreateCheckbox();
            }

            public void PaintUI()
            {
                _button.Paint();
                _checkbox.Paint();
            }
        }



        //MAINNNNNNNNNNNN FUNCTION
        public static void Main(string[] args)
        {
            Console.WriteLine("Chon 0 va 1 : neu 0 la winUI , 1 la MacUI: ");
            var input = Console.ReadLine().ToString();

            IGuiFactory mainFactory;

            switch(input)
            {
                case "0": mainFactory =  new WinFactory(); break;
                case "1": mainFactory = new MacFactory(); break;
                default: throw new Exception("Nhap sai gia tri");
            }

            Application app = new Application(mainFactory);
            app.PaintUI();
        }
    }
}