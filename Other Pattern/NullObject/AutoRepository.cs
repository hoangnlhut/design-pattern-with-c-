namespace NullObject
{
    public class AutoRepository
    {
        public AutomobileBase Find(string carName)
        {
            if (carName.Contains("mini"))
                return new MiniCooper();

            if (carName.Contains("bmw456"))
                return new BMW456();

            return AutomobileBase.NULL;
        }
    }
}