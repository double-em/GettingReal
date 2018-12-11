using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLib
{
    public class ProductRepo : DatabaseRepo
    {
        List<ProductType> products = new List<ProductType>();

        public ProductRepo()
        {
            Setup();
        }

        private void Setup()
        {
            if (TestConnection())
            {
                var allProducts = GetAllProducts();
                foreach (var product in allProducts)
                {
                    int.TryParse(product[2], out int amount);
                    products.Add(new ProductType(product[0], product[1], amount));
                }
            }
            throw new Exception("Can't connect to the Database...");
        }

        public bool CreateProduct(string productName, int amount, string placement)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spInsertProduct", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReservedelsNavn", SqlDbType.NVarChar).Value = productName;
                    cmd.Parameters.Add("@Antal", SqlDbType.Int).Value = amount;
                    cmd.Parameters.Add("@Placering", SqlDbType.NVarChar).Value = placement;

                    connection.Open();

                    int countRowsAffected = cmd.ExecuteNonQuery();
                    return countRowsAffected > 0;
                }
            }
        }

        public bool UpdateNumberOfProducts(int id, int amount)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spUpdateNumberOFProducts", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@Antal", SqlDbType.Int).Value = amount;

                    connection.Open();

                    int countRowsAffected = cmd.ExecuteNonQuery();
                    return countRowsAffected > 0;
                }
            }
        }

        public bool RemoveProduct(int id)
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spRemoveProduct", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                    connection.Open();

                    int countRowsAffected = cmd.ExecuteNonQuery();
                    return countRowsAffected > 0;
                }
            }
        }

        public List<List<string>> GetAllProducts()
        {
            using (SqlConnection connection = GetDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand("spGetAllProducts", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    return ListResult(cmd);
                }
            }
        }
    }
}
