using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Assignment.Model.Coin_From_Coingecko
{
    public class Image
    {
        public Uri? Small { get; set; }
        public override string ToString()
        {
            return Small.ToString();
        }
    }
}
