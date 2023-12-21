
namespace LazyLoad.ValueHolder
{
    public class ValueHolder<T>
    {
        private T _value;
        private readonly IValueLoader<T> _loader;
        public ValueHolder(IValueLoader<T> _value)
        {
            _loader = _value;
        }

        public T Value
        {
            get
            {
                if (_value == null)
                {
                    _value = _loader.Load();
                }
                return _value;
            }
        }
    }
}