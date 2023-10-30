using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter_Wrapper_Class
{
    public class ClassAdapterImplementation
    {
        // object adapter pattern
        public class CityFromExternalSystem
        {
            public string Name { get; private set; }
            public string NickName { get; private set; }
            public int Inhabitants { get; private set; }

            public CityFromExternalSystem(string name, string nickname, int inhabitants)
            {
                Name = name;
                NickName = nickname;
                Inhabitants = inhabitants;
            }
        }

        /// <summary>
        /// Adaptee
        /// </summary>
        public class ExternalSystem
        {
            public CityFromExternalSystem GetCity()
            {
                return new CityFromExternalSystem("hoang", "pooh", 100000);
            }
        }

        public class City
        {
            public string FullName { get; private set; }
            public long Inhabitants { get; private set; }

            public City(string fullName, long inhabitants)
            {
                FullName = fullName;
                Inhabitants = inhabitants;
            }
        }

        public interface ICityAdapter
        {
            City GetCity();
        }

        public class Adapter : ExternalSystem, ICityAdapter
        {
            public City GetCity()
            {
                var getCity = base.GetCity();
                return new City($"{getCity.Name} - {getCity.NickName}", getCity.Inhabitants);
            }
        }
    }
}
