using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_PIU
{
    class Sofer: Persoana
    {
        public int kmParcursi { get; set; }

        public Sofer(string nume, string prenume, int varsta, float km):base(nume,prenume,varsta)
        {
            kmParcursi = km;
        }
        public override void Afisare()
        {
            Console.WriteLine("Nume sofer: {Nume}");
            Console.WriteLine("Prenume sofer: {Prenume}");
            Console.WriteLine("Varsta sofer: {Varsta}");
        }
    }
}
