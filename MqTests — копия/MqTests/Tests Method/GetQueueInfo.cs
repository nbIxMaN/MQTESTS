using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;

namespace MqTests.Tests_Method
{
    [TestFixture]
    class GetQueueInfo : Data
    {
        //Как это всё проверяется?


        //тут должна быть какая-то ошибка
        [Test]
        public void GetQueueInfo_Empty()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options options = new Options();

                var result = mq.GetQueueInfo(cr, options);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }


        [Test]
        public void GetQueueInfo_DateReport()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options options = new Options { DateReport = OptionData.options.DateReport };

                var result = mq.GetQueueInfo(cr, options);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void GetQueueInfo_ProfileMedService()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options options = new Options
                {
                    ReferralInfo = new ReferralInfo
                    {
                        ProfileMedService = OptionData.options.ReferralInfo.ProfileMedService
                    }
                };

                var result = mq.GetQueueInfo(cr, options);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void GetQueueInfo_TargetLpu()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options options = new Options
                {
                    Target = new ReferralTarget { Lpu = OptionData.options.Target.Lpu }
                };

                var result = mq.GetQueueInfo(cr, options);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void GetQueueInfo_Full()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options options = new Options
                {
                    DateReport = OptionData.options.DateReport,
                    Target = new ReferralTarget { Lpu = OptionData.options.Target.Lpu },
                    ReferralInfo = new ReferralInfo
                    {
                        ProfileMedService = OptionData.options.ReferralInfo.ProfileMedService
                    }
                };

                var result = mq.GetQueueInfo(cr, options);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

    }
}
