using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MqTests
{
    class TestEventSource
    {
        EventSource source;
        public TestEventSource(EventSource r)
        {
            if (r != null)
            {
                source = r;
            }
        }
        static public TestEventSource BuildSourceFromDataBaseData(string idReferral)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                //ReferralCreateDate что с этим делать???
                string findPatient = "SELECT is_referral_review_source_mo, planned_date, referral_out_date, referral_review_date_source_mo FROM public.referral WHERE id_referral = '" + idReferral + "'ORDER BY id_referral DESC LIMIT 1";
                NpgsqlCommand person = new NpgsqlCommand(findPatient, connection);
                using (NpgsqlDataReader personFromDataBase = person.ExecuteReader())
                {
                    EventSource p = new EventSource();
                    while (personFromDataBase.Read())
                    {
                        if (personFromDataBase["is_referral_review_source_mo"].ToString() != "")
                            p.IsReferralReviewed = Convert.ToBoolean(personFromDataBase["is_referral_review_source_mo"]);
                        if (personFromDataBase["planned_date"].ToString() != "")
                            p.PlannedDate = Convert.ToDateTime(personFromDataBase["planned_date"]);
                        if (personFromDataBase["referral_out_date"].ToString() != "")
                            p.ReferralOutDate = Convert.ToDateTime(personFromDataBase["referral_out_date"]);
                        if (personFromDataBase["referral_review_date_source_mo"].ToString() != "")
                            p.ReferralReviewDate = Convert.ToDateTime(personFromDataBase["referral_review_date_source_mo"]);
                        TestEventSource pers = new TestEventSource(p);
                        return pers;
                    }
                }
            }
            return null;
        }
        private void FindMismatch(TestEventSource r)
        {
            if (this.source.IsReferralReviewed != r.source.IsReferralReviewed)
                Global.errors3.Add("Несовпадение IsReferralReviewed TestEventSource");
            if (this.source.IsReferralReviewedSpecified != r.source.IsReferralReviewedSpecified)
                Global.errors3.Add("Несовпадение IsReferralReviewedSpecified TestEventSource");
            if (this.source.PlannedDate != r.source.PlannedDate)
                Global.errors3.Add("Несовпадение PlannedDate TestEventSource");
            if (this.source.PlannedDateSpecified != r.source.PlannedDateSpecified)
                Global.errors3.Add("Несовпадение PlannedDateSpecified TestEventSource");
            if (this.source.ReferralCreateDate != r.source.ReferralCreateDate)
                Global.errors3.Add("Несовпадение ReferralCreateDate TestEventSource");
            if (this.source.ReferralCreateDateSpecified != r.source.ReferralCreateDateSpecified)
                Global.errors3.Add("Несовпадение ReferralCreateDateSpecified TestEventSource");
            if (this.source.ReferralOutDate != r.source.ReferralOutDate)
                Global.errors3.Add("Несовпадение ReferralOutDate TestEventSource");
            if (this.source.ReferralOutDateSpecified != r.source.ReferralOutDateSpecified)
                Global.errors3.Add("Несовпадение ReferralOutDateSpecified TestEventSource");
            if (this.source.ReferralReviewDate != r.source.ReferralReviewDate)
                Global.errors3.Add("Несовпадение ReferralReviewDate TestEventSource");
            if (this.source.ReferralReviewDateSpecified!= r.source.ReferralReviewDateSpecified)
                Global.errors3.Add("Несовпадение ReferralReviewDateSpecified TestEventSource");
        }
        public override bool Equals(Object obj)
        {
            TestEventSource p = obj as TestEventSource;
            if ((object)p == null)
            {
                return false;
            }
            if (this.source == p.source)
                return true;
            if ((this.source == null) || (p.source == null))
            {
                return false;
            }
            if ((this.source.IsReferralReviewed == p.source.IsReferralReviewed)&&
            (this.source.IsReferralReviewedSpecified == p.source.IsReferralReviewedSpecified)&&
            (this.source.PlannedDate == p.source.PlannedDate)&&
            (this.source.PlannedDateSpecified == p.source.PlannedDateSpecified)&&
            (this.source.ReferralCreateDate == p.source.ReferralCreateDate)&&
            (this.source.ReferralCreateDateSpecified == p.source.ReferralCreateDateSpecified)&&
            (this.source.ReferralOutDate == p.source.ReferralOutDate)&&
            (this.source.ReferralOutDateSpecified == p.source.ReferralOutDateSpecified)&&
            (this.source.ReferralReviewDate == p.source.ReferralReviewDate)&&
            (this.source.ReferralReviewDateSpecified== p.source.ReferralReviewDateSpecified))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestEventSource");
                return false;
            }
        }
        public static bool operator ==(TestEventSource a, TestEventSource b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestEventSource a, TestEventSource b)
        {
            return !(a.Equals(b));
        }
    }
}
