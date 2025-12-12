using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class Invoice
    {
        // -----------------------------
        // STATIC EXTENT
        // -----------------------------
        private static List<Invoice> _extent = new();
        public static IReadOnlyList<Invoice> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Invoice inv)
        {
            if (inv == null)
                throw new ArgumentException("Invoice cannot be null.");
            _extent.Add(inv);
        }

        private static void RemoveFromExtent(Invoice inv)
        {
            _extent.Remove(inv);
        }

        // -----------------------------
        // ATTRIBUTES
        // -----------------------------
        private string _name;
        private decimal _amount;
        private DateTime _paidAt;
        private DateTime _dueDate;
        private string _method;
        private string? _scholarship;
        private string _number; // QUALIFIER — MUST BE UNIQUE PER STUDENT

        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentException("Name cannot be null.");
        }

        public decimal Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public DateTime PaidAt
        {
            get => _paidAt;
            set => _paidAt = value;
        }

        public DateTime DueDate
        {
            get => _dueDate;
            set => _dueDate = value;
        }

        public string Method
        {
            get => _method;
            set => _method = value ?? throw new ArgumentException("Payment method cannot be null.");
        }

        public string? Scholarship
        {
            get => _scholarship;
            set => _scholarship = value;
        }

        /// <summary>
        /// QUALIFIER field — Number değişirse Student içindeki dictionary de güncellenmelidir.
        /// </summary>
        public string Number
        {
            get => _number;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Invoice Number cannot be empty.");

                // If Number changes, update Student's dictionary (Qualified Association logic)
                if (_student != null && _number != value)
                {
                    _student.UpdateInvoiceQualifier(_number, value, this);
                }

                _number = value;
            }
        }

        // -----------------------------
        // QUALIFIED ASSOCIATION (Invoice → Student: 1)
        // -----------------------------
        private Student? _student;
        public Student? Student => _student;

        /// <summary>
        /// INTERNAL — Reverse connection only from Student.AddInvoice()
        /// </summary>
        internal void SetStudentInternal(Student s)
        {
            if (s == null)
                throw new ArgumentException("Student cannot be null.");

            _student = s;
        }

        /// <summary>
        /// INTERNAL — Reverse connection only from Student.RemoveInvoice()
        /// </summary>
        internal void ClearStudentInternal(Student s)
        {
            if (_student != s)
                throw new Exception("This invoice does not belong to the given student.");

            _student = null;
        }

        // -----------------------------
        // CONSTRUCTORS
        // -----------------------------
        public Invoice()
        {
            // Qualified association gereği student burada set edilmez.
            AddToExtent(this);
        }

        public Invoice(
            string name,
            decimal amount,
            DateTime paidAt,
            DateTime dueDate,
            string method,
            string number,
            string? scholarship = null
        )
        {
            Name = name;
            Amount = amount;
            PaidAt = paidAt;
            DueDate = dueDate;
            Method = method;
            Number = number;
            Scholarship = scholarship;

            AddToExtent(this);
        }

        // -----------------------------
        // DELETE INVOICE
        // (Örneğin Student tarafından kaldırıldığında kullanılabilir)
        // -----------------------------
        public void Delete()
        {
            // önce öğrenci ilişkisi koparılır
            if (_student != null)
            {
                _student.RemoveInvoice(_number);
            }

            RemoveFromExtent(this);
        }
    }
}
