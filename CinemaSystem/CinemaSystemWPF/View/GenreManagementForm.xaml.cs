using System;
using System.Collections.Generic;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.ViewModel;
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

        public GenreManagementForm()
        {
        }

        private void GenreManagementForm_Loaded(object sender, RoutedEventArgs e)
        {
            List<Genre> genres = _genreManagement.GetAllGenres();
            dgGenres.ItemsSource = genres;
            if ((Genre)dgGenres.SelectedItem != null)
            {
                txtGenreName.Text = ((Genre)dgGenres.SelectedItem).Name;
            }
            
        }

        private void AddGenre_Click(object sender, RoutedEventArgs e)
        {
            string genreName = txtGenreName.Text;

            _genreManagement.AddGenre(genreName);

            dgGenres.ItemsSource = _genreManagement.GetAllGenres();
        }

        private void UpdateGenre_Click(object sender, RoutedEventArgs e)
        {
            int genreId = ((Genre)dgGenres.SelectedItem).GenreId;
            string genreName = txtGenreName.Text;
            _genreManagement.UpdateGenre(genreId, genreName);

            dgGenres.ItemsSource = _genreManagement.GetAllGenres();
        }

        private void DeleteGenre_Click(object sender, RoutedEventArgs e)
        {
            int genreId = ((Genre)dgGenres.SelectedItem).GenreId;

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
            if (genre != null){
                txtGenreName.Text = genre.Name;
                txtGenreID.Text = genre.GenreId.ToString();
            }
        }
    }
}
