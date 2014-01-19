using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cfglib;

namespace cfgtest
{
    [TestClass]
    public class HorizonLineItemTests : _BaseTest
    {
        [TestMethod]
        [TestCategory("Horizon Files")]
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
                try
                {
                    HorizonLineItem item = HorizonUtils.ProcessRowText(s);
                    Assert.Fail("Invalid file format not caught: " + s);
                }
                catch { }
            }
        }

        [TestMethod]
        [TestCategory("Horizon Files")]
        public void TestParser()
        {
            string testString = "\"COASTAL BENEFITS\",\"005766\",\"J & J TRI-STATE DELIVERY SERVICE, INC\",\"81331\",\"DACCS\",\"2011-05-01\",\"2014-05-01\",\"2013-11-01\",\"100\",\"5766\",\"495\",\"30227.47\",\"4318.21\",\"213.75\"";

            HorizonLineItem lineItem = HorizonUtils.ProcessRowText(testString);

            //Assert.AreEqual(lineItem.BrokerageName, "COASTAL BENEFITS");
            //Assert.AreEqual(lineItem.BrokerageNumber, "005766");
            Assert.AreEqual("J & J TRI-STATE DELIVERY SERVICE, INC", lineItem.GroupName);
            Assert.AreEqual("81331", lineItem.GroupNumber);
            Assert.AreEqual("DACCS", lineItem.Product);

            Assert.AreEqual("2011-05-01", lineItem.EffectiveDate);
            Assert.AreEqual("2014-05-01", lineItem.RenewalDate);
            Assert.AreEqual("2013-11-01", lineItem.InsuredPeriod);

            Assert.AreEqual("100", lineItem.C_CodeOneHundredd);
            Assert.AreEqual("5766", lineItem.C_CoastalCode);

            Assert.AreEqual("30227.47", lineItem.C_TotalPremiumYTD);
            Assert.AreEqual(4318.21m, lineItem.PremiumReceived);
            Assert.AreEqual( "495", lineItem.CommissionSchedule.ToString());
        }
    }
}
