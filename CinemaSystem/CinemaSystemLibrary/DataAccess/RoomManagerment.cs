using System;
using System.Collections.Generic;
using System.Linq;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.ViewModel;

namespace CinemaSystemLibrary.Controller
{
    public class RoomManagerment: IRoomManagement
    {
        private static RoomManagerment instance = null;
        private static readonly object instanceLock = new object();

        private readonly CinemaSystemContext context;

        public RoomManagerment()
        {
            context = new CinemaSystemContext();
        }

        public static RoomManagerment Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoomManagerment();
                    }
                    return instance;
                }
            }
        }

        public void AddRoom(string roomName, int numberOfRows, int numberOfColumns)
        {
            // Kiểm tra xem đã tồn tại phòng với tên tương tự trong cơ sở dữ liệu chưa
            var existingRoom = context.Rooms.FirstOrDefault(r => r.Name == roomName && r.NumberRows == numberOfRows && r.NumberCols == numberOfColumns);

            if (existingRoom == null)
            {
                // Nếu không có phòng nào có tên tương tự, thì tạo một phòng mới
                Room room = new Room
                {
                    Name = roomName,
                    NumberRows = numberOfRows,
                    NumberCols = numberOfColumns
                };

                context.Rooms.Add(room);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Phòng đã tồn tại!");
            }
        }


        public void UpdateRoom(int roomId, string roomName, int numberOfRows, int numberOfColumns)
        {
            var room = context.Rooms.FirstOrDefault(r => r.RoomId == roomId);

            if (room != null)
            {
                room.Name = roomName;
                room.NumberRows = numberOfRows;
                room.NumberCols = numberOfColumns;

                context.SaveChanges();
            }
        }

        public void DeleteRoom(int roomId)
        {
            var room = context.Rooms.FirstOrDefault(r => r.RoomId == roomId);

            if (room != null)
            {
                context.Rooms.Remove(room);
                context.SaveChanges();
            }
        }

        public List<Room> GetAllRooms()
        {
            return context.Rooms.ToList();
        }
    }
}
