﻿using System;
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
            Console.WriteLine("Task 1 - execute tasks asynchronously, with priority set.");

            var token = new CancellationTokenSource();
            var PriorityCollection = new PriorityCollection();

            var param = new Dictionary<string, object> { { "Param1", 1 } };
            Query1 qryHigh = new Query1 { Statement = "SELECT * FROM All Kinds Of Tasks", Params = param, Priority1 = Priority1.High };
            Query1 qryHigh1 = new Query1 { Statement = "SELECT * FROM All Kinds Of Tasks", Params = param, Priority1 = Priority1.High };
            Query1 qryHigh2 = new Query1 { Statement = "SELECT * FROM All Kinds Of Tasks", Params = param, Priority1 = Priority1.High };

            Query1 qryLow = new Query1 { Statement = "SELECT * FROM All Kinds Of Tasks", Params = param, Priority1 = Priority1.Low };
            Query1 qryLow1 = new Query1 { Statement = "SELECT * FROM All Kinds Of Tasks", Params = param, Priority1 = Priority1.Low };
            Query1 qryLow2 = new Query1 { Statement = "SELECT * FROM All Kinds Of Tasks", Params = param, Priority1 = Priority1.Low };

            Query1 qryMed = new Query1 { Statement = "SELECT * FROM All Kinds Of Tasks", Params = param, Priority1 = Priority1.Middle };
            Query1 qryMed1 = new Query1 { Statement = "SELECT * FROM All Kinds Of Tasks", Params = param, Priority1 = Priority1.Middle };
            Query1 qryMed2 = new Query1 { Statement = "SELECT * FROM All Kinds Of Tasks", Params = param, Priority1 = Priority1.Middle };


            Task.Factory.ContinueWhenAll(new[]
                                             {
                                                Task.Factory.StartNew(() => PriorityCollection.ProcessItems(token.Token)),
                                                Task.Factory.StartNew(() => PriorityCollection.Publish(qryHigh)),
                                                Task.Factory.StartNew(() => PriorityCollection.Publish(qryLow)),

                                                Task.Factory.StartNew(() => PriorityCollection.Publish(qryMed2)),
                                                Task.Factory.StartNew(() => PriorityCollection.Publish(qryHigh1)),
                                                Task.Factory.StartNew(() => PriorityCollection.Publish(qryLow1)),


                                                 Task.Factory.StartNew(() => PriorityCollection.Publish(qryMed1)),
                                                Task.Factory.StartNew(() => PriorityCollection.Publish(qryHigh2)),

                                                Task.Factory.StartNew(() => PriorityCollection.Publish(qryLow2)),
                                                Task.Factory.StartNew(() => PriorityCollection.Publish(qryMed)),



                                             }, tasks => { });
            Thread.Sleep(5000);
            token.Cancel();


             
            //Threading t = new Threading();

           
            //Query qry = new Query { Statement = "SELECT * FROM All Kinds Of Tasks", Params = param };


            //t.Schedule(qry, Priority.Low); 
            //t.Schedule(qry, Priority.Medium); 
            //t.Schedule(qry, Priority.High);

            //t.Start();

            //ConcurrentQueue<Action> _writeLineActions = new ConcurrentQueue<Action>(); // used to avoid Console.WriteLine before everything had completed

            //var cancelSource = new CancellationTokenSource();

            //// Define schedulers
            //var lPriorityScheduler = new CustomPriorityScheduler(
            //    ThreadPriority.Lowest,
            //    "Lowest Thread",
            //    Environment.ProcessorCount);

            //var mPriorityScheduler = new CustomPriorityScheduler(
            //    ThreadPriority.Normal,
            //    "Medium Thread",
            //    Environment.ProcessorCount);


            //var hPriorityScheduler = new CustomPriorityScheduler(
            //    ThreadPriority.Normal,
            //    "Highest Thread",
            //    Environment.ProcessorCount);

            //List<Task> waitingList = new List<Task>();
            //for (int i = 0; i < 10; i++) // race
            //{
            //    // schedule task on lowest priority
            //    Task tCustom = Task.Factory.StartNew(() => WriteThreadInfo(Thread.CurrentThread), cancelSource.Token, TaskCreationOptions.None, lPriorityScheduler);
            //    waitingList.Add(tCustom);

            //    // schedule task on normal priority
            //    tCustom = Task.Factory.StartNew(() => WriteThreadInfo(Thread.CurrentThread), cancelSource.Token, TaskCreationOptions.None, mPriorityScheduler);
            //    waitingList.Add(tCustom);

            //    // schedule task on highest priority
            //    tCustom = Task.Factory.StartNew(() => WriteThreadInfo(Thread.CurrentThread), cancelSource.Token, TaskCreationOptions.None, hPriorityScheduler);
            //    waitingList.Add(tCustom);
            //}

            //Task.WaitAll(waitingList.ToArray());



            //Console.ReadKey();


            Console.WriteLine(Environment.NewLine + "Task 2 - calculate the maximum profit - buy once, sell once." + Environment.NewLine);

            TradingCalcs tc = new TradingCalcs();

            Console.WriteLine("Sample data: MSFT quotes for year.");

            List <StockPrice> lstSampleData = TradingCalcs.GetTestData();

            tc.CalculateMaxProfit(lstSampleData);


            Console.WriteLine(Environment.NewLine + "Task 3 - Create SQL code to calculate 95th percentile of requests duration over some date range for each distinct URI in the activity log.");
             
            Console.WriteLine("Select distinct uri, PERCENTILE_DISC(0.5) WITHIN GROUP (ORDER BY duration_ms)");
            Console.WriteLine("OVER(PARTITION BY uri) AS duration from client_activity");
            Console.WriteLine("WHERE timestamp BETWEEN @StartDate AND @EndDate");
            Console.WriteLine("ORDER by uri ASC"); 
            Console.ReadKey();
        }


        //[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        //private static void SpinWork(int milliseconds)
        //{
        //    var sw = Stopwatch.StartNew();
        //    while (sw.ElapsedMilliseconds < milliseconds) ; // spin for the duration
        //    sw.Stop();
        //}

        //private static Action<string> WriteThreadInfo(Thread t)
        //{
        //    string s = string.Format("Name = {0}, Priority = {1}, Is pool = {2}, id = {3}",
        //        t.Name,
        //        t.Priority,
        //        t.IsThreadPoolThread,
        //        t.ManagedThreadId);

        //    SpinWork(SPIN_WORK_DURATION);

        //    Console.WriteLine(s);

        //    return null;
        //}
         
    }
}
