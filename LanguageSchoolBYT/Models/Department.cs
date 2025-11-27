using System;
using System.Collections.Generic;

namespace LanguageSchoolBYT.Models
{
    public class Department
    {
        private static List<Department> _extent = new();
        public static IReadOnlyList<Department> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Department d) => _extent.Add(d);

        // ATTRIBUTES
        private string _deptID;
        private string _name;

        public string DeptID
        {
            get => _deptID;
            set => _deptID = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        // CONSTRUCTORS
        public Department() => AddToExtent(this);

        public Department(string deptID, string name)
        {
            DeptID = deptID;
            Name = name;

            AddToExtent(this);
        }
    }
}
