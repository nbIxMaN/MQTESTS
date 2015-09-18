using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;

namespace MqTests.Tests_Method
{
    [TestFixture]
    class SetOrChangeTargetMO : Data
    {
        [Test]
        public void ChangeTargetMO()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                //Изменяем целевую МО
                referral = (new SetData()).MinSetOrChangeTargetMO(result.IdMq);
                var res2 = mq.SetOrChangeTargetMo(cr,referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }


    }
}
