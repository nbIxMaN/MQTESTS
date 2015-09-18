using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;

namespace MqTests.Tests_Method
{
    class GetResultDocument : Data
    {
        //ResultDocument в выходных параметрах должен быть пуст
        [Test]
        public void GetResultDocument_EmptyDoc()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                referral = (new SetData()).GetResultDocument(result.IdMq);
                var res2 = mq.GetResultDocument(cr, referral);

                if (res2.IsDocProvided || res2.ResultDocument != null)
                    Global.errors1.Add("Документ не пуст");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //ResultDocument в выходных параметрах не пуст
        [Test]
        public void GetResultDocument_NotEmptyDoc()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                //Задаём статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).SetStatus_RegisterMin();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Выдано пациенту"
                referral = (new SetData()).SetStatus_PatientDocumentIssue(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                //Задаём статус "Выделена единица ресурса, целевой МО назначена дата приема"
                referral = (new SetData()).MinChangePlannedResource(result.IdMq);
                var res3 = mq.ChangePlannedResource(cr, referral);

                //Задаём статус "Начато оказание медицинской помощи в целевой МО"
                referral = (new SetData()).SetStatus_HealthCareStart(result.IdMq, DocumentData.SingleOMS);
                var res4 = mq.UpdateFromTargetMo(cr, referral);

                //Задаём статус "Завершено оказание медицинской помощи в целевой МО"
                referral = (new SetData()).SetStatus_HealthCareEnd(result.IdMq);
                var res5 = mq.UpdateFromTargetMo(cr, referral);

                referral = (new SetData()).GetResultDocument(result.IdMq);
                var res6 = mq.GetResultDocument(cr, referral);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
