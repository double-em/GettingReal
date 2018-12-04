using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace GettingReal
{
    public class DatabaseRepo
    {
        readonly string connectionString;

        public DatabaseRepo()
        {
            connectionString = "Server=EALSQL1.eal.local;Database=B_DB24_2018;User Id=B_STUDENT24;Password=B_OPENDB24;";
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
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            list.Add(CleanRecord(reader[i]));
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

        string CleanRecord(object record)
        {
            return record.ToString();
        }
    }
}
