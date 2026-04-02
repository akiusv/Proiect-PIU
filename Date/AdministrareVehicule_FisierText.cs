using Clase;
using System.Collections.Generic;
using System.IO;
using System.Linq;


// Tema 5 - A doua entitate

namespace Date
{
    public class AdministrareVehicule_FisierText
    {
        private string numeFisier = "Vehicule.txt";

        public AdministrareVehicule_FisierText()
        {
            if (!File.Exists(numeFisier)) File.Create(numeFisier).Close();
        }

        public void AdaugaVehicul(Vehicul v)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(v.ConversieLaSir_PentruFisier());
            }
        }

        public List<Vehicul> GetVehicule()
        {
            List<Vehicul> vehicule = new List<Vehicul>();
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    vehicule.Add(new Vehicul(linie));
                }
            }
            return vehicule;
        }
    }
}