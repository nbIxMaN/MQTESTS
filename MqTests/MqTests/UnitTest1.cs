using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MqTests.WebReference;

namespace MqTests
{
    [TestClass]
    public class UnitTest1 : Data
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (TestMqServiceClient client = new TestMqServiceClient())
            {
                Credentials cr = new Credentials
                {
                    Organization = "1.2.643.5.1.13.3.25.78.6",
                    Token = "dda0e909-93cd-4549-b7ff-d1caaa1f0bc2"
                };
                Referral r = client.SearchOne(cr, new WebReference.Options
                {
                    IdMq = "78154000000101",
                    //Source = new WebReference.ReferralSource
                    //{
                    //    Lpu = new WebReference.Coding
                    //    {
                    //        Code = "1.2.643.5.1.13.3.25.78.125",
                    //        System = "urn:oid:1.2.643.2.69.1.1.1.64"
                    //    }
                    //}
                }).Referral;
                //List<string> a = TestOptions.GetReferralId(new WebReference.Options
                //{
                //    IdMq = "78154000000101",
                //    Source = new WebReference.ReferralSource
                //    {
                //        Lpu = new WebReference.Coding
                //        {
                //            Code = "1.2.643.5.1.13.3.25.78.125",
                //            System = "urn:oid:1.2.643.2.69.1.1.1.64"
                //        }
                //    }
                //});
            }
            Assert.Fail(Global.errors);
        }
    }
}
