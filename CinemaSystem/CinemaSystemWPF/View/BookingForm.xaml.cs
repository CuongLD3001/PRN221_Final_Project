using CinemaSystemLibrary.Controller;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.ViewModel;
using CinemaSystemWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace CinemaSystemLibrary.Views
{
    public partial class BookingForm : Window
    {
        private IBookingManagement _bookingManagement;
        private IFilmManagement _filmManagement;
        private IShowManagement _showManagement;
        private ISeatManagement _seatManagement;
        private Show currentShow = new Show();
        private int showId;
        private double showPrice = 0;
        public List<String> selectedSeats = new List<string>();
        public double totalPrice = 0;
        public BookingForm(IBookingManagement bookingManagement, IFilmManagement filmManagement, IShowManagement showManagement, ISeatManagement seatManagement)
        {
            _bookingManagement = bookingManagement;
            _filmManagement = filmManagement;
            _showManagement = showManagement;
            _seatManagement = seatManagement;
            InitializeComponent();

            // Thêm sự kiện khi form được khởi tạo
            Loaded += BookingForm_Loaded;

        }

        // Sự kiện xảy ra khi form được tải
        private void BookingForm_Loaded(object sender, RoutedEventArgs e)
        {
            cboFilm.ItemsSource = _filmManagement.GetAllFilms().ToList();
            cboFilm.DisplayMemberPath = "Title";
            cboFilm.SelectedValuePath = "FilmId";
            cboFilm.SelectedIndex = 0;
            dgBookingInfo.ItemsSource = _bookingManagement.GetBookings();
        }

        // Sự kiện xảy ra khi click nút "Book Seats"
        private void BookSeats_Click(object sender, RoutedEventArgs e)
        {
            if(showPrice == 0)
            {
                MessageBox.Show("Hãy chọn giờ chiếu phim.");
                return;
            }
            if (selectedSeats.Count == 0)
            {
                return;
            }
            foreach (var stt in selectedSeats)
            {
                _seatManagement.UpdateSeat(showId, 1, txtCustomerName.Text, stt);
                

            }
            _bookingManagement.AddBooking(showId, getSeat(selectedSeats), totalPrice, txtCustomerName.Text);
            seat_Load();
            dgBookingInfo.ItemsSource = _bookingManagement.GetBookings();
        }
        private string getSeat(List<string> selectedSeats)
        {
            String seats = "";
            foreach (var stt in selectedSeats)
            {
                if ((int.Parse(stt) - 1) / 5 == 0)
                {
                    seats += "; A" + (int.Parse(stt) - 1) % 5;
                }
                if ((int.Parse(stt) - 1) / 5 == 1)
                {
                    seats += "; B" + (int.Parse(stt) - 1) % 5;
                    
                }
                if ((int.Parse(stt) - 1) / 5 == 2)
                {
                    seats += "; C" + (int.Parse(stt) - 1) % 5;
                    
                }
                if ((int.Parse(stt) - 1) / 5 == 3)
                {
                    seats += "; D" + (int.Parse(stt) - 1) % 5;
                    
                }
                if ((int.Parse(stt) - 1) / 5 == 4)
                {
                    seats += "; E" + (int.Parse(stt) - 1) % 5;
                    
                }
            }
            return seats.Substring(2);
        }


        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuForm menu = new MenuForm();
            this.Visibility = Visibility.Hidden;
            menu.Show();
        }

        private void dpkDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DateTime? date = dpkDate.SelectedDate;
            int filmId = ((Film)cboFilm.SelectedItem).FilmId;
            List<Show> shows = _showManagement.FindShowByFilmAndDate(date, filmId);
            if(shows.Count == 0 && date.HasValue)
            {
                MessageBox.Show("Không có suất chiếu nào trong ngày này.");
                for (int i = 1; i <= 25; i++)
                {
                    Button seatButton = (Button)FindName("seat_" + i);
                    seatButton.IsEnabled = false;
                }
            }
            if(shows.Count > 0){
                cboSlot.ItemsSource = shows;
                cboSlot.DisplayMemberPath = "Slot";
                cboSlot.SelectedIndex = 0;
            }

        }

        private void seat_Click(object sender, RoutedEventArgs e)
        {
            if (showPrice == 0)
            {
                MessageBox.Show("Hãy chọn giờ chiếu phim.");
                return;
            }
            Button clickedButton = (Button)sender;
            if(clickedButton.Background == Brushes.AntiqueWhite)
            {
                clickedButton.Background = Brushes.OrangeRed;
                string stt = clickedButton.Name.Split("_")[1];
                selectedSeats.Add(stt);
                totalPrice += showPrice;
                String msg = "Film: " + currentShow.Film.Title;
                msg += "\nRoom: " + currentShow.Room.Name;
                msg += "\nSeat: " + getSeat(selectedSeats);
                msg += "\nTotal Price: $" + totalPrice;
                totalPriceLabel.Content = msg;
                return;
            }
            if (clickedButton.Background == Brushes.OrangeRed)
            {
                clickedButton.Background = Brushes.AntiqueWhite;
                string stt = clickedButton.Name.Split("_")[1];
                selectedSeats.Remove(stt);
                totalPrice -= showPrice;
                String msg = "Film: " + currentShow.Film.Title;
                msg += "\nRoom: " + currentShow.Room.Name;
                msg += "\nSeat: " + getSeat(selectedSeats);
                msg += "\nTotal Price: $" + totalPrice;
                totalPriceLabel.Content = msg;
                return;
            }

        }

        private void cboSlot_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Show show = (Show)cboSlot.SelectedItem;
            if(show != null)
            {
                showId = show.ShowId;
                showPrice = show.Price;
                currentShow = show;
            }
            seat_Load();
        }
        private void seat_Load()
        {
            List<Seat> seats = _seatManagement.GetSeatByShow(showId);
            foreach (Seat s in seats)
            {
                
                Button seatButton = (Button)FindName("seat_" + s.Stt);
                seatButton.Background = Brushes.AntiqueWhite;
                if (s.Status == 1)
                {
                    if (seatButton != null)
                    {
                        seatButton.Background = Brushes.Red;
                        seatButton.IsEnabled = false;
                    }
                }else
                {
                    seatButton.IsEnabled = true;
                }
            }
            selectedSeats = new List<string>();
            totalPrice = 0;
            totalPriceLabel.Content = "Total Price: $0" ;

        }

        private void dgBookingInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        // Tùy chỉnh và thêm các phương thức khác cần thiết

        // Đặt tên cho các Controls trong XAML để dễ dàng truy cập từ mã C#
        // Ví dụ: txtFilmTitle, cboShowTime, txtCustomerName, lstSelectedSeats, btnBookSeats, dgBookingInfo
    }
}