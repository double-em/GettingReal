using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductLib;

namespace GettingReal
{
    public class Controller
    {
        ProductRepo products;
        OrderRepo orders;

        public Controller()
        {
            try
            {
                products = new ProductRepo();
                orders = new OrderRepo(products.products);
            }
            catch (SqlConnectionException e)
            {
                WriteErrorToLog(e);
                throw e;
            }
        }

        void WriteErrorToLog(Exception e)
        {
            string path = "errorLog.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(DateTime.Now.ToString());
                writer.WriteLine(e.ToString());
                writer.WriteLine();
            }
        }

        public void CreateProduct(string productName, int amount, string placement)
        {
            products.CreateProduct(productName, amount, placement);
        }

        public List<ProductType> GetAllProducts()
        {
            return products.products;
        }

        public bool RemoveProduct(int id)
        {
            return products.RemoveProduct(id);
        }

        public bool ProductOrdered(int productId, int orderNumber, string date)
        {
            ProductType product = products.GetProduct(productId);
            return orders.ProductOrdered(product, orderNumber, date);
        }

        public bool UpdateNumberOfProducts(int id, int amount)
        {
            return products.UpdateNumberOfProducts(id, amount);
        }

        public List<ProductType> SearchProducts(string searched)
        {
            return products.SearchProducts(searched);
        }

        public List<Order> GetOrders()
        {
            return orders.orders;
        }

        public bool FinishOrder(int id)
        {
            return orders.FinishOrder(id);
        }
    }
}
