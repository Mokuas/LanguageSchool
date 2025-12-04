using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LanguageSchoolBYT.Models
{
    public class Student : Person
    {
        // -----------------------------
        // STATIC EXTENT (UML)
        // -----------------------------
        private static List<Student> _extent = new();

        public static IReadOnlyList<Student> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Student s)
        {
            if (s == null)
                throw new ArgumentException("Student cannot be null.");

            _extent.Add(s);
        }

        // -----------------------------
        // INSTANCE ATTRIBUTES (UML)
        // -----------------------------
        private int _studentNumber;
        private decimal _accountBalance;
        private int _yearOfStudy;
        private double _gpa;
        private int _currentSemester;
        private int _attendance;

        // -----------------------------
        // PROPERTIES (VALIDATION)
        // -----------------------------
        public int StudentNumber
        {
            get => _studentNumber;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Student number must be > 0.");
                _studentNumber = value;
            }
        }

        public decimal AccountBalance
        {
            get => _accountBalance;
            set => _accountBalance = value;
        }

        public int YearOfStudy
        {
            get => _yearOfStudy;
            set
            {
                if (value < 1 || value > 5)
                    throw new ArgumentException("Year of study must be 1-5.");
                _yearOfStudy = value;
            }
        }

        public double GPA
        {
            get => _gpa;
            set
            {
                if (value < 0 || value > 4.0)
                    throw new ArgumentException("GPA must be 0–4.0.");
                _gpa = value;
            }
        }

        public int CurrentSemester
        {
            get => _currentSemester;
            set
            {
                if (value < 1 || value > 10)
                    throw new ArgumentException("Semester must be 1–10.");
                _currentSemester = value;
            }
        }

        public int Attendance
        {
            get => _attendance;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Attendance must be 0–100.");
                _attendance = value;
            }
        }

        // -----------------------------
        // BASIC ASSOCIATION (Student → Group, 0..1)
        // -----------------------------
        private Group? _group;
        public Group? Group => _group;

        internal void AssignGroup(Group g)
        {
            if (g == null)
                throw new ArgumentException("Group cannot be null.");

            if (_group == g)
                return;

            // Eğer başka bir gruptaysa, o gruptan çıkar
            if (_group != null)
            {
                _group.RemoveStudent(this);
            }

            _group = g;
        }

        internal void RemoveGroup(Group g)
        {
            if (_group != g)
                throw new Exception("Student is not assigned to this group.");

            _group = null;
        }

        // -----------------------------
        // QUALIFIED ASSOCIATION: Student 1 —— 0..* Invoice
        // qualifier: Invoice.Number (string)
        // -----------------------------
        private Dictionary<string, Invoice> _invoicesByNumber = new();
        public IReadOnlyDictionary<string, Invoice> InvoicesByNumber =>
            new Dictionary<string, Invoice>(_invoicesByNumber);

        /// <summary>
        /// Adds an invoice to this student using Invoice.Number as qualifier.
        /// </summary>
        public void AddInvoice(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentException("Invoice cannot be null.");

            if (string.IsNullOrWhiteSpace(invoice.Number))
                throw new ArgumentException("Invoice must have a valid Number as qualifier.");

            if (_invoicesByNumber.ContainsKey(invoice.Number))
                throw new Exception($"Invoice with number {invoice.Number} already exists for this student.");

            // Invoice başka bir öğrenciye bağlıysa
            if (invoice.Student != null && invoice.Student != this)
                throw new Exception("Invoice is already assigned to another student.");

            _invoicesByNumber.Add(invoice.Number, invoice);

            // reverse connection
            invoice.SetStudentInternal(this);
        }

        /// <summary>
        /// Removes invoice by its number (qualifier).
        /// </summary>
        public void RemoveInvoice(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Invoice number cannot be empty.");

            if (!_invoicesByNumber.TryGetValue(number, out var invoice))
                throw new Exception("Invoice with this number is not registered for this student.");

            _invoicesByNumber.Remove(number);

            // reverse connection
            invoice.ClearStudentInternal(this);
        }

        /// <summary>
        /// Returns invoice by qualifier or null if not found.
        /// </summary>
        public Invoice? GetInvoiceByNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Invoice number cannot be empty.");

            return _invoicesByNumber.TryGetValue(number, out var inv) ? inv : null;
        }

        /// <summary>
        /// Qualified association kuralına göre:
        /// Invoice.Number değişirse dictionary'deki key de güncellenmeli.
        /// Bu method Invoice içinden çağrılır.
        /// </summary>
        internal void UpdateInvoiceQualifier(string oldNumber, string newNumber, Invoice invoice)
        {
            if (string.IsNullOrWhiteSpace(oldNumber) || string.IsNullOrWhiteSpace(newNumber))
                throw new ArgumentException("Invoice numbers cannot be empty.");

            if (!_invoicesByNumber.ContainsKey(oldNumber))
                throw new Exception("Old invoice number not found for this student.");

            if (_invoicesByNumber.ContainsKey(newNumber))
                throw new Exception("Another invoice with the new number already exists for this student.");

            _invoicesByNumber.Remove(oldNumber);
            _invoicesByNumber.Add(newNumber, invoice);
        }

        // -----------------------------
        // ASSOCIATION CLASS:
        // Student 1 —— 0..* Enrollment
        // -----------------------------
        private HashSet<Enrollment> _enrollments = new();
        public IReadOnlyCollection<Enrollment> Enrollments =>
            _enrollments.ToList().AsReadOnly();

        /// <summary>
        /// INTERNAL — Enrollment tarafından çağrılır.
        /// Doğrudan dışarıdan çağrılmamalı.
        /// </summary>
        internal void AddEnrollmentInternal(Enrollment e)
        {
            if (e == null)
                throw new ArgumentException("Enrollment cannot be null.");

            _enrollments.Add(e);
        }

        /// <summary>
        /// INTERNAL — Enrollment cancel edildiğinde çağrılır.
        /// </summary>
        internal void RemoveEnrollmentInternal(Enrollment e)
        {
            _enrollments.Remove(e);
        }

        /// <summary>
        /// Bu öğrencinin belirli bir derse kayıtlı olup olmadığını kontrol eder.
        /// </summary>
        public bool IsEnrolledInCourse(Course c)
        {
            if (c == null)
                throw new ArgumentException("Course cannot be null.");

            foreach (var en in _enrollments)
            {
                if (en.Course == c)
                    return true;
            }

            return false;
        }

        // -----------------------------
        // METHODS (UML’dan boş bırakılmış)
        // -----------------------------
        public void ViewAvailableCourse() { }
        public void ViewInvoiceAndPayment() { }
        public void ApplyForScholarship() { }
        public void EnrollInCourse() { }
        public void ViewAssessmentAndGrades() { }
        public void DownloadMaterials() { }

        // -----------------------------
        // CONSTRUCTORS
        // -----------------------------
        public Student() { }

        public Student(
            string name,
            string surname,
            DateTime birthDate,
            string email,
            int studentNumber,
            decimal accountBalance,
            int yearOfStudy,
            double gpa,
            int currentSemester,
            int attendance,
            string? middleName = null
        ) : base(name, surname, birthDate, email)
        {
            MiddleName = middleName;

            StudentNumber = studentNumber;
            AccountBalance = accountBalance;
            YearOfStudy = yearOfStudy;
            GPA = gpa;
            CurrentSemester = currentSemester;
            Attendance = attendance;

            AddToExtent(this);
        }

        // -----------------------------
        // TXT PERSISTENCY 
        // -----------------------------
        public static void Save(string path = "students.txt")
        {
            var lines = new List<string>();

            foreach (var s in _extent)
            {
                string line =
                    $"{s.Name};{s.MiddleName};{s.Surname};{s.Email};{s.BirthDate:yyyy-MM-dd};" +
                    $"{s.StudentNumber};{s.AccountBalance};{s.YearOfStudy};{s.GPA};" +
                    $"{s.CurrentSemester};{s.Attendance}";

                lines.Add(line);
            }

            File.WriteAllLines(path, lines);
        }

        public static void Load(string path = "students.txt")
        {
            _extent = new List<Student>();

            if (!File.Exists(path))
                return;

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var p = line.Split(';');

                string name = p[0];
                string? middle = p[1] == "" ? null : p[1];
                string surname = p[2];
                string email = p[3];
                DateTime birth = DateTime.Parse(p[4]);

                int number = int.Parse(p[5]);
                decimal balance = decimal.Parse(p[6]);
                int year = int.Parse(p[7]);
                double gpa = double.Parse(p[8]);
                int sem = int.Parse(p[9]);
                int att = int.Parse(p[10]);

                new Student(name, surname, birth, email, number, balance, year, gpa, sem, att, middle);
            }
        }
    }
}
