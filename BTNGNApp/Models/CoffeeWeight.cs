using System;
using System.Collections.Generic;

namespace BTNGNApp.Models
{
    public class CoffeeWeight
    {
        public int Value { get; set; }
        public string Option { get; set; }
    }

    public class CoffeeWeightData
    {
        public static IList<CoffeeWeight> CoffeeWeight { get; set; }
        static CoffeeWeightData()
        {
            CoffeeWeight = new List<CoffeeWeight>
            {
                new CoffeeWeight
                {
                    Value = 9,
                    Option = "9 g"
                },
                new CoffeeWeight
                {
                    Value = 50,
                    Option = "50 g"
                },
                new CoffeeWeight
                {
                    Value = 90,
                    Option = "90 g"
                },
                new CoffeeWeight
                {
                    Value = 100,
                    Option = "100 g"
                },
                new CoffeeWeight
                {
                    Value = 200,
                    Option = "200 g"
                },
                new CoffeeWeight
                {
                    Value = 300,
                    Option = "300 g"
                },
                new CoffeeWeight
                {
                    Value = 500,
                    Option = "500 g"
                }
            };
        }
    }
}
