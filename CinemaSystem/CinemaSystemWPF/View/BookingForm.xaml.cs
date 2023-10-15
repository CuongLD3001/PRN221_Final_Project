using CinemaSystemLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CinemaSystemLibrary.Views
{
    public partial class BookingForm : Window
    {
        private IBookingManagement _bookingManagement;
        private int showId;
        private double showPrice;

        public BookingForm(IBookingManagement bookingManagement, int showId, double showPrice)
        {
            _bookingManagement = bookingManagement;
            this.showId = showId;
            this.showPrice = showPrice;
            InitializeComponent();

            // Thêm sự kiện khi form được khởi tạo
            Loaded += BookingForm_Loaded;

            // Thêm sự kiện khi click nút "Book Seats"
            btnBookSeats.Click += BookSeats_Click;
        }

        // Sự kiện xảy ra khi form được tải
        private void BookingForm_Loaded(object sender, RoutedEventArgs e)
        {
            // Thêm logic để nạp thông tin showtime, phim, và ghế vào ComboBox và ListBox tại đây
        }

        // Sự kiện xảy ra khi click nút "Book Seats"
        private void BookSeats_Click(object sender, RoutedEventArgs e)
        {
            List<string> selectedSeats = new List<string>();

            foreach (var item in lstSelectedSeats.SelectedItems)
            {
                // Lấy ghế được chọn từ ListBox và thêm vào danh sách selectedSeats
                selectedSeats.Add(item.ToString());
            }

            if (selectedSeats.Count > 0)
            {
                // Thực hiện đặt vé
                BookSeats(selectedSeats);
            }
            else
            {
                MessageBox.Show("Please select at least one seat.");
            }
        }

        private void BookSeats(List<string> selectedSeats)
        {
            foreach (var seat in selectedSeats)
            {
                // Thực hiện đặt vé cho từng ghế
                _bookingManagement.AddBooking(showId, seat, CalculateAmount(selectedSeats.Count), "Booked");
            }

            // Cập nhật giao diện hoặc thông báo khi đặt vé thành công
            // Cập nhật DataGrid dgBookingInfo hoặc hiển thị thông tin đặt vé
        }

        private double CalculateAmount(int numSeats)
        {
            // Tính giá vé dựa trên số lượng ghế đã đặt và thông tin show
            // Có thể thêm logic tính giá vé ở đây
            return numSeats * showPrice;
        }

        // Tùy chỉnh và thêm các phương thức khác cần thiết

        // Đặt tên cho các Controls trong XAML để dễ dàng truy cập từ mã C#
        // Ví dụ: txtFilmTitle, cboShowTime, txtCustomerName, lstSelectedSeats, btnBookSeats, dgBookingInfo
    }
}
