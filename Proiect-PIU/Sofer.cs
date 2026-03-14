using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_PIU
{
    class Sofer: Persoana
    {
        public double kmParcursi { get; set; }

        public Sofer(string nume, string prenume, int varsta, string telefon, double km):base(nume,prenume,varsta,telefon)
        {
            kmParcursi = km;
        }
        public override void Afisare()
        {
            Console.WriteLine("Nume sofer: " + Nume);
            Console.WriteLine("Prenume sofer: " + Prenume);
            Console.WriteLine("Varsta sofer: " + Varsta);
            Console.WriteLine("Telefon sofer: " + Telefon);
            Console.WriteLine("Kilometrii parcursi de sofer: " + kmParcursi);
            Console.WriteLine();
        }
    }
}
