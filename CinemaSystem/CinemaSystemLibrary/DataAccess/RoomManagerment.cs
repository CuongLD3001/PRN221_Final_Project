using System;
using System.Collections.Generic;
using System.Linq;
using CinemaSystemLibrary.DataAccess;

namespace CinemaSystemLibrary.Controller
{
    public class RoomManagerment
    {
        private static RoomManagerment instance = null;
        private static readonly object instanceLock = new object();

        private readonly CinemaSystemContext context;

        private RoomManagerment()
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
            Room room = new Room
            {
                Name = roomName,
                NumberRows = numberOfRows,
                NumberCols = numberOfColumns
            };

            context.Rooms.Add(room);
            context.SaveChanges();
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
