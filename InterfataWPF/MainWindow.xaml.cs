using Clase;
using Date;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
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

        SoferViewModel soferVM = new SoferViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = soferVM;
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
            lblPrenume.Foreground = Brushes.Black;
            lblEroareSofer.Visibility = Visibility.Collapsed;

            // Numele si Varsta nu le mai validam manual aici, o face ViewModel-ul prin MVVM!
            if (!soferVM.EsteValid()) { valid = false; } // Verificam ViewModel-ul

            if (string.IsNullOrWhiteSpace(txtPrenumeSofer.Text)) { lblPrenume.Foreground = Brushes.Red; valid = false; }

            if (string.IsNullOrWhiteSpace(txtTelefonSofer.Text) || txtTelefonSofer.Text.Length != 10 || !txtTelefonSofer.Text.All(char.IsDigit))
            { lblTelefon.Foreground = Brushes.Red; valid = false; }

            if (!valid)
            {
                lblEroareSofer.Content = "Eroare! Verifică textul cu roșu (Vârsta min 18, Tel 10 cifre).";
                lblEroareSofer.Visibility = Visibility.Visible;
            }
            return valid;
        }

        private void BtnAdaugaSofer_Click(object sender, RoutedEventArgs e)
        {
            if (ValideazaSofer())
            {
                // Preluăm kilometrii din txtKmSofer. Dacă e gol sau text, punem 0 default.
                int.TryParse(txtKmSofer.Text, out int km);

                Sofer s = new Sofer(soferVM.Nume, txtPrenumeSofer.Text, soferVM.Varsta, txtTelefonSofer.Text, km);
                adminSoferi.AdaugaSofer(s);

                soferVM.Nume = ""; soferVM.Varsta = 18;
                txtPrenumeSofer.Text = ""; txtTelefonSofer.Text = ""; txtKmSofer.Text = "0"; // Resetam si km

                IncarcaToateDatele();
            }
        }        // Tema 9.2 modificare
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


        private void BtnRefreshSoferi_Click(object sender, RoutedEventArgs e)
        {
            txtCautaSofer.Text = "";
            IncarcaToateDatele();
        }



        private void BtnAdaugaVehicul_Click(object sender, RoutedEventArgs e)
        {
            if(ValideazaVehicul())
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
        }



        private void BtnAdaugaClient_Click(object sender, RoutedEventArgs e)
        {
            if(ValideazaClient())
            {
                Client c = new Client(txtNumeClient.Text, txtPrenumeClient.Text, int.Parse(txtVarstaClient.Text), txtTelefonClient.Text, txtFirmaClient.Text);
                adminClienti.AdaugaClient(c);
                IncarcaToateDatele();
            }
        }



        private void BtnAdaugaJob_Click(object sender, RoutedEventArgs e)
        {
            if (ValideazaJob())
            {
                // Deoarece a trecut de ValideazaJob(), suntem 100% siguri ca datele se pot converti!
                string[] formate = { "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm", "dd.MM.yyyy HH:mm" };

                string startComplet = $"{dpDataStart.SelectedDate.Value:dd/MM/yyyy} {txtOraStart.Text}";
                string finalComplet = $"{dpDataFinal.SelectedDate.Value:dd/MM/yyyy} {txtOraFinal.Text}";

                DateTime start = DateTime.ParseExact(startComplet, formate, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                DateTime final = DateTime.ParseExact(finalComplet, formate, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);

                Job j = new Job((Sofer)cmbSoferi.SelectedItem, (Vehicul)cmbVehicule.SelectedItem, (Client)cmbClienti.SelectedItem, start, final, txtPlecare.Text, txtDestinatie.Text, double.Parse(txtDistantaJob.Text));

                adminJoburi.AdaugaJob(j);
                IncarcaToateDatele();
            }
        }
        // ================= CĂUTĂRI AVANSATE =================

        private void BtnCautaSofer_Click(object sender, RoutedEventArgs e)
        {
            string text = txtCautaSofer.Text.ToLower();
            dgSoferi.ItemsSource = adminSoferi.GetSoferi()
                .Where(s => s.Nume.ToLower().Contains(text) || s.Prenume.ToLower().Contains(text)).ToList();
        }

        private void BtnCautaVehicul_Click(object sender, RoutedEventArgs e)
        {
            string text = txtCautaVehicul.Text.ToLower();
            dgVehicule.ItemsSource = adminVehicule.GetVehicule()
                .Where(v => v.Marca.ToLower().Contains(text) || v.Model.ToLower().Contains(text)).ToList();
        }

        private void BtnCautaClient_Click(object sender, RoutedEventArgs e)
        {
            string text = txtCautaClient.Text.ToLower();
            dgClienti.ItemsSource = adminClienti.GetClienti()
                .Where(c => c.Nume.ToLower().Contains(text) || c.Prenume.ToLower().Contains(text)).ToList();
        }

        private void BtnCautaJob_Click(object sender, RoutedEventArgs e)
        {
            string text = txtCautaJob.Text.ToLower();
            var soferi = adminSoferi.GetSoferi();
            var vehicule = adminVehicule.GetVehicule();
            var clienti = adminClienti.GetClienti();

            dgJoburi.ItemsSource = adminJoburi.GetJoburi(soferi, vehicule, clienti)
                .Where(j => j.PunctPlecare.ToLower().Contains(text) || j.PunctDestinatie.ToLower().Contains(text)).ToList();
        }

        // Reseteaza toate cautarile si reincarca listele
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtCautaSofer.Text = "";
            txtCautaVehicul.Text = "";
            txtCautaClient.Text = "";
            txtCautaJob.Text = "";
            IncarcaToateDatele();
        }

        // ================= DESCHIDERE ECRANE CRUD =================

        private void DeschideCRUD_Soferi_Click(object sender, RoutedEventArgs e)
        {
            SoferiWindow sw = new SoferiWindow();
            sw.ShowDialog();
            IncarcaToateDatele(); // Refresh la final
        }

        private void DeschideCRUD_Vehicule_Click(object sender, RoutedEventArgs e)
        {
            VehiculeWindow vw = new VehiculeWindow();
            vw.ShowDialog();
            IncarcaToateDatele();
        }

        private void DeschideCRUD_Clienti_Click(object sender, RoutedEventArgs e)
        {
            ClientiWindow cw = new ClientiWindow();
            cw.ShowDialog();
            IncarcaToateDatele();
        }

        private void DeschideCRUD_Joburi_Click(object sender, RoutedEventArgs e)
        {
            JoburiWindow jw = new JoburiWindow();
            jw.ShowDialog();
            IncarcaToateDatele();
        }

        private bool ValideazaVehicul()
        {
            bool valid = true;
            lblMarca.Foreground = Brushes.Black;
            lblModel.Foreground = Brushes.Black;
            lblAn.Foreground = Brushes.Black;
            lblKm.Foreground = Brushes.Black;
            lblEroareVehicul.Visibility = Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(txtMarca.Text)) { lblMarca.Foreground = Brushes.Red; valid = false; }
            if (string.IsNullOrWhiteSpace(txtModel.Text)) { lblModel.Foreground = Brushes.Red; valid = false; }

            // Verificam daca Anul este numar valabil
            if (!int.TryParse(txtAn.Text, out int an) || an < 1900 || an > DateTime.Now.Year + 1)
            { lblAn.Foreground = Brushes.Red; valid = false; }

            // Verificam daca Km sunt numar valabil
            if (!int.TryParse(txtKm.Text, out int km) || km < 0)
            { lblKm.Foreground = Brushes.Red; valid = false; }

            if (!valid)
            {
                lblEroareVehicul.Content = "Te rog să completezi corect câmpurile roșii!";
                lblEroareVehicul.Visibility = Visibility.Visible;
            }
            return valid;
        }

        private bool ValideazaClient()
        {
            bool valid = true;
            lblNumeClient.Foreground = Brushes.Black;
            lblPrenumeClient.Foreground = Brushes.Black;
            lblVarstaClient.Foreground = Brushes.Black;
            lblTelefonClient.Foreground = Brushes.Black;
            lblEroareClient.Visibility = Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(txtNumeClient.Text)) { lblNumeClient.Foreground = Brushes.Red; valid = false; }
            if (string.IsNullOrWhiteSpace(txtPrenumeClient.Text)) { lblPrenumeClient.Foreground = Brushes.Red; valid = false; }
            if (string.IsNullOrWhiteSpace(txtTelefonClient.Text)) { lblTelefonClient.Foreground = Brushes.Red; valid = false; }

            // VARSTA DOAR MINIM 18 PENTRU CLIENT
            if (!int.TryParse(txtVarstaClient.Text, out int varsta) || varsta < 18)
            { lblVarstaClient.Foreground = Brushes.Red; valid = false; }

            if (!valid)
            {
                lblEroareClient.Content = "Completează câmpurile roșii (Vârsta minimă: 18 ani).";
                lblEroareClient.Visibility = Visibility.Visible;
            }
            return valid;
        }

        private bool ValideazaJob()
        {
            bool valid = true;

            // 1. Resetam toate culorile la negru
            lblSoferJob.Foreground = Brushes.Black;
            lblVehiculJob.Foreground = Brushes.Black;
            lblClientJob.Foreground = Brushes.Black;
            lblPlecare.Foreground = Brushes.Black;
            lblDestinatie.Foreground = Brushes.Black;
            lblDistanta.Foreground = Brushes.Black;
            lblDataStartJob.Foreground = Brushes.Black;
            lblDataFinalJob.Foreground = Brushes.Black;
            lblEroareJob.Visibility = Visibility.Collapsed;

            // 2. Validare ComboBox-uri (daca a uitat sa selecteze ceva din liste)
            if (cmbSoferi.SelectedItem == null) { lblSoferJob.Foreground = Brushes.Red; valid = false; }
            if (cmbVehicule.SelectedItem == null) { lblVehiculJob.Foreground = Brushes.Red; valid = false; }
            if (cmbClienti.SelectedItem == null) { lblClientJob.Foreground = Brushes.Red; valid = false; }

            // 3. Validare Texte
            if (string.IsNullOrWhiteSpace(txtPlecare.Text)) { lblPlecare.Foreground = Brushes.Red; valid = false; }
            if (string.IsNullOrWhiteSpace(txtDestinatie.Text)) { lblDestinatie.Foreground = Brushes.Red; valid = false; }
            if (!double.TryParse(txtDistantaJob.Text, out double distanta) || distanta <= 0) { lblDistanta.Foreground = Brushes.Red; valid = false; }

            // 4. Validare Date si Ore
            bool dateValide = true;
            DateTime start = DateTime.MinValue;
            DateTime final = DateTime.MinValue;
            string[] formate = { "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm", "dd.MM.yyyy HH:mm" };

            // Verificam Start-ul
            if (dpDataStart.SelectedDate == null)
            {
                lblDataStartJob.Foreground = Brushes.Red; valid = false; dateValide = false;
            }
            else
            {
                string startComplet = $"{dpDataStart.SelectedDate.Value:dd/MM/yyyy} {txtOraStart.Text}";
                // Parsam data si verificam sa NU fie din trecut (DateTime.Now)
                if (!DateTime.TryParseExact(startComplet, formate, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out start) || start < DateTime.Now)
                {
                    lblDataStartJob.Foreground = Brushes.Red; valid = false; dateValide = false;
                }
            }

            // Verificam Finalul
            if (dpDataFinal.SelectedDate == null)
            {
                lblDataFinalJob.Foreground = Brushes.Red; valid = false; dateValide = false;
            }
            else
            {
                string finalComplet = $"{dpDataFinal.SelectedDate.Value:dd/MM/yyyy} {txtOraFinal.Text}";
                if (!DateTime.TryParseExact(finalComplet, formate, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out final))
                {
                    lblDataFinalJob.Foreground = Brushes.Red; valid = false; dateValide = false;
                }
            }

            // Daca ambele date sunt completate corect, dar Finalul e inainte de Start -> EROARE
            if (dateValide && final <= start)
            {
                lblDataFinalJob.Foreground = Brushes.Red;
                valid = false;
            }

            // Daca ceva a picat, afisam mesajul general
            if (!valid)
            {
                lblEroareJob.Content = "Te rog să verifici câmpurile marcate cu roșu!";
                lblEroareJob.Visibility = Visibility.Visible;
            }

            return valid;
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