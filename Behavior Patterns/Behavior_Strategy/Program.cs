using static Behavior_Strategy.Program;

namespace Behavior_Strategy
{
    public class Program
    {
        #region Bài toán 1 : Vấn chuyển bằng nhiều phương thức (car, xe đạp, máy bay) qua một bộ navigator
        //public interface ITransfortStrategy
        //{
        //    void Move();
        //}

        //public class Car : ITransfortStrategy
        //{
        //    public void Move()
        //    {
        //        Console.WriteLine("Moving by Car");
        //    }
        //}

        //public class Bicycle : ITransfortStrategy
        //{
        //    public void Move()
        //    {
        //        Console.WriteLine("Moving by Bicycle");
        //    }
        //}


        //public class Plane : ITransfortStrategy
        //{
        //    public void Move()
        //    {
        //        Console.WriteLine("Moving by Plane");
        //    }
        //}


        //public class Navigator
        //{
        //    private ITransfortStrategy _transfort;

        //    public void SetNavigator(ITransfortStrategy transport)
        //    {
        //        _transfort = transport;
        //    }

        //    public void Moving()
        //    {
        //        _transfort.Move();
        //    }
        //}


        //static void Main(string[] args)
        //{
        //    Navigator navigator = new Navigator();

        //    Console.WriteLine("Client choose Car transportation");
        //    navigator.SetNavigator(new Car());
        //    navigator.Moving();

        //    Console.WriteLine("Client choose Bicycle transportation");
        //    navigator.SetNavigator(new Bicycle());
        //    navigator.Moving();


        //    Console.WriteLine("Client choose Plane transportation");
        //    navigator.SetNavigator(new Plane());
        //    navigator.Moving();

        //}
        #endregion

        #region: Bài toán 2: xuất đơn hàng bằng nhiều cách (XML, Json..) 
        public interface IExporeService
        {
            void Export();
        }

        public class JsonExport : IExporeService
        {
            public void Export()
            {
                Console.WriteLine("Export data to Json format!!!");
            }
        }

        public class XmlExport : IExporeService
        {
            public void Export()
            {
                Console.WriteLine("Export data to Xml format!!!");
            }
        }

        public class ExcelExport : IExporeService
        {
            public void Export()
            {
                Console.WriteLine("Export data to Excel format!!!");
            }
        }

        public class Orders
        {
            private IExporeService _exportService;
            public Orders(IExporeService exportService)
            {
                _exportService = exportService;
            }

            public void GetDataToWantedFormat()
            {
                Console.WriteLine("Process to get data from database....");
                _exportService.Export();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Process and Export orders data to XML");
            Orders order1 = new Orders(new XmlExport());
            order1.GetDataToWantedFormat();
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Process and Export orders data to JSON");
            Orders order2 = new Orders(new JsonExport());
            order2.GetDataToWantedFormat();
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Process and Export orders data to Excel file");
            Orders order3 = new Orders(new ExcelExport());
            order3.GetDataToWantedFormat();
            Console.WriteLine("-------------------------------------");
        }
        #endregion
    }
}