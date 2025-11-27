using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class ElectiveCourse : Course
    {
        private static List<ElectiveCourse> _extent = new();
        public static IReadOnlyList<ElectiveCourse> Extent => _extent.AsReadOnly();

        private static void AddToExtent(ElectiveCourse ec) => _extent.Add(ec);

        // ATTRIBUTE (optional 0..1)
        private string? _extraCertificate;

        public string? ExtraCertificate
        {
            get => _extraCertificate;
            set => _extraCertificate = value;
        }

        // CONSTRUCTORS
        public ElectiveCourse()
        {
            AddToExtent(this);
        }

        public ElectiveCourse(string? extraCertificate)
        {
            ExtraCertificate = extraCertificate;
            AddToExtent(this);
        }
    }
}
