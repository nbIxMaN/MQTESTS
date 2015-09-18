using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;

namespace MqTests.Tests_Method
{
    [TestFixture]
    class SetOrChangeTargetMO : Data
    {
        [Test]
        public void MinChangeTargetMO()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                referral.Target = new ReferralTarget
                {
                    Lpu = ReferralData.referralTarget.Lpu
                };
                var result = mq.Register(cr, referral);

                //Изменяем целевую МО
                referral = (new SetData()).MinSetOrChangeTargetMO(result.IdMq);
                var res2 = mq.SetOrChangeTargetMo(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void FullChangeTargetMO()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                referral.ReferralSurvey = new Survey
                {
                    SurveyOrgan = ReferralData.survey.SurveyOrgan,
                    SurveyType = ReferralData.survey.SurveyType
                };
                referral.Target = new ReferralTarget
                {
                    Lpu = ReferralData.referralTarget.Lpu
                };
                var result = mq.Register(cr, referral);

                //Изменяем целевую МО
                referral = (new SetData()).FullSetOrChangeTargetMO(result.IdMq);
                var res2 = mq.SetOrChangeTargetMo(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void MinSetTargetMO()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                //Задаём целевую МО
                referral = (new SetData()).MinSetOrChangeTargetMO(result.IdMq);
                var res2 = mq.SetOrChangeTargetMo(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void FullSetTargetMO()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                //Задаём целевую МО
                referral = (new SetData()).FullSetOrChangeTargetMO(result.IdMq);
                var res2 = mq.SetOrChangeTargetMo(cr, referral);
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }


    }
}
