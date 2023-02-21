using LiveChartsCore.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Assignment.Model.Coin_From_Coingecko
{
    public class OHLC
    {
        public DateTime Time { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public FinancialPoint ToFinancialPoint() => new FinancialPoint(Time, High, Open, Close, Low);
        public override string ToString()
        {
            return $"Time:{Time}\nOpen: {Open}\nHigh: {High}\nLow: {Low}\nClose: {Close}\n";
        }
    }
}
