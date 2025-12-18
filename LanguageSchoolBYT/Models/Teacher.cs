using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class Teacher : Staff
    {
        private static List<Teacher> _extent = new();
        public static IReadOnlyList<Teacher> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Teacher t) => _extent.Add(t);

        // ATTRIBUTES
        private string _specialization;
        private string _contract;

        public string Specialization
        {
            get => _specialization;
            set => _specialization = value;
        }

        public string Contract
        {
            get => _contract;
            set => _contract = value;
        }

        // CONSTRUCTORS
        public Teacher() : base()
        {
            AddToExtent(this);
        }

        public Teacher(
            string specialization,
            string contract
        ) : base()
        {
            Specialization = specialization;
            Contract = contract;

            AddToExtent(this);
        }
    }
}