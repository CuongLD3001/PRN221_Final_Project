using System;
using System.Collections.Generic;
using CinemaSystemLibrary.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystemLibrary.DataAccess
{
    public class BookingManagerment: IBookingManagement
    {
        private static BookingManagerment instance = null;
        private static readonly object instanceLock = new object();

        private readonly CinemaSystemContext context;

        public BookingManagerment()
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
            // Kiểm tra xem đã tồn tại đặt vé với thông tin tương tự trong cơ sở dữ liệu chưa
            var existingBooking = context.Bookings.FirstOrDefault(b => b.ShowId == showId && b.SeatNumber == seatNumber);

            if (existingBooking == null)
            {
                // Nếu không có đặt vé nào có thông tin tương tự, thì tạo một đặt vé mới
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
            else
            {
                // Báo lỗi hoặc thực hiện xử lý phù hợp nếu đặt vé đã tồn tại
                // Ví dụ: throw new Exception("Đặt vé đã tồn tại!");
            }
        }


        public List<Booking> GetBookingsForShow(int showId)
        {
            return context.Bookings.Where(b => b.ShowId == showId).ToList();
        }
    }
}
