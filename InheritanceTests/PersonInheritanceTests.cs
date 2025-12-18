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
                "Ali",
                "Yilmaz",
                new DateTime(2001, 5, 5),
                "ali@student.com",
                123,
                0,
                2,
                3.2,
                3,
                85
            );

            Assert.IsTrue(student is Person);
        }

        [Test]
        public void Person_Inheritance_Is_Overlapping()
        {
            var student = new Student(
                "Ahmet",
                "Kaya",
                new DateTime(1999, 1, 1),
                "a@student.com",
                456,
                0,
                3,
                3.6,
                5,
                90
            );

            var teacher = new Teacher("Programming", "Full-time");

            Assert.IsTrue(student is Person);
            Assert.IsTrue(teacher is Person);
        }
    }
}