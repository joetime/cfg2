using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cfglib;
using System.Collections.Generic;

namespace cfgtest
{
    [TestClass]
    public class RawOxfordTests : _BaseTest
    {
        string OxfordTestFile = "../../TestFiles/Oxford 11.11 (1).xls";

        [TestMethod]
        [TestCategory("Oxford Files")]
        public void TestReadFile()
        {
            List<OxfordLineItem> oxfords = OxfordUtils.ReadFile(OxfordTestFile);

            // total line count
            Assert.AreEqual(318, oxfords.Count);
        }

        [TestMethod]
        [TestCategory("Oxford Files")]
        [Ignore]
        public void TestDataAnnotations()
        {
            OxfordLineItem lineItem = new OxfordLineItem();
            
            try {
                lineItem.Month = 0;
                lineItem.Year = 13;
            }
            catch (Exception ex) {
                //Assert.Inconclusive(ex.Message);
            }

            Assert.AreNotEqual(0, lineItem.Month, "Validation failed: Month == 0");
            Assert.AreNotEqual(13, lineItem.Year, "Validation failed: Year == 13");
        }

        [TestMethod]
        [TestCategory("Oxford Files")]
        [TestCategory("Long Tests")]
        [Ignore]
        public void TestSaveAndDeleteRecords()
        {
            int month = 1;
            int year = 1990;

            repos.DeleteOxfordRecords(year, month);
            repos.SaveChanges();

            int count = repos.RawOxfordRecordsCount(year, month);
            Assert.AreEqual(0, count, String.Format("Should be no records (M/Y): {0}/{1}, they were deleted; found: {2}.", month, year, count));

            // read from file, save items
            OxfordLineItemCollection oxfords = new OxfordLineItemCollection(OxfordUtils.ReadFile(OxfordTestFile));
            oxfords.SaveToDatabase(year, month, true);

            count = repos.RawOxfordRecordsCount(year, month);
            Assert.AreEqual(318, count, String.Format("Should be 318 records (M/Y): {0}/{1}; found {2}.", month, year, count));

            // try to do it again, should fail
            try 
            {
                oxfords.SaveToDatabase(year, month);
                Assert.Fail("Records overwritten!");
            } catch {
                // all good, check count
                count = repos.RawOxfordRecordsCount(year, month);
                Assert.AreEqual(318, count, String.Format("Should be 318 records (M/Y): {0}/{1}; found {2}.", month, year, count));
            }

            //try again, with overwrite -
            oxfords.SaveToDatabase(year, month, true);
            count = repos.RawOxfordRecordsCount(year, month);
            Assert.AreEqual(318, count, String.Format("Should be 318 records (M/Y): {0}/{1}; found {2}.", month, year, count));

        }

        [TestMethod]
        [TestCategory("Oxford Files")]
        public void TestOverwrite()
        {
            OxfordLineItemCollection oxfords = new OxfordLineItemCollection(OxfordUtils.ReadFile(OxfordTestFile));

            // try to overwrite
            try
            {
                oxfords.SaveToDatabase(1990, 1);
                Assert.Fail("Records Overwritten!");
            }
            catch
            {
                Assert.IsTrue(true, "");
            }
        }
    }
}
