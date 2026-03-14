using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_PIU
{
    class Persoana
    {
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public int Varsta { get; set; }
        
        public Persoana(string nume, string prenume, int varsta)
        {
            Nume = nume;
            Prenume = prenume;
            Varsta = varsta;
        }
        public virtual void Afisare()
        {
            Console.WriteLine("Nume: {Nume}");
            Console.WriteLine("Prenume: {Prenume}");
            Console.WriteLine("Varsta: {Varsta}");
        }
    }
}
