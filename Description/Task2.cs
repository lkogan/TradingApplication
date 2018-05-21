using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class StockPrice
    {
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public DateTime QuoteDate { get; set; }
    }


    /*
     * https://finance.yahoo.com/chart/MSFT
     * 
     * 
     */
    public interface IBacktester
    {
        /*
         * Calculate the maximum profit you could have been made by buying and then selling a single share over a given day range.
         * Assume stockPrices are sorted by QuoteDate.
         */
        decimal CalculateMaxProfit(IEnumerable<StockPrice> stockPrices);
    }
}
