using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrwellApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace OrwellApi.Tests
{
    [TestClass()]
    public class DateUtilitiesTests
    {
        [TestMethod()]
        public void DateOfWeekTest()
        {
            var date = DateUtilities.DateOfWeek(2014, 52);                                       
            Assert.AreEqual(new DateTime(2015, 1, 3), date);

            date = DateUtilities.DateOfWeek(2015, 1);
            Assert.AreEqual(new DateTime(2015, 1, 10), date);

            date = DateUtilities.DateOfWeek(2015, 51);
            Assert.AreEqual(new DateTime(2015, 12, 26), date);
        }

        [TestMethod()]
        public void NumberOfWeekTest()
        {
            int week = DateUtilities.NumberOfWeek(new DateTime(2015, 12, 26));
            Assert.AreEqual(51, week);

            week = DateUtilities.NumberOfWeek(new DateTime(2015, 1, 3));
            Assert.AreEqual(52, week);
        }

        [TestMethod()]
        public void EndOfWeekTest()
        {
            var date = DateUtilities.EndOfWeek(new DateTime(2015, 1, 1));
            Assert.AreEqual(new DateTime(2015, 1, 3), date);

            date = DateUtilities.EndOfWeek(new DateTime(2015, 12, 25));
            Assert.AreEqual(new DateTime(2015, 12, 26), date);
        }
    }
}