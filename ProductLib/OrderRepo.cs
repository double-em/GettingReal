using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLib
{
    public class OrderRepo : DatabaseRepo
    {
        public bool ProductOrdered(int productId, int orderNumber, string date)
        {
            try
            {
                using (SqlConnection connection = GetDatabaseConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spGetOrderedProducts", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = productId;
                        cmd.Parameters.Add("@Ordrenummer", SqlDbType.Int).Value = orderNumber;
                        cmd.Parameters.Add("@OrdreDato", SqlDbType.NChar).Value = date;

                        connection.Open();

                        int countRowsAffected = cmd.ExecuteNonQuery();
                        return countRowsAffected > 0;
                    }
                }
            }
            catch (SqlException)
            {
                throw new SqlConnectionException();
            }
        }
    }
}
