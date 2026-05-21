using Clase;
using Date;
using System;
using System.Windows;
using System.Windows.Controls;

namespace InterfataWPF
{
    public partial class VehiculeWindow : Window
    {
        AdministrareVehicule_FisierText adminVehicule = new AdministrareVehicule_FisierText();

        public VehiculeWindow()
        {
            InitializeComponent();
            IncarcaVehicule();
        }

        private void IncarcaVehicule()
        {
            dgVehicule.ItemsSource = adminVehicule.GetVehicule();
        }

        private void BtnAdauga_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Vehicul vNou = CreazaVehiculDinFormular();
                adminVehicule.AdaugaVehicul(vNou);
                IncarcaVehicule();
            }
            catch { MessageBox.Show("Verifică datele introduse!"); }
        }

        private void BtnModifica_Click(object sender, RoutedEventArgs e)
        {
            if (dgVehicule.SelectedItem is Vehicul vehiculSelectat)
            {
                try
                {
                    Vehicul vNou = CreazaVehiculDinFormular();
                    adminVehicule.ModificaVehicul(vehiculSelectat.Marca, vNou);
                    IncarcaVehicule();
                }
                catch { MessageBox.Show("Verifică datele introduse!"); }
            }
            else MessageBox.Show("Selectează un vehicul din tabel!");
        }

        private void BtnSterge_Click(object sender, RoutedEventArgs e)
        {
            if (dgVehicule.SelectedItem is Vehicul vehiculSelectat)
            {
                adminVehicule.StergeVehicul(vehiculSelectat.Marca);
                IncarcaVehicule();
            }
            else MessageBox.Show("Selectează un vehicul din tabel pentru ștergere!");
        }

        private void dgVehicule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgVehicule.SelectedItem is Vehicul v)
            {
                txtMarca.Text = v.Marca;
                txtModel.Text = v.Model;
                txtAn.Text = v.An.ToString();
                txtKm.Text = v.Kilometri.ToString();
            }
        }

        private Vehicul CreazaVehiculDinFormular()
        {
            Culoare culoareAleasa = Culoare.Alb;
            if (rbNegru.IsChecked == true) culoareAleasa = Culoare.Negru;
            else if (rbRosu.IsChecked == true) culoareAleasa = Culoare.Rosu;

            OptiuniVehicul optiuniAlese = OptiuniVehicul.Niciuna;
            if (chkAC.IsChecked == true) optiuniAlese |= OptiuniVehicul.AerConditionat;
            if (chkNav.IsChecked == true) optiuniAlese |= OptiuniVehicul.Navigatie;

            return new Vehicul(txtMarca.Text, txtModel.Text, int.Parse(txtAn.Text), int.Parse(txtKm.Text), culoareAleasa, optiuniAlese);
        }
    }
}