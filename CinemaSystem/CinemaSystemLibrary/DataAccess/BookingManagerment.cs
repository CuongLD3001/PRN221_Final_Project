using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystemLibrary.DataAccess
{
    public class BookingManagerment
    {
        private static BookingManagerment instance = null;
        private static readonly object instanceLock = new object();

        private readonly CinemaSystemContext context;

        private BookingManagerment()
        {
            context = new CinemaSystemContext();
        }

        public static BookingManagerment Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookingManagerment();
                    }
                    return instance;
                }
            }
        }

        public void AddBooking(int showId, string seatNumber, double? amount, string status)
        {
            Booking booking = new Booking
            {
                ShowId = showId,
                SeatNumber = seatNumber,
                Amount = amount,
                Status = status
            };

            context.Bookings.Add(booking);
            context.SaveChanges();
        }

        public List<Booking> GetBookingsForShow(int showId)
        {
            return context.Bookings.Where(b => b.ShowId == showId).ToList();
        }
    }
}
