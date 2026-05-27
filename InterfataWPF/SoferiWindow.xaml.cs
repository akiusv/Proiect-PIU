using Clase;
using Date;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        private bool Valideaza()
        {
            bool valid = true;
            lblNume.Foreground = Brushes.Black;
            lblPrenume.Foreground = Brushes.Black;
            lblVarsta.Foreground = Brushes.Black;
            lblTelefon.Foreground = Brushes.Black;
            lblKmSofer.Foreground = Brushes.Black;
            lblEroare.Visibility = Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(txtNume.Text)) { lblNume.Foreground = Brushes.Red; valid = false; }
            if (string.IsNullOrWhiteSpace(txtPrenume.Text)) { lblPrenume.Foreground = Brushes.Red; valid = false; }
            if (!int.TryParse(txtVarsta.Text, out int varsta) || varsta < 18 || varsta > 70) { lblVarsta.Foreground = Brushes.Red; valid = false; }
            if (string.IsNullOrWhiteSpace(txtTelefon.Text) || txtTelefon.Text.Length != 10 || !txtTelefon.Text.All(char.IsDigit)) { lblTelefon.Foreground = Brushes.Red; valid = false; }
            if (!int.TryParse(txtKmSofer.Text, out int km) || km < 0) { lblKmSofer.Foreground = Brushes.Red; valid = false; }

            if (!valid)
            {
                lblEroare.Content = "Te rog să corectezi câmpurile marcate cu roșu!";
                lblEroare.Visibility = Visibility.Visible;
            }

            return valid;
        }

        private void BtnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (Valideaza())
            {
                adminSoferi.AdaugaSofer(new Sofer(txtNume.Text, txtPrenume.Text, int.Parse(txtVarsta.Text), txtTelefon.Text, int.Parse(txtKmSofer.Text)));
                IncarcaDate();
            }
        }

        private void BtnModifica_Click(object sender, RoutedEventArgs e)
        {
            if (dgSoferi.SelectedItem is Sofer s && Valideaza())
            {
                adminSoferi.ModificaSofer(s.Nume, new Sofer(txtNume.Text, txtPrenume.Text, int.Parse(txtVarsta.Text), txtTelefon.Text, int.Parse(txtKmSofer.Text)));
                IncarcaDate();
            }
            else if (dgSoferi.SelectedItem == null)
            {
                MessageBox.Show("Selectează un șofer!");
            }
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
                txtNume.Text = s.Nume;
                txtPrenume.Text = s.Prenume;
                txtVarsta.Text = s.Varsta.ToString();
                txtTelefon.Text = s.Telefon;
                txtKmSofer.Text = s.KmParcursi.ToString();
            }
        }
    }
}