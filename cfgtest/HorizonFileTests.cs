using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cfglib;

namespace cfgtest
{
    [TestClass]
    public class HorizonFileTests
    {
        Repos repos = new Repos();

        [TestMethod]
        public void GetHorizonFileById()
        {
            HorizonFile file = repos.HorizonFile(18);
            Assert.IsTrue(file.Id == 18, "Horizon record not found.");
        }

        [TestMethod]
        public void GetHorizonFileByMonthAndYear()
        {
            HorizonFile file = repos.HorizonFile(5, 2011);
            Assert.IsTrue(file.Id == 37, "Incorrect or missing horizon record for month/year.");
        }

        [TestMethod]
        public void OpenFile()
        {
            HorizonFile file = repos.HorizonFile(93);
            int lineCount = file.Load();
            Assert.IsTrue(lineCount > 0, "No data loaded from file.");
            Assert.AreEqual(4314, lineCount, "Incorrect number of lines loaded.");
        }
    }
}
