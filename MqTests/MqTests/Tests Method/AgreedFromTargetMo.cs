using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;

namespace MqTests.Tests_Method
{
    [TestFixture]
    public class AgreedFromTargetMo: Data
    {
        [Test]
        public void MinAgreedFromTargetMo()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                //"Выдано пациенту"
                referral = (new SetData()).MinPatientDocumentIssue(result.IdMq);
                var res2 = mq.PatientDocumentIssue(cr, referral);

                referral = (new SetData()).MinAgreedFromTargetMo(result.IdMq);
                var resultAgreed = mq.AgreedFromTargetMo(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void FullAgreedFromTargetMo()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                //"Выдано пациенту"
                referral = (new SetData()).MinPatientDocumentIssue(result.IdMq);
                var res2 = mq.PatientDocumentIssue(cr, referral);

                referral = (new SetData()).FullAgreedFromTargetMo(result.IdMq);
                var resultAgreed = mq.AgreedFromTargetMo(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
