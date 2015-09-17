using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;

namespace MqTests.Tests_Method
{
    [TestFixture]
    public class PatientDocumentIssue : Data
    {
        [Test]
        public void MinPatientDocumentIssue()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                referral = (new SetData()).MinPatientDocumentIssue(result.IdMq);
                var result2 = mq.PatientDocumentIssue(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void FullPatientDocumentIssue()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);
                
                referral = (new SetData()).FullPatientDocumentIssue(result.IdMq);
                mq.PatientDocumentIssue(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
