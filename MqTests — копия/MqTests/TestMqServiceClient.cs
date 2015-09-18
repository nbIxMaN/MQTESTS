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
        public void getErrors(object obj)
        {
            MqFault error = obj as MqFault;
            if (error != null)
            {
                Array errors = error.MqFaults as Array;
                if (errors != null)
                {
                    foreach (MqFault i in errors)
                    {
                        Global.errors1.Add(i.PropertyName + " - " + i.Message);
                        getErrors(i.MqFaults);
                    }
                }
            }
        }
        public MqResult Register(Credentials cr, Referral r)
        {
            try
            {
                MqResult x = client.Register(cr, r);
                r.ReferralInfo.MqReferralStatus = x.MqReferralStatus;
                r.ReferralInfo.IdMq = x.IdMq;
                if (!new TestReferral(r, cr.Organization).Equals(TestReferral.BuildReferralFromDataBaseData(x.IdMq)))
                {
                    Global.errors1.Add("Несовпадение");
                    Global.errors1.AddRange(Global.errors2);
                }
                return x;
            }
            catch (System.ServiceModel.FaultException<MqTests.WebReference.MqFault> e)
            {
                getErrors(e.Detail);
                Global.errors1.Add("ЭКСЕПШН");
                return null;
            }
        }
        public MqResult UpdateFromSourcedMo(Credentials cr, Referral r)
        {
            try
            {
                MqResult x = client.UpdateFromSourcedMo(cr, r);
                if (new TestReferral(r, cr.Organization) != TestReferral.BuildReferralFromDataBaseData(x.IdMq))
                {
                    Global.errors1.Add("Несовпадение");
                    Global.errors1.AddRange(Global.errors2);
                }
                return x;
            }
            catch (System.ServiceModel.FaultException<MqTests.WebReference.MqFault> e)
            {
                getErrors(e.Detail);
                Global.errors1.Add("ЭКСЕПШН");
                return null;
            }
        }
        public MqResult UpdateFromTargetMo(Credentials cr, Referral r)
        {
            try
            {
                MqResult x = client.UpdateFromTargetMo(cr, r);
                if (new TestReferral(r, cr.Organization) != TestReferral.BuildReferralFromDataBaseData(x.IdMq))
                {
                    Global.errors1.Add("Несовпадение");
                    Global.errors1.AddRange(Global.errors2);
                }
                return x;
            }
            catch (System.ServiceModel.FaultException<MqTests.WebReference.MqFault> e)
            {
                getErrors(e.Detail);
                Global.errors1.Add("ЭКСЕПШН");
                return null;
            }
        } 
        public MqResult Cancellation(Credentials cr, Referral r)
        {
            try
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
            catch (System.ServiceModel.FaultException<MqTests.WebReference.MqFault> e)
            {
                getErrors(e.Detail);
                Global.errors1.Add("ЭКСЕПШН");
                return null;
            }
        }
        public MqResult HealthCareEnd(Credentials cr, Referral r)
        {
            try
            {
                MqResult x = client.HealthCareEnd(cr, r);
                if (new TestReferral(r, cr.Organization) != TestReferral.BuildReferralFromDataBaseData(x.IdMq))
                {
                    Global.errors1.Add("Несовпадение");
                    Global.errors1.AddRange(Global.errors2);
                }
                return x;
            }
            catch (System.ServiceModel.FaultException<MqTests.WebReference.MqFault> e)
            {
                getErrors(e.Detail);
                Global.errors1.Add("ЭКСЕПШН");
                return null;
            }
        }
        
        public void UpdateMedServiceProfile(Credentials cr, ProfileMedService p)
        {
            try
            {
                client.UpdateMedServiceProfile(cr, p);
                if (new TestProfileMedService(p) !=
                    TestProfileMedService.BuildProfileMedServiceFromDataBaseData(cr.Organization))
                {
                    Global.errors1.Add("Несовпадение");
                    Global.errors1.AddRange(Global.errors2);
                }
            }
            catch (System.ServiceModel.FaultException<MqTests.WebReference.MqFault> e)
            {
                getErrors(e.Detail);
                Global.errors1.Add("ЭКСЕПШН");
            }
        }
        public SearchOneDirectionResult SearchOne(Credentials cr, Options o)
        {
            try
            {
                List<string> s = TestOptions.GetReferralId(o);
                var r = client.SearchOne(cr, o);
                if (s.Count != 1)
                    if (r.QLength != 0)
                        Global.errors1.Add("Найдено больше одного совпадения, но SearchOne нашел " +
                                           r.QLength.ToString());
                    else ;
                else if (!TestReferral.BuildReferralFromDataBaseData(s[0]).Equals(new TestReferral(r.Referral)))
                {
                    Global.errors1.AddRange(Global.errors2);
                    Global.errors1.Add("Несовпадение");
                }
                return r;
            }
            catch (System.ServiceModel.FaultException<MqTests.WebReference.MqFault> e)
            {
                getErrors(e.Detail);
                Global.errors1.Add("ЭКСЕПШН");
                return null;
            }
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
