using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TradingApplication
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("* Task 1 - execute tasks asynchronously, with priority set.");    
                     
            var pt = new PriorityThreading(); 
            pt.ScheduleBatchToRun();    //Load test data + run Schedule method            
            pt.Start();

            Thread.Sleep(2000);
            pt.Stop();

            Thread.Sleep(5000);
              

            Console.WriteLine(Environment.NewLine + "* Task 2 - calculate the maximum profit - buy once, sell once.");

            TradingCalcs tc = new TradingCalcs();
            Console.WriteLine("Sample data: MSFT quotes for year.");

            List <StockPrice> lstSampleData = TradingCalcs.GetTestData();
            tc.CalculateMaxProfit(lstSampleData);
             
            Console.WriteLine(Environment.NewLine + "* Task 3 - Create SQL code to calculate 95th percentile of requests duration over some date range for each distinct URI in the activity log.");
             
            Console.WriteLine("Select distinct uri, PERCENTILE_DISC(0.5) WITHIN GROUP (ORDER BY duration_ms)");
            Console.WriteLine("OVER(PARTITION BY uri) AS duration from client_activity");
            Console.WriteLine("WHERE timestamp BETWEEN @StartDate AND @EndDate");
            Console.WriteLine("ORDER by uri ASC"); 
            Console.ReadKey();
        } 
    }
}
