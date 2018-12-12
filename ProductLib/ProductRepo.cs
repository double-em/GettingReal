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
        public List<ProductType> products = new List<ProductType>();

        public ProductRepo()
        {
            Setup();
        }

        private void Setup()
        {
            var allProducts = GetAllProducts();
            foreach (List<string> product in allProducts)
            {
                if (int.TryParse(product[0], out int id) && int.TryParse(product[2], out int amount))
                {
                    products.Add(new ProductType(id, product[1], product[4], amount));
                }
            }
        }

        public bool CreateProduct(string productName, int amount, string placement)
        {
            try
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
            catch (SqlException)
            {
                throw new SqlConnectionException();
            }
        }

        public bool UpdateNumberOfProducts(int id, int amount)
        {
            try
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
            catch (SqlException)
            {
                throw new SqlConnectionException();
            }
        }

        public bool RemoveProduct(int id)
        {
            try
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
            catch (SqlException)
            {
                throw new SqlConnectionException();
            }
        }

        public List<List<string>> GetAllProducts()
        {
            try
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
            catch (SqlException)
            {
                throw new SqlConnectionException();
            }
        }
    }
}
