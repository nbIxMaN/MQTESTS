using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqTests
{
    class TestCancellation
    {
        CancellationData cancellation;
        TestCoding source;
        TestCoding reason;
        public TestCancellation(CancellationData r)
        {
            cancellation = r ?? new CancellationData();
            source = new TestCoding(cancellation.CancellationSource);
            reason = new TestCoding(cancellation.CancellationReason);
        }

        private TestCancellation()
        {
            
        }
        //как искать?
        static public TestCancellation BuildCancellationFromDataBaseData(string idReferral)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT cancellation_date, cancellation_reason_comment, id_cancellation_code, id_cancellation_reason FROM public.referral WHERE id_referral = '" + idReferral + "' ORDER BY id_referral DESC LIMIT 1";
                NpgsqlCommand person = new NpgsqlCommand(findPatient, connection);
                using (NpgsqlDataReader personFromDataBase = person.ExecuteReader())
                {
                    CancellationData p = new CancellationData();
                    while (personFromDataBase.Read())
                    {
                        //что делать с DateSpecified и Мисами? 
                        if (personFromDataBase["cancellation_date"] != DBNull.Value)
                            p.Date = Convert.ToDateTime(personFromDataBase["cancellation_date"]);
                        if (personFromDataBase["cancellation_reason_comment"] != DBNull.Value)
                            p.ReasonComment = Convert.ToString(personFromDataBase["cancellation_reason_comment"]);
                        TestCancellation pers = new TestCancellation(p);
                        if (personFromDataBase["id_cancellation_code"] != DBNull.Value)
                            pers.source = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_cancellation_code"]));
                        if (personFromDataBase["id_cancellation_reason"] != DBNull.Value)
                            pers.source = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_cancellation_reason"]));
                        return pers;
                    }
                }
            }
            return null;
        }
        private void FindMismatch(TestCancellation r)
        {
            if (this.cancellation.Date != r.cancellation.Date)
                Global.errors3.Add("Несовпадение Date TestCacellation");
            if (this.cancellation.ReasonComment != r.cancellation.ReasonComment)
                Global.errors3.Add("Несовпадение Date TestCacellation");
            if (Global.GetLength(this.reason) != Global.GetLength(r.reason))
                Global.errors3.Add("Несовпадение длинны reason TestCacellation");
            if (Global.GetLength(this.source) != Global.GetLength(r.source))
                Global.errors3.Add("Несовпадение длинны source TestCacellation");
        }
        public override bool Equals(Object obj)
        {
            TestCancellation p = obj as TestCancellation;
            if ((object)p == null)
            {
                return false;
            }
            if (this.cancellation == p.cancellation)
                return true;
            if ((this.cancellation == null) || (p.cancellation == null))
            {
                return false;
            }
            if ((this.cancellation.Date == p.cancellation.Date)&&
            (this.cancellation.ReasonComment == p.cancellation.ReasonComment)&&
            (Global.Equals(this.reason, p.reason))&&
            (Global.Equals(this.source, p.source)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestCacellation");
                return false;
            }
        }
        public static bool operator ==(TestCancellation a, TestCancellation b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestCancellation a, TestCancellation b)
        {
            return !(a.Equals(b));
        }
    }
}
