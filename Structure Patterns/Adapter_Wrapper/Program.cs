
//using static Adapter_Wrapper_Object.ObjectAdapterImplementation; // using Adapter Object

// using class object
using static Adapter_Wrapper_Class.ClassAdapterImplementation;

namespace Adapter_Wrapper
{
    

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Adapter Object");

            ICityAdapter cityAdapter = new Adapter();
            var city = cityAdapter.GetCity();

            Console.WriteLine($"{city.FullName} - {city.Inhabitants}") ;
        }
    }
}