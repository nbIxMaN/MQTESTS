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
