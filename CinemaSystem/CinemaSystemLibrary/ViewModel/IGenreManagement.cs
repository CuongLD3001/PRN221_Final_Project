using CinemaSystemLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSystemLibrary.ViewModel
{
    public interface IGenreManagement
    {
       public void AddGenre(string name)
        {
            GenreManagement.Instance.AddGenre(name);
        }
        void DeleteGenre(int genreId)
        {
            GenreManagement.Instance.DeleteGenre(genreId);
        }
        void UpdateGenre(int genreId, string name)
        {
            GenreManagement.Instance.UpdateGenre(genreId, name);
        }
        List<Genre> GetAllGenres()
        {
            return GenreManagement.Instance.GetAllGenres();
        }
    }
}
