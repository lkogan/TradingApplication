using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TradingApplication
{

    public class Query
    {
        public string Statement { get; set; }

        public IDictionary<string, object> Params { get; set; }

        public Priority Priority { get; set; }
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

    public class PriorityThreading : IQueryExecutor 
    {
        private readonly BlockingCollection<Query> low = new BlockingCollection<Query>();
        private readonly BlockingCollection<Query> middle = new BlockingCollection<Query>();
        private readonly BlockingCollection<Query> high = new BlockingCollection<Query>();
        private readonly BlockingCollection<Guid> main = new BlockingCollection<Guid>();
        private readonly BlockingCollection<Query>[] queue;

        private readonly Dictionary<Priority, BlockingCollection<Query>> PriorityMap = new Dictionary<Priority, BlockingCollection<Query>>();

        public CancellationTokenSource _Cts;
          
        public PriorityThreading()
        {
            queue = new[] { high, middle, low }; 
            PriorityMap.Add(Priority.Low, low);
            PriorityMap.Add(Priority.Medium, middle);
            PriorityMap.Add(Priority.High, high);
        }

        public void Start()
        {
            if (_Cts != null)
                throw new InvalidOperationException("Producer has already been started.");

            _Cts = new CancellationTokenSource();

            Task.Factory.StartNew(() => ExecuteQueries(_Cts.Token)); 
        }

        public void Stop()
        {
            var cts = _Cts;

            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
                _Cts = null;
            }

            main.CompleteAdding();
        }


        /*
        * Schedule a new query for execution with corresponding priority.
        * This method should not be blocked by query execution.
        * Queries with higher priority should be executed before the ones with lower priority.
        * If a query with a lower priority is being executed at the time, it may not be canceled (or re-scheduled).
        */

        public void Schedule(Query query, Priority priority)
        {
            //Made priority a property of query class itself, for convenience
            query.Priority = priority;

            var guid = Guid.NewGuid();
            PriorityMap[priority].Add(query);
            main.Add(guid);

            Console.WriteLine("Adding query: " + query.Statement + " Priority: " + query.Priority); 
        }
         
        public void ScheduleBatchToRun()
        {
            //Test data
            var param = new Dictionary<string, object> { { "Param1", 1 } };
            Query qryHigh = new Query { Statement = "SELECT TOP 1 * FROM AllKindsOfTasks", Params = param };
            Query qryHigh1 = new Query { Statement = "SELECT TOP 2 * FROM AllKindsOfTasks", Params = param };
            Query qryHigh2 = new Query { Statement = "SELECT TOP 3 * FROM AllKindsOfTasks", Params = param };

            Query qryMed = new Query { Statement = "SELECT TOP 4 * FROM AllKindsOfTasks", Params = param };
            Query qryMed1 = new Query { Statement = "SELECT TOP 5 * FROM AllKindsOfTasks", Params = param };
            Query qryMed2 = new Query { Statement = "SELECT TOP 6 * FROM AllKindsOfTasks", Params = param };

            Query qryLow = new Query { Statement = "SELECT TOP 7 * FROM AllKindsOfTasks", Params = param };
            Query qryLow1 = new Query { Statement = "SELECT TOP 8 * FROM AllKindsOfTasks", Params = param };
            Query qryLow2 = new Query { Statement = "SELECT TOP 9 * FROM AllKindsOfTasks", Params = param };
             

            Task.Factory.ContinueWhenAll(new[]
                            {
                                Task.Factory.StartNew(() => Schedule(qryHigh, Priority.High)),
                                Task.Factory.StartNew(() => Schedule(qryLow, Priority.Low)),
                                Task.Factory.StartNew(() => Schedule(qryMed2, Priority.Medium)),
                                Task.Factory.StartNew(() => Schedule(qryHigh1, Priority.High)),
                                Task.Factory.StartNew(() => Schedule(qryLow1, Priority.Low)),
                                Task.Factory.StartNew(() => Schedule(qryMed1, Priority.Medium)),
                                Task.Factory.StartNew(() => Schedule(qryHigh2, Priority.High)),
                                Task.Factory.StartNew(() => Schedule(qryLow2, Priority.Low)),
                                Task.Factory.StartNew(() => Schedule(qryMed, Priority.Medium)),
                            }, tasks => { });

        }

        public void ExecuteQueries(CancellationToken token)
        {
            try
            {
                foreach (var guid in main.GetConsumingEnumerable(token))
                {
                    Query query;
                    BlockingCollection<Query>.TakeFromAny(queue, out query);
                    var Priority = query.Priority;
                    Console.Out.WriteLine("Query with Priority {0} is processed: " + query.Statement, Priority);

                    RunQuery(query);
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.Out.WriteLine("Batch execution has been cancelled");
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
