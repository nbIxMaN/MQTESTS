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
                    var result = mq.Register(cr, referral);

                    //if (Global.errors == "")
                    //    Assert.Pass();
                    //else
                    //    Assert.Fail(Global.errors);
                    using (var x = Global.GetSqlConnection())
                    {
                        var s = "SELECT  * " +
                            "FROM    public.referral " +
                            "WHERE id_referral = '" + result.IdMq + "'";

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
                    string s = e.Detail.MqFaults[0].Message;
                }
            }
        }

        [Test]
        public void FullRegister()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Referral referral = (new SetData()).FullRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                try
                {
                    var result = mq.Register(cr, referral);
                }
                catch (FaultException<MqTests.WebReference.MqFault> e)
                {
                    string s = e.Detail.MqFaults[0].Message;
                }

                //if (Global.errors == "")
                //    Assert.Pass();
                //else
                //    Assert.Fail(Global.errors);
            }
        }
    }
}
