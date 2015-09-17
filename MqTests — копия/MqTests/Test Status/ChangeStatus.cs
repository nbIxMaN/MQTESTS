using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;
using System.ServiceModel;

namespace MqTests
{
    [TestFixture]
    class ChangeStatus : Data
    {
        //Задаём статус "Зарегистрировано в РЕГИЗ.УО"
        [Test]
        public void StatusRegister()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Referral referral = (new SetData()).SetStatus_RegisterMin();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                if (result.MqReferralStatus.Code != "Зарегистрировано в РЕГИЗ.УО")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + ""); 
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }


        //тест с омс старого образца!!

        //Задаём статус "Согласовано в направляющей МО".
        //Текущий статус направления "Зарегистрировано в РЕГИЗ.УО".
        [Test]
        public void StatusAgreedInSourcedMO()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                //Задаём статус "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).SetStatus_RegisterMin();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Согласовано в направляющей МО"
                referral = (new SetData()).SetStatus_AgreedInSourcedMO(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                if (res2.MqReferralStatus.Code != "Согласовано в направляющей МО")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //Задаём статус "Выдано пациенту".
        //Текущий статус направления "Зарегистрировано в РЕГИЗ.УО".
        [Test]
        public void StatusPatientDocumentIssue_curStat1()
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

                if (res2.MqReferralStatus.Code != "Выдано пациенту")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //Задаём статус "Выдано пациенту".
        //Текущий статус направления "Согласовано в направляющей МО".
        [Test]
        public void StatusPatientDocumentIssue_curStat2()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                //Задаём статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).SetStatus_RegisterMin();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Согласовано в направляющей МО"
                referral = (new SetData()).SetStatus_AgreedInSourcedMO(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                //Задаём статус "Выдано пациенту"
                referral = (new SetData()).SetStatus_PatientDocumentIssue(result.IdMq);
                var res3 = mq.UpdateFromSourcedMo(cr, referral);

                if (res3.MqReferralStatus.Code != "Выдано пациенту")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //Задаём статус "Признано обоснованным в целевой МО". 
        //Текущий статус направления "Выдано пациенту".
        [Test]
        public void StatusAgreedInTargedMO()
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

                //Задаём статус "Признано обоснованным в целевой МО"
                referral = (new SetData()).SetStatus_AgreedInTargedMO(result.IdMq);
                var res3 = mq.UpdateFromTargetMo(cr, referral);

                if (res3.MqReferralStatus.Code != "Признано обоснованным в целевой МО")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //Задаём статус "Выделена единица ресурса, целевой МО назначена дата приема".
        //Текущий статус направления "Выдано пациенту".
        [Test]
        public void StatusChangePlannedResource_CurStat3()
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

                if (res3.MqReferralStatus.Code != "Выделена единица ресурса, целевой МО назначена дата приема")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //Задаём статус "Выделена единица ресурса, целевой МО назначена дата приема". 
        //Текущий статус направления "Признано обоснованным в целевой МО".
        [Test]
        public void StatusChangePlannedResource_CurStat4()
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

                //Задаём статус "Признано обоснованным в целевой МО"
                referral = (new SetData()).SetStatus_AgreedInTargedMO(result.IdMq);
                var res3 = mq.UpdateFromTargetMo(cr, referral);

                //Задаём статус "Выделена единица ресурса, целевой МО назначена дата приема"
                referral = (new SetData()).MinChangePlannedResource(result.IdMq);
                var res4 = mq.ChangePlannedResource(cr, referral);

                if (res4.MqReferralStatus.Code != "Выделена единица ресурса, целевой МО назначена дата приема")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //Задаём статус "Начато оказание медицинской помощи в целевой МО".
        //Текущий статус направления "Выделена единица ресурса, целевой МО назначена дата приема".
        [Test]
        public void StatusHealthCareStart()
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
                referral = (new SetData()).SetStatus_HealthCareStart(result.IdMq);
                var res4 = mq.UpdateFromTargetMo(cr, referral);

                if (res4.MqReferralStatus.Code != "Начато оказание медицинской помощи в целевой МО")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
