using MqTests.WebReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqTests
{
    class TestEventsInfo
    {
        EventsInfo info;
        public TestCancellation cancellation;
        TestEventSource source;
        TestEventTarget target;
        public TestEventsInfo(EventsInfo r)
        {
            if (r != null)
                info = r;
            else
                info = new EventsInfo();
            cancellation = new TestCancellation(info.Cancellation);
            source = new TestEventSource(info.Source);
            target = new TestEventTarget(info.Target);
        }

        private TestEventsInfo()
        {
        }

        static public TestEventsInfo BuildAdditionalFromDataBaseData(string idReferral)
        {
            TestEventsInfo p = new TestEventsInfo();
            p.cancellation = TestCancellation.BuildCancellationFromDataBaseData(idReferral);
            p.source = TestEventSource.BuildSourceFromDataBaseData(idReferral);
            p.target = TestEventTarget.BuildTargetFromDataBaseData(idReferral);
            return p;
        }

        internal void UpdateTestEventsInfo(EventsInfo eventsInfo)
        {
            if (eventsInfo.Cancellation != null)
                cancellation = new TestCancellation(info.Cancellation);
            if (eventsInfo.Source != null)
                source.UpdateTestEventSource(eventsInfo.Source);
            if (eventsInfo.Target != null)
                target.UpdateTestEventTarget(eventsInfo.Target);
        }

        private void FindMismatch(TestEventsInfo r)
        {
            if (Global.GetLength(this.cancellation) != Global.GetLength(r.cancellation))
                Global.errors3.Add("Несовпадение длины cancellation TestEventsInfo");
            if (Global.GetLength(this.source) != Global.GetLength(r.source))
                Global.errors3.Add("Несовпадение длины source TestEventsInfo");
            if (Global.GetLength(this.target) != Global.GetLength(r.target))
                Global.errors3.Add("Несовпадение длины target TestEventsInfo");
        }
        public override bool Equals(Object obj)
        {
            TestEventsInfo p = obj as TestEventsInfo;
            if (p == null)
                return false;
            if ((Global.IsEqual(this.cancellation, p.cancellation)) &&
            (Global.IsEqual(this.source, p.source)) &&
            (Global.IsEqual(this.target, p.target)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestEventsInfo");
                return false;
            }
        }
        public static bool operator ==(TestEventsInfo a, TestEventsInfo b)
        {
            return Equals(a, b);
        }
        public static bool operator !=(TestEventsInfo a, TestEventsInfo b)
        {
            return !Equals(a, b);
        }
    }
}
