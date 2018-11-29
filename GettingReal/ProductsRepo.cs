using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace GettingReal
{
    public class ProductsRepo : DatabaseRepo
    {
        internal bool CreateProduct(string productName, string placement)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("InsertProduct"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ProductName", SqlDbType.NChar).Value = productName;
                    cmd.Parameters.Add("@Placement", SqlDbType.NChar).Value = placement;

                    connection.Open();

                    int countRowsAffected = cmd.ExecuteNonQuery();
                    if (countRowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        internal List<List<string>> GetAllPrducts()
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetAllProducts"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    return ListResult(cmd);
                }
            }
        }
    }
}
