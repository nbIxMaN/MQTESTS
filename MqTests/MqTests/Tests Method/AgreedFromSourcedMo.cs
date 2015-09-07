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
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var id = mq.Register(cr, referral);
                referral = (new SetData()).MinAgreedFromSourcedMo(id.IdMq);
                mq.AgreedFromSourcedMo(cr, referral);
            }
        }

        [Test]
        public void FullAgreedFromSourcedMo()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var id = mq.Register(cr, referral);
                referral = (new SetData()).FullAgreedFromSourcedMo(id.IdMq);
                mq.AgreedFromSourcedMo(cr, referral);
            }
        }

    }
}
