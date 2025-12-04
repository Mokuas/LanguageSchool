using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class ClassSession
    {
        // -----------------------------
        // STATIC EXTENT
        // -----------------------------
        private static List<ClassSession> _extent = new();
        public static IReadOnlyList<ClassSession> Extent => _extent.AsReadOnly();

        internal static void AddToExtent(ClassSession cs) => _extent.Add(cs);
        internal static void RemoveFromExtent(ClassSession cs) => _extent.Remove(cs);

        // -----------------------------
        // ATTRIBUTES
        // -----------------------------
        private DateTime _startTime;
        private DateTime _endTime;
        private string _topic;

        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                if (value == default)
                    throw new ArgumentException("StartTime cannot be default.");
                _startTime = value;
            }
        }

        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                if (value <= StartTime)
                    throw new ArgumentException("EndTime must be after StartTime.");
                _endTime = value;
            }
        }

        public string Topic
        {
            get => _topic;
            set => _topic = value ?? throw new ArgumentException("Topic cannot be null.");
        }

        // -----------------------------
        // COMPOSITION: belongs to ONE Group
        // -----------------------------
        private Group _group;
        public Group Group => _group;

        // -----------------------------
        // BASIC ASSOCIATION: placed in ONE Room (şimdilik basit)
        // -----------------------------
        private Room _room;
        public Room Room => _room;

        // -----------------------------
        // CONSTRUCTOR - INTERNAL
        // Sadece Group.CreateSession(...) üzerinden çağrılmalı
        // -----------------------------
        internal ClassSession(DateTime startTime, DateTime endTime, string topic, Group group, Room room)
        {
            if (group == null)
                throw new ArgumentException("Group cannot be null for ClassSession (composition).");
            if (room == null)
                throw new ArgumentException("Room cannot be null.");

            StartTime = startTime;
            EndTime = endTime;
            Topic = topic;

            _group = group;
            _room = room;

            AddToExtent(this);
        }

        // -----------------------------
        // COMPOSITION DELETION LOGIC
        // -----------------------------
        /// <summary>
        /// Sadece Group tarafından çağrılır; session'ı extent'ten siler.
        /// </summary>
        internal void DeleteFromGroup()
        {
            RemoveFromExtent(this);
            // İleride Room ile reverse connection eklersek burada onu da temizleriz.
        }
    }
}
