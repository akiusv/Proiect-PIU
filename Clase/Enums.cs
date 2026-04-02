using System;

// Tema 4 - enumuri

namespace Clase
{
    public enum Culoare
    {
        Alb,
        Negru,
        Rosu,
        Albastru,
        Gri
    }

    [Flags]
    public enum OptiuniVehicul
    {
        Niciuna = 0,
        AerConditionat = 1,
        Navigatie = 2,
        CutieAutomata = 4,
        IncalzireScaune = 8
    }
}