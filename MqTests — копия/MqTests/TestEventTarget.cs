using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MqTests
{
    class TestEventTarget
    {
        EventTarget target;
        TestCoding caseAidForm;
        TestCoding caseAidPlace;
        TestCoding caseAidType;
        TestCoding lpu;
        public TestEventTarget(EventTarget r)
        {
            if (r != null)
                target = r;
            else
                target = new EventTarget();
            if (target.CaseAidForm != null)
                caseAidForm = new TestCoding(target.CaseAidForm);
            if (target.CaseAidPlace != null)
                caseAidPlace = new TestCoding(target.CaseAidPlace);
            if (target.CaseAidType != null)
                caseAidType = new TestCoding(target.CaseAidType);
            if (target.Lpu != null)
                lpu = new TestCoding(target.Lpu);
        }

        private TestEventTarget()
        {
            
        }

        public void UpdateTestEventTarget(EventTarget e)
        {
            if (e.CaseCloseDate != null)
                this.target.CaseCloseDate = e.CaseCloseDate;
            if (e.CaseOpenDate != null)
                this.target.CaseOpenDate = e.CaseOpenDate;
            if (e.IsReferralReviwed != null)
                this.target.IsReferralReviwed = e.IsReferralReviwed;
            if (e.ReceptionAppointComment != null)
                this.target.ReceptionAppointComment = e.ReceptionAppointComment;
            if (e.ReceptionAppointDate != null)
                this.target.ReceptionAppointDate = e.ReceptionAppointDate;
            if (e.ReceptionAppointTime != null)
                this.target.ReceptionAppointTime = e.ReceptionAppointTime;
            if (e.ReferralReviewDate != null)
                this.target.ReferralReviewDate = e.ReferralReviewDate;
            if (e.RefferalCreatedDate != null)
                this.target.RefferalCreatedDate = e.RefferalCreatedDate;
            if (e.CaseAidForm != null)
                this.caseAidForm = new TestCoding(e.CaseAidForm);
            if (e.CaseAidPlace != null)
                this.caseAidPlace = new TestCoding(e.CaseAidPlace);
            if (e.CaseAidType != null)
                this.caseAidType = new TestCoding(e.CaseAidType);
            if (e.Lpu != null)
                this.lpu = new TestCoding(e.Lpu);
        }
        static public TestEventTarget BuildTargetFromDataBaseData(string idReferral)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT id_case_aid_type, id_case_aid_form, id_case_aid_place, id_target_lpu, is_referral_review_target_mo, case_open_date, case_close_date, referral_review_date_target_mo, reception_appoint_date, reception_appoint_time_comment, reception_appoint_additional_comment, id_target_lpu_case_mis FROM public.referral WHERE id_referral = '" + idReferral + "' ORDER BY id_referral DESC LIMIT 1";
                NpgsqlCommand person = new NpgsqlCommand(findPatient, connection);
                using (NpgsqlDataReader personFromDataBase = person.ExecuteReader())
                {
                    EventTarget p = new EventTarget();
                    while (personFromDataBase.Read())
                    {
                        //что делать с RefferalCreatedDate? 
                        if (personFromDataBase["case_close_date"] != DBNull.Value)
                            p.CaseCloseDate = Convert.ToDateTime(personFromDataBase["case_close_date"]);
                        if (personFromDataBase["case_open_date"] != DBNull.Value)
                            p.CaseOpenDate = Convert.ToDateTime(personFromDataBase["case_open_date"]);
                        if (personFromDataBase["reception_appoint_date"] != DBNull.Value)
                            p.ReceptionAppointDate = Convert.ToDateTime(personFromDataBase["reception_appoint_date"]);
                        if (personFromDataBase["referral_review_date_target_mo"] != DBNull.Value)
                            p.ReferralReviewDate =
                                Convert.ToDateTime(personFromDataBase["referral_review_date_target_mo"]);
                        if (personFromDataBase["is_referral_review_target_mo"] != DBNull.Value)
                            p.IsReferralReviwed = Convert.ToBoolean(personFromDataBase["is_referral_review_target_mo"]);
                        if (personFromDataBase["reception_appoint_time_comment"] != DBNull.Value)
                            p.ReceptionAppointTime = personFromDataBase["reception_appoint_time_comment"].ToString();
                        if (personFromDataBase["reception_appoint_additional_comment"] != DBNull.Value)
                            p.ReceptionAppointComment =
                                personFromDataBase["reception_appoint_additional_comment"].ToString();
                        //id_target_lpu_case_mis;
                        //if (personFromDataBase["id_target_lpu_case_mis"] != DBNull.Value)
                        TestEventTarget pers = new TestEventTarget(p);
                        if (personFromDataBase["id_case_aid_form"] != DBNull.Value)
                            pers.caseAidForm = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_case_aid_form"]));
                        if (personFromDataBase["id_case_aid_place"] != DBNull.Value)
                            pers.caseAidPlace = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_case_aid_place"]));
                        if (personFromDataBase["id_case_aid_type"] != DBNull.Value)
                            pers.caseAidType = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_case_aid_type"]));
                        if (personFromDataBase["id_target_lpu"] != DBNull.Value)
                            pers.lpu = TestCoding.BuildCodingFromDataBaseData(personFromDataBase["id_target_lpu"].ToString());
                        return pers;
                    }
                }
            }
            return null;
        }
        private void FindMismatch(TestEventTarget r)
        {
            if (this.target.CaseCloseDate != r.target.CaseCloseDate)
                Global.errors3.Add("Несовпадение CaseCloseDate TestEventTarget");
            if (this.target.CaseOpenDate != r.target.CaseOpenDate)
                Global.errors3.Add("Несовпадение CaseOpenDate TestEventTarget");
            if (this.target.IsReferralReviwed != r.target.IsReferralReviwed)
                Global.errors3.Add("Несовпадение IsReferralReviwed TestEventTarget");
            if (this.target.ReceptionAppointComment != r.target.ReceptionAppointComment)
                Global.errors3.Add("Несовпадение ReceptionAppointComment TestEventTarget");
            if (this.target.ReceptionAppointDate != r.target.ReceptionAppointDate)
                Global.errors3.Add("Несовпадение ReceptionAppointDate TestEventTarget");
            if (this.target.ReceptionAppointTime != r.target.ReceptionAppointTime)
                Global.errors3.Add("Несовпадение ReceptionAppointTime TestEventTarget");
            if (this.target.ReferralReviewDate != r.target.ReferralReviewDate)
                Global.errors3.Add("Несовпадение ReferralReviewDate TestEventTarget");
            if (this.target.RefferalCreatedDate!= r.target.RefferalCreatedDate)
                Global.errors3.Add("Несовпадение RefferalCreatedDate TestEventTarget");
            if (Global.GetLength(this.caseAidForm) != Global.GetLength(r.caseAidForm))
                Global.errors3.Add("Несовпадение длинны caseAidForm TestEventTarget");
            if (Global.GetLength(this.caseAidPlace) != Global.GetLength(r.caseAidPlace))
                Global.errors3.Add("Несовпадение длинны caseAidPlace TestEventTarget");
            if (Global.GetLength(this.caseAidType) != Global.GetLength(r.caseAidType))
                Global.errors3.Add("Несовпадение длинны caseAidType TestEventTarget");
            if (Global.GetLength(this.lpu) != Global.GetLength(r.lpu))
                Global.errors3.Add("Несовпадение длинны lpu TestEventTarget");
        }
        public override bool Equals(Object obj)
        {
            TestEventTarget p = obj as TestEventTarget;
            if (p == null)
            {
                return false;
            }
            if ((this.target.CaseCloseDate == p.target.CaseCloseDate)&&
            (this.target.CaseOpenDate == p.target.CaseOpenDate)&&
            (this.target.IsReferralReviwed == p.target.IsReferralReviwed)&&
            (this.target.ReceptionAppointComment == p.target.ReceptionAppointComment)&&
            (this.target.ReceptionAppointDate == p.target.ReceptionAppointDate)&&
            (this.target.ReceptionAppointTime == p.target.ReceptionAppointTime)&&
            (this.target.ReferralReviewDate == p.target.ReferralReviewDate)&&
            (this.target.RefferalCreatedDate== p.target.RefferalCreatedDate)&&
            (Global.IsEqual(this.caseAidForm, p.caseAidForm))&&
            (Global.IsEqual(this.caseAidPlace, p.caseAidPlace))&&
            (Global.IsEqual(this.caseAidType, p.caseAidType))&&
            (Global.IsEqual(this.lpu, p.lpu)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestEventTarget");
                return false;
            }
        }
        public static bool operator ==(TestEventTarget a, TestEventTarget b)
        {
            return Equals(a, b);
        }
        public static bool operator !=(TestEventTarget a, TestEventTarget b)
        {
            return !Equals(a, b);
        }
    }
}
