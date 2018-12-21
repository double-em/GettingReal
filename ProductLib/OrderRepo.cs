using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLib
{
    public class OrderRepo : DatabaseRepo
    {
        public List<Order> orders = new List<Order>();
        public OrderRepo(List<ProductType> productsList)
        {
            Setup(productsList);
        }

        public void Setup(List<ProductType> productsList)
        {
            var orderedProducts = GetOrderedProducts();
            foreach (List<string> o in orderedProducts)
            {
                if (int.TryParse(o[0], out int id) && bool.TryParse(o[2], out bool aktiv))
                {
                    Order order = new Order(id, o[1], aktiv);
                    orders.Add(order);
                    List<ProductType> products = GetProductsRelatedToOrder(id, productsList);
                    foreach (ProductType product in products)
                    {
                        order.AddProduct(product);
                    }
                }
            }

        }

        List<ProductType> GetProductsRelatedToOrder(int orderId, List<ProductType> productsList)
        {
            try
            {
                using (SqlConnection connection = GetDatabaseConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spGetOrderRelatedProducts", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = orderId;

                        connection.Open();

                        List<List<string>> list =  ListResult(cmd);
                        List<int> ids = new List<int>();
                        for (int i = 0; i < list.Count; i++)
                        {
                            int.TryParse(list[i][0], out int idResult);
                            ids.Add(idResult);
                        }

                        List<ProductType> resultList = new List<ProductType>();
                        foreach (ProductType p in productsList)
                        {
                            foreach (int id in ids)
                            {
                                if (p.Id == id)
                                {
                                    resultList.Add(p);
                                }
                            }
                        }
                        return resultList;
                    }
                }
            }
            catch (SqlException)
            {
                throw new SqlConnectionException();
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

        public bool FinishOrder(int id)
        {
            try
            {
                using (SqlConnection connection = GetDatabaseConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("spOrderStateChange", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                        connection.Open();

                        int countRowsAffected = cmd.ExecuteNonQuery();
                        if (countRowsAffected == 0) return false;
                        Order order = GetOrder(id);
                        order.Aktiv = false;
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
