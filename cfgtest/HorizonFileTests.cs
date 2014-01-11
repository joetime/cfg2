using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cfglib;

namespace cfgtest
{
    [TestClass]
    public class HorizonFileTests
    {
        [TestMethod]
        public void GetHorizonFile()
        {
            Repos repos = new Repos();
            HorizonFile file = repos.HorizonFile(18);
            Assert.IsTrue(file.Id == 18, "Horizon record not found.");
        }

    }
}
