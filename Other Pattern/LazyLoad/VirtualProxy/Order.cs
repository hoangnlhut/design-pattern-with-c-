using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoad.VirtualProxy
{
    public class Order
    {
        public int Id { get; set; }
        public virtual Customer Customer { get; set; }
        public string PrintLabel()
        {
            return Customer.CompanyName + "\n" + Customer.Address;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


    }
}
