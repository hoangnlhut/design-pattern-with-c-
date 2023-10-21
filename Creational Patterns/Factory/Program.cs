using System;
using System.ComponentModel;
using System.Reflection;

namespace Factory
{

    #region example by Hoang
    //public class Program
    //{
    //    public interface IPizza
    //    {
    //        int GetPrice();
    //    }

    //    public class BeefPizza : IPizza
    //    {
    //        private int Price = 10;
    //        public int GetPrice()
    //        {
    //            return Price;   
    //        }
    //    }

    //    public class ChickenPizza : IPizza
    //    {
    //        private int Price = 9;
    //        public int GetPrice()
    //        {
    //            return Price;
    //        }
    //    }

    //    public class PorkPizza : IPizza
    //    {
    //        private int Price = 5;
    //        public int GetPrice()
    //        {
    //            return Price;
    //        }
    //    }

    //    public enum PizzaType
    //    {
    //        [Description("Beef")]
    //        Beef,
    //        [Description("Chicken")]
    //        Chicken,
    //        [Description("Pork")]
    //        Pork,
    //    }

    //    public class FactoryPizza
    //    {
    //        public IPizza? CreatePizza(PizzaType type)
    //        {
    //            IPizza? interfacePizza = null;
    //            switch(type)
    //            {
    //                case PizzaType.Beef:
    //                    interfacePizza = new BeefPizza();
    //                    break;
    //                case PizzaType.Chicken:
    //                    interfacePizza = new ChickenPizza();
    //                    break;
    //                case PizzaType.Pork:
    //                    interfacePizza = new PorkPizza();
    //                    break;
    //            } 

    //            return interfacePizza;
    //        }
    //    }

    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("Welcome to Factory Pattern");

    //        PizzaType type = PizzaType.Chicken;
    //        IPizza? concretePizza = null;

    //        //// normal approach
    //        //switch (type)
    //        //{
    //        //    case PizzaType.Beef:
    //        //        concretePizza = new BeefPizza();
    //        //        break;
    //        //    case PizzaType.Chicken:
    //        //        concretePizza = new ChickenPizza();
    //        //        break;
    //        //    case PizzaType.Pork:
    //        //        concretePizza = new PorkPizza();
    //        //        break;
    //        //}



    //        //Factory appoach
    //        concretePizza = new FactoryPizza().CreatePizza(type);

    //        //var typeName = type
    //        //.GetType()
    //        //.GetMember(type.ToString())
    //        //.FirstOrDefault()
    //        //?.GetCustomAttribute<DescriptionAttribute>()
    //        //?.Description;

    //        Console.WriteLine($"Price of {type.ToString()} is {concretePizza!.GetPrice()}");

    //    }
    //}
    #endregion

    #region example in plurasight course : https://app.pluralsight.com/course-player?clipId=52ada72e-abfa-491b-9242-dc8f6172e3f1
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Factory Method");

            var factories = new List<DiscountFactory>
            {
                new CodeDiscountFactory(Guid.NewGuid().ToString()),
                new CountryDiscountFactory("BE"),
                new CityDiscountFactory("200")
            };

            foreach (var fact in factories)
            {
                var discountService = fact.CreateDiscountService();
                Console.WriteLine($"Percentage {discountService.DiscountPercentage} comming from {discountService}");
            }

            Console.ReadLine();
        }
    }
    #endregion
}