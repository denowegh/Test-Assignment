using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Assignment.Model.Coin_From_Coingecko
{
    public class Price
    {
        public decimal? Uah { get; set; }
        public decimal? Usd { get; set; }
        public decimal? Eur { get; set; }

        public override string ToString()
        {
            return $"Uah: {Uah} Usd: {Usd} Eur: {Eur}";
        }
    }
}
