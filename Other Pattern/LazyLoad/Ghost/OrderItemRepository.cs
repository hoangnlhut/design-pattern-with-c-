using LazyLoad.ValueHolder;
using System.Diagnostics;

namespace LazyLoad.Ghost
{
    public class OrderItemRepository
    {
       
        public IEnumerable<OrderItem> ListForOrder(int orderId)
        {
            Debug.Print("Fetching order items from database");
            var items = new List<OrderItem>()
            {
                new OrderItem(),
                new OrderItem(),
                new OrderItem()
            };
            return items;
        }
    }
}