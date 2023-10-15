using System;
using System.Collections.Generic;

namespace CinemaSystemLibrary.DataAccess;

public partial class Show
{
    public int ShowId { get; set; }

    public int RoomId { get; set; }

    public int FilmId { get; set; }

    public DateTime ShowDate { get; set; }

    public double Price { get; set; }

    public string Status { get; set; } = null!;

    public int Slot { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Film Film { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
