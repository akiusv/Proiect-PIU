using Clase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Date
{
    public class AdministrareJoburi_FisierText
    {
        private string numeFisier = "Joburi.txt";

        public AdministrareJoburi_FisierText()
        {
            if (!File.Exists(numeFisier)) File.Create(numeFisier).Close();
        }

        public void AdaugaJob(Job j)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(j.ConversieLaSir_PentruFisier());
            }
        }

        public List<Job> GetJoburi(List<Sofer> soferi, List<Vehicul> vehicule, List<Client> clienti)
        {
            List<Job> joburi = new List<Job>();
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    var date = linie.Split(';');

                    Sofer soferLogat = soferi.FirstOrDefault(s => s.Nume == date[0]);
                    Vehicul vehiculLogat = vehicule.FirstOrDefault(v => v.Marca == date[1]);
                    Client clientLogat = clienti.FirstOrDefault(c => c.Nume == date[2]);

                    string[] formateAcceptate = { "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm", "dd.MM.yyyy HH:mm" };

                    DateTime start = DateTime.ParseExact(date[3], formateAcceptate, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                    DateTime final = DateTime.ParseExact(date[4], formateAcceptate, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

                    if (soferLogat != null && vehiculLogat != null && clientLogat != null)
                    {
                        joburi.Add(new Job(soferLogat, vehiculLogat, clientLogat, start, final, date[5], date[6], double.Parse(date[7])));
                    }
                }
            }
            return joburi;
        }
    }
}