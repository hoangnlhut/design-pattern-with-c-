namespace NullObject
{
    public class NullAutoMobile : AutomobileBase
    {
        public override Guid Id =>  Guid.Empty;

        public override string Name => string.Empty;

        public override void Start()
        {
        }

        public override void Stop()
        {
        }
    }
}