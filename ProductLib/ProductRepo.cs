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

        void Setup()
        {
            var allProducts = GetAllProducts();
            foreach (List<string> product in allProducts)
            {
                bool idParse = int.TryParse(product[0], out int id);
                bool amountParse = int.TryParse(product[2], out int amount);
                bool orderedParse = int.TryParse(product[3], out int ordered);

                if (idParse && amountParse && orderedParse)
                {
                    products.Add(new ProductType(id, product[1], product[4], amount, ordered));
                }
            }
        }

        public ProductType GetProduct(int id)
        {
            foreach (ProductType p in products)
            {
                if (p.Id == id)
                {
                    return p;
                }
            }
            return null;
        }

        public void CreateProduct(string productName, int amount, string placement)
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

                        var list = ListResult(cmd)[0][0];
                        int.TryParse(list, out int ID);
                        ProductType newProduct = new ProductType(ID, productName, placement, amount);
                        products.Add(newProduct);
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
                        if (countRowsAffected == 0) return false;
                        GetProduct(id).Amount = amount;
                        return true;
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

                        // Inverted If Statement
                        if (countRowsAffected == 0) return false;
                        products.Remove(GetProduct(id));
                        return true;
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

        public List<ProductType> SearchProducts(string searched)
        {
            List<ProductType> resultList = new List<ProductType>();
            foreach (ProductType p in products)
            {
                searched = searched.ToLower();
                if (p.Id.ToString().StartsWith(searched) || p.Navn.ToLower().Contains(searched))
                {
                        resultList.Add(p);
                }
            }
            return resultList;
        }
    }
}
