using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cfglib;

namespace cfgtest
{
    [TestClass]
    public class BrokerTests
    {
        Repos repos = new Repos();

        [TestMethod]
        public void GetBrokerById()
        {
            Broker broker = repos.Broker(1);
            Assert.IsTrue(broker.Id == 1, "Broker record not found.");
        }

        [TestMethod]
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
        public void GetBrokerByNumber()
        {
            string[] brokerNumbers = new string[] { "014673", "006563", "006510", "006210" };

            foreach (string s in brokerNumbers)
            {
                Broker broker = repos.Broker(s);
                Assert.IsTrue(broker.BrokerNumber == s, "Broker record not found: BrokerNumber: " + s);
            }
        }

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

        public void GetBrokerByName()
        {
            string[] brokerNames = new string[] { "ADVANCED INSURANCE AGENCY", "SHERWOOD, IAN" };

            foreach (string s in brokerNames)
            {
                Broker broker = repos.BrokerByName(s);
                Assert.IsTrue(broker.BrokerNumber == s, "Broker record not found: BrokerName: " + s);
            }
        }

        //[TestMethod]
        //public void GetHorizonFileByMonthAndYear()
        //{
        //    HorizonFile file = repos.HorizonFile(5, 2011);
        //    Assert.IsTrue(file.Id == 37, "Incorrect or missing horizon record for month/year.");
        //}

        //[TestMethod]
        //public void OpenFile()
        //{
        //    HorizonFile file = repos.HorizonFile(93);
        //    int lineCount = file.Load();
        //    Assert.IsTrue(lineCount > 0, "No data loaded from file.");
        //    Assert.IsTrue(lineCount == 4314, "Incorrect number of lines loaded.");
        //}


    }
}
