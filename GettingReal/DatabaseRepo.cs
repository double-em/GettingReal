using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace GettingReal
{
    public class DatabaseRepo
    {
        readonly string connectionString;

        public DatabaseRepo()
        {
            connectionString = "Server=EALSQL1.eal.local;Database=B_DB24_2018;user id=B_STUDENT24;pwd=B_OPENDB24;";
        }

        protected SqlConnection GetDatabaseConnection()
        {
            return new SqlConnection(connectionString);
        }

        protected List<List<string>> ListResult(SqlCommand cmd)
        {
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                List<List<string>> products = new List<List<string>>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        List<string> list = new List<string>();
                        foreach (var read in reader)
                        {
                            list.Add(CleanRecord(read));
                        }
                        products.Add(list);
                    }
                }
                else
                {
                    products.Add(new List<string> {"No rows found"});
                }

                return products;
            }
        }

        string CleanRecord(Object record)
        {
            return record.ToString().Replace(" ", string.Empty);
        }
    }
}
