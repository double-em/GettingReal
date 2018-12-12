using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLib
{
    public class ProductType
    {
        public int Id { get; }
        public string Navn { get; }
        public string Placering { get; }
        public int Amount { get; }

        public ProductType(int id, string navn, string placering, int amount)
        {
            Navn = navn;
            Placering = placering;
            Amount = amount;
            Id = id;
        }
    }
}
