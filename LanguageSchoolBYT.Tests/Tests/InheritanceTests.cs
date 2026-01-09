using NUnit.Framework;
using LanguageSchoolBYT.Models;

namespace LanguageSchoolBYT.Tests
{
    public class InheritanceTests
    {
        [Test]
        public void Student_IsPerson()
        {
            Student student = new Student();
            Assert.IsTrue(student is Person);
        }

        [Test]
        public void Teacher_IsStaff()
        {
            Teacher teacher = new Teacher();
            Assert.IsTrue(teacher is Staff);
        }

        [Test]
        public void MandatoryCourse_IsCourse()
        {
            Course course = new MandatoryCourse();
            Assert.IsTrue(course is Course);
        }

        [Test]
        public void Course_MultiAspectInheritance_Works()
        {
            Course course = new MandatoryCourse();

            course.LanguageAspect.AddLanguage(CourseLanguage.English);
            course.LanguageAspect.AddLanguage(CourseLanguage.Turkish);

            Assert.AreEqual(2, course.LanguageAspect.Languages.Count);
        }
    }
}