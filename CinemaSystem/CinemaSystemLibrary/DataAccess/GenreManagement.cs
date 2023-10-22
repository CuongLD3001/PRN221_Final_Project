using CinemaSystemLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSystemLibrary.DataAccess
{
    public class GenreManagement:IGenreManagement
    {
        private static GenreManagement instance = null;
        private static readonly object instanceLock = new object();
        private readonly CinemaSystemContext context;
        public GenreManagement()
        {
            context = new CinemaSystemContext();
        }
        public static GenreManagement Instance
        {
            get
            {
                lock(instanceLock)
                {
                    instance= new GenreManagement();
                }
                return instance;
            }
        }

        public void AddGenre(string name)
        {
            Genre g = new Genre()
            {
                Name = name
            };
            context.Genres.Add(g);
            context.SaveChanges();
        }
        public void UpdateGenre(int genreId, string name)
        {
            var genre = context.Genres.FirstOrDefault( g => g.GenreId == genreId);
            if (genre != null)
            {
                genre.Name = name;
            }
            context.SaveChanges();
        }

        public void DeleteGenre(int genreId)
        {
            var genre = context.Genres.FirstOrDefault(g => g.GenreId==genreId);
            if (genre != null)
            {
                context.Genres.Remove(genre);
                context.SaveChanges();
            }
        }
        public List<Genre> GetAllGenres()
        {
            return context.Genres.ToList();
        }
    }
}
