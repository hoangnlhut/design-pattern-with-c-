namespace NullObject
{
    public abstract class AutomobileBase
    {
        public abstract Guid Id { get;}
        public abstract string Name { get; }
        public abstract void Start();
        public abstract void Stop();

        #region NULL
        static readonly NullAutoMobile nullAutoMobile = new NullAutoMobile();

        public static NullAutoMobile NULL => nullAutoMobile;
        
        #endregion
    }
}