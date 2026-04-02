using Clase;
using System.Collections.Generic;

namespace Date
{
    public interface AdministrareSoferi
    {
        void AdaugaSofer(Sofer s);
        List<Sofer> GetSoferi();
        List<Sofer> CautaSoferDupaNume(string nume);
        void ModificaSofer(string nume, Sofer soferNou);
    }
}