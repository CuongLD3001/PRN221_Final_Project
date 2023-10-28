using CinemaSystemLibrary.ViewModel;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;

namespace CinemaSystemWPF.View
{
    /// <summary>
    /// Interaction logic for CountryManagementForm.xaml
    /// </summary>
    public partial class CountryManagementForm : Window
    {
        ICountryManagement _countryManagement;
        public CountryManagementForm(ICountryManagement countryManagement)
        {
            _countryManagement = countryManagement;
            InitializeComponent();
           Loaded += CountryManagementForm_Loaded;
        }

        private void CountryManagementForm_Loaded(object sender, RoutedEventArgs e)
        {
            List<Country> countries = _countryManagement.GetAllCountries();
            dgCountrys.ItemsSource = countries;
            if ((Country)dgCountrys.SelectedItem!= null)
            {
                txtCountryCode.Text = ((Country)dgCountrys.SelectedItem).CountryCode;
                txtCountryName.Text = ((Country)dgCountrys.SelectedItem).CountryName;
            }
        }

        private void AddCountry_Click(object sender, RoutedEventArgs e)
        {
            string countryCode = txtCountryCode.Text;
            string countryName = txtCountryName.Text;
            if (countryCode == null || countryName == null)
            {
                MessageBox.Show("Code and Name must be required");
            }
            try
            {
                _countryManagement.AddCountry(countryCode, countryName);
                dgCountrys.ItemsSource = _countryManagement.GetAllCountries();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateCountry_Click(object sender, RoutedEventArgs e)
        {
            string countryCode = txtCountryCode.Text;
            string countryName = txtCountryName.Text;
            try
            {
                _countryManagement.UpdateCountry(countryCode, countryName);
                dgCountrys.ItemsSource = _countryManagement.GetAllCountries();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteCountry_Click(object sender, RoutedEventArgs e)
        {
            string countryCode = txtCountryCode.Text;
            try
            {
                _countryManagement.DeleteCountry(countryCode);
                dgCountrys.ItemsSource = _countryManagement.GetAllCountries();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuForm menu = new MenuForm();
            this.Visibility = Visibility.Hidden;
            menu.ShowDialog();
        }

        private void dgCountrys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Country c = (Country)dgCountrys.SelectedItem;
            if (c != null)
            {
                txtCountryCode.Text = c.CountryCode;
                txtCountryName.Text = c.CountryName;
            }
        }
    }
}
