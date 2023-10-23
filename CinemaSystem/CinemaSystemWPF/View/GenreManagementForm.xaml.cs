using System;
using System.Collections.Generic;
using CinemaSystemLibrary.ViewModel;
using CinemaSystemLibrary.DataAccess;
using System.Windows;
using CinemaSystemWPF.View;

namespace CinemaSystemLibrary.Views
{
    public partial class GenreManagementForm : Window
    {
        private IGenreManagement _genreManagement;

        public GenreManagementForm(IGenreManagement genreManagement)
        {
            _genreManagement = genreManagement;
            InitializeComponent();
            Loaded += GenreManagementForm_Loaded;
        }

        // Sự kiện xảy ra khi form được tải
        private void GenreManagementForm_Loaded(object sender, RoutedEventArgs e)
        {
            List<Genre> genres = _genreManagement.GetAllGenres();
            dgGenres.ItemsSource = genres;
            if ((Genre)dgGenres.SelectedItem != null)
            {
                txtGenreName.Text = ((Genre)dgGenres.SelectedItem).Name;
                txtGenreID.Text = ((Genre)dgGenres.SelectedItem).GenreId.ToString(); // Hiển thị Genre ID
            }
        }

        // Sự kiện xảy ra khi click nút "Add Genre"
        private void AddGenre_Click(object sender, RoutedEventArgs e)
        {
            string genreName = txtGenreName.Text;

            try
            {
                // Thêm thể loại mới
                _genreManagement.AddGenre(genreName);

                // Cập nhật DataGrid hoặc thông báo thành công
                dgGenres.ItemsSource = _genreManagement.GetAllGenres();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu thể loại đã tồn tại
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Sự kiện xảy ra khi click nút "Update Genre"
        private void UpdateGenre_Click(object sender, RoutedEventArgs e)
        {
            int genreId = int.Parse(txtGenreID.Text); // Lấy Genre ID từ TextBox
            string genreName = txtGenreName.Text;
            _genreManagement.UpdateGenre(genreId, genreName);

            dgGenres.ItemsSource = _genreManagement.GetAllGenres();
        }

        // Sự kiện xảy ra khi click nút "Delete Genre"
        private void DeleteGenre_Click(object sender, RoutedEventArgs e)
        {
            int genreId = int.Parse(txtGenreID.Text); // Lấy Genre ID từ TextBox

            _genreManagement.DeleteGenre(genreId);

            dgGenres.ItemsSource = _genreManagement.GetAllGenres();
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuForm menu = new MenuForm();
            this.Visibility = Visibility.Hidden;
            menu.Show();
        }

        private void dgGenres_Selected(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Genre genre = (Genre)dgGenres.SelectedItem;
            if (genre != null)
            {
                txtGenreName.Text = genre.Name;
                txtGenreID.Text = genre.GenreId.ToString();
            }
        }

        // Tùy chỉnh và thêm các phương thức khác cần thiết
    }
}
