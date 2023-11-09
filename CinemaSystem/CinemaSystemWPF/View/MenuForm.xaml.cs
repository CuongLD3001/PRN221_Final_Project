using CinemaSystemLibrary.Controller;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.ViewModel;
using CinemaSystemLibrary.Views;
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

namespace CinemaSystemWPF.View
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class MenuForm : Window
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void GenreManagement_Click(object sender, RoutedEventArgs e)
        {
            IGenreManagement genreManagement = new GenreManagement();
            GenreManagementForm genreForm = new GenreManagementForm(genreManagement);
            this.Visibility = Visibility.Hidden;
            genreForm.Show();


        }

        private void FilmManagement_Click(object sender, RoutedEventArgs e)
        {
            IFilmManagement filmManagement = new FilmManagerment();
            ICountryManagement countryManagement = new CountryManagement();
            IGenreManagement genreManagement = new GenreManagement();
            FilmManagementForm firmForm = new FilmManagementForm(filmManagement, countryManagement, genreManagement);
            this.Visibility = Visibility.Hidden;
            firmForm.Show();
        }

        private void BookingManagement_Click(object sender, RoutedEventArgs e)
        {
            IBookingManagement bookingManagement = new BookingManagerment();
            IFilmManagement filmManagement = new FilmManagerment();
            IShowManagement showManagement = new ShowManagement();
            ISeatManagement seatManagement = new SeatManagement();
            BookingForm booking = new BookingForm(bookingManagement, filmManagement, showManagement, seatManagement);
            this.Visibility = Visibility.Hidden;
            booking.Show();
        }

        private void ShowManagement_Click(object sender, RoutedEventArgs e)
        {
            IShowManagement showManagement = new ShowManagement();
            IRoomManagement roomManagement = new RoomManagerment();
            IFilmManagement filmManagement = new FilmManagerment();
            ISeatManagement seatManagement = new SeatManagement();
            ShowManagementForm showForm = new ShowManagementForm(showManagement, roomManagement, filmManagement, seatManagement);
            this.Visibility = Visibility.Hidden;
            showForm.Show();

        }

        private void RoomManagement_Click(object sender, RoutedEventArgs e)
        {
            IRoomManagement roomManagement = new RoomManagerment();
            RoomManagementForm roomForm = new RoomManagementForm(roomManagement);
            this.Visibility = Visibility.Hidden;
            roomForm.Show();

        }

        private void CountryManagement_Click(object sender, RoutedEventArgs e)
        {
            ICountryManagement countryManagement = new CountryManagement();
            CountryManagementForm countryForm = new CountryManagementForm(countryManagement);
            this.Visibility = Visibility.Hidden;
            countryForm.Show();
        }
    }
}
