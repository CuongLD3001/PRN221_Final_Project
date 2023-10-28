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
        public List<int> slots = new List<int> { 1, 2, 3, 4, 5,6, 7, 8, 9 };

        public ShowManagementForm(IShowManagement showManagement, IRoomManagement roomManagement, IFilmManagement filmManagement)
        {
            _showManagement = showManagement;
            _roomManagement = roomManagement;
            _filmManagement = filmManagement;
            InitializeComponent();

            // Thêm sự kiện khi form được khởi tạo
            Loaded += ShowManagementForm_Loaded;

            // Thêm sự kiện khi click nút "Add Show"
            btnAddShow.Click += AddShow_Click;

            // Thêm sự kiện khi click nút "Update Show"
            btnUpdateShow.Click += UpdateShow_Click;

            // Thêm sự kiện khi click nút "Delete Show"
            btnDeleteShow.Click += DeleteShow_Click;
            _roomManagement = roomManagement;
            _filmManagement = filmManagement;
        }

        // Sự kiện xảy ra khi form được tải
        private void ShowManagementForm_Loaded(object sender, RoutedEventArgs e)
        {
            // Nạp danh sách các show từ _showManagement và hiển thị trong DataGrid
            List<Show> shows = _showManagement.GetAllShows();
            dgShows.ItemsSource = shows;
        }

        // Sự kiện xảy ra khi click nút "Add Show"
        private void AddShow_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ TextBox, ComboBox và DatePicker
            int roomId = ((Room)cboRoom.SelectedItem).RoomId;
            int filmId = ((Film)cboFilm.SelectedItem).FilmId;
            DateTime showDate = dpShowDate.SelectedDate ?? DateTime.Now;
            double price = double.Parse(txtPrice.Text);
            string status = txtStatus.Text;
            int slot = int.Parse(cboSlot.Text);

            // Thêm show mới
            _showManagement.AddShow(roomId, filmId, showDate, price, status, slot);

            // Cập nhật DataGrid hoặc thông báo thành công
            List<Show> shows = _showManagement.GetAllShows();
            dgShows.ItemsSource = shows;
        }

        // Sự kiện xảy ra khi click nút "Update Show"
        private void UpdateShow_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ TextBox, ComboBox, DatePicker và DataGrid
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
        }

        // Sự kiện xảy ra khi click nút "Delete Show"
        private void DeleteShow_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ DataGrid
            int showId = ((Show)dgShows.SelectedItem).ShowId;

            // Xóa show
            _showManagement.DeleteShow(showId);

            // Cập nhật DataGrid hoặc thông báo thành công
            List<Show> shows = _showManagement.GetAllShows();
            dgShows.ItemsSource = shows;
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
