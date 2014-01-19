using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cfglib;
using System.Collections.Generic;

namespace cfgtest
{
    [TestClass]
    public class RawHorizonTests : _BaseTest
    {
        string HorizonTestFileText = "../../TestFiles/2013-10-edd5500e-d927-4394-b95b-c2609eea0f9f.txt";
        string HorizonTestFileExcel = "../../TestFiles/1_14_Horizon.xls";

        [TestMethod]
        [TestCategory("Horizon Files")]
        public void TestReadFileText()
        {
            List<HorizonLineItem> Horizons = HorizonUtils.ReadFile(HorizonTestFileText);

            // total line count
            Assert.AreEqual(4269, Horizons.Count);
        }

        [TestMethod]
        [TestCategory("Horizon Files")]
        public void TestReadFileExcel()
        {
            List<HorizonLineItem> Horizons = HorizonUtils.ReadFile(HorizonTestFileExcel);

            // total line count
            Assert.AreEqual(4150, Horizons.Count);
        }

        [TestMethod]
        [TestCategory("Horizon Files")]
        [TestCategory("Long Tests")]
        //[Ignore]
        public void TestSaveAndDeleteRecordsText()
        {
            int month = 1;
            int year = 1990;

            repos.DeleteHorizonRecords(year, month);
            repos.SaveChanges();

            int count = repos.RawHorizonRecordsCount(year, month);
            Assert.AreEqual(0, count, String.Format("Should be no records (M/Y): {0}/{1}, they were deleted; found: {2}.", month, year, count));

            // read from file, save items
            HorizonLineItemCollection Horizons = new HorizonLineItemCollection(HorizonUtils.ReadFile(HorizonTestFileText));
            Horizons.SaveToDatabase(year, month, true);

            count = repos.RawHorizonRecordsCount(year, month);
            Assert.AreEqual(4269, count, String.Format("Should be 318 records (M/Y): {0}/{1}; found {2}.", month, year, count));

        }

        [TestMethod]
        [TestCategory("Horizon Files")]
        public void TestOverwrite()
        {
            HorizonLineItemCollection Horizons = new HorizonLineItemCollection(HorizonUtils.ReadFile(HorizonTestFileText));

            // try to overwrite
            try
            {
                Horizons.SaveToDatabase(1990, 1);
                Assert.Fail("Records Overwritten!");
            }
            catch
            {
                Assert.IsTrue(true, "");
            }
        }
    }
}
