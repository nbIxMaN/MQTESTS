using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;
using System.ServiceModel;

namespace MqTests.Tests_Method
{
    [TestFixture]
    class HealthCareStart : Data
    {
        [Test]
        public void MinHealthCareStart()
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

                referral = (new SetData()).MinHealthCareStart(result.IdMq);
                var res4 = mq.HealthCareStart(cr, referral);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void FullHealthCareStart()
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

                referral = (new SetData()).FullHealthCareStart(result.IdMq);
                var res4 = mq.HealthCareStart(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
