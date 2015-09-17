using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqTests
{
    class TestReferralTarget
    {
        ReferralTarget target;
        List<TestDoctor> docs = new List<TestDoctor>();
        public Array doctors
        {
            get
            {
                if (docs.Count != 0)
                    return docs.ToArray();
                else
                    return null;
            }
        }
        List<TestMainDiagnosis> diag = new List<TestMainDiagnosis>();
        public Array mainDiagnosis
        {
            get
            {
                if (diag.Count != 0)
                    return diag.ToArray();
                else
                    return null;
            }
        }
        TestCoding lpu;
        public TestReferralTarget(ReferralTarget r)
        {
            if (r != null)
                target = r;
            else
                target = new ReferralTarget();
            if (target.Doctors != null)
                foreach(Doctor i in target.Doctors)
                    docs.Add(new TestDoctor(i));
            if (target.MainDiagnosis != null)
                foreach(MainDiagnosis i in target.MainDiagnosis)
                    diag.Add(new TestMainDiagnosis(i));
            if (target.Lpu != null)
                lpu = new TestCoding(target.Lpu);
        }
        
        static public TestReferralTarget BuildTargetFromDataBaseData(string idReferral)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT id_target_lpu_case_mis, is_referral_review_target_mo, reception_appoint_additional_comment, reception_appoint_time_comment, referral_review_date_target_mo, id_target_lpu, reception_appoint_date FROM public.referral WHERE id_referral = '" + idReferral + "' ORDER BY id_referral DESC LIMIT 1";
                NpgsqlCommand person = new NpgsqlCommand(findPatient, connection);
                using (NpgsqlDataReader personFromDataBase = person.ExecuteReader())
                {
                    ReferralTarget p = new ReferralTarget();
                    while (personFromDataBase.Read())
                    {
                        //что делать с DateSpecified и Мисами? 
                        if (personFromDataBase["id_target_lpu_case_mis"] != DBNull.Value)
                            p.IdCaseMis = Convert.ToString(personFromDataBase["id_source_lpu_case_mis"]);
                        if (personFromDataBase["is_referral_review_target_mo"] != DBNull.Value)
                            p.IsReferralReviewed = Convert.ToBoolean(personFromDataBase["is_referral_review_target_mo"]);
                        if (personFromDataBase["reception_appoint_additional_comment"] != DBNull.Value)
                            p.ReceptionAppointComment = Convert.ToString(personFromDataBase["reception_appoint_additional_comment"]);
                        if (personFromDataBase["reception_appoint_time_comment"] != DBNull.Value)
                            p.ReceptionAppointTime = Convert.ToString(personFromDataBase["reception_appoint_time_comment"]);
                        if (personFromDataBase["reception_appoint_date"] != DBNull.Value)
                            p.ReceptionAppointDate = Convert.ToDateTime(personFromDataBase["reception_appoint_date"]);
                        if (personFromDataBase["referral_review_date_target_mo"] != DBNull.Value)
                            p.ReferralReviewDate = Convert.ToDateTime(personFromDataBase["referral_review_date_target_mo"]);
                        TestReferralTarget pers = new TestReferralTarget(p);
                        if (personFromDataBase["id_target_lpu"] != DBNull.Value)
                            pers.lpu = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_target_lpu"]));
                        return pers;
                    }
                }
            }
            return null;
        }

        private void FindMismatch(TestReferralTarget r)
        {
            if (this.target.IdCaseMis != r.target.IdCaseMis)
                Global.errors3.Add("Несовпадение IdCaseMis TestReferralTarget");
            if (this.target.IsReferralReviewed != r.target.IsReferralReviewed)
                Global.errors3.Add("Несовпадение IsReferralReviewed TestReferralTarget");
            if (this.target.ReceptionAppointComment != r.target.ReceptionAppointComment)
                Global.errors3.Add("Несовпадение ReceptionAppointComment TestReferralTarget");
            if (this.target.ReceptionAppointDate != r.target.ReceptionAppointDate)
                Global.errors3.Add("Несовпадение ReceptionAppointDate TestReferralTarget");
            if (this.target.ReceptionAppointTime != r.target.ReceptionAppointTime)
                Global.errors3.Add("Несовпадение ReceptionAppointTime TestReferralTarget");
            if (this.target.ReferralReviewDate != r.target.ReferralReviewDate)
                Global.errors3.Add("Несовпадение ReferralReviewDate TestReferralTarget");
            if (Global.GetLength(this.doctors) != Global.GetLength(r.doctors))
                Global.errors3.Add("Несовпадение длинны documents TestReferralTarget");
            if (Global.GetLength(this.mainDiagnosis) != Global.GetLength(r.mainDiagnosis))
                Global.errors3.Add("Несовпадение длинны mainDiagnosis TestReferralTarget");
            if (Global.GetLength(this.lpu) != Global.GetLength(r.lpu))
                Global.errors3.Add("Несовпадение длинны lpu TestReferralTarget");
        }
        public override bool Equals(Object obj)
        {
            TestReferralTarget p = obj as TestReferralTarget;
            if (p == null)
            {
                return false;
            }
            if ((this.target.IdCaseMis == p.target.IdCaseMis)&&
            (this.target.IsReferralReviewed == p.target.IsReferralReviewed)&&
            (this.target.ReceptionAppointComment == p.target.ReceptionAppointComment)&&
            (this.target.ReceptionAppointDate == p.target.ReceptionAppointDate)&&
            (this.target.ReceptionAppointTime == p.target.ReceptionAppointTime)&&
            (this.target.ReferralReviewDate == p.target.ReferralReviewDate)&&
            (Global.IsEqual(this.lpu, p.lpu))&&
            (Global.IsEqual(this.mainDiagnosis, p.mainDiagnosis))&&
            (Global.IsEqual(this.doctors, p.doctors)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestReferralTarget");
                return false;
            }
        }
        public static bool operator ==(TestReferralTarget a, TestReferralTarget b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestReferralTarget a, TestReferralTarget b)
        {
            return !(a.Equals(b));
        }
    }
}
