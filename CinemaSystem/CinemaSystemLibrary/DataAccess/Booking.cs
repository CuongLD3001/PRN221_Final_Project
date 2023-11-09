using System;
using System.Collections.Generic;

namespace CinemaSystemLibrary.DataAccess
{
    public partial class Booking
    {
        public int BookingId { get; set; }
        public int ShowId { get; set; }
        public string SeatNumber { get; set; } = null!;
        public double? Amount { get; set; }
        public string Status { get; set; } = null!;

        public virtual Show Show { get; set; } = null!;
    }
}
