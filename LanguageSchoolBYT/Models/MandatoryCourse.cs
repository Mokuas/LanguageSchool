using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class MandatoryCourse : Course
    {
        private static List<MandatoryCourse> _extent = new();
        public static IReadOnlyList<MandatoryCourse> Extent => _extent.AsReadOnly();

        private static void AddToExtent(MandatoryCourse mc) => _extent.Add(mc);

        // ATTRIBUTE
        private string _dressCode;

        public string DressCode
        {
            get => _dressCode;
            set => _dressCode = value;
        }

        // CONSTRUCTORS
        public MandatoryCourse()
        {
            AddToExtent(this);
        }

        public MandatoryCourse(string dressCode)
        {
            DressCode = dressCode;
            AddToExtent(this);
        }
    }
}
