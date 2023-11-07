﻿using System;
using System.Collections.Generic;
using System.Linq;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.ViewModel;
using Microsoft.EntityFrameworkCore;

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
            // Kiểm tra xem đã tồn tại bộ phim với Title và Year tương tự trong cơ sở dữ liệu chưa
            var existingFilm = context.Films.FirstOrDefault(f => f.Title == title && f.Year == year);

            if (existingFilm == null)
            {
                // Nếu không có bộ phim nào có Title và Year tương tự, thì tạo một bộ phim mới
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
            else
            {
                throw new Exception("Bộ phim đã tồn tại!");
            }
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
            return context.Films
                .Include(x => x.Genre)
                .Include(x => x.CountryCodeNavigation)
                .Include(x => x.Shows)
                .ToList();
        }
    }
}
