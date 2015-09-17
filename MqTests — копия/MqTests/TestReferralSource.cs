using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MqTests
{
    class TestReferralSource
    {
        ReferralSource sourse;
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
        public TestReferralSource(ReferralSource r)
        {
            if (r != null)
                sourse = r;
            else
                sourse = new ReferralSource();
            if (sourse.Doctors != null)
                foreach (Doctor i in sourse.Doctors)
                    docs.Add(new TestDoctor(i));
            if (sourse.MainDiagnosis != null)
                foreach (MainDiagnosis i in sourse.MainDiagnosis)
                    diag.Add(new TestMainDiagnosis(i));
            if (sourse.Lpu != null)
                lpu = new TestCoding(sourse.Lpu);
        }
        static public TestReferralSource BuildSourceFromDataBaseData(string idReferral)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT id_source_lpu_case_mis, id_referral_source_mo_mis, id_source_lpu FROM public.referral WHERE id_referral = '" + idReferral + "' ORDER BY id_referral DESC LIMIT 1";
                NpgsqlCommand person = new NpgsqlCommand(findPatient, connection);
                using (NpgsqlDataReader personFromDataBase = person.ExecuteReader())
                {
                    ReferralSource p = new ReferralSource();
                    while (personFromDataBase.Read())
                    {
                        //DOCTORS!
                        if (personFromDataBase["id_source_lpu_case_mis"] != DBNull.Value)
                            p.IdCaseMis = Convert.ToString(personFromDataBase["id_source_lpu_case_mis"]);
                        if (personFromDataBase["id_referral_source_mo_mis"] != DBNull.Value)
                            p.IdReferralMis = Convert.ToString(personFromDataBase["id_referral_source_mo_mis"]);
                        TestReferralSource pers = new TestReferralSource(p);
                        if (personFromDataBase["id_source_lpu"] != DBNull.Value)
                            pers.lpu = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_source_lpu"]));
                        pers.docs = TestDoctor.BuildDoctorFromDataBaseData(idReferral);
                        return pers;
                    }
                }
            }
            return new TestReferralSource(null);
        }
        private void FindMismatch(TestReferralSource r)
        {
            if (this.sourse.IdCaseMis != r.sourse.IdCaseMis)
                Global.errors3.Add("Несовпадение IdCaseMis TestReferralSource");
            if (this.sourse.IdReferralMis != r.sourse.IdReferralMis)
                Global.errors3.Add("Несовпадение IdReferralMis TestReferralSource");
            if (Global.GetLength(this.doctors) != Global.GetLength(r.doctors))
                Global.errors3.Add("Несовпадение длинны doctors TestReferralSource");
            if (Global.GetLength(this.mainDiagnosis) != Global.GetLength(r.mainDiagnosis))
                Global.errors3.Add("Несовпадение длинны mainDiagnosis TestReferralSource");
            if (Global.GetLength(this.lpu) != Global.GetLength(r.lpu))
                Global.errors3.Add("Несовпадение длинны lpu TestReferralSource");
        }
        public override bool Equals(Object obj)
        {
            TestReferralSource p = obj as TestReferralSource;
            if (p == null)
            {
                return false;
            }
            bool x = (this.sourse.IdCaseMis == p.sourse.IdCaseMis);
            x = (this.sourse.IdReferralMis == p.sourse.IdReferralMis);
            x = (Global.IsEqual(this.doctors, p.doctors));
            x = (Global.IsEqual(this.mainDiagnosis, p.mainDiagnosis));
            x = (Global.IsEqual(this.lpu, p.lpu));
            if ((this.sourse.IdCaseMis == p.sourse.IdCaseMis)&&
            (this.sourse.IdReferralMis == p.sourse.IdReferralMis)&&
            (Global.IsEqual(this.doctors, p.doctors))&&
            (Global.IsEqual(this.mainDiagnosis, p.mainDiagnosis))&&
            (Global.IsEqual(this.lpu, p.lpu)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestReferralSource");
                return false;
            }
        }
        public static bool operator ==(TestReferralSource a, TestReferralSource b)
        {
            return Equals(a, b);
        }
        public static bool operator !=(TestReferralSource a, TestReferralSource b)
        {
            return !Equals(a, b);
        }
    }
}
