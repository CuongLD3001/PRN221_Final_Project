using CinemaSystemLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSystemLibrary.ViewModel
{
    public interface ISeatManagement
    {
        List<Seat> GetSeatByShow(int showId);

        Seat GetSeatByStt(string id, int showId);

        void AddSeat (int showId, int status, string customer, string stt);

        void UpdateSeat(int showId, int status, string customer, string stt);
    }
}
