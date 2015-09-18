using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MqTests
{
    class TestAdditional
    {
        Additional additional;
        public TestAdditional(Additional r)
        {
            if (r != null)
                additional = r;
            else
                additional = new Additional();
        }
        static public TestAdditional BuildAdditionalFromDataBaseData(string idReferral)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT patient_allergy_iodine, patient_hight, patient_weight FROM public.referral WHERE id_referral = '" + idReferral + "' ORDER BY id_referral DESC LIMIT 1";
                NpgsqlCommand person = new NpgsqlCommand(findPatient, connection);
                using (NpgsqlDataReader personFromDataBase = person.ExecuteReader())
                {
                    Additional p = new Additional();
                    while (personFromDataBase.Read())
                    {
                        //что делать с DateSpecified и Мисами? 
                        if (personFromDataBase["patient_allergy_iodine"] != DBNull.Value)
                            p.AllergyIodine = Convert.ToString(personFromDataBase["patient_allergy_iodine"]);
                        if (personFromDataBase["patient_hight"] != DBNull.Value)
                            p.Height = Convert.ToString(personFromDataBase["patient_hight"]);
                        if (personFromDataBase["patient_weight"] != DBNull.Value)
                            p.Weight = Convert.ToString(personFromDataBase["patient_weight"]);
                        TestAdditional pers = new TestAdditional(p);
                        return pers;
                    }
                }
            }
            return null;
        }
        private void FindMismatch(TestAdditional r)
        {
            if (this.additional.AllergyIodine != r.additional.AllergyIodine)
                Global.errors3.Add("Несовпадение AllergyIodine TestAdditional");
            if (this.additional.Height != r.additional.Height)
                Global.errors3.Add("Несовпадение Height TestAdditional");
            if (this.additional.Weight != r.additional.Weight)
                Global.errors3.Add("Несовпадение Weight TestAdditional");
        }
        public override bool Equals(Object obj)
        {
            TestAdditional p = obj as TestAdditional;
            if (p == null)
            {
                return false;
            }
            if ((this.additional.AllergyIodine == p.additional.AllergyIodine)&&
            (this.additional.Height == p.additional.Height)&&
            (this.additional.Weight == p.additional.Weight))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestAdditional");
                return false;
            }
        }

        internal void UpdateTestAdditional(Additional a)
        {
            if (a.AllergyIodine != null)
                this.additional.AllergyIodine = a.AllergyIodine;
            if (a.Height != null)
                this.additional.Height = a.Height;
            if (a.Weight != null)
                this.additional.Weight = a.Weight;
        }

        public static bool operator ==(TestAdditional a, TestAdditional b)
        {
            return Equals(a, b);
        }
        public static bool operator !=(TestAdditional a, TestAdditional b)
        {
            return !Equals(a, b);
        }
    }
}
