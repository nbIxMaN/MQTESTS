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
        public Array documents
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
            sourse = r ?? new ReferralSource();
            if (sourse.Doctors != null)
                foreach (Doctor i in sourse.Doctors)
                    docs.Add(new TestDoctor(i));
            if (sourse.MainDiagnosis != null)
                foreach (MainDiagnosis i in sourse.MainDiagnosis)
                    diag.Add(new TestMainDiagnosis(i));
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
                        //referral_creation_date должен быть тут в Date! 
                        if (personFromDataBase["id_source_lpu_case_mis"] != DBNull.Value)
                            p.IdCaseMis = Convert.ToString(personFromDataBase["id_source_lpu_case_mis"]);
                        if (personFromDataBase["id_referral_source_mo_mis"] != DBNull.Value)
                            p.IdReferralMis = Convert.ToString(personFromDataBase["id_referral_source_mo_mis"]);
                        TestReferralSource pers = new TestReferralSource(p);
                        if (personFromDataBase["id_source_lpu"] != DBNull.Value)
                            pers.lpu = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_source_lpu"]));
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
            if (Global.GetLength(this.documents) != Global.GetLength(r.documents))
                Global.errors3.Add("Несовпадение длинны documents TestReferralSource");
            if (Global.GetLength(this.mainDiagnosis) != Global.GetLength(r.mainDiagnosis))
                Global.errors3.Add("Несовпадение длинны mainDiagnosis TestReferralSource");
            if (Global.GetLength(this.lpu) != Global.GetLength(r.lpu))
                Global.errors3.Add("Несовпадение длинны lpu TestReferralSource");
        }
        public override bool Equals(Object obj)
        {
            TestReferralSource p = obj as TestReferralSource;
            if ((object)p == null)
            {
                return false;
            }
            if (this.sourse == p.sourse)
                return true;
            if ((this.sourse == null) || (p.sourse == null))
            {
                return false;
            }
            if ((this.sourse.IdCaseMis == p.sourse.IdCaseMis)&&
            (this.sourse.IdReferralMis == p.sourse.IdReferralMis)&&
            (Global.Equals(this.documents, p.documents))&&
            (Global.Equals(this.mainDiagnosis, p.mainDiagnosis))&&
            (Global.Equals(this.lpu, p.lpu)))
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
            return a.Equals(b);
        }
        public static bool operator !=(TestReferralSource a, TestReferralSource b)
        {
            return !(a.Equals(b));
        }
    }
}
