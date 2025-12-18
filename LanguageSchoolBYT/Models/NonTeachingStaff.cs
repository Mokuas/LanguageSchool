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
            set => _role = value ?? throw new ArgumentException("Role cannot be null.");
        }

        // REFLEXIVE ASSOCIATION
        // supervisor: 0..1
        // subordinate: 0..*

        private NonTeachingStaff? _supervisor;
        public NonTeachingStaff? Supervisor => _supervisor;

        private HashSet<NonTeachingStaff> _subordinates = new();
        public IReadOnlyCollection<NonTeachingStaff> Subordinates =>
            new List<NonTeachingStaff>(_subordinates).AsReadOnly();

        /// Assigns a supervisor to this staff member
        /// Handles reverse connection oto
        public void SetSupervisor(NonTeachingStaff supervisor)
        {
            if (supervisor == null)
                throw new ArgumentException("Supervisor cannot be null.");

            if (supervisor == this)
                throw new Exception("A staff member cannot supervise themselves.");

            // cycle engelleme supervisor zaten bu kişinin altında ise
            if (IsSubordinateOf(supervisor))
                throw new Exception("Circular supervision is not allowed.");

            if (_supervisor != null && _supervisor != supervisor)
            {
                _supervisor.RemoveSubordinateInternal(this);
            }

            _supervisor = supervisor;

            // reverse connection
            supervisor.AddSubordinateInternal(this);
        }

        /// Removes the supervisor if any
        public void RemoveSupervisor()
        {
            if (_supervisor == null)
                return;

            _supervisor.RemoveSubordinateInternal(this);
            _supervisor = null;
        }

        // INTERNAL REVERSE METHODS
        internal void AddSubordinateInternal(NonTeachingStaff staff)
        {
            _subordinates.Add(staff);
        }

        internal void RemoveSubordinateInternal(NonTeachingStaff staff)
        {
            _subordinates.Remove(staff);
        }

        // HELPER: Check recursive subordinate chain
        private bool IsSubordinateOf(NonTeachingStaff possibleSupervisor)
        {
            NonTeachingStaff? current = possibleSupervisor;

            while (current != null)
            {
                if (current == this)
                    return true; // cycle detected

                current = current.Supervisor;
            }

            return false;
        }

        // CONSTRUCTORS
        public NonTeachingStaff() : base()
        {
            AddToExtent(this);
        }

        public NonTeachingStaff(string role) : base()
        {
            Role = role;
            AddToExtent(this);
        }
    }
}
