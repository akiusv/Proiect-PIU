using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_PIU
{
    class Vehicul
    {
        public string Marca {  get; set; }
        public string Model { get; set; }
        public int An { get; set; }
        public int Kilometri {  get; set; }

        public Vehicul(string marca, string model, int an, int kilometri)
        {
            Marca = marca;
            Model = model;
            An = an;
            Kilometri = kilometri;
        }
        public virtual void Afisare()
        {
            Console.WriteLine("Marca: " + Marca);
            Console.WriteLine("Model: " + Model);
            Console.WriteLine("An: " + An);
            Console.WriteLine("Kilometri: " + Kilometri);
            Console.WriteLine();
        }

    }
}
