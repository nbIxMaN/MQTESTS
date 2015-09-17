using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;
using System.ServiceModel;

namespace MqTests.Tests_Method
{
    [TestFixture]
    public class Cancellation : Data
    {
        [Test]
        public void MinCancellation()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                referral = (new SetData()).MinCancellation(result.IdMq);
                try
                {
                    var resultCancel = mq.Cancellation(cr, referral);
                }
                catch (FaultException<MqTests.WebReference.MqFault> e)
                {
                    string s = e.Detail.MqFaults[0].Message;
                }
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void FullCancellation()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                
                var result = mq.Register(cr, referral);
                referral = (new SetData()).FullCancellation(result.IdMq);
                var resultCancel = mq.Cancellation(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
