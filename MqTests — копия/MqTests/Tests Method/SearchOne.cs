using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;

namespace MqTests.Tests_Method
{
    [TestFixture]
    class SearchOne : Data
    {
        //происходит проверка найденного направления?
        //ещё продумать тесты!

        //тут должна быть какая-то ошибка
        [Test]
        public void SearchOne_Empty()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Options opt = new Options();
                var res = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void SearchOne_Test()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                //Регистрируем один случай. Статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).SetStatus_RegisterMin();
                var result = mq.Register(cr, referral);

                //Задаём Options по зарегистрированному направлению и ищем его
                Options opt = (new SetData()).GetRefferalReturnOptions_SearchOne(referral, null, result.IdMq, result.MqReferralStatus);
                var res2 = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void SearchOne_IdMq()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                //Регистрируем один случай. Статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).SetStatus_RegisterMin();
                var result = mq.Register(cr, referral);

                //Задаём IdMq, находим его
                Options opt = new Options();
                opt.IdMq = result.IdMq;
                var res2 = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void SearchOne_Person()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                //Регистрируем один случай. Статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).SetStatus_RegisterMin();
                var result = mq.Register(cr, referral);

                //Задаём Patinet/Person 
                Options opt = new Options
                {
                    Patient = new Patient
                    {
                        Person = new Person
                        {
                            BirthDate = referral.Patient.Person.BirthDate,
                            IdPatientMis = referral.Patient.Person.IdPatientMis
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
        public void SearchOne_Full()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                //Регистрируем один случай. Статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).FullRegister();
                var result = mq.Register(cr, referral);

                //Задаём Options по зарегистрированному направлению и ищем его
                Coding cod = new Coding { Code = "1", System = Dictionary.REFERRAL_STATUS, Version = "1" };
                Options opt = (new SetData()).GetRefferalReturnOptions_SearchOne(referral, referral.Patient.Privileges, result.IdMq, result.MqReferralStatus);
                var res2 = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }


        //проверяем, что после обновления возвращаются актуальные данные, а не старые
        [Test]
        public void SearchOne_CheckRelevance()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                //Регистрируем один случай. Статус направления "Зарегистрировано в РЕГИЗ.УО"
                Referral referral = (new SetData()).FullRegister();
                var result = mq.Register(cr, referral);

                referral = (new SetData()).FullUpdateFromSourcedMo(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                //Задаём Options по зарегистрированному направлению и ищем его
                Options opt = (new SetData()).GetRefferalReturnOptions_SearchOne(referral, referral.Patient.Privileges, result.IdMq, res2.MqReferralStatus);
                var res3 = mq.SearchOne(cr, opt);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
