using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_PIU
{
    class Client: Persoana
    {
        public string Firma { get; set; }

        public Client(string nume, string prenume, int varsta, string telefon, string firma):base(nume,prenume,varsta,telefon)
        {
            Firma = firma;
        }
        public override void Afisare()
        {
            Console.WriteLine("Nume client: " + Nume);
            Console.WriteLine("Prenume client: " + Prenume);
            Console.WriteLine("Varsta client: " + Varsta);
            Console.WriteLine("Telefon client: " + Telefon);
            Console.WriteLine("Firma: " + Firma);
            Console.WriteLine();
        }
    }
}
