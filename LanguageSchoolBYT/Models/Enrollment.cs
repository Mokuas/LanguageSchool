using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class Enrollment
    {
        // STATIC EXTENT
        private static List<Enrollment> _extent = new();
        public static IReadOnlyList<Enrollment> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Enrollment e) => _extent.Add(e);

        // ATTRIBUTES
        private DateTime _enroledOn;
        private string _status;
        private double? _finalGrade; // 0..1 → optional

        public DateTime EnroledOn
        {
            get => _enroledOn;
            set => _enroledOn = value;
        }

        public string Status
        {
            get => _status;
            set => _status = value;
        }

        public double? FinalGrade
        {
            get => _finalGrade;
            set => _finalGrade = value;
        }

        // CONSTRUCTORS
        public Enrollment()
        {
            AddToExtent(this);
        }

        public Enrollment(DateTime enroledOn, string status, double? finalGrade = null)
        {
            EnroledOn = enroledOn;
            Status = status;
            FinalGrade = finalGrade;

            AddToExtent(this);
        }
    }
}
