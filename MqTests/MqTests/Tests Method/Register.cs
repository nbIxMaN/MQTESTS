﻿using System;
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
                var result = mq.Register(cr, referral);
                if (Global.errors == "")
                    Assert.Pass();
                else
                    Assert.Fail(Global.errors);
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
