using System;
using System.Collections.Generic;
using CinemaSystemLibrary.DataAccess;

namespace CinemaSystemLibrary.ViewModel
{
    public interface IBookingManagement
    {
        void AddBooking(int showId, string seatNumber, double? amount, string status);
        List<Booking> GetBookings();
    }
}
