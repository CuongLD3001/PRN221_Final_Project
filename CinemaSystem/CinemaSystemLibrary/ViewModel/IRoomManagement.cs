using System;
using System.Collections.Generic;
using CinemaSystemLibrary.DataAccess;

namespace CinemaSystemLibrary.ViewModel
{
    public interface IRoomManagement
    {
        void AddRoom(string roomName, int numberOfRows, int numberOfColumns);
        void UpdateRoom(int roomId, string roomName, int numberOfRows, int numberOfColumns);
        void DeleteRoom(int roomId);
        List<Room> GetAllRooms();
    }
}
