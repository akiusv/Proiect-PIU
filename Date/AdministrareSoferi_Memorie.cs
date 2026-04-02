using Date;
using Clase;
using System.Collections.Generic;
using System.Linq;

namespace Date
{
    public class AdministrareSoferi_Memorie : AdministrareSoferi
    {
        // Tema 3 - Colectia
        private List<Sofer> soferi = new List<Sofer>();

        // Tema 3 - Salvarea in colectie
        public void AdaugaSofer(Sofer s) => soferi.Add(s);

        public List<Sofer> GetSoferi() => soferi;

        // Tema 4 - utilizare Linq
        public List<Sofer> CautaSoferDupaNume(string nume)
        {
            return soferi.Where(s => s.Nume.ToLower() == nume.ToLower()).ToList();
        }

        public void ModificaSofer(string nume, Sofer soferNou)
        {
            var sofer = soferi.FirstOrDefault(s => s.Nume.ToLower() == nume.ToLower());
            if (sofer != null)
            {
                sofer.Prenume = soferNou.Prenume;
                sofer.Varsta = soferNou.Varsta;
                sofer.Telefon = soferNou.Telefon;
                sofer.KmParcursi = soferNou.KmParcursi;
            }
        }
    }
}