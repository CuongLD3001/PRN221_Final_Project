using System;
using System.Collections.Generic;

namespace CinemaSystemLibrary.DataAccess
{
    public partial class Genre
    {
        public Genre()
        {
            Films = new HashSet<Film>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Film> Films { get; set; }
    }
}
