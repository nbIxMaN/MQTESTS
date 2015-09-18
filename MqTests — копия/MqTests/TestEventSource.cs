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
                source = r;
            else
                source = new EventSource();
        }

        private TestEventSource()
        {
            
        }
        static public TestEventSource BuildSourceFromDataBaseData(string idReferral)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                //ReferralCreateDate что с этим делать???
                string findPatient = "SELECT is_referral_review_source_mo, referral_creation_date, planned_date, referral_out_date, referral_review_date_source_mo FROM public.referral WHERE id_referral = '" + idReferral + "'ORDER BY id_referral DESC LIMIT 1";
                NpgsqlCommand person = new NpgsqlCommand(findPatient, connection);
                using (NpgsqlDataReader personFromDataBase = person.ExecuteReader())
                {
                    EventSource p = new EventSource();
                    while (personFromDataBase.Read())
                    {
                        if (personFromDataBase["is_referral_review_source_mo"] != DBNull.Value)
                            p.IsReferralReviewed = Convert.ToBoolean(personFromDataBase["is_referral_review_source_mo"]);
                        if (personFromDataBase["referral_creation_date"] != DBNull.Value)
                            p.ReferralCreateDate = Convert.ToDateTime(personFromDataBase["referral_creation_date"]);
                        if (personFromDataBase["planned_date"] != DBNull.Value)
                            p.PlannedDate = Convert.ToDateTime(personFromDataBase["planned_date"]);
                        if (personFromDataBase["referral_out_date"] != DBNull.Value)
                            p.ReferralOutDate = Convert.ToDateTime(personFromDataBase["referral_out_date"]);
                        if (personFromDataBase["referral_review_date_source_mo"] != DBNull.Value)
                            p.ReferralReviewDate = Convert.ToDateTime(personFromDataBase["referral_review_date_source_mo"]);
                        TestEventSource pers = new TestEventSource(p);
                        return pers;
                    }
                }
            }
            return null;
        }

        internal void UpdateTestEventSource(EventSource s)
        {
            if (s.IsReferralReviewed != null)
                this.source.IsReferralReviewed = s.IsReferralReviewed;
            if (s.PlannedDate != null)
                this.source.PlannedDate= s.PlannedDate;
            if (s.ReferralCreateDate != null)
                this.source.ReferralCreateDate = s.ReferralCreateDate;
            if (s.ReferralOutDate != null)
                this.source.ReferralOutDate= s.ReferralOutDate;
            if (s.ReferralReviewDate!= null)
                this.source.ReferralReviewDate = s.ReferralReviewDate;
        }

        private void FindMismatch(TestEventSource r)
        {
            if (this.source.IsReferralReviewed != r.source.IsReferralReviewed)
                Global.errors3.Add("Несовпадение IsReferralReviewed TestEventSource");
            if (this.source.PlannedDate != r.source.PlannedDate)
                Global.errors3.Add("Несовпадение PlannedDate TestEventSource");
            if (this.source.ReferralCreateDate != r.source.ReferralCreateDate)
                Global.errors3.Add("Несовпадение ReferralCreateDate TestEventSource");
            if (this.source.ReferralOutDate != r.source.ReferralOutDate)
                Global.errors3.Add("Несовпадение ReferralOutDate TestEventSource");
            if (this.source.ReferralReviewDate != r.source.ReferralReviewDate)
                Global.errors3.Add("Несовпадение ReferralReviewDate TestEventSource");
        }
        public override bool Equals(Object obj)
        {
            TestEventSource p = obj as TestEventSource;
            if (p == null)
            {
                return false;
            }
            if ((this.source.IsReferralReviewed == p.source.IsReferralReviewed)&&
            (this.source.PlannedDate == p.source.PlannedDate)&&
            (this.source.ReferralCreateDate == p.source.ReferralCreateDate)&&
            (this.source.ReferralOutDate == p.source.ReferralOutDate)&&
            (this.source.ReferralReviewDate == p.source.ReferralReviewDate))
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
            return Equals(a, b);
        }
        public static bool operator !=(TestEventSource a, TestEventSource b)
        {
            return !Equals(a, b);
        }
    }
}
