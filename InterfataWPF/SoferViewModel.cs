using System.ComponentModel;

namespace InterfataWPF
{
    // Implementam INotifyPropertyChanged (pentru Binding) si IDataErrorInfo (pentru Validare MVVM)
    public class SoferViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private string nume = "";
        public string Nume
        {
            get { return nume; }
            set { nume = value; OnPropertyChanged("Nume"); }
        }

        private int varsta = 18;
        public int Varsta
        {
            get { return varsta; }
            set { varsta = value; OnPropertyChanged("Varsta"); }
        }

        // 1. Logica pentru BINDING (Notifică interfața că s-a schimbat o valoare)
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 2. Logica pentru VALIDARE MVVM
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string eroare = null;

                if (columnName == "Nume" && string.IsNullOrWhiteSpace(Nume))
                    eroare = "Numele este obligatoriu!";

                if (columnName == "Varsta" && (Varsta < 18 || Varsta > 70))
                    eroare = "Vârsta trebuie să fie între 18 și 70 de ani!";

                return eroare;
            }
        }

        // O metoda utila pentru a verifica in cod daca formularul e valid
        public bool EsteValid()
        {
            return string.IsNullOrWhiteSpace(this["Nume"]) && string.IsNullOrWhiteSpace(this["Varsta"]);
        }
    }
}