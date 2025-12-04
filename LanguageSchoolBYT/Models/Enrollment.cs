using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class Enrollment
    {
        // -----------------------------
        // STATIC EXTENT
        // -----------------------------
        private static List<Enrollment> _extent = new();
        public static IReadOnlyList<Enrollment> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Enrollment e)
        {
            if (e == null)
                throw new ArgumentException("Enrollment cannot be null.");
            _extent.Add(e);
        }

        private static void RemoveFromExtent(Enrollment e)
        {
            _extent.Remove(e);
        }

        // -----------------------------
        // ATTRIBUTES (ASSOCIATION CLASS)
        // -----------------------------
        private DateTime _enroledOn;
        private string _status;
        private double? _finalGrade; // optional

        public DateTime EnroledOn
        {
            get => _enroledOn;
            set
            {
                if (value == default)
                    throw new ArgumentException("EnroledOn cannot be default DateTime.");
                _enroledOn = value;
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Status cannot be empty.");
                _status = value;
            }
        }

        public double? FinalGrade
        {
            get => _finalGrade;
            set
            {
                if (value is < 0 or > 5)
                    throw new ArgumentException("Final grade must be between 0 and 5.");
                _finalGrade = value;
            }
        }

        // -----------------------------
        // ASSOCIATION LINKS
        // Enrollment 1 —— 1 Student
        // Enrollment 1 —— 1 Course
        // -----------------------------
        private Student _student;
        private Course _course;

        public Student Student => _student;
        public Course Course => _course;

        // -----------------------------
        // FACTORY METHOD (ÖNERİLEN KULLANIM)
        // -----------------------------
        public static Enrollment EnrollStudentInCourse(
            Student student,
            Course course,
            DateTime enroledOn,
            string status,
            double? finalGrade = null)
        {
            if (student == null)
                throw new ArgumentException("Student cannot be null.");
            if (course == null)
                throw new ArgumentException("Course cannot be null.");

            // Aynı student + course için ikinci bir enrollment istemiyoruz
            foreach (var e in _extent)
            {
                if (e._student == student && e._course == course)
                    throw new Exception("This student is already enrolled in this course.");
            }

            var enrollment = new Enrollment(student, course, enroledOn, status, finalGrade);
            return enrollment;
        }

        // -----------------------------
        // PRIVATE CONSTRUCTOR
        // Sadece factory method kullanılsın diye
        // -----------------------------
        private Enrollment(Student student, Course course, DateTime enroledOn, string status, double? finalGrade)
        {
            _student = student;
            _course = course;

            EnroledOn = enroledOn;
            Status = status;
            FinalGrade = finalGrade;

            // reverse connection
            student.AddEnrollmentInternal(this);
            course.AddEnrollmentInternal(this);

            AddToExtent(this);
        }

        // -----------------------------
        // CHANGE / UPDATE METHODS
        // -----------------------------
        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
        }

        public void UpdateFinalGrade(double? newGrade)
        {
            FinalGrade = newGrade;
        }

        // -----------------------------
        // DELETE ENROLLMENT
        // -----------------------------
        public void Cancel()
        {
            // reverse connection temizle
            _student.RemoveEnrollmentInternal(this);
            _course.RemoveEnrollmentInternal(this);

            RemoveFromExtent(this);
        }
    }
}
