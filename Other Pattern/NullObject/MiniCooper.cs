namespace NullObject
{
    public class MiniCooper : AutomobileBase
    {
        public override Guid Id => new Guid("dfsfsdfsdfsdfsdfs534t4gtrg");

        public override string Name => "mini cooper";

        public override void Start()
        {
            Console.WriteLine("Mini cooper starts");
        }

        public override void Stop()
        {
            Console.WriteLine("Mini cooper stop");

        }
    }
}