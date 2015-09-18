﻿using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;

namespace MqTests.Tests_Method
{
    [TestFixture]
    class SearchMany : Data
    {
        //происходит проверка найденных направлений?

        //тут должна быть какая-то ошибка
        [Test]
        public void SearchMany_Empty()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options opt = new Options();
                var res = mq.SearchMany(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        //тут должно вернуться одно направление
        [Test]
        public void SearchMany_Test()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                //Регистрируем один случай. Статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).SetStatus_RegisterMin();
                var result = mq.Register(cr, referral);

                //Задаём Options по зарегистрированному направлению и ищем его
                Options opt = (new SetData()).GetRefferalReturnOptions_SearchMany(referral, null, result.MqReferralStatus);
                var res2 = mq.SearchOne(cr, opt);
            }
        }

        [Test]
        public void SearchMany_ProfileMedService()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options opt = new Options
                {
                    ReferralInfo = new ReferralInfo
                    {
                        ProfileMedService = ReferralData.referralInfo.ProfileMedService
                    }
                };
                var res2 = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void SearchMany_ReferralType()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options opt = new Options
                {
                    ReferralInfo = new ReferralInfo
                    {
                        ReferralType = ReferralData.referralInfo.ReferralType
                    }
                };
                var res2 = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void SearchMany_TargetLpu()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options opt = new Options
                {
                    Target = new ReferralTarget { Lpu = ReferralData.referralTarget.Lpu }
                };
                var res2 = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void SearchMany_SourceLpu()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options opt = new Options
                {
                    Source = new ReferralSource { Lpu = ReferralData.referralSource.Lpu }
                };
                var res2 = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void SearchMany_MqReferralStatus()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options opt = new Options
                {
                    ReferralInfo = new ReferralInfo
                    {
                        MqReferralStatus = new Coding { Code = "1", System = Dictionary.REFERRAL_STATUS, Version = "1" }
                    }
                };
                var res2 = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void SearchMany_Survey()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options opt = new Options
                {
                    Survey = new Survey
                    {
                        SurveyOrgan = ReferralData.survey.SurveyOrgan,
                        SurveyType = ReferralData.survey.SurveyType
                    }
                };
                var res2 = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void SearchMany_PlannedDate()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options opt = new Options
                {
                    EventsInfo = new EventsInfo
                    {
                        Source = new EventSource
                        {
                            PlannedDate = ReferralData.eventsInfo.Source.PlannedDate
                        }
                    }
                };
                var res2 = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void SearchMany_IsReferralReviwed()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options opt = new Options
                {
                    EventsInfo = new EventsInfo
                    {
                        Target = new EventTarget { IsReferralReviwed = true }
                    }
                };
                var res2 = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void SearchMany_Privilege()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options opt = new Options
                {
                    Patient = new Patient
                    {
                        Privileges = new Privilege[] 
                        {
                            new Privilege { PrivilegeType = PersonData.patient.Privileges[0].PrivilegeType }
                        }
                    }
                };
                var res2 = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
