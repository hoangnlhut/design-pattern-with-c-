using DevExpress.XtraEditors.Controls;
using LazyLoad.ValueHolder;
using System.Collections;

namespace LazyLoad.Ghost
{
    public class Order : DomainObject
    {
        public Order(int id) : base(id)
        {
        }

        private string _shipMethod;
        public string ShipMethod
        {
            get
            {
                Load();
                return _shipMethod;
            }
            set
            {
                Load();
                _shipMethod = value;
            }
        }

        private CustomerGhost _customer;
        public CustomerGhost Customer
        {
            get
            {
                Load();
                return _customer;
            }
            set
            {
                Load();
                _customer = value;
            }
        }

        private Lazy<List<OrderItem>> _items;
        public IEnumerable<OrderItem> Items
        {
            get
            {
                Load();
                return _items.Value.AsReadOnly();
            }
        }

        protected override void DoLoadLine(ArrayList dataRow)
        {
            ShipMethod = (string)dataRow[0];
            Customer = new CustomerGhost((int)dataRow[1]);
            _items = new Lazy<List<OrderItem>>(() => new OrderItemRepository().ListForOrder(Id).ToList());
        }

        protected override ArrayList GetDataRow()
        {
            var row = new ArrayList();
            row.Add("FEDEX");
            row.Add(123);
            return row;
        }
    }
}
