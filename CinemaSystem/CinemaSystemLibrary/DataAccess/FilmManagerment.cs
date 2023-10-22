using System;
using System.Collections.Generic;
using System.Linq;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.ViewModel;

namespace CinemaSystemLibrary.Controller
{
    public class FilmManagerment: IFilmManagement
    {
        private static FilmManagerment instance = null;
        private static readonly object instanceLock = new object();

        private readonly CinemaSystemContext context;

        public FilmManagerment()
        {
            context = new CinemaSystemContext();
        }

        public static FilmManagerment Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new FilmManagerment();
                    }
                    return instance;
                }
            }
        }

        public void AddFilm(string title, int year, int genreId, string countryCode)
        {
            Film film = new Film
            {
                Title = title,
                Year = year,
                GenreId = genreId,
                CountryCode = countryCode
            };

            context.Films.Add(film);
            context.SaveChanges();
        }

        public void UpdateFilm(int filmId, string title, int year, int genreId, string countryCode)
        {
            var film = context.Films.FirstOrDefault(f => f.FilmId == filmId);

            if (film != null)
            {
                film.Title = title;
                film.Year = year;
                film.GenreId = genreId;
                film.CountryCode = countryCode;

                context.SaveChanges();
            }
        }

        public void DeleteFilm(int filmId)
        {
            var film = context.Films.FirstOrDefault(f => f.FilmId == filmId);

            if (film != null)
            {
                context.Films.Remove(film);
                context.SaveChanges();
            }
        }

        public List<Film> GetAllFilms()
        {
            return context.Films.ToList();
        }
    }
}
