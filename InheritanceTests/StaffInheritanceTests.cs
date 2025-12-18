using NUnit.Framework;
using LanguageSchoolBYT.Models;
using System;

namespace InheritanceTests
{
    public class StaffInheritanceTests
    {
        [Test]
        public void Staff_Is_Abstract_Class()
        {
            Assert.IsTrue(typeof(Staff).IsAbstract);
        }

        [Test]
        public void Teacher_Is_Subclass_Of_Staff()
        {
            Assert.IsTrue(typeof(Teacher).IsSubclassOf(typeof(Staff)));
        }

        [Test]
        public void NonTeachingStaff_Is_Subclass_Of_Staff()
        {
            Assert.IsTrue(typeof(NonTeachingStaff).IsSubclassOf(typeof(Staff)));
        }
    }
}