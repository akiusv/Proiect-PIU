using System;

namespace Clase
{
    public class Job
    {
        public Sofer SoferAlocat { get; set; }
        public Vehicul VehiculAlocat { get; set; }
        public Client ClientCursa { get; set; }

        public DateTime DataIncepere { get; set; }
        public DateTime DataFinalizare { get; set; }

        public string PunctPlecare { get; set; }
        public string PunctDestinatie { get; set; }
        public double DistantaTraseu { get; set; }

        public Job(Sofer sofer, Vehicul vehicul, Client client, DateTime start, DateTime final, string plecare, string destinatie, double distanta)
        {
            SoferAlocat = sofer;
            VehiculAlocat = vehicul;
            ClientCursa = client;
            DataIncepere = start;
            DataFinalizare = final;
            PunctPlecare = plecare;
            PunctDestinatie = destinatie;
            DistantaTraseu = distanta;
        }

        public string ConversieLaSir_PentruFisier()
        {
            string startStr = DataIncepere.ToString("dd/MM/yyyy HH:mm");
            string finalStr = DataFinalizare.ToString("dd/MM/yyyy HH:mm");

            return $"{SoferAlocat.Nume};{VehiculAlocat.Marca};{ClientCursa.Nume};{startStr};{finalStr};{PunctPlecare};{PunctDestinatie};{DistantaTraseu}";
        }

        public void Afisare()
        {
            Console.WriteLine("\n=== DETALII JOB / CURSĂ ===");
            Console.WriteLine($"Sofer: {SoferAlocat.Nume} {SoferAlocat.Prenume}");
            Console.WriteLine($"Vehicul: {VehiculAlocat.Marca} {VehiculAlocat.Model}");
            Console.WriteLine($"Client: {ClientCursa.Nume} {ClientCursa.Prenume} ({ClientCursa.Firma})");
            Console.WriteLine($"Traseu: {PunctPlecare} -> {PunctDestinatie} ({DistantaTraseu} km)");
            Console.WriteLine($"Interval de lucru: {DataIncepere.ToString("dd.MM.yyyy HH:mm")} - {DataFinalizare.ToString("dd.MM.yyyy HH:mm")}");
            Console.WriteLine("===========================\n");
        }
    }
}