using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLib
{
    internal class ProductType : ProductRepo
    {
        public string Navn { get; }
        public string Placering { get; }
        public int Amount { get; }

        public ProductType(string navn, string placering, int amount)
        {
            Navn = navn;
            Placering = placering;
            Amount = amount;
        }
    }
}
