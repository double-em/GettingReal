using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLib
{
    public class DatabaseRepo
    {
        readonly string connectionString;

        public DatabaseRepo()
        {
            connectionString = "Server=EALSQL1.eal.local;Database=B_DB24_2018;User Id=B_STUDENT24;Password=B_OPENDB24;";
        }

        public SqlConnection GetDatabaseConnection()
        {
            return new SqlConnection(connectionString);
        }

        public void TestConnection()
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException)
                {
                    throw new SqlConnectionException();
                }
            }
        }

        public List<List<string>> ListResult(SqlCommand cmd)
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
                    products.Add(new List<string> { "No rows found" });
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
