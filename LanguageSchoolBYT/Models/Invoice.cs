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

        private static void AddToExtent(Invoice inv) => _extent.Add(inv);

        // -----------------------------
        // ATTRIBUTES
        // -----------------------------
        private string _name;
        private decimal _amount;
        private DateTime _paidAt;
        private DateTime _dueDate;
        private string _method;
        private string? _scholarship;     // 0..1 → optional
        private string _number;           // invoice number

        public string Name
        {
            get => _name;
            set => _name = value;
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
            set => _method = value;
        }

        public string? Scholarship
        {
            get => _scholarship;
            set => _scholarship = value;
        }

        public string Number
        {
            get => _number;
            set => _number = value;
        }

        // -----------------------------
        // CONSTRUCTORS
        // -----------------------------
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
    }
}
