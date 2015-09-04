using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueTest.MqService;
using NUnit.Framework;

namespace QueueTest
{
    [TestFixture]
    public class Register: Data
    {
        [Test]
        public void Min()
        {
            using ( mq )
            {
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials
                {
                    Organization = idLpu,
                    Token = guid
                };
                mq.Register(cr, referral);
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
