using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Assignment.Model
{
    class RequestCoin<T>
    {
        public IEnumerable<T> Data { get; set; }
        public double Timestamp { get; set; }
        
    }
}
