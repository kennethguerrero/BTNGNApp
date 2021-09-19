using System;
namespace BTNGNApp.Models
{
    public class Coffee
    {
        public int Id { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Type { get; set; }
        public int Weight { get; set; }
        public int MultipliedWeight { get; set; }
        public int Quantity { get; set; }
        public bool IsStock { get; set; }
        public string Image { get; set; }
        public string SoldTo { get; set; }
        public DateTime Timestamp { get; set; }

        public string StockDisplay { get { return IsStock ? "Stock" : "Sale"; } }
        public string TimestampDisplay { get { return Timestamp.ToLocalTime().ToString("d"); } }
        public string QuantityDisplay { get { return Quantity + " pc/s"; } }
        public string WeightDisplay { get { return Weight + " g"; } }
        public string MultipliedWeightDisplay { get { return MultipliedWeight + " g"; } }
        public string CoffeeType { get { return Type; } }

        public string ImageDisplay
        {
            get
            {
                switch (CoffeeType)
                {
                    case "Beans":
                        return "https://btngn.com/Products-Beans.JPG";
                    case "Ground":
                        return "https://btngn.com/Products-Ground.JPG";
                    case "Tablea de Cacao":
                        return "https://btngn.com/Products-Tablea.JPG";
                    case "Drip":
                        return "https://btngn.com/Products-Drip.JPG";
                    case "Drip Solo":
                        return "https://btngn.com/Products-Solo.JPG";
                    case "Cold Brew":
                        return "https://btngn.com/Products-ColdBrew.JPG";
                    case "Pepper":
                        return "https://btngn.com/Products-Pepper.JPG";
                    default:
                        return "https://www.yesplz.coffee/app/uploads/2020/11/emptybag-min.png";
                }
            }
        }
    }
}
