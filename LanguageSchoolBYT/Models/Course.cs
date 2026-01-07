
namespace LanguageSchoolBYT.Models
{
    public class Course
    {
        public CourseLanguageAspect LanguageAspect { get; } = new();
        // STATIC EXTENT
     
        private static List<Course> _extent = new();
        public static IReadOnlyList<Course> Extent => _extent.AsReadOnly();

        private static void AddToExtent(Course c)
        {
            if (c == null)
                throw new ArgumentException("Course cannot be null.");
            _extent.Add(c);
        }

      
        // ATTRIBUTES
     
        private string _name;
        private string _title;
        private int _level;
        private int _ects;
        private bool _isFull;
        private double _gpaWeight;

        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentException("Name cannot be null.");
        }

        public string Title
        {
            get => _title;
            set => _title = value ?? throw new ArgumentException("Title cannot be null.");
        }

        public int Level
        {
            get => _level;
            set => _level = value;
        }

        public int Ects
        {
            get => _ects;
            set => _ects = value;
        }

        public bool IsFull
        {
            get => _isFull;
            set => _isFull = value;
        }

        public double GPAWeight
        {
            get => _gpaWeight;
            set => _gpaWeight = value;
        }

        
        // ASSOCIATION CLASS:
        // Course 1 —— 0..* Enrollment
      
        private HashSet<Enrollment> _enrollments = new();
        public IReadOnlyCollection<Enrollment> Enrollments =>
            _enrollments.ToList().AsReadOnly();

       
        /// INTERNAL — Enrollment tarafından çağrılır.
        /// Direkt kullanma — her ilişki Enrollment factory tarafından kurulmalıdır.
       
        internal void AddEnrollmentInternal(Enrollment e)
        {
            if (e == null)
                throw new ArgumentException("Enrollment cannot be null.");

            _enrollments.Add(e);
        }

       
        /// INTERNAL — Enrollment cancel edildiğinde çağrılır.
        internal void RemoveEnrollmentInternal(Enrollment e)
        {
            _enrollments.Remove(e);
        }
        
        public bool IsStudentEnrolled(Student s)
        {
            foreach (var en in _enrollments)
            {
                if (en.Student == s)
                    return true;
            }
            return false;
        }

      
        // CONSTRUCTORS
        
        public Course()
        {
            AddToExtent(this);
        }

        public Course(string name, string title, int level, int ects, bool isFull, double gpaWeight)
        {
            Name = name;
            Title = title;
            Level = level;
            Ects = ects;
            IsFull = isFull;
            GPAWeight = gpaWeight;

            AddToExtent(this);
        }
    }
}
