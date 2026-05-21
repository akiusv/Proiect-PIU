using System;

namespace Clase
{
    public class Client : Persoana
    {
        public string Firma { get; set; }

        public Client(string nume, string prenume, int varsta, string telefon, string firma)
            : base(nume, prenume, varsta, telefon)
        {
            Firma = firma;
        }

        public Client(string linieFisier) : base("", "", 0, "")
        {
            var date = linieFisier.Split(';');
            Nume = date[0];
            Prenume = date[1];
            Varsta = int.Parse(date[2]);
            Telefon = date[3];
            Firma = date[4];
        }

        public string ConversieLaSir_PentruFisier()
        {
            return $"{Nume};{Prenume};{Varsta};{Telefon};{Firma}";
        }

        public override void Afisare()
        {
            Console.WriteLine($"[CLIENT] {Nume} {Prenume} | Firma: {Firma} | Tel: {Telefon}");
        }
    }
}