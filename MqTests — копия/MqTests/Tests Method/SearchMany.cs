using System;
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
            using (MqServiceClient mq = new MqServiceClient())
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
            using (MqServiceClient mq = new MqServiceClient())
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
            using (MqServiceClient mq = new MqServiceClient())
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
            using (MqServiceClient mq = new MqServiceClient())
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
            using (MqServiceClient mq = new MqServiceClient())
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
            using (MqServiceClient mq = new MqServiceClient())
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
            using (MqServiceClient mq = new MqServiceClient())
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
            using (MqServiceClient mq = new MqServiceClient())
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
            using (MqServiceClient mq = new MqServiceClient())
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
            using (MqServiceClient mq = new MqServiceClient())
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
            using (MqServiceClient mq = new MqServiceClient())
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
