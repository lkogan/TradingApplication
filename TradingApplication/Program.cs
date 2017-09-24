using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 2 - Calculate the maximum profit - buy once, sell once");

            TradingCalcs tc = new TradingCalcs();

            Console.WriteLine("Sample data: MSFT quotes for year");

            List <StockPrice> lstSampleData = TradingCalcs.GetTestData();

            tc.CalculateMaxProfit(lstSampleData);
             
            Console.ReadKey();
        }

    }
}
