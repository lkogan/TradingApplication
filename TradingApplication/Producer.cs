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
    public class Producer : IQueryExecutor, IDisposable
    {
        private CancellationTokenSource _Cts;
        private Random _Random = new Random();
        private int _WorkCounter = 0;
        private BlockingCollection<Task<String>> _Workers;
        private Task _WorkProducer;
          
        public Producer()
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
            _WorkProducer = Task.Factory.StartNew(() => Run(_Cts.Token));            
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


            for (int i = 0; i < 5; i++)
            {
                if (!token.IsCancellationRequested)
                {
                    var worker = StartNewWorker();
                    _Workers.Add(worker);
                    Task.Delay(100);
                }               
            }


            for (int i = 0; i < 10; i++)
            {
                if (!token.IsCancellationRequested)
                {
                    var worker = StartAnotherWorker();
                    _Workers.Add(worker);
                    Task.Delay(100);
                }
            }

            _Workers.CompleteAdding();
            _Workers = new BlockingCollection<Task<String>>();
        }

        private Task<String> StartNewWorker()
        {
            return Task.Factory.StartNew<String>(Worker);
 
        }

        private Task<String> StartAnotherWorker()
        {
            return Task.Factory.StartNew<String>(AnotherWorker);
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

        private String AnotherWorker()
        {
            var result = GetRandomString();

            try
            {
                // Change the thread priority to the one required.
                Thread.CurrentThread.Priority = ThreadPriority.Highest;

                var workerId = Interlocked.Increment(ref _WorkCounter);
                var neededTime = TimeSpan.FromSeconds(_Random.NextDouble() * 5);
                Console.WriteLine("Worker " + workerId + " starts in " + neededTime);
                Task.Delay(neededTime).Wait();

                Console.WriteLine("Worker " + workerId + " finished with " + result);
            }
            finally
            {
                // Restore the thread default priority.
                Thread.CurrentThread.Priority = ThreadPriority.Normal;
            }

            return result;
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
                    object parameter = (SqlParameter)item.Value;
                     
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
