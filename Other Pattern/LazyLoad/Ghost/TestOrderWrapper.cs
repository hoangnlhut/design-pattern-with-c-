using System.Collections;

namespace LazyLoad.Ghost
{
    public class TestOrderWrapper : Order
    {
        public bool WasLoadCalled = false;
        public int GetDataRowCount = 0;
        public TestOrderWrapper(int id) : base(id)
        {
        }

        public override void Load()
        {
            WasLoadCalled = true;
            Console.WriteLine($"WasLoadCalled in TestOrderWrapper class is: {WasLoadCalled}");
            base.Load();
        }

        protected override ArrayList GetDataRow()
        {
            GetDataRowCount++;
            Console.WriteLine($"GetDataRowCount in TestOrderWrapper class is: {GetDataRowCount}");
            return base.GetDataRow();
        }
    }
}
