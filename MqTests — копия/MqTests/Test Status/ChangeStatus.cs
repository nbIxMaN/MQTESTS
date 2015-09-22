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
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                if (result.MqReferralStatus.Code != "1")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + ""); 
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //Задаём статус "Согласовано в направляющей МО".
        //Текущий статус направления "Зарегистрировано в РЕГИЗ.УО".
        [Test]
        public void StatusAgreedInSourcedMO_CurStat1()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                //Задаём статус "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Согласовано в направляющей МО"
                referral = (new SetData()).SetStatus_AgreedInSourcedMO(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                if (res2.MqReferralStatus.Code != "2")
                    Global.errors1.Add("Неверный статус:" + res2.MqReferralStatus.Code + "");
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
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                //Задаём статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Выдано пациенту"
                referral = (new SetData()).SetStatus_PatientDocumentIssue(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                if (res2.MqReferralStatus.Code != "3")
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
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                //Задаём статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Согласовано в направляющей МО"
                referral = (new SetData()).SetStatus_AgreedInSourcedMO(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                //Задаём статус "Выдано пациенту"
                referral = (new SetData()).SetStatus_PatientDocumentIssue(result.IdMq);
                var res3 = mq.UpdateFromSourcedMo(cr, referral);

                if (res3.MqReferralStatus.Code != "3")
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
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                //Задаём статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Выдано пациенту"
                referral = (new SetData()).SetStatus_PatientDocumentIssue(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                //Задаём статус "Признано обоснованным в целевой МО"
                referral = (new SetData()).SetStatus_AgreedInTargedMO(result.IdMq);
                var res3 = mq.UpdateFromTargetMo(cr, referral);

                if (res3.MqReferralStatus.Code != "4")
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
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Выдано пациенту"
                referral = (new SetData()).SetStatus_PatientDocumentIssue(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                //Задаём статус "Выделена единица ресурса, целевой МО назначена дата приема"
                referral = (new SetData()).MinChangePlannedResource(result.IdMq);
                var res3 = mq.ChangePlannedResource(cr, referral);

                if (res3.MqReferralStatus.Code != "5")
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
                Referral referral = (new SetData()).MinRegister();
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

                if (res4.MqReferralStatus.Code != "5")
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
                Referral referral = (new SetData()).MinRegister();
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

                if (res4.MqReferralStatus.Code != "6")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //Задаём статус "Завершено оказание медицинской помощи в целевой МО".
        //Текущий статус направления "Начато оказание медицинской помощи в целевой МО".
        [Test]
        public void StatusHealthCareEnd()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                //Задаём статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).MinRegister();
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

                //Задаём статус "Завершено оказание медицинской помощи в целевой МО"
                referral = (new SetData()).SetStatus_HealthCareEnd(result.IdMq);
                var res5 = mq.UpdateFromTargetMo(cr, referral);

                if (res5.MqReferralStatus.Code != "7")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //Задаём статус "Признано не обоснованным в целевой МО".
        //Текущий статус направления "Выдано пациенту".
        [Test]
        public void StatusNotAgreedInNargedMO()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                //Задаём статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Выдано пациенту"
                referral = (new SetData()).SetStatus_PatientDocumentIssue(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                //Задаём статус "Признано не обоснованным в целевой МО"
                referral = (new SetData()).SetStatus_NotAgreedInTargedMO(result.IdMq);
                var res3 = mq.UpdateFromTargetMo(cr, referral);

                if (res3.MqReferralStatus.Code != "8")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //Задаём статус "Аннулировано".
        //Текущий статус направления "Зарегистрировано в РЕГИЗ.УО".
        [Test]
        public void StatusCancellation_CurStat1()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                //Задаём статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Аннулировано"
                referral = (new SetData()).MinCancellation(result.IdMq);
                var res2 = mq.Cancellation(cr, referral);

                if (res2.MqReferralStatus.Code != "0")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //Задаём статус "Аннулировано".
        //Текущий статус направления "Согласовано в направляющей МО".
        [Test]
        public void StatusCancellation_CurStat2()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                //Задаём статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Согласовано в направляющей МО"
                referral = (new SetData()).SetStatus_AgreedInSourcedMO(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                //Задаём статус "Аннулировано"
                referral = (new SetData()).MinCancellation(result.IdMq);
                var res3 = mq.Cancellation(cr, referral);

                if (res3.MqReferralStatus.Code != "0")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //Задаём статус "Аннулировано".
        //Текущий статус направления "Выдано пациенту".
        [Test]
        public void StatusCancellation_CurStat3()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                //Задаём статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Выдано пациенту"
                referral = (new SetData()).SetStatus_PatientDocumentIssue(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                //Задаём статус "Аннулировано"
                referral = (new SetData()).MinCancellation(result.IdMq);
                var res3 = mq.Cancellation(cr, referral);

                if (res3.MqReferralStatus.Code != "0")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //Задаём статус "Аннулировано".
        //Текущий статус направления "Признано обоснованным в целевой МО".
        [Test]
        public void StatusCancellation_CurStat4()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                //Задаём статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Выдано пациенту"
                referral = (new SetData()).SetStatus_PatientDocumentIssue(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                //Задаём статус "Признано обоснованным в целевой МО"
                referral = (new SetData()).SetStatus_AgreedInTargedMO(result.IdMq);
                var res3 = mq.UpdateFromTargetMo(cr, referral);

                //Задаём статус "Аннулировано"
                referral = (new SetData()).MinCancellation(result.IdMq);
                var res4 = mq.Cancellation(cr, referral);

                if (res4.MqReferralStatus.Code != "0")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //Задаём статус "Аннулировано".
        //Текущий статус направления "Выделена единица ресурса, целевой МО назначена дата приема".
        [Test]
        public void StatusCancellation_CurStat5()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                //Задаём статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Выдано пациенту"
                referral = (new SetData()).SetStatus_PatientDocumentIssue(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                //Задаём статус "Выделена единица ресурса, целевой МО назначена дата приема"
                referral = (new SetData()).MinChangePlannedResource(result.IdMq);
                var res3 = mq.ChangePlannedResource(cr, referral);

                //Задаём статус "Аннулировано"
                referral = (new SetData()).MinCancellation(result.IdMq);
                var res4 = mq.Cancellation(cr, referral);

                if (res4.MqReferralStatus.Code != "0")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
