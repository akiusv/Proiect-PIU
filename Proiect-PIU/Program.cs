using Clase;
using Date;
using System;
using System.Collections.Generic;

namespace Interfata
{
    class Program
    {
        static void Main()
        {
            // Poti schimba aici cu AdministrareSoferi_Memorie() pentru a testa stocarea in RAM
            AdministrareSoferi adminSoferi = new AdministrareSoferi_FisierText();
            AdministrareVehicule_FisierText adminVehicule = new AdministrareVehicule_FisierText();

            while (true)
            {
                Console.WriteLine("\n=== MENIU ===");
                Console.WriteLine("1. Adauga Sofer");
                Console.WriteLine("2. Afiseaza Soferi");
                Console.WriteLine("3. Cauta Sofer");
                Console.WriteLine("4. Modifica Sofer");
                Console.WriteLine("5. Adauga Vehicul");
                Console.WriteLine("6. Afiseaza Vehicule");
                Console.WriteLine("X. Iesire");
                Console.WriteLine("Alege optiunea: ");

                string optiune = Console.ReadLine();

                switch (optiune.ToUpper())
                {
                    case "1":
                        Sofer s = CitesteSoferTastatura();
                        adminSoferi.AdaugaSofer(s);
                        Console.WriteLine("Sofer adaugat cu succes!");
                        break;

                    case "2":
                        
                        // Tema 3 - Afisarea din colectie

                        List<Sofer> listaSoferi = adminSoferi.GetSoferi();
                        foreach (var sofer in listaSoferi) sofer.Afisare();
                        break;


                    // Tema 3 - cautarea dupa criteriu

                    case "3":
                        Console.Write("Introdu numele soferului cautat: ");
                        string numeCautat = Console.ReadLine();
                        var rezultate = adminSoferi.CautaSoferDupaNume(numeCautat);
                        if (rezultate.Count > 0)
                            foreach (var rez in rezultate) rez.Afisare();
                        else
                            Console.WriteLine("Nu a fost gasit niciun sofer cu acest nume.");
                        break;

                    case "4":
                        Console.Write("Numele soferului pe care vrei sa il modifici: ");
                        string numeModif = Console.ReadLine();
                        Console.WriteLine("Introdu datele noi:");
                        Sofer soferNou = CitesteSoferTastatura();
                        adminSoferi.ModificaSofer(numeModif, soferNou);
                        Console.WriteLine("Sofer modificat!");
                        break;

                    case "5":
                        Vehicul v = CitesteVehiculTastatura();
                        adminVehicule.AdaugaVehicul(v);
                        Console.WriteLine("Vehicul adaugat cu succes!");
                        break;

                    case "6":
                        foreach (var vehicul in adminVehicule.GetVehicule()) vehicul.Afisare();
                        break;

                    case "X":
                        return;

                    default:
                        Console.WriteLine("Optiune invalida!");
                        break;
                }
            }
        }

        static Sofer CitesteSoferTastatura()
        {
            Console.Write("Nume: ");
            string nume = Console.ReadLine();
            Console.Write("Prenume: ");
            string prenume = Console.ReadLine();
            Console.Write("Varsta: ");
            int varsta = int.Parse(Console.ReadLine());
            Console.Write("Telefon: ");
            string telefon = Console.ReadLine();
            Console.Write("Kilometri: ");
            double km = double.Parse(Console.ReadLine());

            return new Sofer(nume, prenume, varsta, telefon, km);
        }

        static Vehicul CitesteVehiculTastatura()
        {
            Console.Write("Marca: ");
            string marca = Console.ReadLine();
            Console.Write("Model: ");
            string model = Console.ReadLine();
            Console.Write("An: ");
            int an = int.Parse(Console.ReadLine());
            Console.Write("Kilometri: ");
            int km = int.Parse(Console.ReadLine());

            Console.Write("Culoare (0-Alb, 1-Negru, 2-Rosu, 3-Albastru, 4-Gri): ");
            Culoare culoare = (Culoare)int.Parse(Console.ReadLine());

            Console.Write("Bifeaza optiuni (ex: 1 pentru AC, 2 pentru Navigatie, 3 pentru ambele): ");
            OptiuniVehicul optiuni = (OptiuniVehicul)int.Parse(Console.ReadLine());

            return new Vehicul(marca, model, an, km, culoare, optiuni);
        }
    }
}