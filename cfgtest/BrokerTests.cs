using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cfglib;

namespace cfgtest
{
    [TestClass]
    public class BrokerTests : _BaseTest
    {
        [TestMethod]
        [TestCategory("DB")]
        public void GetBrokerById()
        {
            Broker broker = repos.Broker(1);
            Assert.IsTrue(broker.Id == 1, "Broker record not found.");
        }

        [TestMethod]
        [TestCategory("DB")]
        public void TestBrokerFieldsAndCollections()
        {
            Broker broker = repos.Broker(1);
            
            Assert.AreEqual(broker.Id, 1);
            Assert.AreEqual(broker.Name, "ADVANCED INSURANCE AGENCY");
            Assert.AreEqual(broker.BrokerNumber, "014673");
            Assert.AreEqual(broker.Street, "1009 B Arnold Ave");
            Assert.AreEqual(broker.City, "Pt. Pleasant");
            Assert.AreEqual(broker.State, "NJ");
            Assert.AreEqual(broker.Zip, "08742");
            Assert.AreEqual(broker.ConstantCommission, 6.00m);
            Assert.AreEqual(broker.Deleted, false);

            Assert.AreEqual(broker.GroupsCount, 11);
            Assert.AreEqual(broker.CommissionRatesCount, 3);
        }

        [TestMethod]
        [TestCategory("DB")]
        public void GetBrokerByNumber()
        {
            string[] brokerNumbers = new string[] { "014673", "006563", "006510", "006210" };

            foreach (string s in brokerNumbers)
            {
                Broker broker = repos.Broker(s);
                Assert.IsTrue(broker.BrokerNumber == s, "Broker record not found: BrokerNumber: " + s);
            }
        }

        [TestMethod]
        [TestCategory("DB")]
        public void GetBrokerByNumberWithMultiples()
        {
            string brokerNumber = "100511"; // theres more than one broker in the db with this number...

            try {
                Broker broker = repos.Broker(brokerNumber);
                Assert.IsTrue(false, "More than one broker has this number, should get error: " + brokerNumber);
            }
            catch 
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        [TestCategory("DB")]
        [Ignore]
        public void GetBrokerByName()
        {
            string[] brokerNames = new string[] { "ADVANCED INSURANCE AGENCY", "SHERWOOD, IAN" };

            foreach (string s in brokerNames)
            {
                Broker broker = repos.BrokerByName(s);
                Assert.IsTrue(broker.BrokerNumber == s, "Broker record not found: BrokerName: " + s);
            }
        }

    }
}
