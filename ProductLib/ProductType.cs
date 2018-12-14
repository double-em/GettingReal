using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLib
{
    public class ProductType
    {
        public int Id { get; }
        public string Navn { get; }
        public int Amount { get; set; }
        public int Bestilt { get; set; }
        public string Placering { get; }

        public ProductType(int id, string navn, string placering, int amount, int bestilt = 0)
        {
            Navn = navn;
            Placering = placering;
            Amount = amount;
            Bestilt = bestilt;
            Id = id;
        }

        public override string ToString()
        {
            return Id + "\t" + Utility.LengthenString(Navn) + "\t" + Amount + "\t" + Bestilt + "\t" + Placering;
        }
    }
}
