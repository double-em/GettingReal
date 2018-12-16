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
        List<Order> orders = new List<Order>();
        public OrderRepo(List<ProductType> products)
        {
            Setup();
        }

        public void Setup()
        {
            var orderedProducts = GetOrderedProducts();
            foreach (List<string> o in orderedProducts)
            {
                if (int.TryParse(o[0], out int id) && bool.TryParse(o[2], out bool aktiv))
                {
                    orders.Add(new Order(id, o[1], aktiv));
                }
            }

        }

        public Order GetOrder(int id)
        {
            foreach (Order o in orders)
            {
                if (o.OrderId == id)
                {
                    return o;
                }
            }
            return null;
        }

        public List<List<string>> GetOrderedProducts()
        {
            try
            {
                using (SqlConnection connection = GetDatabaseConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spGetOrderedProducts", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        return ListResult(cmd);
                    }
                }
            }
            catch (SqlException)
            {
                throw  new SqlConnectionException();
            }
        }

        public bool ProductOrdered(ProductType product, int orderNumber, string date)
        {
            try
            {
                using (SqlConnection connection = GetDatabaseConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spProductOrdered", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = product.Id;
                        cmd.Parameters.Add("@Ordrenummer", SqlDbType.Int).Value = orderNumber;
                        cmd.Parameters.Add("@OrdreDato", SqlDbType.NChar).Value = date;

                        connection.Open();

                        int countRowsAffected = cmd.ExecuteNonQuery();
                        if (countRowsAffected == 0) return false;
                        Order order = new Order(orderNumber, date, true);
                        order.AddProduct(product);
                        product.Bestilt++;
                        orders.Add(order);
                        return true;
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
