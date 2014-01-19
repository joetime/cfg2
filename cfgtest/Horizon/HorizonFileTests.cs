using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cfglib;
using System.Collections.Generic;

namespace cfgtest
{
    [TestClass]
    public class HorizonFileTests : _BaseTest
    {
        [TestMethod]
        [TestCategory("DB")]
        public void GetHorizonFileById()
        {
            HorizonFile file = repos.HorizonFile(18);
            Assert.IsTrue(file.Id == 18, "Horizon record not found.");
        }

        [TestMethod]
        [TestCategory("DB")]
        public void GetHorizonFileByMonthAndYear()
        {
            HorizonFile file = repos.HorizonFile(5, 2011);
            Assert.IsTrue(file.Id == 37, "Incorrect or missing horizon record for month/year.");
        }

        [TestMethod]
        [TestCategory("Horizon Files")]
        public void OpenFile()
        {
            HorizonFile file = repos.HorizonFile(93);
            
            List<HorizonLineItem> items =
                HorizonUtils.ReadFile("../../TestFiles/" + file.Filename);
            
            Assert.IsTrue(items.Count > 0, "No data loaded from file.");
            Assert.AreEqual(4314, items.Count, "Incorrect number of lines loaded.");
        }
    }
}
