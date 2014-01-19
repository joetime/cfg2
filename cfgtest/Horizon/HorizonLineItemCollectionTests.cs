using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cfglib;

namespace cfgtest
{
    [TestClass]
    public class HorizonLineItemCollectionTests : _BaseTest
    {
        [TestMethod]
        [TestCategory("Horizon Files")]
        public void TestTotals()
        {
            HorizonFile file = repos.HorizonFile(93);
            
            HorizonLineItemCollection collection = new HorizonLineItemCollection(
                HorizonUtils.ReadFile("../../TestFiles/" + file.Filename));

            // calculated with excel
            // 4495816.46	
            // 269079.10
            Assert.AreEqual(269079.10m, collection.CommissionReceived, "Commission calculated incorrectly.");
            Assert.AreEqual(4495816.46m, collection.PremiumReceived, "Premium calculated incorrectly.");

            // check for range
            Assert.IsTrue(.0598m < collection.CommissionPercentage, "Percentage calculated incorrectly.");
            Assert.IsTrue(.0599m > collection.CommissionPercentage, "Percentage calculated incorrectly.");
        }   
    }
}
