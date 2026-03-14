using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_PIU
{
    class Job
    {
        public Sofer Sofer { get; set; }
        public Client Client { get; set; }
        public Vehicul Vehicul { get; set; }
        public int Distanta { get; set; }

        public Job(Sofer sofer, Client client, Vehicul vehicul, int distanta)
        {
            Sofer = sofer;
            Client = client;
            Vehicul = vehicul;
            Distanta = distanta;
        }
        public void Afisare()
        {
            Console.WriteLine("===JOB===");
            Sofer.Afisare();
            Client.Afisare();
            Vehicul.Afisare();
            Console.WriteLine("Distanta traseu: " + Distanta);
            Console.WriteLine();
        }
    }
}
