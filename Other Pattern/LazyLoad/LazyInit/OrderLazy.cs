namespace LazyLoad.LazyInit
{
    public class OrderLazy
    {
        private Lazy<Customer> _customerInitializer;
        public OrderLazy()
        {
            // Lazy<T> defaults to thread safe - set isThreadSafe to false if not needed
            _customerInitializer = new Lazy<Customer>(() => new Customer());

        }

       public Customer Customer
        { get { 
                return _customerInitializer.Value;} }

        public string PrintLabel()
        {
            string result = Customer.CompanyName; // ok to access Customer
            return result + "\n" + _customerInitializer.Value.Address;
        }
    }
}
