namespace LazyLoad.LazyInit
{
    public class OrderGood
    {
        private Customer _customer;
        public Customer Customer
        {
            get
            {
                if (_customer == null)
                {
                    _customer = new Customer();
                }
                return _customer;
            }
        }

        public string PrintLabel()
        {

            return Customer.CompanyName + "\n" + Customer.Address; // ok to access Customer
        }
    }
}
