using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLib
{
    public class Order
    {
        public List<ProductType> products = new List<ProductType>();
        public int OrderId { get; }
        public bool Aktiv { get; set; }

        public string Dato { get; }

        public Order(int orderId, string dato, bool aktiv)
        {
            OrderId = orderId;
            Dato = dato;
            Aktiv = aktiv;
        }

        public void AddProduct(ProductType product)
        {
            products.Add(product);
        }
    }

}