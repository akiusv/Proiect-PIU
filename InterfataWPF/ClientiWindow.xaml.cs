using Clase;
using Date;
using System.Windows;
using System.Windows.Controls;

namespace InterfataWPF
{
    public partial class ClientiWindow : Window
    {
        AdministrareClienti_FisierText adminClienti = new AdministrareClienti_FisierText();

        public ClientiWindow()
        {
            InitializeComponent();
            IncarcaDate();
        }

        private void IncarcaDate() => dgClienti.ItemsSource = adminClienti.GetClienti();

        private void BtnAdauga_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                adminClienti.AdaugaClient(new Client(txtNume.Text, txtPrenume.Text, int.Parse(txtVarsta.Text), txtTelefon.Text, txtFirma.Text));
                IncarcaDate();
            }
            catch { MessageBox.Show("Date invalide!"); }
        }

        private void BtnModifica_Click(object sender, RoutedEventArgs e)
        {
            if (dgClienti.SelectedItem is Client c)
            {
                adminClienti.ModificaClient(c.Nume, new Client(txtNume.Text, txtPrenume.Text, int.Parse(txtVarsta.Text), txtTelefon.Text, txtFirma.Text));
                IncarcaDate();
            }
            else MessageBox.Show("Selectează un client!");
        }

        private void BtnSterge_Click(object sender, RoutedEventArgs e)
        {
            if (dgClienti.SelectedItem is Client c)
            {
                adminClienti.StergeClient(c.Nume);
                IncarcaDate();
            }
            else MessageBox.Show("Selectează un client!");
        }

        private void dgClienti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClienti.SelectedItem is Client c)
            {
                txtNume.Text = c.Nume; txtPrenume.Text = c.Prenume; txtVarsta.Text = c.Varsta.ToString(); txtTelefon.Text = c.Telefon; txtFirma.Text = c.Firma;
            }
        }
    }
}