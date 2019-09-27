using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ToDoListTest
{
    [TestClass]
    public class ToDoUnitTests
    {
        [TestMethod]
        public void ValidateNullDate()
        {
            Assert.AreEqual(ToDoList.DataLayer.DataFormatter.FormattedDate(null), string.Empty);
        }

        [TestMethod]
        public void ValidateValidDate()
        {
            Assert.AreNotEqual(ToDoList.DataLayer.DataFormatter.FormattedDate(DateTime.Now), string.Empty);
        }

        [TestMethod]
        public void ValidateDateFormat()
        {
            Assert.AreNotEqual(ToDoList.Constants.Constants.datetimeformat, "dd MMM yyyy HH: mm tt");
        }

        [TestMethod]
        public void ValidateDateTimeFormat()
        {
            Assert.AreEqual(ToDoList.Constants.Constants.datetimesecformat, "dd MMM yyyy HH:mm:ss tt");
        }
    }
}
