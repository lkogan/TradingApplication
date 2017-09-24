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
    
    public interface IBacktester
    {
        /*
         * Calculate the maximum profit you could have been made by buying and then selling 
         * a single share over a given day range.
         * Assume stockPrices are sorted by QuoteDate.
         */
        decimal CalculateMaxProfit(List<StockPrice> stockPrices);
    }
     
    public class TradingCalcs : IBacktester
    {  
        public static List<StockPrice> GetTestData()
        {

            /* https://finance.yahoo.com/chart/MSFT  */

            StockPrice quote;

            List<StockPrice> lst = new List<StockPrice>();

            quote = new StockPrice { Open = (decimal)75.35, High = (decimal)75.55, Low = (decimal)74.31, Close = (decimal)74.94, QuoteDate = Convert.ToDateTime("09/20/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)75.21, High = (decimal)75.71, Low = (decimal)75.01, Close = (decimal)75.44, QuoteDate = Convert.ToDateTime("09/19/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)75.23, High = (decimal)75.97, Low = (decimal)75.04, Close = (decimal)75.16, QuoteDate = Convert.ToDateTime("09/18/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)74.83, High = (decimal)75.39, Low = (decimal)74.07, Close = (decimal)75.31, QuoteDate = Convert.ToDateTime("09/15/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)75, High = (decimal)75.49, Low = (decimal)74.52, Close = (decimal)74.77, QuoteDate = Convert.ToDateTime("09/14/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)74.93, High = (decimal)75.23, Low = (decimal)74.55, Close = (decimal)75.21, QuoteDate = Convert.ToDateTime("09/13/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)74.76, High = (decimal)75.24, Low = (decimal)74.37, Close = (decimal)74.68, QuoteDate = Convert.ToDateTime("09/12/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)74.31, High = (decimal)74.94, Low = (decimal)74.31, Close = (decimal)74.76, QuoteDate = Convert.ToDateTime("09/11/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)74.33, High = (decimal)74.44, Low = (decimal)73.84, Close = (decimal)73.98, QuoteDate = Convert.ToDateTime("09/08/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.68, High = (decimal)74.6, Low = (decimal)73.6, Close = (decimal)74.34, QuoteDate = Convert.ToDateTime("09/07/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.74, High = (decimal)74.04, Low = (decimal)73.35, Close = (decimal)73.4, QuoteDate = Convert.ToDateTime("09/06/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.34, High = (decimal)73.89, Low = (decimal)72.98, Close = (decimal)73.61, QuoteDate = Convert.ToDateTime("09/05/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)74.71, High = (decimal)74.74, Low = (decimal)73.64, Close = (decimal)73.94, QuoteDate = Convert.ToDateTime("09/01/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)74.03, High = (decimal)74.96, Low = (decimal)73.8, Close = (decimal)74.77, QuoteDate = Convert.ToDateTime("08/31/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.01, High = (decimal)74.21, Low = (decimal)72.83, Close = (decimal)74.01, QuoteDate = Convert.ToDateTime("08/30/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.25, High = (decimal)73.16, Low = (decimal)72.05, Close = (decimal)73.05, QuoteDate = Convert.ToDateTime("08/29/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.06, High = (decimal)73.09, Low = (decimal)72.55, Close = (decimal)72.83, QuoteDate = Convert.ToDateTime("08/28/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.86, High = (decimal)73.35, Low = (decimal)72.48, Close = (decimal)72.82, QuoteDate = Convert.ToDateTime("08/25/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.74, High = (decimal)72.86, Low = (decimal)72.07, Close = (decimal)72.69, QuoteDate = Convert.ToDateTime("08/24/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.96, High = (decimal)73.15, Low = (decimal)72.53, Close = (decimal)72.72, QuoteDate = Convert.ToDateTime("08/23/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.35, High = (decimal)73.24, Low = (decimal)72.35, Close = (decimal)73.16, QuoteDate = Convert.ToDateTime("08/22/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.47, High = (decimal)72.48, Low = (decimal)71.7, Close = (decimal)72.15, QuoteDate = Convert.ToDateTime("08/21/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.27, High = (decimal)72.84, Low = (decimal)71.93, Close = (decimal)72.49, QuoteDate = Convert.ToDateTime("08/18/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.58, High = (decimal)73.87, Low = (decimal)72.4, Close = (decimal)72.4, QuoteDate = Convert.ToDateTime("08/17/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.34, High = (decimal)74.1, Low = (decimal)73.17, Close = (decimal)73.65, QuoteDate = Convert.ToDateTime("08/16/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.59, High = (decimal)73.59, Low = (decimal)73.04, Close = (decimal)73.22, QuoteDate = Convert.ToDateTime("08/15/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.06, High = (decimal)73.72, Low = (decimal)72.95, Close = (decimal)73.59, QuoteDate = Convert.ToDateTime("08/14/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)71.61, High = (decimal)72.7, Low = (decimal)71.28, Close = (decimal)72.5, QuoteDate = Convert.ToDateTime("08/11/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)71.9, High = (decimal)72.19, Low = (decimal)71.35, Close = (decimal)71.41, QuoteDate = Convert.ToDateTime("08/10/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.25, High = (decimal)72.51, Low = (decimal)72.05, Close = (decimal)72.47, QuoteDate = Convert.ToDateTime("08/09/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.09, High = (decimal)73.13, Low = (decimal)71.75, Close = (decimal)72.79, QuoteDate = Convert.ToDateTime("08/08/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.8, High = (decimal)72.9, Low = (decimal)72.26, Close = (decimal)72.4, QuoteDate = Convert.ToDateTime("08/07/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.4, High = (decimal)73.04, Low = (decimal)72.24, Close = (decimal)72.68, QuoteDate = Convert.ToDateTime("08/04/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.19, High = (decimal)72.44, Low = (decimal)71.85, Close = (decimal)72.15, QuoteDate = Convert.ToDateTime("08/03/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.55, High = (decimal)72.56, Low = (decimal)71.44, Close = (decimal)72.26, QuoteDate = Convert.ToDateTime("08/02/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.1, High = (decimal)73.42, Low = (decimal)72.49, Close = (decimal)72.58, QuoteDate = Convert.ToDateTime("08/01/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.3, High = (decimal)73.44, Low = (decimal)72.41, Close = (decimal)72.7, QuoteDate = Convert.ToDateTime("07/31/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.67, High = (decimal)73.31, Low = (decimal)72.54, Close = (decimal)73.04, QuoteDate = Convert.ToDateTime("07/28/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.76, High = (decimal)74.42, Low = (decimal)72.32, Close = (decimal)73.16, QuoteDate = Convert.ToDateTime("07/27/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)74.34, High = (decimal)74.38, Low = (decimal)73.81, Close = (decimal)74.05, QuoteDate = Convert.ToDateTime("07/26/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.8, High = (decimal)74.31, Low = (decimal)73.5, Close = (decimal)74.19, QuoteDate = Convert.ToDateTime("07/25/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.53, High = (decimal)73.75, Low = (decimal)73.13, Close = (decimal)73.6, QuoteDate = Convert.ToDateTime("07/24/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.45, High = (decimal)74.29, Low = (decimal)73.17, Close = (decimal)73.79, QuoteDate = Convert.ToDateTime("07/21/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)74.18, High = (decimal)74.3, Low = (decimal)73.28, Close = (decimal)74.22, QuoteDate = Convert.ToDateTime("07/20/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.5, High = (decimal)74.04, Low = (decimal)73.45, Close = (decimal)73.86, QuoteDate = Convert.ToDateTime("07/19/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)73.09, High = (decimal)73.39, Low = (decimal)72.66, Close = (decimal)73.3, QuoteDate = Convert.ToDateTime("07/18/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.8, High = (decimal)73.45, Low = (decimal)72.72, Close = (decimal)73.35, QuoteDate = Convert.ToDateTime("07/17/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.24, High = (decimal)73.27, Low = (decimal)71.96, Close = (decimal)72.78, QuoteDate = Convert.ToDateTime("07/14/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)71.5, High = (decimal)72.04, Low = (decimal)71.31, Close = (decimal)71.77, QuoteDate = Convert.ToDateTime("07/13/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)70.69, High = (decimal)71.28, Low = (decimal)70.55, Close = (decimal)71.15, QuoteDate = Convert.ToDateTime("07/12/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)70, High = (decimal)70.68, Low = (decimal)69.75, Close = (decimal)69.99, QuoteDate = Convert.ToDateTime("07/11/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)69.46, High = (decimal)70.25, Low = (decimal)69.2, Close = (decimal)69.98, QuoteDate = Convert.ToDateTime("07/10/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.7, High = (decimal)69.84, Low = (decimal)68.7, Close = (decimal)69.46, QuoteDate = Convert.ToDateTime("07/07/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.27, High = (decimal)68.78, Low = (decimal)68.12, Close = (decimal)68.57, QuoteDate = Convert.ToDateTime("07/06/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.26, High = (decimal)69.44, Low = (decimal)68.22, Close = (decimal)69.08, QuoteDate = Convert.ToDateTime("07/05/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)69.33, High = (decimal)69.6, Low = (decimal)68.02, Close = (decimal)68.17, QuoteDate = Convert.ToDateTime("07/03/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.78, High = (decimal)69.38, Low = (decimal)68.74, Close = (decimal)68.93, QuoteDate = Convert.ToDateTime("06/30/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)69.38, High = (decimal)69.49, Low = (decimal)68.09, Close = (decimal)68.49, QuoteDate = Convert.ToDateTime("06/29/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)69.21, High = (decimal)69.84, Low = (decimal)68.79, Close = (decimal)69.8, QuoteDate = Convert.ToDateTime("06/28/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)70.11, High = (decimal)70.18, Low = (decimal)69.18, Close = (decimal)69.21, QuoteDate = Convert.ToDateTime("06/27/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)71.4, High = (decimal)71.71, Low = (decimal)70.44, Close = (decimal)70.53, QuoteDate = Convert.ToDateTime("06/26/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)70.09, High = (decimal)71.25, Low = (decimal)69.92, Close = (decimal)71.21, QuoteDate = Convert.ToDateTime("06/23/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)70.54, High = (decimal)70.59, Low = (decimal)69.71, Close = (decimal)70.26, QuoteDate = Convert.ToDateTime("06/22/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)70.21, High = (decimal)70.62, Low = (decimal)69.94, Close = (decimal)70.27, QuoteDate = Convert.ToDateTime("06/21/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)70.82, High = (decimal)70.87, Low = (decimal)69.87, Close = (decimal)69.91, QuoteDate = Convert.ToDateTime("06/20/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)70.5, High = (decimal)70.94, Low = (decimal)70.35, Close = (decimal)70.87, QuoteDate = Convert.ToDateTime("06/19/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)69.73, High = (decimal)70.03, Low = (decimal)69.22, Close = (decimal)70, QuoteDate = Convert.ToDateTime("06/16/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)69.27, High = (decimal)70.21, Low = (decimal)68.8, Close = (decimal)69.9, QuoteDate = Convert.ToDateTime("06/15/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)70.91, High = (decimal)71.1, Low = (decimal)69.43, Close = (decimal)70.27, QuoteDate = Convert.ToDateTime("06/14/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)70.02, High = (decimal)70.82, Low = (decimal)69.96, Close = (decimal)70.65, QuoteDate = Convert.ToDateTime("06/13/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)69.25, High = (decimal)69.94, Low = (decimal)68.13, Close = (decimal)69.78, QuoteDate = Convert.ToDateTime("06/12/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.04, High = (decimal)72.08, Low = (decimal)68.59, Close = (decimal)70.32, QuoteDate = Convert.ToDateTime("06/09/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.51, High = (decimal)72.52, Low = (decimal)71.5, Close = (decimal)71.95, QuoteDate = Convert.ToDateTime("06/08/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.64, High = (decimal)72.77, Low = (decimal)71.95, Close = (decimal)72.39, QuoteDate = Convert.ToDateTime("06/07/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)72.3, High = (decimal)72.62, Low = (decimal)72.27, Close = (decimal)72.52, QuoteDate = Convert.ToDateTime("06/06/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)71.97, High = (decimal)72.89, Low = (decimal)71.81, Close = (decimal)72.28, QuoteDate = Convert.ToDateTime("06/05/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)70.44, High = (decimal)71.86, Low = (decimal)70.24, Close = (decimal)71.76, QuoteDate = Convert.ToDateTime("06/02/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)70.24, High = (decimal)70.61, Low = (decimal)69.45, Close = (decimal)70.1, QuoteDate = Convert.ToDateTime("06/01/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)70.53, High = (decimal)70.74, Low = (decimal)69.81, Close = (decimal)69.84, QuoteDate = Convert.ToDateTime("05/31/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)69.79, High = (decimal)70.41, Low = (decimal)69.77, Close = (decimal)70.41, QuoteDate = Convert.ToDateTime("05/30/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)69.8, High = (decimal)70.22, Low = (decimal)69.52, Close = (decimal)69.96, QuoteDate = Convert.ToDateTime("05/26/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.97, High = (decimal)69.88, Low = (decimal)68.91, Close = (decimal)69.62, QuoteDate = Convert.ToDateTime("05/25/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.87, High = (decimal)68.88, Low = (decimal)68.45, Close = (decimal)68.77, QuoteDate = Convert.ToDateTime("05/24/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.72, High = (decimal)68.75, Low = (decimal)68.38, Close = (decimal)68.68, QuoteDate = Convert.ToDateTime("05/23/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)67.89, High = (decimal)68.5, Low = (decimal)67.5, Close = (decimal)68.45, QuoteDate = Convert.ToDateTime("05/22/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)67.5, High = (decimal)68.1, Low = (decimal)67.43, Close = (decimal)67.69, QuoteDate = Convert.ToDateTime("05/19/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)67.4, High = (decimal)68.13, Low = (decimal)67.14, Close = (decimal)67.71, QuoteDate = Convert.ToDateTime("05/18/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.89, High = (decimal)69.1, Low = (decimal)67.43, Close = (decimal)67.48, QuoteDate = Convert.ToDateTime("05/17/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.23, High = (decimal)69.44, Low = (decimal)68.16, Close = (decimal)69.41, QuoteDate = Convert.ToDateTime("05/16/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.14, High = (decimal)68.48, Low = (decimal)67.57, Close = (decimal)68.43, QuoteDate = Convert.ToDateTime("05/15/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.61, High = (decimal)68.61, Low = (decimal)68.04, Close = (decimal)68.38, QuoteDate = Convert.ToDateTime("05/12/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.36, High = (decimal)68.73, Low = (decimal)68.12, Close = (decimal)68.46, QuoteDate = Convert.ToDateTime("05/11/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.99, High = (decimal)69.56, Low = (decimal)68.92, Close = (decimal)69.31, QuoteDate = Convert.ToDateTime("05/10/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.86, High = (decimal)69.28, Low = (decimal)68.68, Close = (decimal)69.04, QuoteDate = Convert.ToDateTime("05/09/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.97, High = (decimal)69.05, Low = (decimal)68.42, Close = (decimal)68.94, QuoteDate = Convert.ToDateTime("05/08/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.9, High = (decimal)69.03, Low = (decimal)68.49, Close = (decimal)69, QuoteDate = Convert.ToDateTime("05/05/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)69.03, High = (decimal)69.08, Low = (decimal)68.64, Close = (decimal)68.81, QuoteDate = Convert.ToDateTime("05/04/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)69.38, High = (decimal)69.38, Low = (decimal)68.71, Close = (decimal)69.08, QuoteDate = Convert.ToDateTime("05/03/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)69.71, High = (decimal)69.71, Low = (decimal)69.13, Close = (decimal)69.3, QuoteDate = Convert.ToDateTime("05/02/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.68, High = (decimal)69.55, Low = (decimal)68.5, Close = (decimal)69.41, QuoteDate = Convert.ToDateTime("05/01/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.91, High = (decimal)69.14, Low = (decimal)67.69, Close = (decimal)68.46, QuoteDate = Convert.ToDateTime("04/28/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.15, High = (decimal)68.38, Low = (decimal)67.58, Close = (decimal)68.27, QuoteDate = Convert.ToDateTime("04/27/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)68.08, High = (decimal)68.31, Low = (decimal)67.62, Close = (decimal)67.83, QuoteDate = Convert.ToDateTime("04/26/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)67.9, High = (decimal)68.04, Low = (decimal)67.6, Close = (decimal)67.92, QuoteDate = Convert.ToDateTime("04/25/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)67.48, High = (decimal)67.66, Low = (decimal)67.1, Close = (decimal)67.53, QuoteDate = Convert.ToDateTime("04/24/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.67, High = (decimal)66.7, Low = (decimal)65.45, Close = (decimal)66.4, QuoteDate = Convert.ToDateTime("04/21/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.46, High = (decimal)65.75, Low = (decimal)65.14, Close = (decimal)65.5, QuoteDate = Convert.ToDateTime("04/20/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.65, High = (decimal)65.75, Low = (decimal)64.89, Close = (decimal)65.04, QuoteDate = Convert.ToDateTime("04/19/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.33, High = (decimal)65.71, Low = (decimal)65.16, Close = (decimal)65.39, QuoteDate = Convert.ToDateTime("04/18/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.04, High = (decimal)65.49, Low = (decimal)65.01, Close = (decimal)65.48, QuoteDate = Convert.ToDateTime("04/17/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.29, High = (decimal)65.86, Low = (decimal)64.95, Close = (decimal)64.95, QuoteDate = Convert.ToDateTime("04/13/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.42, High = (decimal)65.51, Low = (decimal)65.11, Close = (decimal)65.23, QuoteDate = Convert.ToDateTime("04/12/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.6, High = (decimal)65.61, Low = (decimal)64.85, Close = (decimal)65.48, QuoteDate = Convert.ToDateTime("04/11/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.61, High = (decimal)65.82, Low = (decimal)65.36, Close = (decimal)65.53, QuoteDate = Convert.ToDateTime("04/10/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.85, High = (decimal)65.96, Low = (decimal)65.44, Close = (decimal)65.68, QuoteDate = Convert.ToDateTime("04/07/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.6, High = (decimal)66.06, Low = (decimal)65.48, Close = (decimal)65.73, QuoteDate = Convert.ToDateTime("04/06/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)66.3, High = (decimal)66.35, Low = (decimal)65.44, Close = (decimal)65.56, QuoteDate = Convert.ToDateTime("04/05/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.39, High = (decimal)65.81, Low = (decimal)65.28, Close = (decimal)65.73, QuoteDate = Convert.ToDateTime("04/04/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.81, High = (decimal)65.94, Low = (decimal)65.19, Close = (decimal)65.55, QuoteDate = Convert.ToDateTime("04/03/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.65, High = (decimal)66.19, Low = (decimal)65.45, Close = (decimal)65.86, QuoteDate = Convert.ToDateTime("03/31/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.42, High = (decimal)65.98, Low = (decimal)65.36, Close = (decimal)65.71, QuoteDate = Convert.ToDateTime("03/30/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.12, High = (decimal)65.5, Low = (decimal)64.95, Close = (decimal)65.47, QuoteDate = Convert.ToDateTime("03/29/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.96, High = (decimal)65.47, Low = (decimal)64.65, Close = (decimal)65.29, QuoteDate = Convert.ToDateTime("03/28/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.63, High = (decimal)65.22, Low = (decimal)64.35, Close = (decimal)65.1, QuoteDate = Convert.ToDateTime("03/27/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.36, High = (decimal)65.45, Low = (decimal)64.76, Close = (decimal)64.98, QuoteDate = Convert.ToDateTime("03/24/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.94, High = (decimal)65.24, Low = (decimal)64.77, Close = (decimal)64.87, QuoteDate = Convert.ToDateTime("03/23/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.12, High = (decimal)65.14, Low = (decimal)64.12, Close = (decimal)65.03, QuoteDate = Convert.ToDateTime("03/22/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.19, High = (decimal)65.5, Low = (decimal)64.13, Close = (decimal)64.21, QuoteDate = Convert.ToDateTime("03/21/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.91, High = (decimal)65.18, Low = (decimal)64.72, Close = (decimal)64.93, QuoteDate = Convert.ToDateTime("03/20/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.91, High = (decimal)65.24, Low = (decimal)64.68, Close = (decimal)64.87, QuoteDate = Convert.ToDateTime("03/17/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.75, High = (decimal)64.76, Low = (decimal)64.3, Close = (decimal)64.64, QuoteDate = Convert.ToDateTime("03/16/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.55, High = (decimal)64.92, Low = (decimal)64.25, Close = (decimal)64.75, QuoteDate = Convert.ToDateTime("03/15/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.53, High = (decimal)64.55, Low = (decimal)64.15, Close = (decimal)64.41, QuoteDate = Convert.ToDateTime("03/14/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.01, High = (decimal)65.19, Low = (decimal)64.57, Close = (decimal)64.71, QuoteDate = Convert.ToDateTime("03/13/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.11, High = (decimal)65.26, Low = (decimal)64.75, Close = (decimal)64.93, QuoteDate = Convert.ToDateTime("03/10/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.19, High = (decimal)65.2, Low = (decimal)64.48, Close = (decimal)64.73, QuoteDate = Convert.ToDateTime("03/09/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.26, High = (decimal)65.08, Low = (decimal)64.25, Close = (decimal)64.99, QuoteDate = Convert.ToDateTime("03/08/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.19, High = (decimal)64.78, Low = (decimal)64.19, Close = (decimal)64.4, QuoteDate = Convert.ToDateTime("03/07/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.97, High = (decimal)64.56, Low = (decimal)63.81, Close = (decimal)64.27, QuoteDate = Convert.ToDateTime("03/06/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.99, High = (decimal)64.28, Low = (decimal)63.62, Close = (decimal)64.25, QuoteDate = Convert.ToDateTime("03/03/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.69, High = (decimal)64.75, Low = (decimal)63.88, Close = (decimal)64.01, QuoteDate = Convert.ToDateTime("03/02/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.13, High = (decimal)64.99, Low = (decimal)64.02, Close = (decimal)64.94, QuoteDate = Convert.ToDateTime("03/01/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.08, High = (decimal)64.2, Low = (decimal)63.76, Close = (decimal)63.98, QuoteDate = Convert.ToDateTime("02/28/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.54, High = (decimal)64.54, Low = (decimal)64.05, Close = (decimal)64.23, QuoteDate = Convert.ToDateTime("02/27/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.53, High = (decimal)64.8, Low = (decimal)64.14, Close = (decimal)64.62, QuoteDate = Convert.ToDateTime("02/24/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.42, High = (decimal)64.73, Low = (decimal)64.19, Close = (decimal)64.62, QuoteDate = Convert.ToDateTime("02/23/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.33, High = (decimal)64.39, Low = (decimal)64.05, Close = (decimal)64.36, QuoteDate = Convert.ToDateTime("02/22/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.61, High = (decimal)64.95, Low = (decimal)64.45, Close = (decimal)64.49, QuoteDate = Convert.ToDateTime("02/21/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.47, High = (decimal)64.69, Low = (decimal)64.3, Close = (decimal)64.62, QuoteDate = Convert.ToDateTime("02/17/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.74, High = (decimal)65.24, Low = (decimal)64.44, Close = (decimal)64.52, QuoteDate = Convert.ToDateTime("02/16/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.5, High = (decimal)64.57, Low = (decimal)64.16, Close = (decimal)64.53, QuoteDate = Convert.ToDateTime("02/15/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.41, High = (decimal)64.72, Low = (decimal)64.02, Close = (decimal)64.57, QuoteDate = Convert.ToDateTime("02/14/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.24, High = (decimal)64.86, Low = (decimal)64.13, Close = (decimal)64.72, QuoteDate = Convert.ToDateTime("02/13/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.25, High = (decimal)64.3, Low = (decimal)63.98, Close = (decimal)64, QuoteDate = Convert.ToDateTime("02/10/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.52, High = (decimal)64.44, Low = (decimal)63.32, Close = (decimal)64.06, QuoteDate = Convert.ToDateTime("02/09/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.57, High = (decimal)63.81, Low = (decimal)63.22, Close = (decimal)63.34, QuoteDate = Convert.ToDateTime("02/08/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.74, High = (decimal)63.78, Low = (decimal)63.23, Close = (decimal)63.43, QuoteDate = Convert.ToDateTime("02/07/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.5, High = (decimal)63.65, Low = (decimal)63.14, Close = (decimal)63.64, QuoteDate = Convert.ToDateTime("02/06/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.5, High = (decimal)63.7, Low = (decimal)63.07, Close = (decimal)63.68, QuoteDate = Convert.ToDateTime("02/03/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.25, High = (decimal)63.41, Low = (decimal)62.75, Close = (decimal)63.17, QuoteDate = Convert.ToDateTime("02/02/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.36, High = (decimal)64.62, Low = (decimal)63.47, Close = (decimal)63.58, QuoteDate = Convert.ToDateTime("02/01/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.86, High = (decimal)65.15, Low = (decimal)64.26, Close = (decimal)64.65, QuoteDate = Convert.ToDateTime("01/31/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.69, High = (decimal)65.79, Low = (decimal)64.8, Close = (decimal)65.13, QuoteDate = Convert.ToDateTime("01/30/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)65.39, High = (decimal)65.91, Low = (decimal)64.89, Close = (decimal)65.78, QuoteDate = Convert.ToDateTime("01/27/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)64.12, High = (decimal)64.54, Low = (decimal)63.55, Close = (decimal)64.27, QuoteDate = Convert.ToDateTime("01/26/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.95, High = (decimal)64.1, Low = (decimal)63.45, Close = (decimal)63.68, QuoteDate = Convert.ToDateTime("01/25/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.2, High = (decimal)63.74, Low = (decimal)62.94, Close = (decimal)63.52, QuoteDate = Convert.ToDateTime("01/24/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.7, High = (decimal)63.12, Low = (decimal)62.57, Close = (decimal)62.96, QuoteDate = Convert.ToDateTime("01/23/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.67, High = (decimal)62.82, Low = (decimal)62.37, Close = (decimal)62.74, QuoteDate = Convert.ToDateTime("01/20/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.24, High = (decimal)62.98, Low = (decimal)62.2, Close = (decimal)62.3, QuoteDate = Convert.ToDateTime("01/19/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.67, High = (decimal)62.7, Low = (decimal)62.12, Close = (decimal)62.5, QuoteDate = Convert.ToDateTime("01/18/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.68, High = (decimal)62.7, Low = (decimal)62.03, Close = (decimal)62.53, QuoteDate = Convert.ToDateTime("01/17/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.62, High = (decimal)62.87, Low = (decimal)62.35, Close = (decimal)62.7, QuoteDate = Convert.ToDateTime("01/13/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.06, High = (decimal)63.4, Low = (decimal)61.95, Close = (decimal)62.61, QuoteDate = Convert.ToDateTime("01/12/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.61, High = (decimal)63.23, Low = (decimal)62.43, Close = (decimal)63.19, QuoteDate = Convert.ToDateTime("01/11/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.73, High = (decimal)63.07, Low = (decimal)62.28, Close = (decimal)62.62, QuoteDate = Convert.ToDateTime("01/10/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.76, High = (decimal)63.08, Low = (decimal)62.54, Close = (decimal)62.64, QuoteDate = Convert.ToDateTime("01/09/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.3, High = (decimal)63.15, Low = (decimal)62.04, Close = (decimal)62.84, QuoteDate = Convert.ToDateTime("01/06/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.19, High = (decimal)62.66, Low = (decimal)62.03, Close = (decimal)62.3, QuoteDate = Convert.ToDateTime("01/05/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.48, High = (decimal)62.75, Low = (decimal)62.12, Close = (decimal)62.3, QuoteDate = Convert.ToDateTime("01/04/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.79, High = (decimal)62.84, Low = (decimal)62.13, Close = (decimal)62.58, QuoteDate = Convert.ToDateTime("01/03/17") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.96, High = (decimal)62.99, Low = (decimal)62.03, Close = (decimal)62.14, QuoteDate = Convert.ToDateTime("12/30/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.86, High = (decimal)63.2, Low = (decimal)62.73, Close = (decimal)62.9, QuoteDate = Convert.ToDateTime("12/29/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.4, High = (decimal)63.4, Low = (decimal)62.83, Close = (decimal)62.99, QuoteDate = Convert.ToDateTime("12/28/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.21, High = (decimal)64.07, Low = (decimal)63.21, Close = (decimal)63.28, QuoteDate = Convert.ToDateTime("12/27/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.45, High = (decimal)63.54, Low = (decimal)62.8, Close = (decimal)63.24, QuoteDate = Convert.ToDateTime("12/23/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.84, High = (decimal)64.1, Low = (decimal)63.41, Close = (decimal)63.55, QuoteDate = Convert.ToDateTime("12/22/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.43, High = (decimal)63.7, Low = (decimal)63.12, Close = (decimal)63.54, QuoteDate = Convert.ToDateTime("12/21/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63.69, High = (decimal)63.8, Low = (decimal)63.03, Close = (decimal)63.54, QuoteDate = Convert.ToDateTime("12/20/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.56, High = (decimal)63.77, Low = (decimal)62.42, Close = (decimal)63.62, QuoteDate = Convert.ToDateTime("12/19/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.95, High = (decimal)62.95, Low = (decimal)62.12, Close = (decimal)62.3, QuoteDate = Convert.ToDateTime("12/16/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.7, High = (decimal)63.15, Low = (decimal)62.3, Close = (decimal)62.58, QuoteDate = Convert.ToDateTime("12/15/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)63, High = (decimal)63.45, Low = (decimal)62.53, Close = (decimal)62.68, QuoteDate = Convert.ToDateTime("12/14/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)62.5, High = (decimal)63.42, Low = (decimal)62.24, Close = (decimal)62.98, QuoteDate = Convert.ToDateTime("12/13/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)61.82, High = (decimal)62.3, Low = (decimal)61.72, Close = (decimal)62.17, QuoteDate = Convert.ToDateTime("12/12/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)61.18, High = (decimal)61.99, Low = (decimal)61.13, Close = (decimal)61.97, QuoteDate = Convert.ToDateTime("12/09/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)61.3, High = (decimal)61.58, Low = (decimal)60.84, Close = (decimal)61.01, QuoteDate = Convert.ToDateTime("12/08/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.01, High = (decimal)61.38, Low = (decimal)59.8, Close = (decimal)61.37, QuoteDate = Convert.ToDateTime("12/07/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.43, High = (decimal)60.46, Low = (decimal)59.8, Close = (decimal)59.95, QuoteDate = Convert.ToDateTime("12/06/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)59.7, High = (decimal)60.59, Low = (decimal)59.56, Close = (decimal)60.22, QuoteDate = Convert.ToDateTime("12/05/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)59.08, High = (decimal)59.47, Low = (decimal)58.8, Close = (decimal)59.25, QuoteDate = Convert.ToDateTime("12/02/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.11, High = (decimal)60.15, Low = (decimal)58.94, Close = (decimal)59.2, QuoteDate = Convert.ToDateTime("12/01/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.86, High = (decimal)61.18, Low = (decimal)60.22, Close = (decimal)60.26, QuoteDate = Convert.ToDateTime("11/30/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.65, High = (decimal)61.41, Low = (decimal)60.52, Close = (decimal)61.09, QuoteDate = Convert.ToDateTime("11/29/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.34, High = (decimal)61.02, Low = (decimal)60.21, Close = (decimal)60.61, QuoteDate = Convert.ToDateTime("11/28/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.3, High = (decimal)60.53, Low = (decimal)60.13, Close = (decimal)60.53, QuoteDate = Convert.ToDateTime("11/25/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)61.01, High = (decimal)61.1, Low = (decimal)60.25, Close = (decimal)60.4, QuoteDate = Convert.ToDateTime("11/23/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.98, High = (decimal)61.26, Low = (decimal)60.81, Close = (decimal)61.12, QuoteDate = Convert.ToDateTime("11/22/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.5, High = (decimal)60.97, Low = (decimal)60.42, Close = (decimal)60.86, QuoteDate = Convert.ToDateTime("11/21/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.78, High = (decimal)61.14, Low = (decimal)60.3, Close = (decimal)60.35, QuoteDate = Convert.ToDateTime("11/18/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.41, High = (decimal)60.95, Low = (decimal)59.97, Close = (decimal)60.64, QuoteDate = Convert.ToDateTime("11/17/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)58.94, High = (decimal)59.66, Low = (decimal)58.81, Close = (decimal)59.65, QuoteDate = Convert.ToDateTime("11/16/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)58.33, High = (decimal)59.49, Low = (decimal)58.32, Close = (decimal)58.87, QuoteDate = Convert.ToDateTime("11/15/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)59.02, High = (decimal)59.08, Low = (decimal)57.28, Close = (decimal)58.12, QuoteDate = Convert.ToDateTime("11/14/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)58.23, High = (decimal)59.12, Low = (decimal)58.01, Close = (decimal)59.02, QuoteDate = Convert.ToDateTime("11/11/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.48, High = (decimal)60.49, Low = (decimal)57.63, Close = (decimal)58.7, QuoteDate = Convert.ToDateTime("11/10/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60, High = (decimal)60.59, Low = (decimal)59.2, Close = (decimal)60.17, QuoteDate = Convert.ToDateTime("11/09/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.55, High = (decimal)60.78, Low = (decimal)60.15, Close = (decimal)60.47, QuoteDate = Convert.ToDateTime("11/08/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)59.78, High = (decimal)60.52, Low = (decimal)59.78, Close = (decimal)60.42, QuoteDate = Convert.ToDateTime("11/07/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)58.65, High = (decimal)59.28, Low = (decimal)58.52, Close = (decimal)58.71, QuoteDate = Convert.ToDateTime("11/04/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)59.53, High = (decimal)59.64, Low = (decimal)59.11, Close = (decimal)59.21, QuoteDate = Convert.ToDateTime("11/03/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)59.82, High = (decimal)59.93, Low = (decimal)59.3, Close = (decimal)59.43, QuoteDate = Convert.ToDateTime("11/02/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)59.97, High = (decimal)60.02, Low = (decimal)59.25, Close = (decimal)59.8, QuoteDate = Convert.ToDateTime("11/01/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.16, High = (decimal)60.42, Low = (decimal)59.92, Close = (decimal)59.92, QuoteDate = Convert.ToDateTime("10/31/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.01, High = (decimal)60.52, Low = (decimal)59.58, Close = (decimal)59.87, QuoteDate = Convert.ToDateTime("10/28/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.61, High = (decimal)60.83, Low = (decimal)60.09, Close = (decimal)60.1, QuoteDate = Convert.ToDateTime("10/27/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.81, High = (decimal)61.2, Low = (decimal)60.47, Close = (decimal)60.63, QuoteDate = Convert.ToDateTime("10/26/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.85, High = (decimal)61.37, Low = (decimal)60.8, Close = (decimal)60.99, QuoteDate = Convert.ToDateTime("10/25/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)59.94, High = (decimal)61, Low = (decimal)59.93, Close = (decimal)61, QuoteDate = Convert.ToDateTime("10/24/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)60.28, High = (decimal)60.45, Low = (decimal)59.49, Close = (decimal)59.66, QuoteDate = Convert.ToDateTime("10/21/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.5, High = (decimal)57.52, Low = (decimal)56.66, Close = (decimal)57.25, QuoteDate = Convert.ToDateTime("10/20/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.47, High = (decimal)57.84, Low = (decimal)57.4, Close = (decimal)57.53, QuoteDate = Convert.ToDateTime("10/19/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.53, High = (decimal)57.95, Low = (decimal)57.41, Close = (decimal)57.66, QuoteDate = Convert.ToDateTime("10/18/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.36, High = (decimal)57.46, Low = (decimal)56.87, Close = (decimal)57.22, QuoteDate = Convert.ToDateTime("10/17/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.12, High = (decimal)57.74, Low = (decimal)57.12, Close = (decimal)57.42, QuoteDate = Convert.ToDateTime("10/14/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)56.7, High = (decimal)57.3, Low = (decimal)56.32, Close = (decimal)56.92, QuoteDate = Convert.ToDateTime("10/13/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.11, High = (decimal)57.27, Low = (decimal)56.4, Close = (decimal)57.11, QuoteDate = Convert.ToDateTime("10/12/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.89, High = (decimal)58.02, Low = (decimal)56.89, Close = (decimal)57.19, QuoteDate = Convert.ToDateTime("10/11/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.91, High = (decimal)58.39, Low = (decimal)57.87, Close = (decimal)58.04, QuoteDate = Convert.ToDateTime("10/10/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.85, High = (decimal)57.98, Low = (decimal)57.42, Close = (decimal)57.8, QuoteDate = Convert.ToDateTime("10/07/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.74, High = (decimal)57.86, Low = (decimal)57.28, Close = (decimal)57.74, QuoteDate = Convert.ToDateTime("10/06/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.29, High = (decimal)57.96, Low = (decimal)57.26, Close = (decimal)57.64, QuoteDate = Convert.ToDateTime("10/05/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.27, High = (decimal)57.6, Low = (decimal)56.97, Close = (decimal)57.24, QuoteDate = Convert.ToDateTime("10/04/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.41, High = (decimal)57.55, Low = (decimal)57.06, Close = (decimal)57.42, QuoteDate = Convert.ToDateTime("10/03/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.57, High = (decimal)57.77, Low = (decimal)57.34, Close = (decimal)57.6, QuoteDate = Convert.ToDateTime("09/30/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.81, High = (decimal)58.17, Low = (decimal)57.21, Close = (decimal)57.4, QuoteDate = Convert.ToDateTime("09/29/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.88, High = (decimal)58.06, Low = (decimal)57.67, Close = (decimal)58.03, QuoteDate = Convert.ToDateTime("09/28/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)56.93, High = (decimal)58.06, Low = (decimal)56.68, Close = (decimal)57.95, QuoteDate = Convert.ToDateTime("09/27/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.08, High = (decimal)57.14, Low = (decimal)56.83, Close = (decimal)56.9, QuoteDate = Convert.ToDateTime("09/26/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.87, High = (decimal)57.91, Low = (decimal)57.38, Close = (decimal)57.43, QuoteDate = Convert.ToDateTime("09/23/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.92, High = (decimal)58, Low = (decimal)57.63, Close = (decimal)57.82, QuoteDate = Convert.ToDateTime("09/22/16") }; lst.Add(quote);
            quote = new StockPrice { Open = (decimal)57.51, High = (decimal)57.85, Low = (decimal)57.08, Close = (decimal)57.76, QuoteDate = Convert.ToDateTime("09/21/16") }; lst.Add(quote);

            return lst;
        }

        public decimal CalculateMaxProfit(List<StockPrice> stockPrices)
        {
            decimal minSharePrice = stockPrices[0].Low;
            decimal maxSharePrice = 0;
            decimal MaxProfit = 0;

            decimal shareBuyValue = stockPrices[0].Low;
            decimal shareSellValue = stockPrices[0].High;

            DateTime shareBuyDate = DateTime.MinValue;
            DateTime shareSellDate = DateTime.MinValue;

            for (int i = 0; i < stockPrices.Count; i++)
            {
                if (stockPrices[i].Low < minSharePrice)
                {
                    minSharePrice = stockPrices[i].Low;

                    //If we update the min value of share, we need to reset the Max value 
                    //since we have to buy first, and sell only afterwards.
                    maxSharePrice = 0;
                }
                else
                {
                    maxSharePrice = stockPrices[i].High;
                }

                    //Check if max and min share value of stock are going to give us better profit
                    //If yes - save those share values.
                if (MaxProfit < (maxSharePrice - minSharePrice))
                {
                    shareBuyValue = minSharePrice;
                    shareSellValue = maxSharePrice;
                }

                MaxProfit = Math.Max(MaxProfit, maxSharePrice - minSharePrice);
            }

            string result = string.Format("Buy stock on at ${0} and sell on at ${1}, to obtain a profit of ${2}.", shareBuyValue, shareSellValue, MaxProfit);

            Console.WriteLine(result);

            return MaxProfit;  
        } 
    }

}