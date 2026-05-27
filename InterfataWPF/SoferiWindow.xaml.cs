using Clase;
using Date;
using System.Windows;
using System.Windows.Controls;

namespace InterfataWPF
{
    public partial class SoferiWindow : Window
    {
        AdministrareSoferi_FisierText adminSoferi = new AdministrareSoferi_FisierText();

        public SoferiWindow()
        {
            InitializeComponent();
            IncarcaDate();
        }

        private void IncarcaDate() => dgSoferi.ItemsSource = adminSoferi.GetSoferi();

        private void BtnAdauga_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                adminSoferi.AdaugaSofer(new Sofer(txtNume.Text, txtPrenume.Text, int.Parse(txtVarsta.Text), txtTelefon.Text, 0));
                IncarcaDate();
            }
            catch { MessageBox.Show("Date invalide!"); }
        }

        private void BtnModifica_Click(object sender, RoutedEventArgs e)
        {
            if (dgSoferi.SelectedItem is Sofer s)
            {
                adminSoferi.ModificaSofer(s.Nume, new Sofer(txtNume.Text, txtPrenume.Text, int.Parse(txtVarsta.Text), txtTelefon.Text, s.KmParcursi));
                IncarcaDate();
            }
            else MessageBox.Show("Selectează un șofer!");
        }

        private void BtnSterge_Click(object sender, RoutedEventArgs e)
        {
            if (dgSoferi.SelectedItem is Sofer s)
            {
                adminSoferi.StergeSofer(s.Nume);
                IncarcaDate();
            }
            else MessageBox.Show("Selectează un șofer!");
        }

        private void dgSoferi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSoferi.SelectedItem is Sofer s)
            {
                txtNume.Text = s.Nume; txtPrenume.Text = s.Prenume; txtVarsta.Text = s.Varsta.ToString(); txtTelefon.Text = s.Telefon;
            }
        }
    }
}