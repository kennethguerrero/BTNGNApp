using System.Collections.Generic;

namespace BTNGNApp.Models
{
    public class CoffeeType
    {
        public string Option { get; set; }
    }

    public class CoffeeTypeData
    {
        public static IList<CoffeeType> CoffeeType { get; set; }
        static CoffeeTypeData()
        {
            CoffeeType = new List<CoffeeType>
            {
                new CoffeeType
                {
                    Option = "Beans"
                },
                new CoffeeType
                {
                    Option = "Ground"
                },
                new CoffeeType
                {
                    Option = "Drip"
                },
                new CoffeeType
                {
                    Option = "Drip Solo"
                },
                new CoffeeType
                {
                    Option = "Cold Brew"
                },
                new CoffeeType
                {
                    Option = "Tablea de Cacao"
                },
                new CoffeeType
                {
                    Option = "Pepper"
                },
            };
        }
    }
}
