using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingReal
{
    public class Controller
    {
        ProductsRepo products = new ProductsRepo();
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
            return products.GetAllPrducts();
        }

        internal bool RemoveProduct(int id)
        {
            return products.RemoveProduct(id);
        }

        internal bool ProductOrdered(int productID, int orderNumber, string date)
        {
            return products.ProductOrdered(productID, orderNumber, date);
        }

        public string LengthenString(string Text)
        {
            if (Text.Length <= 32)
            {
                for (int i = Text.Length; i <= 32; i++)
                {
                    Text += " ";
                }
            }
            return Text;
        }
    }
}
