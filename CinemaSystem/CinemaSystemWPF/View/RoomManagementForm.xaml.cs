using System;
using System.Collections.Generic;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.ViewModel;
using System.Windows;

namespace CinemaSystemLibrary.Views
{
    public partial class RoomManagementForm : Window
    {
        private IRoomManagement _roomManagement;

        public RoomManagementForm(IRoomManagement roomManagement)
        {
            _roomManagement = roomManagement;
            InitializeComponent();

            // Thêm sự kiện khi form được khởi tạo
            Loaded += RoomManagementForm_Loaded;

            // Thêm sự kiện khi click nút "Add Room"
            btnAddRoom.Click += AddRoom_Click;

            // Thêm sự kiện khi click nút "Update Room"
            btnUpdateRoom.Click += UpdateRoom_Click;

            // Thêm sự kiện khi click nút "Delete Room"
            btnDeleteRoom.Click += DeleteRoom_Click;
        }

        // Sự kiện xảy ra khi form được tải
        private void RoomManagementForm_Loaded(object sender, RoutedEventArgs e)
        {
            // Nạp danh sách phòng từ _roomManagement và hiển thị trong DataGrid
            List<Room> rooms = _roomManagement.GetAllRooms();
            dgRooms.ItemsSource = rooms;
        }

        // Sự kiện xảy ra khi click nút "Add Room"
        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ TextBox
            string roomName = txtRoomName.Text;
            int numberOfRows = int.Parse(txtNumberOfRows.Text);
            int numberOfColumns = int.Parse(txtNumberOfColumns.Text);

            // Thêm phòng mới
            _roomManagement.AddRoom(roomName, numberOfRows, numberOfColumns);

            // Cập nhật DataGrid hoặc thông báo thành công
            dgRooms.ItemsSource = _roomManagement.GetAllRooms();
        }

        // Sự kiện xảy ra khi click nút "Update Room"
        private void UpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ TextBox và DataGrid
            int roomId = ((Room)dgRooms.SelectedItem).RoomId;
            string roomName = txtRoomName.Text;
            int numberOfRows = int.Parse(txtNumberOfRows.Text);
            int numberOfColumns = int.Parse(txtNumberOfColumns.Text);

            // Cập nhật phòng
            _roomManagement.UpdateRoom(roomId, roomName, numberOfRows, numberOfColumns);

            // Cập nhật DataGrid hoặc thông báo thành công
            dgRooms.ItemsSource = _roomManagement.GetAllRooms();
        }

        // Sự kiện xảy ra khi click nút "Delete Room"
        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ DataGrid
            int roomId = ((Room)dgRooms.SelectedItem).RoomId;

            // Xóa phòng
            _roomManagement.DeleteRoom(roomId);

            // Cập nhật DataGrid hoặc thông báo thành công
            dgRooms.ItemsSource = _roomManagement.GetAllRooms();
        }

        // Tùy chỉnh và thêm các phương thức khác cần thiết
    }
}
