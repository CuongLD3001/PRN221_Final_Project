using System;
using System.Collections.Generic;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystemLibrary.Controller
{
    public class ShowManagement:IShowManagement
    {
        private static ShowManagement instance = null;
        private static readonly object instanceLock = new object();

        private readonly CinemaSystemContext context;

        public ShowManagement()
        {
            context = new CinemaSystemContext();
        }

        public static ShowManagement Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShowManagement();
                    }
                    return instance;
                }
            }
        }

        public void AddShow(int roomId, int filmId, DateTime showDate, double price, string status, int slot)
        {
            
                Show show = new Show
                {
                    RoomId = roomId,
                    FilmId = filmId,
                    ShowDate = showDate,
                    Price = price,
                    Status = status,
                    Slot = slot
                };

                context.Shows.Add(show);
                context.SaveChanges();
            
            
        }



        public void UpdateShow(int showId, int roomId, int filmId, DateTime showDate, double price, string status, int slot)
        {
            var show = context.Shows.FirstOrDefault(s => s.ShowId == showId);

            if (show != null)
            {
                show.RoomId = roomId;
                show.FilmId = filmId;
                show.ShowDate = showDate;
                show.Price = price;
                show.Status = status;
                show.Slot = slot;

                context.SaveChanges();
            }
        }

        public void DeleteShow(int showId)
        {
            var show = context.Shows.FirstOrDefault(s => s.ShowId == showId);

            if (show != null)
            {
                context.Shows.Remove(show);
                context.SaveChanges();
            }
        }

        public List<Show> GetAllShows()
        {

            return context.Shows.Include(s => s.Film).Include( s => s.Room).ToList();
        }
    }
}
