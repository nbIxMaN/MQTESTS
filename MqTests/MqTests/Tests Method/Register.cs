using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;
using System.ServiceModel;

namespace MqTests
{
    [TestFixture]
    public class Register : Data
    {
        [Test]
        public void MinRegister()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                try
                {
                    var num = mq.Register(cr, referral);
                    //if (Global.errors == "")
                    //    Assert.Pass();
                    //else
                    //    Assert.Fail(Global.errors);
                    using (var x = Global.GetSqlConnection())
                    {
                        var s = "SELECT  * " +
                            "FROM    public.referral " +
                            "WHERE id_referral = '" + num.IdMq + "'";

                        NpgsqlCommand c = new NpgsqlCommand(s, x);
                        using (var reader = c.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var r = reader.GetValue(0);
                            }
                        }
                    }
                }
                catch (FaultException<MqTests.WebReference.MqFault> e)
                {
                    string r = e.Detail.MqFaults[0].Message;
                }
            }
        }

        [Test]
        public void FullRegister()
        {
            using (MqServiceClient mq = new MqServiceClient())
            {
                Referral referral = (new SetData()).FullRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                mq.Register(cr, referral);
                if (Global.errors == "")
                    Assert.Pass();
                else
                    Assert.Fail(Global.errors);
            }
        }


    }
}
