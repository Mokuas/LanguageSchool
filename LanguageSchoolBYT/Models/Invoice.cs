using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class Invoice
    {
        // STATIC EXTENT
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
        
        // ATTRIBUTES
        private string _name;
        private decimal _amount;
        private DateTime _paidAt;
        private DateTime _dueDate;
        private string _method;
        private string? _scholarship;
        private string _number; 

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

        
        public string Number
        {
            get => _number;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Invoice Number cannot be empty.");

                
                if (_student != null && _number != value)
                {
                    _student.UpdateInvoiceQualifier(_number, value, this);
                }

                _number = value;
            }
        }

        
        // QUALIFIED ASSOCIATION (Invoice → Student: 1)
        private Student? _student;
        public Student? Student => _student;

        
        public void SetStudentInternal(Student s)
        {
            if (s == null)
                throw new ArgumentException("Student cannot be null.");

            _student = s;
        }

        
        public void ClearStudentInternal(Student s)
        {
            if (_student != s)
                throw new Exception("This invoice does not belong to the given student.");

            _student = null;
        }

       
        // CONSTRUCTORS
        public Invoice()
        {
            
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

        
        public void Delete()
        {
            
            if (_student != null)
            {
                _student.RemoveInvoice(_number);
            }

            RemoveFromExtent(this);
        }
    }
}
