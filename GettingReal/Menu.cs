using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingReal
{
    public class Menu
    {
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

                        break;

                    case "2":

                        break;

                    case "3":

                        break;

                    case "4":

                        break;

                    case "5":

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

    }
}
