using Clase;
using Date;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interfata
{
    class Program
    {
        static void Main()
        {
            AdministrareSoferi adminSoferi = new AdministrareSoferi_FisierText();
            AdministrareVehicule_FisierText adminVehicule = new AdministrareVehicule_FisierText();

            AdministrareClienti_FisierText adminClienti = new AdministrareClienti_FisierText();
            AdministrareJoburi_FisierText adminJoburi = new AdministrareJoburi_FisierText();

            while (true)
            {
                Console.WriteLine("\n=== MENIU PRINCIPAL ===");
                Console.WriteLine("1. Adauga Sofer");
                Console.WriteLine("2. Afiseaza Soferi");
                Console.WriteLine("3. Cauta Sofer");
                Console.WriteLine("4. Modifica Sofer");
                Console.WriteLine("5. Adauga Vehicul");
                Console.WriteLine("6. Afiseaza Vehicule");
                Console.WriteLine("7. Adauga Client");
                Console.WriteLine("8. Aloca JOB / Cursa Noua");
                Console.WriteLine("9. Afiseaza Istoric Joburi");
                Console.WriteLine("X. Iesire");
                Console.Write("Alege optiunea: ");

                string optiune = Console.ReadLine();

                switch (optiune.ToUpper())
                {
                    case "1":
                        adminSoferi.AdaugaSofer(CitesteSoferTastatura());
                        Console.WriteLine("Sofer adaugat cu succes!");
                        break;

                    case "2":
                        // Tema 3-Afisarea din colectie
                        foreach (var sofer in adminSoferi.GetSoferi()) sofer.Afisare();
                        break;

                    // Tema 3-cautarea dupa criteriu
                    case "3":
                        Console.Write("Introdu numele soferului cautat: ");
                        var rezultate = adminSoferi.CautaSoferDupaNume(Console.ReadLine());
                        if (rezultate.Count > 0) foreach (var rez in rezultate) rez.Afisare();
                        else Console.WriteLine("Nu a fost gasit niciun sofer.");
                        break;

                    case "4":
                        Console.Write("Numele soferului de modificat: ");
                        string numeModif = Console.ReadLine();
                        Console.WriteLine("Introdu datele noi:");
                        adminSoferi.ModificaSofer(numeModif, CitesteSoferTastatura());
                        Console.WriteLine("Sofer modificat!");
                        break;

                    case "5":
                        adminVehicule.AdaugaVehicul(CitesteVehiculTastatura());
                        Console.WriteLine("Vehicul adaugat cu succes!");
                        break;

                    case "6":
                        foreach (var vehicul in adminVehicule.GetVehicule()) vehicul.Afisare();
                        break;

                    case "7":
                        adminClienti.AdaugaClient(CitesteClientTastatura());
                        Console.WriteLine("Client adaugat in fisier!");
                        break;

                    case "8":
                        var soferiDisp = adminSoferi.GetSoferi();
                        var vehiculeDisp = adminVehicule.GetVehicule();
                        var clientiDisp = adminClienti.GetClienti();

                        Job jobNou = CitesteJobTastatura(soferiDisp, vehiculeDisp, clientiDisp);

                        if (jobNou != null)
                        {
                            adminJoburi.AdaugaJob(jobNou);
                            Console.WriteLine("Job salvat in fisier cu succes!");
                        }
                        break;

                    case "9":
                        var listaJoburi = adminJoburi.GetJoburi(adminSoferi.GetSoferi(), adminVehicule.GetVehicule(), adminClienti.GetClienti());

                        if (listaJoburi.Count == 0) Console.WriteLine("Nu exista joburi inregistrate.");
                        else foreach (var j in listaJoburi) j.Afisare();
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
            Console.Write("Nume: "); string nume = Console.ReadLine();
            Console.Write("Prenume: "); string prenume = Console.ReadLine();
            Console.Write("Varsta: "); int varsta = int.Parse(Console.ReadLine());
            Console.Write("Telefon: "); string telefon = Console.ReadLine();
            Console.Write("Kilometri: "); double km = double.Parse(Console.ReadLine());
            return new Sofer(nume, prenume, varsta, telefon, km);
        }

        static Vehicul CitesteVehiculTastatura()
        {
            Console.Write("Marca: "); string marca = Console.ReadLine();
            Console.Write("Model: "); string model = Console.ReadLine();
            Console.Write("An: "); int an = int.Parse(Console.ReadLine());
            Console.Write("Kilometri: "); int km = int.Parse(Console.ReadLine());
            Console.Write("Culoare (0-Alb, 1-Negru, 2-Rosu, 3-Albastru, 4-Gri): ");
            Culoare culoare = (Culoare)int.Parse(Console.ReadLine());
            Console.Write("Bifeaza optiuni (ex: 1 pt AC, 2 pt Navigatie, 3 pt ambele): ");
            OptiuniVehicul optiuni = (OptiuniVehicul)int.Parse(Console.ReadLine());
            return new Vehicul(marca, model, an, km, culoare, optiuni);
        }

        static Client CitesteClientTastatura()
        {
            Console.Write("Nume: "); string nume = Console.ReadLine();
            Console.Write("Prenume: "); string prenume = Console.ReadLine();
            Console.Write("Varsta: "); int varsta = int.Parse(Console.ReadLine());
            Console.Write("Telefon: "); string telefon = Console.ReadLine();
            Console.Write("Firma: "); string firma = Console.ReadLine();
            return new Client(nume, prenume, varsta, telefon, firma);
        }

        static Job CitesteJobTastatura(List<Sofer> soferi, List<Vehicul> vehicule, List<Client> clienti)
        {
            if (soferi.Count == 0 || vehicule.Count == 0 || clienti.Count == 0)
            {
                Console.WriteLine("Eroare: Trebuie sa ai cel putin un sofer, un vehicul si un client inregistrati pentru a crea un job!");
                return null;
            }

            Console.WriteLine("--- Selectare Sofer ---");
            for (int i = 0; i < soferi.Count; i++) Console.WriteLine($"{i}. {soferi[i].Nume} {soferi[i].Prenume}");
            Console.Write("Alege ID-ul soferului: ");
            int idSofer = int.Parse(Console.ReadLine());

            Console.WriteLine("--- Selectare Vehicul ---");
            for (int i = 0; i < vehicule.Count; i++) Console.WriteLine($"{i}. {vehicule[i].Marca} {vehicule[i].Model}");
            Console.Write("Alege ID-ul vehiculului: ");
            int idVehicul = int.Parse(Console.ReadLine());

            Console.WriteLine("--- Selectare Client ---");
            for (int i = 0; i < clienti.Count; i++) Console.WriteLine($"{i}. {clienti[i].Nume} ({clienti[i].Firma})");
            Console.Write("Alege ID-ul clientului: ");
            int idClient = int.Parse(Console.ReadLine());

            Console.Write("Punct de plecare: "); string plecare = Console.ReadLine();
            Console.Write("Punct de destinatie: "); string destinatie = Console.ReadLine();
            Console.Write("Distanta estimata (km): "); double distanta = double.Parse(Console.ReadLine());

            Console.Write("Data inceperii (ex: 15/05/2026 08:00): ");
            DateTime start = DateTime.Parse(Console.ReadLine());

            Console.Write("Data finalizarii (ex: 16/05/2026 14:00): ");
            DateTime final = DateTime.Parse(Console.ReadLine());

            return new Job(soferi[idSofer], vehicule[idVehicul], clienti[idClient], start, final, plecare, destinatie, distanta);
        }
    }
}