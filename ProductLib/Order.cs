using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLib
{
    public class Order
    {
        List<ProductType> products = new List<ProductType>();
        public int OrderId { get; }
        public bool Status { get; }

        public string Dato { get; }

        public Order(int orderId, string dato, bool status)
        {
            OrderId = orderId;
            Dato = dato;
            Status = status;
        }

        public void AddProduct(ProductType product)
        {
            products.Add(product);
        }
    }

}