using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;

namespace MqTests.Tests_Method
{
    [TestFixture]
    class UpdateFromSourcedMo: Data
    {
        [Test]
        public void MinUpdateFromSourcedMo()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);



                if (Global.errors == "")
                    Assert.Pass();
                else
                    Assert.Fail(Global.errors);
            }
        }

        [Test]
        public void FullUpdateFromSourcedMo()
        {

        }
    }
}
