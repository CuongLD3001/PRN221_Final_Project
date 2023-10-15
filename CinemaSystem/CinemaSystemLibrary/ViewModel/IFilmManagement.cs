using System;
using System.Collections.Generic;
using CinemaSystemLibrary.DataAccess;

namespace CinemaSystemLibrary.ViewModel
{
    public interface IFilmManagement
    {
        void AddFilm(string title, int year, int genreId, string countryCode);
        void UpdateFilm(int filmId, string title, int year, int genreId, string countryCode);
        void DeleteFilm(int filmId);
        List<Film> GetAllFilms();
    }
}
