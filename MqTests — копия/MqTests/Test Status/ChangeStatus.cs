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
                Referral referral = (new SetData()).SetStatusRegisterMin();
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

        //Задаём статус "Согласовано в направляющей МО"
        public void StatusAgreedInSourcedMO()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                //Задаём статус "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).SetStatusRegisterMin();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var result = mq.Register(cr, referral);

                //Задаём статус "Согласовано в направляющей МО"
                // с помощью метода UpdateFromSourcedMo()
                referral = (new SetData()).SetStatusAgreedInSourcedMO_UpdateFromSourcedMo(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                if (res2.MqReferralStatus.Code != "Согласовано в направляющей МО")
                    Global.errors1.Add("Неверный статус:" + result.MqReferralStatus.Code + "");
            }
            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
