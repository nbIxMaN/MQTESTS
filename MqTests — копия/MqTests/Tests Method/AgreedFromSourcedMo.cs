using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;

namespace MqTests.Tests_Method
{
    [TestFixture]
    public class AgreedFromSourcedMo : Data
    {
        [Test]
        public void MinAgreedFromSourcedMo()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                referral = (new SetData()).MinAgreedFromSourcedMo(result.IdMq);
                var resultAgreed = mq.AgreedFromSourcedMo(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void FullAgreedFromSourcedMo()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                referral = (new SetData()).FullAgreedFromSourcedMo(result.IdMq);
                var resultAgreed = mq.AgreedFromSourcedMo(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

    }
}
