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
            using (mq)
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var id = mq.Register(cr, referral);
                referral = (new SetData()).MinPatientDocumentIssue(id.IdMq);
                mq.PatientDocumentIssue(cr, referral);
            }
        }

        [Test]
        public void FullPatientDocumentIssue()
        {
            using (mq)
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var id = mq.Register(cr, referral);
                referral = (new SetData()).FullPatientDocumentIssue(id.IdMq);
                mq.PatientDocumentIssue(cr, referral);
            }
        }
    }
}
