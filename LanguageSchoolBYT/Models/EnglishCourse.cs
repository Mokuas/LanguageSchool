using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class EnglishCourse : Course
    {
        private static List<EnglishCourse> _extent = new();
        public static IReadOnlyList<EnglishCourse> Extent => _extent.AsReadOnly();

        private static void AddToExtent(EnglishCourse ec) => _extent.Add(ec);

        // ATTRIBUTE
        private int _studyHours;

        public int StudyHours
        {
            get => _studyHours;
            set => _studyHours = value;
        }

        // CONSTRUCTORS
        public EnglishCourse()
        {
            AddToExtent(this);
        }

        public EnglishCourse(int studyHours)
        {
            StudyHours = studyHours;
            AddToExtent(this);
        }
    }
}
