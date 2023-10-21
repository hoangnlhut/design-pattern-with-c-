using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    /// <summary>
    /// Product
    /// </summary>
    public abstract class DiscountService
    {
        public abstract int DiscountPercentage {  get; }

        public override string ToString()
        {
            return GetType().Name;
        }
    }

    /// <summary>
    /// Concreate Product
    /// </summary>
    public class CountryDiscountSerivce : DiscountService
    {
        private readonly string _contryIdentifier;
        public CountryDiscountSerivce(string contryIdentifier)
        {
            _contryIdentifier = contryIdentifier;
        }

        public override int DiscountPercentage
        {
            get 
            {
                switch(_contryIdentifier)
                {
                    case "BE": return 20;
                    default: return 10;  
                }
            }
        }
    }

    /// <summary>
    /// Concreate Product
    /// </summary>
    public class CityDiscountSerivce : DiscountService
    {
        private readonly string _cityCode;
        public CityDiscountSerivce(string cityCode)
        {
            _cityCode = cityCode;
        }

        public override int DiscountPercentage
        {
            get
            { 
                switch(_cityCode)
                {
                    case "10000": return 30;
                    case "200": return 40;
                    default: return 100;
                }
            }
            
        }
    }

    /// <summary>
    /// Concreate Product
    /// </summary>
    public class CodeDiscountSerivce : DiscountService
    {
        private readonly string _code;
        public CodeDiscountSerivce(string code)
        {
            _code = code;
        }

        public override int DiscountPercentage
        {
            get => 15;

        }
    }

    /// <summary>
    /// Creator
    /// </summary>
    public abstract class DiscountFactory
    {
        public abstract DiscountService CreateDiscountService();
    }

    /// <summary>
    /// Concrete Creator
    /// </summary>
    public class CountryDiscountFactory: DiscountFactory
    {
        private readonly string _contryIdentifier;

        public CountryDiscountFactory(string contryIdentifier)
        {
            _contryIdentifier = contryIdentifier;
        }
        public override DiscountService CreateDiscountService()
        {
            return new CountryDiscountSerivce(_contryIdentifier);
        }
    }

    /// <summary>
    /// Concrete Creator
    /// </summary>
    public class CodeDiscountFactory : DiscountFactory
    {
        private readonly string _code;

        public CodeDiscountFactory(string code)
        {
            _code = code;
        }
        public override DiscountService CreateDiscountService()
        {
            return new CodeDiscountSerivce(_code);
        }
    }

    /// <summary>
    /// Concrete Creator
    /// </summary>
    public class CityDiscountFactory : DiscountFactory
    {
        private readonly string _cityCode;

        public CityDiscountFactory(string cityCode)
        {
            _cityCode = cityCode;
        }
        public override DiscountService CreateDiscountService()
        {
            return new CityDiscountSerivce(_cityCode);
        }
    }
}
