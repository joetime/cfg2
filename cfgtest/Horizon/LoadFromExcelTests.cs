using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cfglib;
using System.Collections.Generic;

namespace cfgtest
{
    [TestClass]
    public class LoadFromExcelTests : _BaseTest
    {
        [TestMethod]
        [Ignore]
        public void OpenExcelFile()
        {
            List<HorizonLineItem> items =  HorizonUtils.ReadFile("../../TestFiles/1_14_Horizon.xls");
            Assert.AreEqual(4150, items.Count, "Line Items not loaded correctly");
        }
    }
}
