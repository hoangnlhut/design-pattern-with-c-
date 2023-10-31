namespace Facade
{
    internal class Program
    {
        /// <summary>
        /// OrderService
        /// </summary>
        /// <param name="args"></param>
        public class OrderService
        {
            public bool IsEnoughOrder(int customerId)
            {
                return customerId > 8 ? true : false;
            }
        }

        public class CustomerDiscountBaseService
        {
            public double CalculateDiscountBase(int customerId)
            {
                return customerId > 15 ? 20 : 10;
            }
        }

        public class Facade
        {
            private readonly OrderService _orderService;
            private readonly CustomerDiscountBaseService _customerDiscountBaseService;

            public Facade(OrderService orderService, CustomerDiscountBaseService customerDiscountBaseService)
            {
                _orderService = orderService;
                _customerDiscountBaseService = customerDiscountBaseService;
            }

            public double CalculateDiscount(int customerId) 
            {
                if(!_orderService.IsEnoughOrder(customerId))
                {
                    return 0;
                }
                
                return _customerDiscountBaseService.CalculateDiscountBase(customerId);
            }
        }

        static void Main(string[] args)
        {
            OrderService order = new OrderService();
            CustomerDiscountBaseService customerDis = new CustomerDiscountBaseService();

            Facade facade = new Facade(order, customerDis);
            Console.WriteLine($"Discount of Customer 1 is {facade.CalculateDiscount(1)}");
            Console.WriteLine($"Discount of Customer 10 is {facade.CalculateDiscount(10)}");
            Console.WriteLine($"Discount of Customer 18 is {facade.CalculateDiscount(18)}");


        }
    }
}