using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Concurrent;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace TradingApplication
{
    public class Query
    {
        public string Statement { get; set; }

        public IDictionary<string, object> Params { get; set; }
    }

    public enum Priority
    {
        Low = 0,
        Medium = 1,
        High = 2
    }

    public interface IQueryExecutor
    {
        void Start();

        void Stop();

        void Schedule(Query query, Priority priority);
    }


    /*
     * Implement IQueryExecutor interface. Class should execute SQL queries against relational database of your choice.
     * 
     * Requerements:
     * - All queries executions should be consecutive (i.e. there should not be more than one query execution at a time).
     * - All methods should be thread safe
     */
    public class Threading : IQueryExecutor, IDisposable
    {
        private CancellationTokenSource _Cts;
        private Random _Random = new Random();
        private int _WorkCounter = 0;
        private BlockingCollection<Task<String>> _Workers;
        private Task _WorkProducer;

        private CustomPriorityScheduler hPriorityScheduler;
        private CustomPriorityScheduler mPriorityScheduler;
        private CustomPriorityScheduler lPriorityScheduler;

        private List<Task> waitingList = new List<Task>();

        private const int SPIN_WORK_DURATION = 1000;

        public Threading()
        {
            _Workers = new BlockingCollection<Task<String>>();
        }

        public IEnumerable<Task<String>> Workers
        {
            get { return _Workers.GetConsumingEnumerable(); }
        }

        public void Dispose()
        {
            Stop();
        }

        /*
        * Start IQueryExecutor asynchronously.
        */
        public void Start()
        {
            if (_Cts != null)
                throw new InvalidOperationException("Producer has already been started.");

            _Cts = new CancellationTokenSource();
             
            // Define schedulers
            lPriorityScheduler = new CustomPriorityScheduler(
                ThreadPriority.Lowest,
                "Lowest Thread",
                Environment.ProcessorCount);

            mPriorityScheduler = new CustomPriorityScheduler(
                ThreadPriority.Normal,
                "Medium Thread",
                Environment.ProcessorCount);


            hPriorityScheduler = new CustomPriorityScheduler(
                ThreadPriority.Normal,
                "Highest Thread",
                Environment.ProcessorCount);




          
            for (int i = 0; i < 10; i++) // race
            {
                // schedule task on lowest priority
                Task tCustom = Task.Factory.StartNew(() => WriteThreadInfo(Thread.CurrentThread), _Cts.Token, TaskCreationOptions.None, lPriorityScheduler);
                waitingList.Add(tCustom);

                // schedule task on normal priority
                tCustom = Task.Factory.StartNew(() => WriteThreadInfo(Thread.CurrentThread), _Cts.Token, TaskCreationOptions.None, mPriorityScheduler);
                waitingList.Add(tCustom);

                // schedule task on highest priority
                tCustom = Task.Factory.StartNew(() => WriteThreadInfo(Thread.CurrentThread), _Cts.Token, TaskCreationOptions.None, hPriorityScheduler);
                waitingList.Add(tCustom);
            }

            Task.WaitAll(waitingList.ToArray());






            //_WorkProducer = Task.Factory.StartNew(() => Run(_Cts.Token));            
        }

        /*
         * Stop IQueryExecutor asynchronously.
         */
        public void Stop()
        {
            var cts = _Cts;

            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
                _Cts = null;
            }

            _WorkProducer.Wait();
        }

        private String GetRandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = new String(Enumerable
                .Repeat(chars, 8)
                .Select(s => s[_Random.Next(s.Length)])
                .ToArray());

            result = "SQL ran: " + result;

            return result;
        }

        private void Run(CancellationToken token)
        {
            //while (!token.IsCancellationRequested)
            //{
            //    var worker = StartNewWorker();
            //    _Workers.Add(worker);
            //    Task.Delay(100);
            //}


            //for (int i = 0; i < 5; i++)
            //{
            //    if (!token.IsCancellationRequested)
            //    {
            //        var worker = StartNewWorker();
            //        _Workers.Add(worker);
            //        Task.Delay(100);
            //    }               
            //}

             
            _Workers.CompleteAdding();
            _Workers = new BlockingCollection<Task<String>>();
        }

        private Task<String> StartNewWorker()
        {
            return Task.Factory.StartNew<String>(Worker);
 
        }
         

        private String Worker()
        {
            var result = GetRandomString();

            try
            {
                // Change the thread priority to the one required.
                Thread.CurrentThread.Priority = ThreadPriority.Highest;

                var workerId = Interlocked.Increment(ref _WorkCounter);
                var neededTime = TimeSpan.FromSeconds(_Random.NextDouble() * 5);
                Console.WriteLine("High Priority Worker " + workerId + " starts in " + neededTime);
                Task.Delay(neededTime).Wait();

                Console.WriteLine("High Priority Worker " + workerId + " finished with " + result);
            }
            finally
            {
                // Restore the thread default priority.
                Thread.CurrentThread.Priority = ThreadPriority.Normal;
            } 

            return result;
        }


        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static void SpinWork(int milliseconds)
        {
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < milliseconds) ; // spin for the duration
            sw.Stop();
        }

        private static Action<string> WriteThreadInfo(Thread t)
        {
            string s = string.Format("Name = {0}, Priority = {1}, Is pool = {2}, id = {3}",
                t.Name,
                t.Priority,
                t.IsThreadPoolThread,
                t.ManagedThreadId);

            SpinWork(SPIN_WORK_DURATION);

            Console.WriteLine(s);

            return null;
        }

        /*
         * Schedule a new query for execution with corresponding priority.
         * This method should not be blocked by query execution.
         * Queries with higher priority should be executed before the ones with lower priority.
         * If a query with a lower priority is being executed at the time, it may not be canceled (or re-scheduled).
         */
        public void Schedule(Query query, Priority priority)
        {
            ThreadPriority tPriority = ThreadPriority.Normal;

            switch (priority)
            {
                case Priority.Low:
                    tPriority = ThreadPriority.Lowest;
                    break;

                case Priority.Medium:
                    tPriority = ThreadPriority.Normal;
                    break;

                case Priority.High:
                    tPriority = ThreadPriority.Highest;
                    break;
            }
              
           // Task worker = Task.Factory.StartNew(() => WriteThreadInfo(Thread.CurrentThread), cancelSource.Token, TaskCreationOptions.None, lPriorityScheduler);

            //_Workers.Add(worker);
        }

        public void RunQuery(Query query)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return;

            //CODE would run something similar to below
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlStatement = query.Statement;

                SqlCommand cmd = new SqlCommand(sqlStatement, conn);

                foreach (var item in query.Params)
                {
                    string paramName = item.Key;
                    object paramValue = item.Value;

                    cmd.Parameters.Add(new SqlParameter(paramName, paramValue));
                }
                 
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    Console.WriteLine(rdr.GetString(0));
                }
                else
                {
                    Console.WriteLine("not available yet");
                }
            }

        }
    } 
}
