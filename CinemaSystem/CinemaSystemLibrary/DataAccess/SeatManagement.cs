using CinemaSystemLibrary.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CinemaSystemLibrary.ViewModel;
using System.Threading.Tasks;

namespace CinemaSystemLibrary.DataAccess
{
    public class SeatManagement : ISeatManagement
    {
        private static SeatManagement instance = null;
        private static readonly object instanceLock = new object();

        private readonly CinemaSystemContext context;

        public static SeatManagement Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new SeatManagement();
                    }
                    return instance;
                }
            }
        }
        public SeatManagement()
        {
            context = new CinemaSystemContext();
        }
        public List<Seat> GetSeatByShow(int showId)
        {
            return context.Seats.Where(x => x.ShowId == showId).ToList();
        }

        public Seat GetSeatByStt(string id, int showId)
        {
            return context.Seats.SingleOrDefault(x => x.Stt == id && x.ShowId == showId);
        }

        public void AddSeat(int showId, int status, string customer, string stt)
        {
            Seat seat = new Seat
            {
                ShowId = showId,
                Stt = stt,
                Customer = customer,
                Status = status
            };
            context.Seats.Add(seat);
            context.SaveChanges();
        }

        public void UpdateSeat(int showId, int status, string customer, string stt)
        {
            Seat seat = context.Seats.SingleOrDefault(x => x.ShowId == showId && x.Stt.Equals(stt));
            seat.Status = status;
            seat.Customer = customer;
            context.SaveChanges();
        }
    }
}
