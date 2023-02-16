using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Assignment.Model
{
    public abstract class  Coin
    {
        public  abstract string Id { get; set; }

        public abstract string Symbol { get; set; }

        public abstract string Name { get; set; }
    }
}
