using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Assignment.Model
{
    public class CoinFromCoincap:Coin
    {
        public int Rank { get; set; }

        public override string Name { get; set; }
        public override string Id { get; set; }

        public override string Symbol { get; set; }

        public string? Supply { get; set; }
        public string? MaxSupply { get; set; }
        public string? MarketCapUsd { get; set; }

        public string? VolumeUsd24Hr { get; set; }
        public string? PriceUsd { get; set; }
        public string? ChangePercent24Hr { get; set; }
        public string? Vwap24Hr { get;set; }

    }
}
