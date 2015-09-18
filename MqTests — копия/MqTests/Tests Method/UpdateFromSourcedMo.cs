using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;
using System.ServiceModel;

namespace MqTests.Tests_Method
{
    [TestFixture]
    class UpdateFromSourcedMo: Data
    {
        [Test]
        public void MinUpdateFromSourcedMo()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);
                
                //обновляем данные
                referral = (new SetData()).MinUpdateFromSourcedMo(result.IdMq);
                var updateResult = mq.UpdateFromSourcedMo(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void FullUpdateFromSourcedMo()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //обновляем данные
                referral = (new SetData()).FullUpdateFromSourcedMo(result.IdMq);
                try
                {
                    var updateResult = mq.UpdateFromSourcedMo(cr, referral);
                }
                catch (FaultException<MqTests.WebReference.MqFault> e)
                {
                    string s = e.Detail.MqFaults[0].Message;
                }
                if (Global.errors == "")
                    Assert.Pass();
                else
                    Assert.Fail(Global.errors);
            }
        }
    }
}
