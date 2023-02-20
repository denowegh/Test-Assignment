using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Assignment.Model.Coin_From_Coingecko
{
    public class CoinTickers
    {
        public string Name { get; set; }

        public List<Ticker>? Tickers { get; set; }
        public override string ToString()
        {
            string RetValue = "";
            foreach (var ticker in Tickers)
            {
                RetValue += "//////////////////////\n";
                RetValue += $"{ticker.ToString()} \n";
                RetValue += "\\\\\\\\\\\\\\\\\\\\\\\n";
            }
            return $"Name: {Name}\n\n Tickers: {RetValue}\n\n";
        }
    }
}
