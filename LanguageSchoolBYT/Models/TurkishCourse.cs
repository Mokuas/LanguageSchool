using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class TurkishCourse : Course
    {
        private static List<TurkishCourse> _extent = new();
        public static IReadOnlyList<TurkishCourse> Extent => _extent.AsReadOnly();

        private static void AddToExtent(TurkishCourse tc) => _extent.Add(tc);

        // ATTRIBUTE
        private string _relatedUniversities;

        public string RelatedUniversities
        {
            get => _relatedUniversities;
            set => _relatedUniversities = value;
        }

        // CONSTRUCTORS
        public TurkishCourse()
        {
            AddToExtent(this);
        }

        public TurkishCourse(string relatedUniversities)
        {
            RelatedUniversities = relatedUniversities;
            AddToExtent(this);
        }
    }
}
