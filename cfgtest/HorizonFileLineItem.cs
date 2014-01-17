using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cfglib;

namespace cfgtest
{
    [TestClass]
    public class HorizonFileLineItemTests
    {
        [TestMethod]
        public void TestInvalidTextFormats()
        {
            string[] errorStrings = new string[] { 
                "", // empty
                    // missing one parameter
                "\"COASTAL BENEFITS\",\"005766\",\"81331\",\"EPO\",\"2011-05-01\",\"2014-05-01\",\"2013-11-01\",\"100\",\"5766\",\"495\",\"95728.69\",\"14066.98\",\"696.31\"",
                    // extra param
                "\"COASTAL BENEFITS\",\"005766\",\"81331\",\"EPO\",\"2011-05-01\",\"2014-05-01\",\"2013-11-01\",\"100\",\"5766\",\"495\",\"95728.69\",\"14066.98\",\"696.31\",\"696.31\",\"696.31\"",
                    // missing first quote
                "COASTAL BENEFITS\",\"005766\",\"J & J TRI-STATE DELIVERY SERVICE, INC\",\"81331\",\"EPO\",\"2011-05-01\",\"2014-05-01\",\"2013-11-01\",\"100\",\"5766\",\"495\",\"95728.69\",\"14066.98\",\"696.31\"",
                    // missing last quote
                "\"COASTAL BENEFITS\",\"005766\",\"J & J TRI-STATE DELIVERY SERVICE, INC\",\"81331\",\"EPO\",\"2011-05-01\",\"2014-05-01\",\"2013-11-01\",\"100\",\"5766\",\"495\",\"95728.69\",\"14066.98\",\"696.31" };

            foreach (string s in errorStrings)
            {
                HorizonFileLineItem item = new HorizonFileLineItem(s);
                Assert.AreEqual(item.status, HorizonFileLineItemStatus.Error);
            }
        }

        [TestMethod]
        public void TestValidTextFormats()
        {
            string validString = "\"COASTAL BENEFITS\",\"005766\",\"J & J TRI-STATE DELIVERY SERVICE, INC\",\"81331\",\"EPO\",\"2011-05-01\",\"2014-05-01\",\"2013-11-01\",\"100\",\"5766\",\"495\",\"95728.69\",\"14066.98\",\"696.31\"";

            HorizonFileLineItem item = new HorizonFileLineItem(validString);
            Assert.AreEqual(item.status, HorizonFileLineItemStatus.Ok);
        }

        [TestMethod]
        public void TestParser()
        {
            string testString = "\"COASTAL BENEFITS\",\"005766\",\"J & J TRI-STATE DELIVERY SERVICE, INC\",\"81331\",\"DACCS\",\"2011-05-01\",\"2014-05-01\",\"2013-11-01\",\"100\",\"5766\",\"495\",\"30227.47\",\"4318.21\",\"213.75\"";

            HorizonFileLineItem lineItem = new HorizonFileLineItem(testString);

            Assert.AreEqual(lineItem.BrokerageName, "COASTAL BENEFITS");
            Assert.AreEqual(lineItem.BrokerageNumber, "005766");
            Assert.AreEqual(lineItem.GroupName, "J & J TRI-STATE DELIVERY SERVICE, INC");
            Assert.AreEqual(lineItem.GroupNumber, "81331");
            Assert.AreEqual(lineItem.Product, "DACCS");

            Assert.AreEqual(lineItem.EffectiveDate.ToString(), "2011-05");
            Assert.AreEqual(lineItem.RenewalDate.ToString(), "2014-05");
            Assert.AreEqual(lineItem.InsuredPeriod.ToString(), "2013-11");

            Assert.AreEqual(lineItem.PremiumReceived, 4318.21m);
            Assert.AreEqual(lineItem.CommissionSchedule.ToString(), "495");
        }
    }
}
