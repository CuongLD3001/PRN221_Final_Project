using System;
using System.Collections.Generic;

namespace CinemaSystemLibrary.DataAccess
{
    public partial class Seat
    {
        public int ShowId { get; set; }
        public int Status { get; set; }
        public string? Customer { get; set; }
        public string Stt { get; set; } = null!;

        public virtual Show Show { get; set; } = null!;
    }
}
