
namespace LazyLoad.ValueHolder
{
    public class OrderItemLoader : IValueLoader<List<OrderItem>>
    {
        private readonly int _orderId;
        public OrderItemLoader(int orderId)
        {
            _orderId = orderId;
        }

        public List<OrderItem> Load()
        {
            Console.WriteLine("Fetch OrderItems from Database");
            return new List<OrderItem>();
        }
    }
}