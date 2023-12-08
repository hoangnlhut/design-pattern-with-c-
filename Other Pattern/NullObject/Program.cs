namespace NullObject
{
    public partial class Program
    {
        static void Main(string[] args)
        {
           var autoRepository = new AutoRepository();

            AutomobileBase automobile = autoRepository.Find("bmw456");
            if (automobile == AutomobileBase.NULL ) {
                Console.WriteLine("Null value object");
                return;
            }

            automobile.Start();
            automobile.Stop();
        }
    }
}