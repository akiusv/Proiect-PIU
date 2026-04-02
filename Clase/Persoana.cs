using System;

namespace Clase
{
    public class Persoana
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public int Varsta { get; set; }
        public string Telefon { get; set; }

        public Persoana(string nume, string prenume, int varsta, string telefon)
        {
            Nume = nume;
            Prenume = prenume;
            Varsta = varsta;
            Telefon = telefon;
        }

        public virtual void Afisare()
        {
            Console.WriteLine($"Nume: {Nume} {Prenume}, Varsta: {Varsta}, Tel: {Telefon}");
        }
    }
}