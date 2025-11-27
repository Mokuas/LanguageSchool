using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class NonTeachingStaff : Staff
    {
        // STATIC EXTENT
        private static List<NonTeachingStaff> _extent = new();
        public static IReadOnlyList<NonTeachingStaff> Extent => _extent.AsReadOnly();

        private static void AddToExtent(NonTeachingStaff nts) => _extent.Add(nts);

        // ATTRIBUTE
        private string _role;

        public string Role
        {
            get => _role;
            set => _role = value;
        }

        // CONSTRUCTORS
        public NonTeachingStaff()
        {
            AddToExtent(this);
        }

        public NonTeachingStaff(string role)
        {
            Role = role;
            AddToExtent(this);
        }
    }
}
