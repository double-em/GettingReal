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
        private ProductRepo products;
        private OrderRepo orders;

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

        internal void CreateProduct(string productName, int amount, string placement)
        {
            products.CreateProduct(productName, amount, placement);
        }

        internal List<ProductType> GetAllProducts()
        {
            return products.products;
        }

        internal bool RemoveProduct(int id)
        {
            return products.RemoveProduct(id);
        }

        internal bool ProductOrdered(int productId, int orderNumber, string date)
        {
            ProductType product = products.GetProduct(productId);
            return orders.ProductOrdered(product, orderNumber, date);
        }

        internal bool UpdateNumberOfProducts(int id, int amount)
        {
            return products.UpdateNumberOfProducts(id, amount);
        }
    }
}
