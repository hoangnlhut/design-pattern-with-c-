using LazyLoad.Ghost;
using LazyLoad.LazyInit;
using LazyLoad.VirtualProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoad
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            #region test Lazy initialization
            // error Nullexception
            //var orderBad = new OrderBad();
            //orderBad.PrintLabel();

            //var orderGood = new OrderGood();
            //orderGood.PrintLabel();

            //var orderLazy = new OrderLazy();
            //orderLazy.PrintLabel();
            #endregion

            #region Test virtual proxy
            //int testOrderId = 123;
            //var order = new OrderFactory().CreateFromId(testOrderId);

            //order.PrintLabel();

            #endregion

            #region Test Ghost
            int orderId = 123;
            // test for load itself if only once on property access
            //var order = new TestOrderWrapper(orderId);
            //Console.WriteLine($"OrderId: {orderId} vs order.Id {order.Id}");
            //Console.WriteLine($"order.WasLoadCalled: {order.WasLoadCalled}");
            //Console.WriteLine($"order.GetDataRowCount: {order.GetDataRowCount}");

            ////should call Load and GetDataRow once
            //var shipMethod = order.ShipMethod;
            //Console.WriteLine($"order.WasLoadCalled: {order.WasLoadCalled}"); // true
            //Console.WriteLine($"order.GetDataRowCount: {order.GetDataRowCount}"); // equal 1

            ////should not increment GetDataRowCount
            //var customer = order.Customer;
            //Console.WriteLine($"order.WasLoadCalled: {order.WasLoadCalled}"); // true
            //Console.WriteLine($"order.GetDataRowCount: {order.GetDataRowCount}"); // equal 1
            // end

            // test for Loading Item in Single Call on Property Acess
            var orderNew = new Ghost.Order(orderId);

            int itemCount = orderNew.Items.Count(); // should call repository here
            Console.WriteLine($"item count: {itemCount} should be equal 3");

            //end 
            #endregion
        }
    }
}
