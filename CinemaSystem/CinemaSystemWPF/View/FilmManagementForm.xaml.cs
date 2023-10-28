using System;
using System.Collections.Generic;
using System.Windows;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.ViewModel;
using CinemaSystemWPF.View;

namespace CinemaSystemLibrary.Views
{
    public partial class FilmManagementForm : Window
    {
        private IFilmManagement _filmManagement;
        private ICountryManagement _countryManagement;
        private IGenreManagement _genreManagement;

        public FilmManagementForm(IFilmManagement filmManagement, ICountryManagement countryManagement, IGenreManagement genreManagement)
        {
            _filmManagement = filmManagement;
            _countryManagement = countryManagement;
            _genreManagement = genreManagement;
            InitializeComponent();

            // Thêm sự kiện khi form được khởi tạo
            Loaded += FilmManagementForm_Loaded;
        }

        // Sự kiện xảy ra khi form được tải
        private void FilmManagementForm_Loaded(object sender, RoutedEventArgs e)
        {
            // Nạp danh sách phim từ _filmManagement và hiển thị trong DataGrid
            List<Film> films = _filmManagement.GetAllFilms();
            dgFilms.ItemsSource = films;
        }

        // Sự kiện xảy ra khi click nút "Add Film"
        private void AddFilm_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ TextBox và ComboBox
            string title = txtFilmTitle.Text;
            int year = int.Parse(txtYear.Text);
            int genreId = ((Genre)cboGenre.SelectedItem).GenreId;
            string countryCode = ((Country)cboCountry.SelectedItem).CountryCode;

            // Thêm phim mới
            _filmManagement.AddFilm(title, year, genreId, countryCode);

            // Cập nhật DataGrid hoặc thông báo thành công
            dgFilms.ItemsSource = _filmManagement.GetAllFilms();
        }


        // Sự kiện xảy ra khi click nút "Update Film"
        private void UpdateFilm_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ TextBox, ComboBox và DataGrid
            int filmId = ((Film)dgFilms.SelectedItem).FilmId;
            string title = txtFilmTitle.Text;
            int year = int.Parse(txtYear.Text);
            int genreId = ((Genre)cboGenre.SelectedItem).GenreId;
            string countryCode = ((Country)cboCountry.SelectedItem).CountryCode;

            // Cập nhật phim
            _filmManagement.UpdateFilm(filmId, title, year, genreId, countryCode);

            // Cập nhật DataGrid hoặc thông báo thành công
            dgFilms.ItemsSource = _filmManagement.GetAllFilms();
        }

        // Sự kiện xảy ra khi click nút "Delete Film"
        private void DeleteFilm_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ DataGrid
            int filmId = ((Film)dgFilms.SelectedItem).FilmId;

            // Xóa phim
            _filmManagement.DeleteFilm(filmId);

            // Cập nhật DataGrid hoặc thông báo thành công
            dgFilms.ItemsSource = _filmManagement.GetAllFilms();
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuForm menu = new MenuForm();
            this.Visibility = Visibility.Hidden;
            menu.Show();
        }

        private void dgFilms_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Film film = (Film)dgFilms.SelectedItem;
            if (film != null)
            {
                txtFilmID.Text = film.FilmId.ToString();
                txtFilmTitle.Text = film.Title;
                txtYear.Text = film.Year.ToString();
                cboGenre.Text = film.GenreId.ToString();
                cboCountry.Text = film.CountryCode.ToString();
            }
        }

        private void cboGenre_Loaded(object sender, RoutedEventArgs e)
        {
           List<Genre> list = _genreManagement.GetAllGenres();
            cboGenre.ItemsSource = list;
            cboGenre.SelectedValuePath = "GenreId";
            cboGenre.DisplayMemberPath = "Name";
        }

        private void cboCountry_Loaded(object sender, RoutedEventArgs e)
        {
            List<Country> countries = _countryManagement.GetAllCountries();
            cboCountry.ItemsSource = countries;
            cboCountry.DisplayMemberPath = "CountryName";
            cboCountry.SelectedValuePath = "CountryCode";
        }





        // Tùy chỉnh và thêm các phương thức khác cần thiết
    }
}
