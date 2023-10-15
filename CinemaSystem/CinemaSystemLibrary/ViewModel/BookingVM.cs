using CinemaSystemLibrary.DataAccess;

namespace CinemaSystemLibrary.ViewModel
{
    public class BookingVM : IBookingManagement
    {
        public void AddBooking(int showId, string seatNumber, double? amount, string status)
        {
            BookingManagerment.Instance.AddBooking(showId, seatNumber, amount, status);
        }

        public List<Booking> GetBookingsForShow(int showId)
        {
            return BookingManagerment.Instance.GetBookingsForShow(showId);
        }
    }
}
