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
        public void ModificaVehicul(string marcaCautata, Vehicul vehiculNou)
        {
            var vehicule = GetVehicule();
            bool modificat = false;

            for (int i = 0; i < vehicule.Count; i++)
            {
                if (vehicule[i].Marca.ToLower() == marcaCautata.ToLower())
                {
                    vehicule[i] = vehiculNou;
                    modificat = true;
                    break;
                }
            }

            if (modificat)
            {
                File.WriteAllLines(numeFisier, vehicule.Select(v => v.ConversieLaSir_PentruFisier()));
            }
        }

        public void StergeVehicul(string marcaCautata)
        {
            var vehicule = GetVehicule();

            vehicule.RemoveAll(v => v.Marca.ToLower() == marcaCautata.ToLower());

            File.WriteAllLines(numeFisier, vehicule.Select(v => v.ConversieLaSir_PentruFisier()));
        }
    }
}