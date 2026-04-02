using Clase;
using System;

namespace Clase
{
    public class Sofer : Persoana
    {
        public double KmParcursi { get; set; }

        public Sofer(string nume, string prenume, int varsta, string telefon, double km)
            : base(nume, prenume, varsta, telefon)
        {
            KmParcursi = km;
        }

        // Constructor pentru citirea din fisier text
        public Sofer(string linieFisier) : base("", "", 0, "")
        {
            var dateFisier = linieFisier.Split(';');
            Nume = dateFisier[0];
            Prenume = dateFisier[1];
            Varsta = int.Parse(dateFisier[2]);
            Telefon = dateFisier[3];
            KmParcursi = double.Parse(dateFisier[4]);
        }

        public string ConversieLaSir_PentruFisier()
        {
            return $"{Nume};{Prenume};{Varsta};{Telefon};{KmParcursi}";
        }

        public override void Afisare()
        {
            Console.WriteLine($"[SOFER] {Nume} {Prenume} | Varsta: {Varsta} | Tel: {Telefon} | Km: {KmParcursi}");
        }
    }
}