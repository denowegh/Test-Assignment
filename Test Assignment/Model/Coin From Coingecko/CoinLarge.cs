using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Assignment.Model.Coin_From_Coingecko
{
    public class CoinLarge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public Links? Links { get; set; }
        public Image? Image { get; set; }
        public DateTime? Genesis_date { get; set; }
        public int? Market_cap_rank { get; set; }
        public Market_data? Market_data { get; set; }
        public DateTime? Last_updated { get; set; }
        public override string ToString() => $"Id: {Id}\n\nName: {Name}\n\nSymbol: {Symbol}\n\n" +
            $"Links: {Links}\n\nImage: {Image}\n\n" +
            $"Genesis_date: {Genesis_date}\n\nMarket_cap_rank: {Market_cap_rank}\n\nMarket_data: {Market_data}\n\n" +
            $"Last_updated: {Last_updated}\n\n";

    }
}
