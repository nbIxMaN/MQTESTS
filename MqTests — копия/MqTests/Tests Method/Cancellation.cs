﻿using System;
using MqTests.WebReference;
using NUnit.Framework;
using Npgsql;
using System.ServiceModel;

namespace MqTests.Tests_Method
{
    [TestFixture]
    public class Cancellation : Data
    {
        [Test]
        public void MinCancellation_AfterRegister()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                referral = (new SetData()).MinCancellation(result.IdMq);
                var resultCancel = mq.Cancellation(cr, referral);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void FullCancellation_AfterRegister()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);
               
                referral = (new SetData()).FullCancellation(result.IdMq);
                var resultCancel = mq.Cancellation(cr, referral);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void MinCancellation_AfterUpdate()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                referral = (new SetData()).MinUpdateFromSourcedMo(result.IdMq);
                var res2 = mq.UpdateFromSourcedMo(cr, referral);

                referral = (new SetData()).MinCancellation(result.IdMq);
                var resultCancel = mq.Cancellation(cr, referral);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }

        [Test]
        public void MinCancellation_AfterPatientDocIssue()
        {
            using (TestMqServiceClient mq = new TestMqServiceClient())
            {
                Credentials cr = new Credentials { Organization = idLpu, Token = guid };
                Referral referral = (new SetData()).MinRegister();
                var result = mq.Register(cr, referral);

                referral = (new SetData()).MinPatientDocumentIssue(result.IdMq);
                var res2 = mq.PatientDocumentIssue(cr, referral);

                referral = (new SetData()).MinCancellation(result.IdMq);
                var resultCancel = mq.Cancellation(cr, referral);
            }

            if (Global.errors == "")
                Assert.Pass();
            else
                Assert.Fail(Global.errors);
        }
    }
}
