using Clase;
using System;

namespace Clase
{
    public class Vehicul
    {
        public string Marca { get; set; }
        public string Model { get; set; }
        public int An { get; set; }
        public int Kilometri { get; set; }

        // Tema 4 - utilizarea enumurilor

        public Culoare CuloareVehicul { get; set; }
        public OptiuniVehicul Optiuni { get; set; }

        public Vehicul(string marca, string model, int an, int kilometri, Culoare culoare, OptiuniVehicul optiuni)
        {
            Marca = marca;
            Model = model;
            An = an;
            Kilometri = kilometri;
            CuloareVehicul = culoare;
            Optiuni = optiuni;
        }

        public Vehicul(string linieFisier)
        {
            var date = linieFisier.Split(';');
            Marca = date[0];
            Model = date[1];
            An = int.Parse(date[2]);
            Kilometri = int.Parse(date[3]);
            CuloareVehicul = (Culoare)Enum.Parse(typeof(Culoare), date[4]);
            Optiuni = (OptiuniVehicul)Enum.Parse(typeof(OptiuniVehicul), date[5]);
        }

        public string ConversieLaSir_PentruFisier()
        {
            return $"{Marca};{Model};{An};{Kilometri};{CuloareVehicul};{Optiuni}";
        }

        public virtual void Afisare()
        {
            Console.WriteLine($"[VEHICUL] {Marca} {Model} ({An}) | {Kilometri} km | Culoare: {CuloareVehicul} | Optiuni: {Optiuni}");
        }
    }
}