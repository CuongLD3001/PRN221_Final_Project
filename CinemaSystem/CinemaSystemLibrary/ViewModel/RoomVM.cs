using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.Controller;

namespace CinemaSystemLibrary.ViewModel
{
    public class RoomVM : IRoomManagement
    {
        public void AddRoom(string roomName, int numberOfRows, int numberOfColumns)
        {
            RoomManagerment.Instance.AddRoom(roomName, numberOfRows, numberOfColumns);
        }

        public void UpdateRoom(int roomId, string roomName, int numberOfRows, int numberOfColumns)
        {
            RoomManagerment.Instance.UpdateRoom(roomId, roomName, numberOfRows, numberOfColumns);
        }

        public void DeleteRoom(int roomId)
        {
            RoomManagerment.Instance.DeleteRoom(roomId);
        }

        public List<Room> GetAllRooms()
        {
            return RoomManagerment.Instance.GetAllRooms();
        }
    }
}
