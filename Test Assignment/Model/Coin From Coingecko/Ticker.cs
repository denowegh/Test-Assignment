using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Assignment.Model.Coin_From_Coingecko
{
    public class Ticker
    {       
        public string Base { get; set; }
        public string Target { get; set; }
        public Market Market { get; set; }
        public Converted_last Converted_last { get; set; }
        public DateTime Timestamp { get; set; }
        public Uri Trade_url { get; set; }
        public string Coin_id { get; set; }
        public string Target_coin_id { get; set; }
        public decimal Last { get; set; }
        public override string ToString()
        {
            return $"Base: {Base}\n\nTarget: {Target}\n\nMarket: {Market}\n\nConverted_last: {Converted_last}\n\nTimestamp: {Timestamp}\n\nTrade_url: {Trade_url}\n\n" +
                $"Coin_id:{Coin_id}\n\n Target_coin_id: {Target_coin_id}\n\n";
        }
    }
}
