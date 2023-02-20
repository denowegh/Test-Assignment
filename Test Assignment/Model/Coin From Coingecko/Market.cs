using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Assignment.Model.Coin_From_Coingecko
{
    public class Market
    {
        public string Name { get; set; }
        public string Identifier { get; set; }
        public override string ToString()
        {
            return $"Name: {Name}\n\n Identifier: {Identifier}\n\n";
        }
    }
}
