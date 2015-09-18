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

        //тут должны быть какая-то ошибка
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
                Coding cod = new Coding { Code = "1", System = Dictionary.REFERRAL_STATUS, Version = "1" };
                Options opt = (new SetData()).GetRefferalReturnOptions_SearchMany(referral, null, cod);
                var res2 = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
