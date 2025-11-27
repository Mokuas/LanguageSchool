using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class Course
    {
        // STATIC EXTENT
        private static List<Course> _extent = new();
        public static IReadOnlyList<Course> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Course c) => _extent.Add(c);

        // ATTRIBUTES
        private string _name;
        private string _title;
        private int _level;
        private int _ects;
        private bool _isFull;
        private double _gpaWeight;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public int Level
        {
            get => _level;
            set => _level = value;
        }

        public int Ects
        {
            get => _ects;
            set => _ects = value;
        }

        public bool IsFull
        {
            get => _isFull;
            set => _isFull = value;
        }

        public double GPAWeight
        {
            get => _gpaWeight;
            set => _gpaWeight = value;
        }

        // CONSTRUCTORS
        public Course()
        {
            AddToExtent(this);
        }

        public Course(string name, string title, int level, int ects, bool isFull, double gpaWeight)
        {
            Name = name;
            Title = title;
            Level = level;
            Ects = ects;
            IsFull = isFull;
            GPAWeight = gpaWeight;

            AddToExtent(this);
        }
    }
}
