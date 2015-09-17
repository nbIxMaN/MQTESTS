using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MqTests.WebReference;
using NUnit.Framework;
using MqTests;

namespace QueueTest
{
    [TestFixture]
    public class Register: Data
    {
        [Test]
        public void Min()
        {
            using ( TestMqServiceClient mq = new TestMqServiceClient() )
            {
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials
                {
                    Organization = idLpu,
                    Token = guid
                };
                var x = mq.Register(cr, referral);
                if (Global.errors != "")
                    NUnit.Framework.Assert.Fail(Global.errors);
                else
                    NUnit.Framework.Assert.Pass();
            }
        }

        [Test]
        public void Full()
        {
            using (mq)
            {
                Referral referral = ReferralData.referral;
                Credentials cr = new Credentials
                {
                    Organization = idLpu,
                    Token = guid
                };
                mq.Register(cr, referral);
            }
        }
    }
}
