using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;


namespace LanguageSchoolBYT.Models
{
    public class Staff : Person
    {
        // -----------------------------
        // STATIC EXTENT
        // -----------------------------
        private static List<Staff> _extent = new();

        public static IReadOnlyList<Staff> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Staff s)
        {
            if (s == null)
                throw new ArgumentException("Staff cannot be null.");

            _extent.Add(s);
        }

        // -----------------------------
        // STATIC ATTRIBUTES (SALARY CONFIG)
        // -----------------------------
        public static decimal ExperienceBonusPerYear { get; private set; } = 200m;

        public static void SetExperienceBonusPerYear(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("Experience bonus per year cannot be negative.");
            ExperienceBonusPerYear = value;
        }

        // -----------------------------
        // CONSTRUCTORS
        // -----------------------------
        // JSON / manual load için boş constructor
        public Staff() { }

        public Staff(
            string name,
            string surname,
            DateTime birthDate,
            string email,
            DateTime hireDate,
            decimal baseSalary,
            int experienceYears,
            string? middleName = null
        ) : base(name, surname, birthDate, email)
        {
            MiddleName = middleName;

            HireDate = hireDate;
            BaseSalary = baseSalary;
            ExperienceYears = experienceYears;

            AddToExtent(this);
        }

        // -----------------------------
        // INSTANCE ATTRIBUTES
        // -----------------------------
        private DateTime _hireDate;
        private decimal _baseSalary;
        private int _experienceYears;

        public DateTime HireDate
        {
            get => _hireDate;
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Hire date cannot be in the future.");
                _hireDate = value;
            }
        }

        public decimal BaseSalary
        {
            get => _baseSalary;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Base salary cannot be negative.");
                _baseSalary = value;
            }
        }

        public int ExperienceYears
        {
            get => _experienceYears;
            set
            {
                if (value < 0 || value > 60)
                    throw new ArgumentException("Experience years must be between 0 and 60.");
                _experienceYears = value;
            }
        }

        // -----------------------------
        // DERIVED SALARY LOGIC
        // -----------------------------
        public decimal TotalSalary
        {
            get
            {
                // Basit formül:
                // Toplam maaş = BaseSalary + ExperienceYears * ExperienceBonusPerYear
                return BaseSalary + ExperienceYears * ExperienceBonusPerYear;
            }
        }


        // -----------------------------
        // XML PERSISTENCY
        // -----------------------------
        public static void SaveXml(string path = "staff.xml")
        {
            // <StaffMembers>
            //   <Staff>
            //     <Name>...</Name>
            //     <MiddleName>...</MiddleName>
            //     <Surname>...</Surname>
            //     <Email>...</Email>
            //     <BirthDate>2000-01-01</BirthDate>
            //     <HireDate>2020-01-01</HireDate>
            //     <BaseSalary>5000</BaseSalary>
            //     <ExperienceYears>5</ExperienceYears>
            //   </Staff>
            //   ...
            // </StaffMembers>

            var root = new XElement("StaffMembers");

            foreach (var s in _extent)
            {
                var staffElement = new XElement("Staff",
                    new XElement("Name", s.Name),
                    new XElement("MiddleName", s.MiddleName ?? string.Empty),
                    new XElement("Surname", s.Surname),
                    new XElement("Email", s.Email),
                    new XElement("BirthDate", s.BirthDate.ToString("yyyy-MM-dd")),
                    new XElement("HireDate", s.HireDate.ToString("yyyy-MM-dd")),
                    new XElement("BaseSalary", s.BaseSalary),
                    new XElement("ExperienceYears", s.ExperienceYears)
                );

                root.Add(staffElement);
            }

            var doc = new XDocument(root);
            doc.Save(path);
        }

        public static void LoadXml(string path = "staff.xml")
        {
            _extent = new List<Staff>();

            if (!File.Exists(path))
                return;

            var doc = XDocument.Load(path);
            var root = doc.Root;

            if (root == null)
                return;

            foreach (var el in root.Elements("Staff"))
            {
                string name = (string?)el.Element("Name") ?? throw new Exception("Name missing in XML");
                string middleRaw = (string?)el.Element("MiddleName") ?? string.Empty;
                string? middle = string.IsNullOrWhiteSpace(middleRaw) ? null : middleRaw;
                string surname = (string?)el.Element("Surname") ?? throw new Exception("Surname missing in XML");
                string email = (string?)el.Element("Email") ?? throw new Exception("Email missing in XML");

                string birthStr = (string?)el.Element("BirthDate") ?? throw new Exception("BirthDate missing in XML");
                DateTime birth = DateTime.Parse(birthStr);

                string hireStr = (string?)el.Element("HireDate") ?? throw new Exception("HireDate missing in XML");
                DateTime hireDate = DateTime.Parse(hireStr);

                decimal baseSalary = decimal.Parse((string?)el.Element("BaseSalary") ?? throw new Exception("BaseSalary missing in XML"));
                int experienceYears = int.Parse((string?)el.Element("ExperienceYears") ?? throw new Exception("ExperienceYears missing in XML"));

                new Staff(name, surname, birth, email, hireDate, baseSalary, experienceYears, middle);
            }
        }
    }

}
