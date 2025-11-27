using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class ClassSession
    {
        private static List<ClassSession> _extent = new();
        public static IReadOnlyList<ClassSession> Extent => _extent.AsReadOnly();

        private static void AddToExtent(ClassSession cs) => _extent.Add(cs);

        // ATTRIBUTES
        private DateTime _startTime;
        private DateTime _endTime;
        private string _topic;

        public DateTime StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        public DateTime EndTime
        {
            get => _endTime;
            set => _endTime = value;
        }

        public string Topic
        {
            get => _topic;
            set => _topic = value;
        }

        // CONSTRUCTORS
        public ClassSession() => AddToExtent(this);

        public ClassSession(DateTime startTime, DateTime endTime, string topic)
        {
            StartTime = startTime;
            EndTime = endTime;
            Topic = topic;

            AddToExtent(this);
        }
    }
}
