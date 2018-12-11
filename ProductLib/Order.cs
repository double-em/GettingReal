using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLib
{
    class Order : OrderRepo
    {
        List<ProductType> products = new List<ProductType>();
        public int OrderId { get; }
        public bool Status { get; }

        public Order(int orderId, bool status)
        {
            OrderId = orderId;
            Status = status;
        }
    }

}