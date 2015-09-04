using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;


namespace MqTests
{
    [TestFixture]
    public class Register : Data
    {
        [Test]
        public void MinRegister()
        {
            using (mq)
            {
                Referral referral = (new SetData()).MinRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                var num = mq.Register(cr, referral);
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
        }

        [Test]
        public void FullRegister()
        {
            using (mq)
            {
                Referral referral = (new SetData()).FullRegister();
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                mq.Register(cr, referral);
            }
        }

        
    }
}
