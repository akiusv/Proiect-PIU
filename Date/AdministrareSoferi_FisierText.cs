using Date;
using Clase;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Tema 5 - Utilizarea fisiere text pt a salva date, cauta, modifica


namespace Date
{
    public class AdministrareSoferi_FisierText : AdministrareSoferi
    {
        private string numeFisier = "Soferi.txt";

        public AdministrareSoferi_FisierText()
        {
            if (!File.Exists(numeFisier)) File.Create(numeFisier).Close();
        }

        public void AdaugaSofer(Sofer s)
        {
            using (StreamWriter streamWriterFisierText = new StreamWriter(numeFisier, true))
            {
                streamWriterFisierText.WriteLine(s.ConversieLaSir_PentruFisier());
            }
        }

        public void StergeSofer(string numeCautat)
        {
            var soferi = GetSoferi();
            soferi.RemoveAll(s => s.Nume.ToLower() == numeCautat.ToLower());
            File.WriteAllLines(numeFisier, soferi.Select(s => s.ConversieLaSir_PentruFisier()));
        }

        public List<Sofer> GetSoferi()
        {
            List<Sofer> soferi = new List<Sofer>();
            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    soferi.Add(new Sofer(linieFisier));
                }
            }
            return soferi;
        }

        public List<Sofer> CautaSoferDupaNume(string nume)
        {
            return GetSoferi().Where(s => s.Nume.ToLower() == nume.ToLower()).ToList();
        }

        public void ModificaSofer(string nume, Sofer soferNou)
        {
            var soferi = GetSoferi();
            bool modificat = false;

            for (int i = 0; i < soferi.Count; i++)
            {
                if (soferi[i].Nume.ToLower() == nume.ToLower())
                {
                    soferi[i] = soferNou;
                    modificat = true;
                    break;
                }
            }

            if (modificat)
            {
                File.WriteAllLines(numeFisier, soferi.Select(s => s.ConversieLaSir_PentruFisier()));
            }
        }
    }
}