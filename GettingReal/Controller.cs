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

        internal bool CreateProduct(string productName, int amount, string placement)
        {
            try
            {
                return products.CreateProduct(productName, amount, placement);
            }
            catch (Exception)
            {
                throw new Exception("Can't connect to the Database");
            }
        }

        internal List<List<string>> GetAllProducts()
        {
            List<List<string>> finalList = new List<List<string>>();
            foreach (var product in products.products)
            {
                finalList.Add(product.ToList());
            }
            return finalList;
        }

        internal bool RemoveProduct(int id)
        {
            return products.RemoveProduct(id);
        }

        internal bool ProductOrdered(int productId, int orderNumber, string date)
        {
            return orders.ProductOrdered(productId, orderNumber, date);
        }

        public string LengthenString(string text)
        {
            if (text.Length > 32) return text;
            for (int i = text.Length; i <= 32; i++)
            {
                text += " ";
            }
            return text;
        }

        internal bool UpdateNumberOfProducts(int id, int amount)
        {
            return products.UpdateNumberOfProducts(id, amount);
        }
    }
}
