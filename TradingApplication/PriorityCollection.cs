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

    public class PriorityCollection : IQueryExecutor 
    {
        private readonly BlockingCollection<Query1> low = new BlockingCollection<Query1>();
        private readonly BlockingCollection<Query1> middle = new BlockingCollection<Query1>();
        private readonly BlockingCollection<Query1> high = new BlockingCollection<Query1>();
        private readonly BlockingCollection<Guid> main = new BlockingCollection<Guid>();
        private readonly BlockingCollection<Query1>[] queue;

        private readonly Dictionary<Priority1, BlockingCollection<Query1>> PriorityMap = new Dictionary<Priority1, BlockingCollection<Query1>>();

        private CancellationTokenSource _Cts;
          
        public PriorityCollection()
        {
            queue = new[] { high, middle, low }; 
            PriorityMap.Add(Priority1.Low, low);
            PriorityMap.Add(Priority1.Middle, middle);
            PriorityMap.Add(Priority1.High, high);
        }

        public void Start()
        {
            if (_Cts != null)
                throw new InvalidOperationException("Producer has already been started.");

            _Cts = new CancellationTokenSource();

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
            string strPriority = string.Empty;

            switch (priority)
            {
                case Priority.Low:
                    strPriority = Priority.Low.ToString();
                    tPriority = ThreadPriority.Lowest;
                    break;

                case Priority.Medium:
                    strPriority = Priority.Medium.ToString();
                    tPriority = ThreadPriority.Normal;
                    break;

                case Priority.High:
                    strPriority = Priority.High.ToString();
                    tPriority = ThreadPriority.Highest;
                    break;
            }

            var item = Task.Run(() =>
            {
                RunQuery(query);
                return strPriority;
            });
             
            //_Workers.CompleteAdding();


        }

        public void RunQuery(Query query)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            Console.WriteLine("Executing: " + query.Statement);

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


        public void Publish(Query1 item)
        {
            var guid = Guid.NewGuid();
            PriorityMap[item.Priority1].Add(item);
            main.Add(guid);
             //Console.Out.WriteLine("item with Priority1 {0} is published", item.Priority1);
        }

        public void ProcessItems(CancellationToken token)
        {
            foreach (var guid in main.GetConsumingEnumerable(token))
            {
                Query1 item;
                BlockingCollection<Query1>.TakeFromAny(queue, out item);
                var Priority1 = item.Priority1;
                Console.Out.WriteLine("item with Priority {0} is processed", Priority1);
                //TestList.Add(Priority1);
            }
        }
    }

    public enum Priority1
    {
        High,
        Middle,
        Low
    }

    public class Query1
    {
        public Priority1 Priority1 { get; set; }
        public string Statement { get; set; }
        public IDictionary<string, object> Params { get; set; }
    }

    public class PriorityCollectionTests
    { 
        public void should_process_items_by_their_priorities()
        {
            var token = new CancellationTokenSource();
            var PriorityCollection = new PriorityCollection();
            Task.Factory.ContinueWhenAll(new[]
                                             {
                                                 Task.Factory.StartNew(
                                                     () => PriorityCollection.ProcessItems(token.Token)),
                                                 GenerateItems(PriorityCollection, Priority1.Low),
                                                 GenerateItems(PriorityCollection, Priority1.High),
                                                 GenerateItems(PriorityCollection, Priority1.Middle)
                                             }, tasks => { });
            Thread.Sleep(5000);
            token.Cancel();

            //PriorityCollection.TestList.Should().ContainInOrder(
            //    Enumerable.Range(0, 10).Select(i => Priority1.High).Union(
            //        Enumerable.Range(0, 10).Select(i => Priority1.Middle)).Union(
            //            Enumerable.Range(0, 10).Select(i => Priority1.Low)));
        }

        private static Task GenerateItems(PriorityCollection PriorityCollection, Priority1 Priority1)
        {
            return Task.Factory.StartNew(() =>
                Enumerable.Range(0, 10).ToList().ForEach(i => PriorityCollection.Publish(new Query1 { Priority1 = Priority1 })));
        }
    }
}
