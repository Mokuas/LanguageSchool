using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class Group
    {
        // STATIC EXTENT
        private static List<Group> _extent = new();
        public static IReadOnlyList<Group> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Group g) => _extent.Add(g);

        // ATTRIBUTES
        private string _name;
        private string _scheduleNote;
        private int _headCount;
        private int _maxCapacity;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string ScheduleNote
        {
            get => _scheduleNote;
            set => _scheduleNote = value;
        }

        public int HeadCount
        {
            get => _headCount;
            set => _headCount = value;
        }

        public int MaxCapacity
        {
            get => _maxCapacity;
            set => _maxCapacity = value;
        }

        // CONSTRUCTORS
        public Group()
        {
            AddToExtent(this);
        }

        public Group(string name, string scheduleNote, int headCount, int maxCapacity)
        {
            Name = name;
            ScheduleNote = scheduleNote;
            HeadCount = headCount;
            MaxCapacity = maxCapacity;

            AddToExtent(this);
        }
    }
}
