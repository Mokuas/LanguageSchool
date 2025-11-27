using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class Room
    {
        private static List<Room> _extent = new();
        public static IReadOnlyList<Room> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Room r) => _extent.Add(r);

        // ATTRIBUTES
        private string _roomNo;
        private int _capacity;
        private string _building;

        public string RoomNo
        {
            get => _roomNo;
            set => _roomNo = value;
        }

        public int Capacity
        {
            get => _capacity;
            set => _capacity = value;
        }

        public string Building
        {
            get => _building;
            set => _building = value;
        }

        // CONSTRUCTORS
        public Room() => AddToExtent(this);

        public Room(string roomNo, int capacity, string building)
        {
            RoomNo = roomNo;
            Capacity = capacity;
            Building = building;

            AddToExtent(this);
        }
    }
}
