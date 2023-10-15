using System.Collections.Generic;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.Controller;

namespace CinemaSystemLibrary.ViewModel
{
    public class FilmVM : IFilmManagement
    {
        public void AddFilm(string title, int year, int genreId, string countryCode)
        {
            FilmManagerment.Instance.AddFilm(title, year, genreId, countryCode);
        }

        public void UpdateFilm(int filmId, string title, int year, int genreId, string countryCode)
        {
            FilmManagerment.Instance.UpdateFilm(filmId, title, year, genreId, countryCode);
        }

        public void DeleteFilm(int filmId)
        {
            FilmManagerment.Instance.DeleteFilm(filmId);
        }

        public List<Film> GetAllFilms()
        {
            return FilmManagerment.Instance.GetAllFilms();
        }
    }
}