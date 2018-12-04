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
    }
}
