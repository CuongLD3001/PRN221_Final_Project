using System;
using System.Collections.Generic;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.Controller;

namespace CinemaSystemLibrary.ViewModel
{
    public class ShowVM : IShowManagement
    {
        public void AddShow(int roomId, int filmId, DateTime showDate, double price, string status, int slot)
        {
            ShowManagement.Instance.AddShow(roomId, filmId, showDate, price, status, slot);
        }

        public void UpdateShow(int showId, int roomId, int filmId, DateTime showDate, double price, string status, int slot)
        {
            ShowManagement.Instance.UpdateShow(showId, roomId, filmId, showDate, price, status, slot);
        }

        public void DeleteShow(int showId)
        {
            ShowManagement.Instance.DeleteShow(showId);
        }

        public List<Show> GetAllShows()
        {
            return ShowManagement.Instance.GetAllShows();
        }
    }
}
