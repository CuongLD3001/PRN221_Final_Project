using System;
using System.Collections.Generic;

namespace CinemaSystemLibrary.DataAccess
{
    public partial class Country
    {
        public Country()
        {
            Films = new HashSet<Film>();
        }

        public string CountryCode { get; set; } = null!;
        public string CountryName { get; set; } = null!;

        public virtual ICollection<Film> Films { get; set; }
    }
}
