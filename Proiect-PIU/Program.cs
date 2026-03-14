using Proiect_PIU;

class Program
{
    static void Main()
    {
        Sofer s1 = new Sofer("Stingheriu", "Alexandru", 20, "0740000000", 120.43);
        Client c1 = new Client("Popescu","Gheorghe",30,"0758777888","SC DET SRL");
        Vehicul v1 = new Vehicul("Mercedes", "Sprinter", 2022, 50000);

        Job j1 = new Job(s1, c1, v1, 350);
        j1.Afisare();

    }
}