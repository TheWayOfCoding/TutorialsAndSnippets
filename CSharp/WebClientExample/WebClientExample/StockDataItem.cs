using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClientExample
{
    /// <summary>
    /// a container for a single day of stock data
    /// </summary>
    public class StockDataItem
    {
        public double open = 0;
        public double high = 0;
        public double low = 0;
        public double close = 0;
        public double volume = 0;
    }
}
