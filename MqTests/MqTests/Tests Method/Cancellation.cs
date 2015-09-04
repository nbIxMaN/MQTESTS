using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;

namespace MqTests.Tests_Method
{
    [TestFixture]
    public class Cancellation : Data
    {
        [Test]
        public void MinCancellation()
        {
            using (mq)
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var id = mq.Register(cr, referral);
                referral = (new SetData()).MinCancellation(id.IdMq);
                mq.PatientDocumentIssue(cr, referral);
            }
        }

        [Test]
        public void FullCancellation()
        {
            using (mq)
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var id = mq.Register(cr, referral);
                referral = (new SetData()).FullCancellation(id.IdMq);
                mq.PatientDocumentIssue(cr, referral);
            }
        }
    }
}
