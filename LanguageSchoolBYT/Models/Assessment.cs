using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class Assessment
    {
        // STATIC EXTENT
        private static List<Assessment> _extent = new();
        public static IReadOnlyList<Assessment> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Assessment a) => _extent.Add(a);

        // ATTRIBUTES
        private string _name;
        private bool _isPass;
        private string _type;
        private double _score;
        private string _givenIn; // örn: exam session, room, course, vs.

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public bool IsPass
        {
            get => _isPass;
            set => _isPass = value;
        }

        public string Type
        {
            get => _type;
            set => _type = value;
        }

        public double Score
        {
            get => _score;
            set => _score = value;
        }

        public string GivenIn
        {
            get => _givenIn;
            set => _givenIn = value;
        }

        // CONSTRUCTORS
        public Assessment()
        {
            AddToExtent(this);
        }

        public Assessment(string name, bool isPass, string type, double score, string givenIn)
        {
            Name = name;
            IsPass = isPass;
            Type = type;
            Score = score;
            GivenIn = givenIn;

            AddToExtent(this);
        }
    }
}
