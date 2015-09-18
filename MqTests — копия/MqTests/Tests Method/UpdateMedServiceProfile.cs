using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;

namespace MqTests.Tests_Method
{
    [TestFixture]
    class UpdateMedServiceProfile : Data
    {
        [Test]
        public void MinUpdateMedServiceProfile()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                ProfileMedService medServ = (new SetData()).MinUpdateMedServiceProfile();
                mq.UpdateMedServiceProfile(cr, medServ);

                Options options = new Options
                {
                    DateReport = OptionData.options.DateReport,
                    Target = new ReferralTarget
                    {
                        Lpu = new Coding { Code = idLpu, System = Dictionary.MO, Version = "1" }
                    },
                    ReferralInfo = new ReferralInfo { ProfileMedService = medServ.IdProfileMedService }
                };

                var result = mq.GetQueueInfo(cr, options);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void FullUpdateMedServiceProfile()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                ProfileMedService medServ = (new SetData()).FullUpdateMedServiceProfile();
                mq.UpdateMedServiceProfile(cr, medServ);

                Options options = new Options
                {
                    DateReport = OptionData.options.DateReport,
                    Target = new ReferralTarget
                    {
                        Lpu = new Coding { Code = idLpu, System = Dictionary.MO, Version = "1" }
                    },
                    ReferralInfo = new ReferralInfo { ProfileMedService = medServ.IdProfileMedService }
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
