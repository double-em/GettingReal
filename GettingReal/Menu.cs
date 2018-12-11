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
                Console.WriteLine("Per Olsen Automobiler - Lagersystem\n");
                Console.WriteLine("\t1. Bestilling");
                Console.WriteLine("\t2. Lager");
                Console.WriteLine("\t3. Reservedele");
                Console.WriteLine("\n\t0. Afslut");
                Console.Write("\nVælg et punkt fra menuen: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        OrderMenu();
                        break;

                    case "2":
                        StorageMenu();
                        break;

                    case "3":
                        ProductMenu();
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

        void OrderMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Per Olsen Automobiler - Bestilling\n");
                Console.WriteLine("\t1. Indskriv bestilt produkt");
                Console.WriteLine("\n\t0. Tilbage");
                Console.Write("\nVælg et punkt fra menuen: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
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

        void StorageMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Per Olsen Automobiler - Lager\n");
                Console.WriteLine("\t1. Tjek lagerbeholding");
                Console.WriteLine("\t2. Indskriv produkt");
                Console.WriteLine("\n\t0. Tilbage");
                Console.Write("\nVælg et punkt fra menuen: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GetAllProducts();
                        Console.WriteLine("\nTryk på en knap for at vende tilbage...");
                        Console.ReadKey(true);
                        break;

                    case "2":
                        UpdateNumberOFProducts();
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

        void ProductMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Per Olsen Automobiler - Reservedele\n");
                Console.WriteLine("\t1. Opret produkt");
                Console.WriteLine("\t2. Slet produkt");
                Console.WriteLine("\n\t0. Tilbage");
                Console.Write("\nVælg et punkt fra menuen: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateProduct();
                        break;

                    case "2":
                        RemoveProduct();
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
            Console.Write("ID på det eksiterende produkt: ");
            string productIDTemp = Console.ReadLine();
            int.TryParse(productIDTemp, out int productID);

            Console.Write("Ordrenummer: ");
            string orderNumberTemp = Console.ReadLine();
            int.TryParse(orderNumberTemp, out int orderNumber);

            Console.Write("Ordre dato (DD-MM-YYYY): ");
            string date = Console.ReadLine();

            if (control.ProductOrdered(productID, orderNumber, date))
            {
                Console.WriteLine("Ordre med ordrenummer: " + orderNumber + " blev tilføjet...");
            }
            else
            {
                Console.WriteLine("Ordren kunne ikke oprettes...");
            }

            Console.ReadKey(true);
        }

        void RemoveProduct()
        {
            GetAllProducts();
            Console.Write("\nVælg produkt ID på produktet som skal fjernes: ");
            string IDTemp = Console.ReadLine();
            int.TryParse(IDTemp, out int ID);

            Console.Write("Er du sikker på at produktet med ID: " + ID + " skal fjernes? (Y/N)");
            string yesNo = Console.ReadLine().ToLower();

            if (yesNo == "y")
            {
                if (control.RemoveProduct(ID))
                {
                    Console.WriteLine("Produktet med ID: " + ID + " blev fjernet...");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("Produktet kunne ikke fjernes...");
                    Console.ReadKey(true);
                }
            }
            else
            {
                Console.WriteLine("Produktet kunne ikke fjernes...");
                Console.ReadKey(true);
            }
        }

        void GetAllProducts()
        {
            Console.Clear();
            Console.WriteLine("ID\t" + control.LengthenString("Navn") + "\tAntal\tBestilt\tPlacering");
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
                        if (j == 1)
                        {
                            Console.Write(control.LengthenString(products[i][j]) + "\t");
                        }
                        else
                        {
                            Console.Write(products[i][j] + "\t");
                        }
                        
                    }
                    Console.WriteLine();
                }
            }
        }

        void UpdateNumberOFProducts()
        {
            Console.Write("Indtast ID på produktet: ");
            string IDTemp = Console.ReadLine();
            int.TryParse(IDTemp, out int ID);

            Console.Write("Indtast det nye antal: ");
            string amountTemp = Console.ReadLine();
            int.TryParse(amountTemp, out int amount);

            if (control.UpdateNumberOFProducts(ID, amount))
            {
                Console.WriteLine("Antallet blev opdateret til " + amount + "...");
            }
            else
            {
                Console.WriteLine("Antallet kunne ikke opdateres...");
            }
            Console.ReadKey(true);
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
