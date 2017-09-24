using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Concurrent;

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
    public class Threading : IQueryExecutor
    {
        private BlockingCollection<Task> _collection = new BlockingCollection<Task>();

        /*
        * Start IQueryExecutor asynchronously.
        */
        public void Start()
        {

        }

        /*
         * Stop IQueryExecutor asynchronously.
         */
        public void Stop()
        {

        }

        /*
         * Schedule a new query for execution with corresponding priority.
         * This method should not be blocked by query execution.
         * Queries with higher priority should be executed before the ones with lower priority.
         * If a query with a lower priority is being executed at the time, it may not be canceled (or re-scheduled).
         */
        public void Schedule(Query query, Priority priority)
        {

        }

        public void RunQuery(Query query)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

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
