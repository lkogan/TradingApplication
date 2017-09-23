using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApplication
{
    public class StockPrice
    {
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public DateTime QuoteDate { get; set; }
    }

    /* https://finance.yahoo.com/chart/MSFT  */

    public interface IBacktester
    {
        /*
         * Calculate the maximum profit you could have been made by buying and then selling 
         * a single share over a given day range.
         * Assume stockPrices are sorted by QuoteDate.
         */
        decimal CalculateMaxProfit(IEnumerable<StockPrice> stockPrices);
    }


    //TO DO: add date parameters

    public class TradingCalcs : IBacktester
    {
        public decimal CalculateMaxProfit(IEnumerable<StockPrice> stockPrices)
        {
            List<decimal> lstLows = stockPrices.Select(x => x.Low).ToList();

            List<decimal> lstHighs = stockPrices.Select(x => x.High).ToList();

            decimal maxDifference = 0;

            int i, j;
            for (i = 0; i < lstLows.Count; i++)
            {
                for (j = 0; j < lstHighs.Count; j++)
                {
                    if (lstHighs[j] - lstLows[i] > maxDifference)
                    {
                        maxDifference = lstHighs[j] - lstLows[i];
                    }
                       
                }
            }
            return maxDifference;

        }
    }

}