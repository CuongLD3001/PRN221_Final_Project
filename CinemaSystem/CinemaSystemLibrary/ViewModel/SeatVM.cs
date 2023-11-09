using CinemaSystemLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaSystemLibrary.ViewModel
{
    public class SeatVM : ISeatManagement
    {
        public void AddSeat(int showId, int status, string customer, string stt)
        {
            SeatManagement.Instance.AddSeat(showId, status, customer, stt);
        }

        public List<Seat> GetSeatByShow(int showId)
        {
            return SeatManagement.Instance.GetSeatByShow(showId);
        }

        public Seat GetSeatByStt(string id, int showId)
        {
           return SeatManagement.Instance.GetSeatByStt(id, showId);
        }

        public void UpdateSeat(int showId, int status, string customer, string stt)
        {
            SeatManagement.Instance.UpdateSeat(showId, status, customer, stt);
        }
    }
}
