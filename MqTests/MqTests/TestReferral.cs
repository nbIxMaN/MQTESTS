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
        public Referral referral;
        public TestEventsInfo evInfo;
        public TestPatient patient;
        public TestReferralInfo refInfo;
        public TestReferralSurvey refSurvey;
        public TestReferralSource refSourse;
        public TestReferralTarget refTarget;
        public TestReferral(Referral r, string idLpu = "")
        {
            if (r != null)
            {
                referral = r;
                evInfo = new TestEventsInfo(r.EventsInfo);
                patient = new TestPatient(r.Patient);
                refInfo = new TestReferralInfo(r.ReferralInfo);
                refSurvey = new TestReferralSurvey(r.ReferralSurvey);
                refSourse = new TestReferralSource(r.Source);
                refTarget = new TestReferralTarget(r.Target);
            }
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
                        TestReferral r = new TestReferral(new Referral());
                        r.evInfo = TestEventsInfo.BuildAdditionalFromDataBaseData(idReferral);
                        if (RReader["id_patient_person"].ToString() != "")
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
                Global.errors3.Add("Несовпадение длинны EventsInfo TestReferral");
            if (Global.GetLength(this.patient) != Global.GetLength(r.patient))
                Global.errors3.Add("Несовпадение длинны TestPatient TestReferral");
            if (Global.GetLength(this.refInfo) != Global.GetLength(r.refInfo))
                Global.errors3.Add("Несовпадение длинны TestReferralInfo TestReferral");
            if (Global.GetLength(this.refSourse) != Global.GetLength(r.refSourse))
                Global.errors3.Add("Несовпадение длинны TestReferralSourse TestReferral");
            if (Global.GetLength(this.refSurvey) != Global.GetLength(r.refSurvey))
                Global.errors3.Add("Несовпадение длинны TestReferralSurvey TestReferral");
            if (Global.GetLength(this.refTarget) != Global.GetLength(r.refTarget))
                Global.errors3.Add("Несовпадение длинны TestReferralTarget TestReferral");
        }
        public override bool Equals(Object obj)
        {
            TestReferral p = obj as TestReferral;
            if ((object)p == null)
            {
                return false;
            }
            if (this.referral == p.referral)
                return true;
            if ((this.referral == null) || (p.referral == null))
            {
                return false;
            }
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
                Global.errors3.Add("несовпадение TestReferral");
                return false;
            }
        }
        public static bool operator ==(TestReferral a, TestReferral b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestReferral a, TestReferral b)
        {
            return !(a.Equals(b));
        }
    }
}
