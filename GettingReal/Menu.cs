using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingReal
{
    public class Menu
    {
        Controller control = new Controller();
        public void ShowMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Lagersystem - Per Olsen Automobiler\n");
                Console.WriteLine("\t1. Opret produkt");
                Console.WriteLine("\t2. Indskriv produkt");
                Console.WriteLine("\t3. Slet produkt");
                Console.WriteLine("\t4. Tjek lagerbeholding");
                Console.WriteLine("\t5. Indskriv bestilt vare");
                Console.WriteLine("");
                Console.WriteLine("\t0. Exit");
                Console.Write("\nVælg et punkt fra menuen: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateProduct();
                        break;

                    case "2":
                        UpdateNumberOFProducts();
                        break;

                    case "3":
                        RemoveProduct();
                        break;

                    case "4":
                        GetAllProducts();
                        break;

                    case "5":
                        ProductOrdered();
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Dette er ikke en valgmulighed");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        void ProductOrdered()
        {
            throw new NotImplementedException();
        }

        void RemoveProduct()
        {
            throw new NotImplementedException();
        }

        void GetAllProducts()
        {
            Console.Clear();
            Console.WriteLine();
            List<List<string>> products = control.GetAllProducts();


            if (products[0][0] == "No rows found")
            {
                Console.WriteLine("Ingen rækker blev fundet");
            }
            else
            {
                for (int i = 0; i < products.Count; i++)
                {
                    for (int j = 0; j < products[i].Count; j++)
                    {
                        Console.Write(products[i][j] + "\t\t\t");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Tryk på en knap for at vende tilbage...");
            Console.ReadKey(true);
        }

        void UpdateNumberOFProducts()
        {
            throw new NotImplementedException();
        }

        void CreateProduct()
        {
            Console.Write("Produkt navn: ");
            string productName = Console.ReadLine();

            Console.Write("Antal: ");
            string amountTemp = Console.ReadLine();
            int.TryParse(amountTemp, out int amount);

            Console.Write("Placering: ");
            string placement = Console.ReadLine();

            try
            {
                if (control.CreateProduct(productName, amount, placement))
                {
                    Console.WriteLine("Produktet: " + productName + " blev tilføjet.");
                }
                else
                {
                    Console.WriteLine("Produktet kunne ikke tilføjes");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Kunne ikke forbinde til databasen...");
            }

            Console.ReadKey(true);
        }
    }
}
