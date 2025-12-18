using System;
using System.Collections.Generic;
using System.Linq;

namespace LanguageSchoolBYT.Models
{
    public class Department
    {
       
        // STATIC EXTENT
        
        private static List<Department> _extent = new();
        public static IReadOnlyList<Department> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Department d)
        {
            if (d == null)
                throw new ArgumentException("Department cannot be null.");
            _extent.Add(d);
        }

       
        // ATTRIBUTES
      
        private string _deptID;
        private string _name;

        public string DeptID
        {
            get => _deptID;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("DeptID cannot be empty.");
                _deptID = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");
                _name = value;
            }
        }

     
        // AGGREGATION: Department 1 —— 0..* Staff
       
        private HashSet<Staff> _staffMembers = new();
        public IReadOnlyCollection<Staff> StaffMembers => _staffMembers.ToList().AsReadOnly();

        public void AddStaff(Staff staff)
        {
            if (staff == null)
                throw new ArgumentException("Staff cannot be null.");

            if (_staffMembers.Contains(staff))
                throw new Exception("This staff is already in this department.");

            // Staff sadece 1 departmana ait olabilir
            if (staff.Department != null && staff.Department != this)
                throw new Exception("Staff already belongs to a different department.");

            _staffMembers.Add(staff);

            // reverse connection
            staff.SetDepartmentInternal(this);
        }

        public void RemoveStaff(Staff staff)
        {
            if (staff == null)
                throw new ArgumentException("Staff cannot be null.");

            if (!_staffMembers.Contains(staff))
                throw new Exception("This staff is not in this department.");

            _staffMembers.Remove(staff);

            // reverse connection
            staff.ClearDepartmentInternal(this);
        }

       
        // CONSTRUCTORS
     
        public Department()
        {
            AddToExtent(this);
        }

        public Department(string deptID, string name)
        {
            DeptID = deptID;
            Name = name;

            AddToExtent(this);
        }
    }
}
