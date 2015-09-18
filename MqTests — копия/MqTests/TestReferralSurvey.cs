using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MqTests
{
    class TestReferralSurvey
    {
        Survey survey;
        TestAdditional additional;
        TestCoding surveyOrgan;
        TestCoding surveyType;
        public TestReferralSurvey(Survey r)
        {
            if (r != null)
                survey = r;
            else
                survey = new Survey();
            additional = new TestAdditional(survey.Additional);
            if (survey.SurveyOrgan != null)
                surveyOrgan = new TestCoding(survey.SurveyOrgan);
            if (survey.SurveyType != null)
                surveyType = new TestCoding(survey.SurveyType);
        }
        static public TestReferralSurvey BuildAdditionalFromDataBaseData(string idReferral)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT id_survey_organ, survey_comment, id_referral, id_survey_type FROM public.referral WHERE id_referral = '" + idReferral + "' ORDER BY id_referral DESC LIMIT 1";
                NpgsqlCommand person = new NpgsqlCommand(findPatient, connection);
                using (NpgsqlDataReader personFromDataBase = person.ExecuteReader())
                {
                    Survey p = new Survey();
                    while (personFromDataBase.Read())
                    {
                        //что делать с DateSpecified и Мисами? 
                        if (personFromDataBase["survey_comment"] != DBNull.Value)
                            p.Comment = Convert.ToString(personFromDataBase["survey_comment"]);
                        TestReferralSurvey pers = new TestReferralSurvey(p);
                        if (personFromDataBase["id_referral"] != DBNull.Value)
                            pers.additional = TestAdditional.BuildAdditionalFromDataBaseData(Convert.ToString(personFromDataBase["id_referral"]));
                        if (personFromDataBase["id_survey_organ"] != DBNull.Value)
                            pers.surveyOrgan = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_survey_organ"]));
                        if (personFromDataBase["id_survey_type"] != DBNull.Value)
                            pers.surveyType = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_survey_type"]));
                        return pers;
                    }
                }
            }
            return null;
        }

        internal void UpdateTestReferralSurvey(Survey r)
        {
            if (r.Comment != null)
                this.survey.Comment = r.Comment;
            if (r.Additional != null)
                this.additional.UpdateTestAdditional(r.Additional);
            if (r.SurveyOrgan != null)
                this.surveyOrgan = new TestCoding(r.SurveyOrgan);
            if (r.SurveyType != null)
                this.surveyType = new TestCoding(r.SurveyType);
        }

        private void FindMismatch(TestReferralSurvey r)
        {
            if (this.survey.Comment != r.survey.Comment)
                Global.errors3.Add("Несовпадение Comment TestReferralSurvey");
            if (Global.GetLength(this.additional) != Global.GetLength(r.additional))
                Global.errors3.Add("Несовпадение длинны additional TestReferralSurvey");
            if (Global.GetLength(this.surveyOrgan) != Global.GetLength(r.surveyOrgan))
                Global.errors3.Add("Несовпадение длинны surveyOrgan TestReferralSurvey");
            if (Global.GetLength(this.surveyType) != Global.GetLength(r.surveyType))
                Global.errors3.Add("Несовпадение длинны surveyType TestReferralSurvey");
        }
        public override bool Equals(Object obj)
        {
            TestReferralSurvey p = obj as TestReferralSurvey;
            if (p == null)
            {
                return false;
            }
            if ((this.survey.Comment == p.survey.Comment)&&
            (Global.IsEqual(this.additional, p.additional))&&
            (Global.IsEqual(this.surveyOrgan, p.surveyOrgan))&&
            (Global.IsEqual(this.surveyType, p.surveyType)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestReferralSurvey");
                return false;
            }
        }
        public static bool operator ==(TestReferralSurvey a, TestReferralSurvey b)
        {
            return Equals(a, b);
        }
        public static bool operator !=(TestReferralSurvey a, TestReferralSurvey b)
        {
            return !(Equals(a, b));
        }
    }
}
