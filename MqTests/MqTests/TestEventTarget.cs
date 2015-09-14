using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
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
            target = r ?? new EventTarget();
            caseAidForm = new TestCoding(target.CaseAidForm);
            caseAidPlace = new TestCoding(target.CaseAidPlace);
            caseAidType = new TestCoding(target.CaseAidType);
            lpu = new TestCoding(target.Lpu);
        }

        private TestEventTarget()
        {
            
        }
        static public TestEventTarget BuildTargetFromDataBaseData(string idReferral)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT id_case_aid_form, id_case_aid_place, id_case_aid_type, case_close_date, case_open_date, reception_appoint_date FROM public.referral WHERE id_referral = '" + idReferral + "' ORDER BY id_referral DESC LIMIT 1";
                NpgsqlCommand person = new NpgsqlCommand(findPatient, connection);
                using (NpgsqlDataReader personFromDataBase = person.ExecuteReader())
                {
                    EventTarget p = new EventTarget();
                    while (personFromDataBase.Read())
                    {
                        //что делать с IsReferralReviwedSpecified, Lpu, ReceptionAppointComment, ReceptionAppointTime, ReferralReviewDate и RefferalCreatedDate? 
                        if (personFromDataBase["case_close_date"] != DBNull.Value)
                            p.CaseCloseDate = Convert.ToDateTime(personFromDataBase["case_close_date"]);
                        if (personFromDataBase["case_open_date"] != DBNull.Value)
                            p.CaseOpenDate = Convert.ToDateTime(personFromDataBase["case_open_date"]);
                        if (personFromDataBase["reception_appoint_date"] != DBNull.Value)
                            p.ReceptionAppointDate = Convert.ToDateTime(personFromDataBase["reception_appoint_date"]);
                        TestEventTarget pers = new TestEventTarget(p);
                        if (personFromDataBase["id_case_aid_form"] != DBNull.Value)
                            pers.caseAidForm = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_case_aid_form"]));
                        if (personFromDataBase["id_case_aid_place"] != DBNull.Value)
                            pers.caseAidPlace = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_case_aid_place"]));
                        if (personFromDataBase["id_case_aid_type"] != DBNull.Value)
                            pers.caseAidType = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_case_aid_type"]));
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
            if ((object)p == null)
            {
                return false;
            }
            if (this.target == p.target)
                return true;
            if ((this.target == null) || (p.target == null))
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
            return a.Equals(b);
        }
        public static bool operator !=(TestEventTarget a, TestEventTarget b)
        {
            return !(a.Equals(b));
        }
    }
}
