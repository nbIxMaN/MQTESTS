using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MqTests.WebReference;

namespace MqTests
{
    class TestMqServiceClient : IDisposable
    {
        MqServiceClient client;
        private bool disposed;
        public TestMqServiceClient()
        {
            client = new MqServiceClient();
            disposed = false;
        }
        public MqResult Register(Credentials cr, Referral r)
        {
            MqResult x = client.Register(cr, r);
            if (new TestReferral(r, cr.Organization) != TestReferral.BuildReferralFromDataBaseData(x.IdMq))
            {
                Global.errors1.Add("Несовпадение");
                Global.errors1.AddRange(Global.errors2);
            }
            return x;
        }
        public MqResult UpdateFromSourcedMo(Credentials cr, Referral r)
        {
            MqResult x = client.UpdateFromSourcedMo(cr, r);
            if (new TestReferral(r, cr.Organization) != TestReferral.BuildReferralFromDataBaseData(x.IdMq))
            {
                Global.errors1.Add("Несовпадение");
                Global.errors1.AddRange(Global.errors2);
            }
            return x;
        }
        public MqResult UpdateFromTargetMo(Credentials cr, Referral r)
        {
            MqResult x = client.UpdateFromTargetMo(cr, r);
            if (new TestReferral(r, cr.Organization) != TestReferral.BuildReferralFromDataBaseData(x.IdMq))
            {
                Global.errors1.Add("Несовпадение");
                Global.errors1.AddRange(Global.errors2);
            }
            return x;
        } 
        public MqResult Cancellation(Credentials cr, Referral r)
        {
            TestReferral tr = TestReferral.BuildReferralFromDataBaseData(r.ReferralInfo.IdMq);
            tr.evInfo.cancellation = new TestCancellation(r.EventsInfo.Cancellation);
            MqResult x = client.Cancellation(cr, r);
            if (tr != TestReferral.BuildReferralFromDataBaseData(x.IdMq))
            {
                Global.errors1.Add("Несовпадение");
                Global.errors1.AddRange(Global.errors2);
            }
            return x;
        }
        public MqResult HealthCareEnd(Credentials cr, Referral r)
        {
            MqResult x = client.HealthCareEnd(cr, r);
            if (new TestReferral(r, cr.Organization) != TestReferral.BuildReferralFromDataBaseData(x.IdMq))
            {
                Global.errors1.Add("Несовпадение");
                Global.errors1.AddRange(Global.errors2);
            }
            return x;
        }
        
        public void UpdateMedServiceProfile(Credentials cr, ProfileMedService p)
        {
            client.UpdateMedServiceProfile(cr, p);
            if (new TestProfileMedService(p) != TestProfileMedService.BuildProfileMedServiceFromDataBaseData(cr.Organization))
            {
                Global.errors1.Add("Несовпадение");
                Global.errors1.AddRange(Global.errors2);
            }
        }
        public SearchOneDirectionResult SearchOne(Credentials cr, Options o)
        {
            List<string> s = TestOptions.GetReferralId(o);
            var r = client.SearchOne(cr, o);
            if (s.Count != 1)
                if (r.QLength != 0)
                    Global.errors1.Add("Найдено больше одного совпадения, но SearchOne нашел " + r.QLength.ToString());
                else ;
            else
                if (TestReferral.BuildReferralFromDataBaseData(s[0]) != new TestReferral(r.Referral))
                    Global.errors1.Add("Несовпадение");
            return r;

        }
        ~TestMqServiceClient()
        {
            client.Close();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                client.Close();
                disposed = true;
            }
        }
    }
}
