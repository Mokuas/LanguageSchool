using NUnit.Framework;
using LanguageSchoolBYT.Models;
using System;

namespace InheritanceTests
{
    public class PersonInheritanceTests
    {
        [Test]
        public void Person_Is_Abstract_Class()
        {
            Assert.IsTrue(typeof(Person).IsAbstract);
        }

        [Test]
        public void Student_Is_Person()
        {
            var student = new Student(
                "Poyraz",
                "Dinler",
                new DateTime(2001, 5, 5),
                "s29979@pjwstk.edu.pl",
                29979,
                0,
                3,
                3.2,
                6,
                85
            );

            Assert.IsTrue(student is Person);
        }

        [Test]
        public void Person_Inheritance_Is_Overlapping()
        {
            var student = new Student(
                "Ileay",
                "Cenik",
                new DateTime(1999, 1, 1),
                "s29914@pjwstk.edu.pl",
                29914,
                0,
                3,
                3.6,
                6,
                90
            );

            var teacher = new Teacher("Programming", "Full-time");

            Assert.IsTrue(student is Person);
            Assert.IsTrue(teacher is Person);
        }
    }
}