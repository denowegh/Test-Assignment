using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Assignment.Model.Coin_From_Coingecko
{
    public class Market_data
    {
        public Price? Current_price { get; set; }
        public Price? High_24h { get; set; }
        public Price? Low_24h { get; set; }
        public Price? Price_change_percentage_24h_in_currency { get; set; }
        public Price? Price_change_percentage_14d_in_currency { get; set; }
        public Price? Price_change_percentage_1y_in_currency { get; set; }
        public decimal? Total_supply { get; set; }
        public decimal? Max_supply { get; set; }
        public decimal? Circulating_supply { get; set; }
        public override string ToString()
        {
            return $"Current_price: {Current_price}\n\n High_24h: {High_24h}\n\n Low_24h: {Low_24h}\n\n" +
                $" Price_change_percentage_24h_in_currency: {Price_change_percentage_24h_in_currency}\n\n " +
                $"Price_change_percentage_14d_in_currency: {Price_change_percentage_14d_in_currency}\n\n " +
                $"Price_change_percentage_1y_in_currency: {Price_change_percentage_1y_in_currency}\n\n" +
                $"Total_supply: {Total_supply}\n\nMax_supply: {Max_supply}\n\nCirculating_supply: {Circulating_supply}\n\n";
        }
    }
}
