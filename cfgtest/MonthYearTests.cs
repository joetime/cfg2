using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cfglib;

namespace cfgtest
{
    [TestClass]
    public class MonthYearTests : _BaseTest
    {
        [TestMethod]
        [TestCategory("utils")]
        public void TestMonthYearFromString()
        {
            MonthYear monthyear1 = new MonthYear("2013-04-01");
            Assert.AreEqual(monthyear1.ToString(), "2013-04");
        }

        [TestMethod]
        [TestCategory("utils")]
        public void TestMonthYearFromDate() 
        {
            DateTime testDate = DateTime.Parse("December 31, 2011 4:50 PM");
            MonthYear monthyear2 = new MonthYear(testDate);
            Assert.AreEqual(monthyear2.ToString(), "2011-12");
        }
    }
}
