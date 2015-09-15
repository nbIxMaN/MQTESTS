using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;

namespace MqTests.Tests_Method
{
    [TestFixture]
    class HealthCareEnd : Data
    {
        [Test]
        public void MinHealthCareEnd()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                //"Выдано пациенту"
                referral = (new SetData()).MinPatientDocumentIssue(result.IdMq);
                var res2 = mq.PatientDocumentIssue(cr, referral);

                //"Выделена единица ресурса, целевой МО назначена дата приема"
                referral = (new SetData()).MinChangePlannedResource(result.IdMq);
                var res3 = mq.ChangePlannedResource(cr, referral);

                //"Начато оказание медицинской помощи в целевой МО"
                referral = (new SetData()).MinHealthCareStart(result.IdMq);
                var res4 = mq.HealthCareStart(cr, referral);

                referral = (new SetData()).MinHealthCareEnd(result.IdMq);
                var res5 = mq.HealthCareEnd(cr, referral);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void FullHealthCareEnd()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                //"Выдано пациенту"
                referral = (new SetData()).MinPatientDocumentIssue(result.IdMq);
                var res2 = mq.PatientDocumentIssue(cr, referral);

                //"Выделена единица ресурса, целевой МО назначена дата приема"
                referral = (new SetData()).MinChangePlannedResource(result.IdMq);
                var res3 = mq.ChangePlannedResource(cr, referral);

                //"Начато оказание медицинской помощи в целевой МО"
                referral = (new SetData()).MinHealthCareStart(result.IdMq);
                var res4 = mq.HealthCareStart(cr, referral);

                referral = (new SetData()).FullHealthCareEnd(result.IdMq);
                var res5 = mq.HealthCareEnd(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
