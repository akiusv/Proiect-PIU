using Clase;
using Date;
using System;
using System.Windows;
using System.Windows.Controls;

namespace InterfataWPF
{
    public partial class JoburiWindow : Window
    {
        AdministrareSoferi_FisierText adminSoferi = new AdministrareSoferi_FisierText();
        AdministrareVehicule_FisierText adminVehicule = new AdministrareVehicule_FisierText();
        AdministrareClienti_FisierText adminClienti = new AdministrareClienti_FisierText();
        AdministrareJoburi_FisierText adminJoburi = new AdministrareJoburi_FisierText();

        public JoburiWindow()
        {
            InitializeComponent();
            IncarcaDate();
        }

        private void IncarcaDate()
        {
            var soferi = adminSoferi.GetSoferi();
            var vehicule = adminVehicule.GetVehicule();
            var clienti = adminClienti.GetClienti();

            cmbSoferi.ItemsSource = soferi;
            cmbVehicule.ItemsSource = vehicule;
            cmbClienti.ItemsSource = clienti;

            dgJoburi.ItemsSource = adminJoburi.GetJoburi(soferi, vehicule, clienti);
        }

        private Job CreazaJobDinFormular()
        {
            string startStr = $"{dpStart.SelectedDate.Value:dd/MM/yyyy} {txtOraStart.Text}";
            string finalStr = $"{dpFinal.SelectedDate.Value:dd/MM/yyyy} {txtOraFinal.Text}";
            string[] form = { "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm" };

            DateTime start = DateTime.ParseExact(startStr, form, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
            DateTime final = DateTime.ParseExact(finalStr, form, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

            return new Job((Sofer)cmbSoferi.SelectedItem, (Vehicul)cmbVehicule.SelectedItem, (Client)cmbClienti.SelectedItem, start, final, txtPlecare.Text, txtDestinatie.Text, double.Parse(txtDistanta.Text));
        }

        private void BtnAdauga_Click(object sender, RoutedEventArgs e)
        {
            try { adminJoburi.AdaugaJob(CreazaJobDinFormular()); IncarcaDate(); }
            catch { MessageBox.Show("Verifică selecțiile și datele introduse!"); }
        }

        private void BtnModifica_Click(object sender, RoutedEventArgs e)
        {
            if (dgJoburi.SelectedItem is Job j)
            {
                try { adminJoburi.ModificaJob(j.PunctDestinatie, CreazaJobDinFormular()); IncarcaDate(); }
                catch { MessageBox.Show("Eroare la modificare!"); }
            }
            else MessageBox.Show("Selectează un job!");
        }

        private void BtnSterge_Click(object sender, RoutedEventArgs e)
        {
            if (dgJoburi.SelectedItem is Job j)
            {
                adminJoburi.StergeJob(j.PunctDestinatie);
                IncarcaDate();
            }
            else MessageBox.Show("Selectează un job!");
        }

        private void dgJoburi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgJoburi.SelectedItem is Job j)
            {
                txtPlecare.Text = j.PunctPlecare;
                txtDestinatie.Text = j.PunctDestinatie;
                txtDistanta.Text = j.DistantaTraseu.ToString();
                dpStart.SelectedDate = j.DataIncepere;
                dpFinal.SelectedDate = j.DataFinalizare;
                txtOraStart.Text = j.DataIncepere.ToString("HH:mm");
                txtOraFinal.Text = j.DataFinalizare.ToString("HH:mm");
            }
        }
    }
}