using System;
using System.Collections.Generic;
using System.Linq;

namespace LanguageSchoolBYT.Models
{
    public class Group
    {
       
        // STATIC EXTENT
      
        private static List<Group> _extent = new();
        public static IReadOnlyList<Group> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Group g) => _extent.Add(g);
        private static void RemoveFromExtent(Group g) => _extent.Remove(g);

      
        // ATTRIBUTES
      
        private string _name;
        private string _scheduleNote;
        private int _headCount;
        private int _maxCapacity;

        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentException("Name cannot be null.");
        }

        public string ScheduleNote
        {
            get => _scheduleNote;
            set => _scheduleNote = value ?? throw new ArgumentException("Schedule Note cannot be null.");
        }

        // HeadCount artık sadece öğrencilerden türetiliyor
        public int HeadCount => _students.Count;

        public int MaxCapacity
        {
            get => _maxCapacity;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("MaxCapacity must be > 0.");
                _maxCapacity = value;
            }
        }

       
        // BASIC ASSOCIATION (1..* WITH STUDENT)
    
        private HashSet<Student> _students = new();
        public IReadOnlyCollection<Student> Students => _students.ToList().AsReadOnly();

        public void AddStudent(Student s)
        {
            if (s == null)
                throw new ArgumentException("Student cannot be null.");

            if (_students.Contains(s))
                throw new Exception("This student is already in the group.");

            if (_students.Count >= MaxCapacity)
                throw new Exception("Group is full.");

            _students.Add(s);

            // Reverse connection
            s.AssignGroup(this);
        }

        public void RemoveStudent(Student s)
        {
            if (s == null)
                throw new ArgumentException("Student cannot be null.");

            if (!_students.Contains(s))
                throw new Exception("Student is not assigned to this group.");

            if (_students.Count == 1)
                throw new Exception("Cannot remove the last student. Minimum multiplicity is 1.");

            _students.Remove(s);

            // Reverse connection
            s.RemoveGroup(this);
        }

    
        // COMPOSITION: Group 0..* —— 1 ClassSession
        
        private HashSet<ClassSession> _sessions = new();
        public IReadOnlyCollection<ClassSession> Sessions => _sessions.ToList().AsReadOnly();

       
        /// Composition: yeni bir ClassSession sadece Group üzerinden yaratılabilir.
      
        public ClassSession CreateSession(DateTime startTime, DateTime endTime, string topic, Room room)
        {
            if (room == null)
                throw new ArgumentException("Room cannot be null.");

            var session = new ClassSession(startTime, endTime, topic, this, room);
            _sessions.Add(session);
            return session;
        }

       
        /// Composition Group içindeki oturumu kaldırır ve part objesini yok eder.
       
        public void RemoveSession(ClassSession session)
        {
            if (session == null)
                throw new ArgumentException("Session cannot be null.");

            if (!_sessions.Contains(session))
                throw new Exception("This session does not belong to this group.");

            _sessions.Remove(session);
            session.DeleteFromGroup(); // part kendi extent’inden silinir
        }

     
        /// Composition: Group silindiğinde tüm ClassSession'lar da silinir.
        /// (Unit test’te bunu kontrol edeceksin)
        
        public void DeleteGroup()
        {
            // önce tüm composition part'ları sil
            foreach (var s in _sessions.ToList())
            {
                s.DeleteFromGroup();
            }
            _sessions.Clear();

            // öğrencilerle ilişkiyi koparmak istersen burada da temizleyebilirsin
            foreach (var st in _students.ToList())
            {
                st.RemoveGroup(this);
            }
            _students.Clear();

            // extent’ten sil
            RemoveFromExtent(this);
        }

       
        // CONSTRUCTORS
       
        public Group()
        {
            AddToExtent(this);
        }

        public Group(string name, string scheduleNote, int maxCapacity)
        {
            Name = name;
            ScheduleNote = scheduleNote;
            MaxCapacity = maxCapacity;

            AddToExtent(this);
        }
    }
}
