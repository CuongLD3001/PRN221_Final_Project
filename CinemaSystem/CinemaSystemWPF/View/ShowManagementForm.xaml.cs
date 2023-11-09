using System;
using System.Collections.Generic;
using System.Windows;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.ViewModel;
using CinemaSystemWPF.View;

namespace CinemaSystemLibrary.Views
{
    public partial class ShowManagementForm : Window
    {
        private IShowManagement _showManagement;
        private IRoomManagement _roomManagement;
        private IFilmManagement _filmManagement;
        private ISeatManagement _seatManagement;
        public List<int> slots = new List<int> { 1, 2, 3, 4, 5,6, 7, 8, 9 };
        

        public ShowManagementForm(IShowManagement showManagement, IRoomManagement roomManagement, IFilmManagement filmManagement, ISeatManagement seatManagement)
        {
            _showManagement = showManagement;
            _roomManagement = roomManagement;
            _filmManagement = filmManagement;
            InitializeComponent();

            // Thêm sự kiện khi form được khởi tạo
            Loaded += ShowManagementForm_Loaded;

            _roomManagement = roomManagement;
            _filmManagement = filmManagement;
            _seatManagement = seatManagement;
        }

        // Sự kiện xảy ra khi form được tải
        private void ShowManagementForm_Loaded(object sender, RoutedEventArgs e)
        {
            // Nạp danh sách các show từ _showManagement và hiển thị trong DataGrid
            List<Show> shows = _showManagement.GetAllShows();
            dgShows.ItemsSource = shows;
        }

        // Sự kiện xảy ra khi click nút "Add Show"
        // Sự kiện xảy ra khi click nút "Add Show"
        private void AddShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy thông tin từ TextBox, ComboBox và DatePicker
                int roomId = ((Room)cboRoom.SelectedItem).RoomId;
                int filmId = ((Film)cboFilm.SelectedItem).FilmId;
                DateTime showDate = dpShowDate.SelectedDate ?? DateTime.Now;
                double price = double.Parse(txtPrice.Text);
                string status = txtStatus.Text;
                int slot = int.Parse(cboSlot.Text);

                // Thêm show mới
                int showId = _showManagement.AddShow(roomId, filmId, showDate, price, status, slot);
                for (int i = 1; i<= 25; i++)
                {
                    _seatManagement.AddSeat(showId, 0, null, i.ToString());
                }
                // Cập nhật DataGrid hoặc thông báo thành công
                List<Show> shows = _showManagement.GetAllShows();
                dgShows.ItemsSource = shows;
                MessageBox.Show("Show đã được thêm thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }


        // Sự kiện xảy ra khi click nút "Update Show"
        // Sự kiện xảy ra khi click nút "Update Show"
        private void UpdateShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra xem có một phần tử nào đang được chọn hay không
                if (dgShows.SelectedItem != null)
                {
                    // Lấy thông tin từ DataGrid
                    int showId = ((Show)dgShows.SelectedItem).ShowId;
                    int roomId = ((Room)cboRoom.SelectedItem).RoomId;
                    int filmId = ((Film)cboFilm.SelectedItem).FilmId;
                    DateTime showDate = dpShowDate.SelectedDate ?? DateTime.Now;
                    double price = double.Parse(txtPrice.Text);
                    string status = txtStatus.Text;
                    int slot = int.Parse(cboSlot.Text);
                    // Cập nhật show
                    _showManagement.UpdateShow(showId, roomId, filmId, showDate, price, status, slot);

                    // Cập nhật DataGrid hoặc thông báo thành công
                    List<Show> shows = _showManagement.GetAllShows();
                    dgShows.ItemsSource = shows;
                    MessageBox.Show("Show đã được cập nhật thành công.");
                }
                else
                {
                    // Xử lý tại đây khi không có phần tử nào được chọn
                    MessageBox.Show("Vui lòng chọn một show để cập nhật.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }


        // Sự kiện xảy ra khi click nút "Delete Show"
        private void DeleteShow_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem có một phần tử nào đang được chọn hay không
            if (dgShows.SelectedItem != null)
            {
                // Lấy thông tin từ DataGrid
                int showId = ((Show)dgShows.SelectedItem).ShowId;

                // Xóa show
                _showManagement.DeleteShow(showId);

                // Cập nhật DataGrid hoặc thông báo thành công
                List<Show> shows = _showManagement.GetAllShows();
                dgShows.ItemsSource = shows;
            }
            else
            {
                // Xử lý tại đây khi không có phần tử nào được chọn
                MessageBox.Show("Vui lòng chọn một show để xóa.");
            }
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuForm menu = new MenuForm();
            this.Visibility = Visibility.Hidden;
            menu.Show();
        }

        private void cboRoom_Loaded(object sender, RoutedEventArgs e)
        {
            List<Room> rooms = _roomManagement.GetAllRooms();
            cboRoom.ItemsSource = rooms;
            cboRoom.DisplayMemberPath = "Name";
            cboRoom.SelectedValuePath = "RoomId";
        }

        private void cboSlot_Loaded(object sender, RoutedEventArgs e)
        {
            cboSlot.ItemsSource = slots;

        }

        private void cboFilm_Loaded(object sender, RoutedEventArgs e)
        {
            List<Film> films = _filmManagement.GetAllFilms();
            cboFilm.ItemsSource = films;
            cboFilm.DisplayMemberPath = "Title";
            cboFilm.SelectedValuePath = "FilmId";
        }


        private void dgShows_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Show show = (Show)dgShows.SelectedItem;
            if (show != null)
            {
                txtShowId.Text = show.ShowId.ToString();
                txtPrice.Text = show.Price.ToString();
                txtStatus.Text = show.Status.ToString();
                cboFilm.Text = show.Film.Title;
                cboRoom.Text = show.Room.Name;
                cboSlot.Text = show.Slot.ToString();
                dpShowDate.Text = show.ShowDate.ToString();
            }
        }



        // Tùy chỉnh và thêm các phương thức khác cần thiết
    }
}
