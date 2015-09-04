using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MqTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestReferral.BuildReferralFromDataBaseData("78154000000261");
        }
    }
}
