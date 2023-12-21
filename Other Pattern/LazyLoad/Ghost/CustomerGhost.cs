using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoad.Ghost
{
    public class CustomerGhost : DomainObject
    {
        public CustomerGhost(int id) : base(id)
        {
        }

        public string CompanyName { get; internal set; }
        public string Address { get; internal set; }

        protected override void DoLoadLine(ArrayList dataRow)
        {
            throw new NotImplementedException();
        }

        protected override ArrayList GetDataRow()
        {
            throw new NotImplementedException();
        }
    }
}
