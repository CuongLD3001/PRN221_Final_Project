using System;
using System.Collections.Generic;
using CinemaSystemLibrary.DataAccess;

namespace CinemaSystemLibrary.ViewModel
{
    public interface IShowManagement
    {
        int AddShow(int roomId, int filmId, DateTime showDate, double price, string status, int slot);
        void UpdateShow(int showId, int roomId, int filmId, DateTime showDate, double price, string status, int slot);
        void DeleteShow(int showId);
        List<Show> GetAllShows();

        List<Show> FindShowByFilmAndDate(DateTime? date, int filmId);
    }
}
