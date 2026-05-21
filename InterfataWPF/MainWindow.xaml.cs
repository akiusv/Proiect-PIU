using Clase;
using Date;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InterfataWPF
{
    public partial class MainWindow : Window
    {
        AdministrareSoferi_FisierText adminSoferi = new AdministrareSoferi_FisierText();
        AdministrareVehicule_FisierText adminVehicule = new AdministrareVehicule_FisierText();
        AdministrareClienti_FisierText adminClienti = new AdministrareClienti_FisierText();
        AdministrareJoburi_FisierText adminJoburi = new AdministrareJoburi_FisierText();
        // Tema 7.2 constantele pentru validare
        private const int VARSTA_MINIMA = 18;
        private const int VARSTA_MAXIMA = 70;

        public MainWindow()
        {
            InitializeComponent();
            IncarcaToateDatele();
            dpDataStart.DisplayDateStart = DateTime.Today;
            dpDataFinal.DisplayDateStart = DateTime.Today;

        }

        private void IncarcaToateDatele()
        {
            var soferi = adminSoferi.GetSoferi();
            var vehicule = adminVehicule.GetVehicule();
            var clienti = adminClienti.GetClienti();

            dgSoferi.ItemsSource = soferi;
            dgVehicule.ItemsSource = vehicule;
            dgClienti.ItemsSource = clienti;
            dgJoburi.ItemsSource = adminJoburi.GetJoburi(soferi, vehicule, clienti);


            cmbSoferi.ItemsSource = soferi;
            cmbVehicule.ItemsSource = vehicule;
            cmbClienti.ItemsSource = clienti;
        }


        // Tema 7.2 validare si schimbare culoare etichete
        private bool ValideazaSofer()
        {
            bool valid = true;
            lblNume.Foreground = Brushes.Black;
            lblPrenume.Foreground = Brushes.Black;
            lblVarsta.Foreground = Brushes.Black;
            lblEroareSofer.Visibility = Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(txtNumeSofer.Text))
            {
                lblNume.Foreground = Brushes.Red;
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(txtPrenumeSofer.Text))
            {
                lblPrenume.Foreground = Brushes.Red;
                valid = false;
            }
            if (!int.TryParse(txtVarstaSofer.Text, out int varsta) || varsta < VARSTA_MINIMA || varsta > VARSTA_MAXIMA)
            {
                lblVarsta.Foreground = Brushes.Red;
                valid = false;
            }

            if (!valid)
            {
                lblEroareSofer.Content = $"Eroare date! Vârsta trebuie să fie {VARSTA_MINIMA}-{VARSTA_MAXIMA}.";
                lblEroareSofer.Visibility = Visibility.Visible;
            }

            return valid;
        }

        private void BtnAdaugaSofer_Click(object sender, RoutedEventArgs e)
        {
            if (ValideazaSofer())
            {
                Sofer s = new Sofer(txtNumeSofer.Text, txtPrenumeSofer.Text, int.Parse(txtVarstaSofer.Text), txtTelefonSofer.Text, 0);
                adminSoferi.AdaugaSofer(s);
                IncarcaToateDatele();
            }
        }

        // Tema 9.2 modificare
        private void BtnModificaSofer_Click(object sender, RoutedEventArgs e)
        {
            if (dgSoferi.SelectedItem is Sofer soferSelectat)
            {
                if (ValideazaSofer())
                {
                    Sofer sNou = new Sofer(txtNumeSofer.Text, txtPrenumeSofer.Text, int.Parse(txtVarstaSofer.Text), txtTelefonSofer.Text, soferSelectat.KmParcursi);
                    adminSoferi.ModificaSofer(soferSelectat.Nume, sNou);
                    MessageBox.Show("Șofer modificat!");
                    IncarcaToateDatele();
                }
            }
            else
            {
                MessageBox.Show("Selectează un șofer din tabel pentru a-l modifica!");
            }
        }

        private void dgSoferi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSoferi.SelectedItem is Sofer s)
            {
                txtNumeSofer.Text = s.Nume;
                txtPrenumeSofer.Text = s.Prenume;
                txtVarstaSofer.Text = s.Varsta.ToString();
                txtTelefonSofer.Text = s.Telefon;
            }
        }

        private void BtnCautaSofer_Click(object sender, RoutedEventArgs e)
        {
            dgSoferi.ItemsSource = adminSoferi.CautaSoferDupaNume(txtCautaSofer.Text);
        }

        private void BtnRefreshSoferi_Click(object sender, RoutedEventArgs e)
        {
            txtCautaSofer.Text = "";
            IncarcaToateDatele();
        }



        private void BtnAdaugaVehicul_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Culoare culoareAleasa = Culoare.Alb;
                if (rbNegru.IsChecked == true) culoareAleasa = Culoare.Negru;
                else if (rbRosu.IsChecked == true) culoareAleasa = Culoare.Rosu;
                else if (rbAlbastru.IsChecked == true) culoareAleasa = Culoare.Albastru;
                else if (rbGri.IsChecked == true) culoareAleasa = Culoare.Gri;

                OptiuniVehicul optiuniAlese = OptiuniVehicul.Niciuna;
                if (chkAC.IsChecked == true) optiuniAlese |= OptiuniVehicul.AerConditionat;
                if (chkNav.IsChecked == true) optiuniAlese |= OptiuniVehicul.Navigatie;

                Vehicul v = new Vehicul(txtMarca.Text, txtModel.Text, int.Parse(txtAn.Text), int.Parse(txtKm.Text), culoareAleasa, optiuniAlese);
                adminVehicule.AdaugaVehicul(v);
                IncarcaToateDatele();
            }
            catch { MessageBox.Show("Verificați datele numerice (An, Km)."); }
        }



        private void BtnAdaugaClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Client c = new Client(txtNumeClient.Text, txtPrenumeClient.Text, int.Parse(txtVarstaClient.Text), txtTelefonClient.Text, txtFirmaClient.Text);
                adminClienti.AdaugaClient(c);
                IncarcaToateDatele();
            }
            catch { MessageBox.Show("Verificați vârsta!"); }
        }



        private void BtnAdaugaJob_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSoferi.SelectedItem == null || cmbVehicule.SelectedItem == null || cmbClienti.SelectedItem == null)
            {
                MessageBox.Show("Selectați Șofer, Vehicul și Client!"); return;
            }

            if (dpDataStart.SelectedDate == null || dpDataFinal.SelectedDate == null)
            {
                MessageBox.Show("Te rog să selectezi datele de Start și Final din calendar!"); return;
            }

            try
            {
                string dataStartText = dpDataStart.SelectedDate.Value.ToString("dd/MM/yyyy");
                string dataFinalText = dpDataFinal.SelectedDate.Value.ToString("dd/MM/yyyy");

                string startComplet = $"{dataStartText} {txtOraStart.Text}";
                string finalComplet = $"{dataFinalText} {txtOraFinal.Text}";

                string[] formate = { "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm", "dd.MM.yyyy HH:mm" };
                DateTime start = DateTime.ParseExact(startComplet, formate, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                DateTime final = DateTime.ParseExact(finalComplet, formate, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

                if (start < DateTime.Now)
                {
                    MessageBox.Show("Data și ora de start nu pot fi în trecut!", "Eroare Dată", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; // Opreste executia, nu adauga jobul
                }

                if (final <= start)
                {
                    MessageBox.Show("Data de finalizare trebuie să fie DUPĂ data de start!", "Eroare Dată", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Job j = new Job((Sofer)cmbSoferi.SelectedItem, (Vehicul)cmbVehicule.SelectedItem, (Client)cmbClienti.SelectedItem, start, final, txtPlecare.Text, txtDestinatie.Text, double.Parse(txtDistantaJob.Text));
                adminJoburi.AdaugaJob(j);
                IncarcaToateDatele();
            }
            catch
            {
                MessageBox.Show("Verifică dacă ai introdus corect ora! (Format corect: 08:30)");
            }
        }
        private void Iesire_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Despre_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aplicație pentru Administrare Transporturi\nProiect PIU - Realizat de: Alexandru Stingheriu\nGrupa: 3123B",
                            "Despre aplicație",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }
        private void DeschideVehicule_Click(object sender, RoutedEventArgs e)
        {
            VehiculeWindow vw = new VehiculeWindow();

            vw.ShowDialog();

            IncarcaToateDatele();
        }
    }
}