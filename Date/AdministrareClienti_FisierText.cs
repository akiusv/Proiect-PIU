using Clase;
using System.Collections.Generic;
using System.IO;

namespace Date
{
    public class AdministrareClienti_FisierText
    {
        private string numeFisier = "Clienti.txt";

        public AdministrareClienti_FisierText()
        {
            if (!File.Exists(numeFisier)) File.Create(numeFisier).Close();
        }

        public void AdaugaClient(Client c)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(c.ConversieLaSir_PentruFisier());
            }
        }

        public void ModificaClient(string numeCautat, Client clientNou)
        {
            var clienti = GetClienti();
            for (int i = 0; i < clienti.Count; i++)
                if (clienti[i].Nume.ToLower() == numeCautat.ToLower()) { clienti[i] = clientNou; break; }
            File.WriteAllLines(numeFisier, clienti.Select(c => c.ConversieLaSir_PentruFisier()));
        }

        public void StergeClient(string numeCautat)
        {
            var clienti = GetClienti();
            clienti.RemoveAll(c => c.Nume.ToLower() == numeCautat.ToLower());
            File.WriteAllLines(numeFisier, clienti.Select(c => c.ConversieLaSir_PentruFisier()));
        }

        public List<Client> GetClienti()
        {
            List<Client> clienti = new List<Client>();
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    clienti.Add(new Client(linie));
                }
            }
            return clienti;
        }
    }
}