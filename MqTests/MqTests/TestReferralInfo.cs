using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MqTests
{
    class TestReferralInfo
    {
        ReferralInfo info;
        TestCancellation cancellation;
        TestCoding mqReferralStatus;
        TestCoding profileMedService;
        TestCoding referralType;
        public TestReferralInfo(ReferralInfo r)
        {
            if (r != null)
            {
                info = r;
                cancellation = new TestCancellation(r.Cancellation);
                mqReferralStatus = new TestCoding(r.MqReferralStatus);
                profileMedService = new TestCoding(r.ProfileMedService);
                referralType = new TestCoding(r.ReferralType);
            }
        }
        static public TestReferralInfo BuildPersonFromDataBaseData(string idReferral)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT comment, priority_comment, referral_paper_date, id_referral_type, id_profile_med_service, referral_reason FROM public.referral WHERE id_referral = '" + idReferral + "' ORDER BY id_referral DESC LIMIT 1";
                NpgsqlCommand person = new NpgsqlCommand(findPatient, connection);
                using (NpgsqlDataReader personFromDataBase = person.ExecuteReader())
                {
                    ReferralInfo p = new ReferralInfo();
                    while (personFromDataBase.Read())
                    {
                        //что делать с mqReferralStatus? 
                        if (personFromDataBase["comment"].ToString() != "")
                            p.Comment = Convert.ToString(personFromDataBase["comment"]);
                        if (personFromDataBase["referral_paper_date"].ToString() != "")
                            p.Date = Convert.ToDateTime(personFromDataBase["referral_paper_date"]);
                        if (personFromDataBase["priority_comment"].ToString() != "")
                            p.Priority = Convert.ToString(personFromDataBase["priority_comment"]);
                        if (personFromDataBase["referral_reason"].ToString() != "")
                            p.Reason = Convert.ToString(personFromDataBase["referral_reason"]);
                        TestReferralInfo pers = new TestReferralInfo(p);
                        pers.cancellation = TestCancellation.BuildCancellationFromDataBaseData(idReferral);
                        if (personFromDataBase["id_referral_type"].ToString() != "")
                            pers.referralType = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_referral_type"]));
                        if (personFromDataBase["id_profile_med_service"].ToString() != "")
                            pers.profileMedService = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_profile_med_service"]));
                        return pers;
                    }
                }
            }
            return null;
        }
        private void FindMismatch(TestReferralInfo r)
        {
            if (this.info.Comment != r.info.Comment)
                Global.errors3.Add("Несовпадение Comment TestReferralInfo");
            if (this.info.Date != r.info.Date)
                Global.errors3.Add("Несовпадение Date TestReferralInfo");
            if (this.info.DateSpecified != r.info.DateSpecified)
                Global.errors3.Add("Несовпадение DateSpecified TestReferralInfo");
            if (this.info.IdMq != r.info.IdMq)
                Global.errors3.Add("Несовпадение IdMq TestReferralInfo");
            if (this.info.Priority != r.info.Priority)
                Global.errors3.Add("Несовпадение Priority TestReferralInfo");
            if (this.info.Reason != r.info.Reason)
                Global.errors3.Add("Несовпадение Reason TestReferralInfo");
            if (Global.GetLength(this.cancellation) != Global.GetLength(r.cancellation))
                Global.errors3.Add("Несовпадение длинны cancellation TestReferralInfo");
            if (Global.GetLength(this.mqReferralStatus) != Global.GetLength(r.mqReferralStatus))
                Global.errors3.Add("Несовпадение длинны mqReferralStatus TestReferralInfo");
            if (Global.GetLength(this.profileMedService) != Global.GetLength(r.profileMedService))
                Global.errors3.Add("Несовпадение длинны profileMedService TestReferralInfo");
            if (Global.GetLength(this.referralType) != Global.GetLength(r.referralType))
                Global.errors3.Add("Несовпадение длинны referralType TestReferralInfo");
        }
        public override bool Equals(Object obj)
        {
            TestReferralInfo p = obj as TestReferralInfo;
            if ((object)p == null)
            {
                return false;
            }
            if (this.info == p.info)
                return true;
            if ((this.info == null) || (p.info == null))
            {
                return false;
            }
            if ((this.info.Comment == p.info.Comment)&&
            (this.info.Date == p.info.Date)&&
            (this.info.DateSpecified == p.info.DateSpecified)&&
            (this.info.IdMq == p.info.IdMq)&&
            (this.info.Priority == p.info.Priority)&&
            (this.info.Reason == p.info.Reason)&&
            (Global.IsEqual(this.cancellation, p.cancellation))&&
            (Global.IsEqual(this.mqReferralStatus, p.mqReferralStatus))&&
            (Global.IsEqual(this.profileMedService, p.profileMedService))&&
            (Global.IsEqual(this.referralType, p.referralType)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestReferralInfo");
                return false;
            }
        }
        public static bool operator ==(TestReferralInfo a, TestReferralInfo b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestReferralInfo a, TestReferralInfo b)
        {
            return !(a.Equals(b));
        }
    }
}
