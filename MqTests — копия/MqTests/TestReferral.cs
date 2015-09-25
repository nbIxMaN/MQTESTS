using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MqTests.WebReference;
using Npgsql;

namespace MqTests
{
    class TestReferral
    {
        public TestEventsInfo evInfo;
        public TestPatient patient;
        public TestReferralInfo refInfo;
        public TestReferralSurvey refSurvey;
        public TestReferralSource refSourse;
        public TestReferralTarget refTarget;
        public TestReferral(Referral r, string idLpu = "")
        {
            evInfo = new TestEventsInfo(r.EventsInfo);
            if (r.Patient != null)
                patient = new TestPatient(r.Patient);
            refInfo = new TestReferralInfo(r.ReferralInfo);
            refSurvey = new TestReferralSurvey(r.ReferralSurvey);
            refSourse = new TestReferralSource(r.Source);
            refTarget = new TestReferralTarget(r.Target);
        }

        private TestReferral()
        {
        }

        public void UpdateTestReferral(Referral r, string idLpu = "")
        {
            if (r.EventsInfo != null)
                this.evInfo.UpdateTestEventsInfo(r.EventsInfo);
            if (r.Patient != null)
                this.patient.UpdateTestPatient(r.Patient);
            if (r.ReferralInfo != null)
            {
                this.refInfo.UpdateTestReferralInfo(r.ReferralInfo);
                if ((r.EventsInfo != null) && (r.EventsInfo.Source != null) && (this.refInfo.mqReferralStatus.Code == "1") && (r.EventsInfo.Source.ReferralReviewDate != null) &&
                    (r.EventsInfo.Source.IsReferralReviewed == true))
                    this.refInfo.mqReferralStatus.Code = "2";
                if ((r.EventsInfo != null) && (r.EventsInfo.Source != null) && (r.EventsInfo.Target != null) &&
                    ((this.refInfo.mqReferralStatus.Code == "1") || (this.refInfo.mqReferralStatus.Code == "2")) &&
                    (r.EventsInfo.Source.ReferralOutDate != null) && (r.Target.Lpu != null))
                    this.refInfo.mqReferralStatus.Code = "3";
                if ((r.EventsInfo != null) && (r.EventsInfo.Target != null) &&
                    (this.refInfo.mqReferralStatus.Code == "3") && (r.EventsInfo.Target.ReferralReviewDate != null) &&
                    (r.EventsInfo.Target.IsReferralReviwed == true))
                    this.refInfo.mqReferralStatus.Code = "4";
                if ((r.EventsInfo != null) && (r.EventsInfo.Target != null) &&
                    (r.EventsInfo.Target.ReceptionAppointDate != null))
                    this.refInfo.mqReferralStatus.Code = "5";
                if ((r.EventsInfo != null) && (r.EventsInfo.Target != null) &&
                    (this.refInfo.mqReferralStatus.Code == "5") && (r.EventsInfo.Target.CaseOpenDate != null))
                    this.refInfo.mqReferralStatus.Code = "6";
                if ((r.EventsInfo != null) && (r.EventsInfo.Target != null) &&
                    (this.refInfo.mqReferralStatus.Code == "6") && (r.EventsInfo.Target.CaseCloseDate != null))
                    this.refInfo.mqReferralStatus.Code = "7";
                if ((r.EventsInfo != null) && (r.EventsInfo.Target != null) &&
                    (this.refInfo.mqReferralStatus.Code == "7") && (r.EventsInfo.Target.ReferralReviewDate != null) &&
                    (r.EventsInfo.Target.IsReferralReviwed == false))
                    this.refInfo.mqReferralStatus.Code = "8";
                if ((r.EventsInfo != null) && (r.EventsInfo.Cancellation != null) &&
                    (r.EventsInfo.Cancellation.Date != null) &&
                    ((this.refInfo.mqReferralStatus.Code == "1") || (this.refInfo.mqReferralStatus.Code == "2") ||
                     (this.refInfo.mqReferralStatus.Code == "3") || (this.refInfo.mqReferralStatus.Code == "4") ||
                     (this.refInfo.mqReferralStatus.Code == "5")))
                    this.refInfo.mqReferralStatus.Code = "0";
            }
            if (r.ReferralSurvey != null)
                this.refSurvey.UpdateTestReferralSurvey(r.ReferralSurvey);
            if (r.Source != null)
                this.refSourse.UpdateTestReferralSource(r.Source);
            if (r.Target != null)
                this.refTarget.UpdateTestReferralTarget(r.Target);
        }
        static public TestReferral BuildReferralFromDataBaseData(string idReferral)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findR = "SELECT * FROM public.referral WHERE id_referral = '" + idReferral + "'";
               // string findR = "select id_patient_person from public.referral";
                NpgsqlCommand Rcommand = new NpgsqlCommand(findR, connection);
                using (NpgsqlDataReader RReader = Rcommand.ExecuteReader())
                {
                    while (RReader.Read())
                    {
                        TestReferral r = new TestReferral();
                        r.evInfo = TestEventsInfo.BuildAdditionalFromDataBaseData(idReferral);
                        if (RReader["id_patient_person"] != DBNull.Value)
                            r.patient = TestPatient.BuildPatientFromDataBaseData(RReader["id_patient_person"].ToString(), RReader["id_patient_source_mo_mis"].ToString());
                        r.refInfo = TestReferralInfo.BuildPersonFromDataBaseData(idReferral);
                        r.refSourse = TestReferralSource.BuildSourceFromDataBaseData(idReferral);
                        r.refSurvey = TestReferralSurvey.BuildAdditionalFromDataBaseData(idReferral);
                        r.refTarget = TestReferralTarget.BuildTargetFromDataBaseData(idReferral);
                        return r;
                    }
                }
            }
            return null;
        }
        private void FindMismatch(TestReferral r)
        {
            if (Global.GetLength(this.evInfo) != Global.GetLength(r.evInfo))
                Global.errors2.Add("Несовпадение длинны EventsInfo TestReferral");
            if (Global.GetLength(this.patient) != Global.GetLength(r.patient))
                Global.errors2.Add("Несовпадение длинны TestPatient TestReferral");
            if (Global.GetLength(this.refInfo) != Global.GetLength(r.refInfo))
                Global.errors2.Add("Несовпадение длинны TestReferralInfo TestReferral");
            if (Global.GetLength(this.refSourse) != Global.GetLength(r.refSourse))
                Global.errors2.Add("Несовпадение длинны TestReferralSourse TestReferral");
            if (Global.GetLength(this.refSurvey) != Global.GetLength(r.refSurvey))
                Global.errors2.Add("Несовпадение длинны TestReferralSurvey TestReferral");
            if (Global.GetLength(this.refTarget) != Global.GetLength(r.refTarget))
                Global.errors2.Add("Несовпадение длинны TestReferralTarget TestReferral");
        }
        public override bool Equals(Object obj)
        {
            TestReferral p = obj as TestReferral;
            if (p == null)
                return false;
            if ((Global.IsEqual(this.evInfo, p.evInfo))&&
            (Global.IsEqual(this.patient, p.patient))&&
            (Global.IsEqual(this.refInfo, p.refInfo))&&
            (Global.IsEqual(this.refSourse, p.refSourse))&&
            (Global.IsEqual(this.refSurvey, p.refSurvey))&&
            (Global.IsEqual(this.refTarget, p.refTarget)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors2.Add("несовпадение TestReferral");
                return false;
            }
        }
        public static bool operator ==(TestReferral a, TestReferral b)
        {
            return Equals(a, b);
        }
        public static bool operator !=(TestReferral a, TestReferral b)
        {
            return !(Equals(a, b));
        }
    }
}
