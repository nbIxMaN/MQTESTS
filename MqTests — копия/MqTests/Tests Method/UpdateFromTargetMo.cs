using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;

namespace MqTests.Tests_Method
{
    [TestFixture]
    class UpdateFromTargetMo : Data
    {
        [Test]
        public void MinUpdateFromTargetMO()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                //"Выдано пациенту"
                referral = (new SetData()).MinPatientDocumentIssue(result.IdMq);
                var res2 = mq.PatientDocumentIssue(cr, referral);

                referral = (new SetData()).MinUpdateFromTargetMO(result.IdMq);
                var res3 = mq.UpdateFromTargetMo(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void FullUpdateFromTargetMO()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                //"Выдано пациенту"
                referral = (new SetData()).MinPatientDocumentIssue(result.IdMq);
                var res2 = mq.PatientDocumentIssue(cr, referral);

                referral = (new SetData()).FullUpdateFromTargetMO(result.IdMq);
                var res3 = mq.UpdateFromTargetMo(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
