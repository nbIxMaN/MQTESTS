using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MqTests
{
    class TestDiagnosisInfo
    {
        public DiagnosisInfo diagnosisInfo;
        public TestCoding diagnosisType;
        public TestCoding mkbCode;
        const string complication = "2";
        const string main = "1";

        public TestDiagnosisInfo(DiagnosisInfo r)
        {
            if (r != null)
            {
                diagnosisInfo = r;
                diagnosisType = new TestCoding(r.DiagnosisType);
                mkbCode = new TestCoding(r.MkbCode);
            }
        }
        static public TestDiagnosisInfo BuildTestMainDiagnosisInfoFromDataBaseData(string idReferral)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                //опять DiagnosedDateSpecified, что с этим делать?
                //ещё есть какой-то idDiagnosisStatus и idLpu, так же idDiagnosisContainer - это не diagnosisType?
                string finddiagnosis = "SELECT * FROM public.diagnosis WHERE id_referral = '" + idReferral + "' AND id_diagnosis_container = '" + complication + "')";
                NpgsqlCommand person = new NpgsqlCommand(finddiagnosis, connection);
                using (NpgsqlDataReader diagnosisReader = person.ExecuteReader())
                {
                    while (diagnosisReader.Read())
                    {
                        DiagnosisInfo diag = new DiagnosisInfo();
                        if (diagnosisReader["comment"].ToString() != "")
                            diag.Comment = Convert.ToString(diagnosisReader["comment"]);
                        if (diagnosisReader["diagnosis_date"].ToString() != "")
                            diag.DiagnosedDate = Convert.ToDateTime(diagnosisReader["diagnosis_date"]);
                        TestDiagnosisInfo tDiag = new TestDiagnosisInfo(diag);
                        if (diagnosisReader["id_mkb_code"].ToString() != "")
                            tDiag.mkbCode = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(diagnosisReader["id_mkb_code"]));
                        return tDiag;
                    }
                }
            }
            return null;
        }
        static public List<TestDiagnosisInfo> BuildTestComplicationDiagnosisInfoFromDataBaseData(string idReferral)
        {
            List<TestDiagnosisInfo> diagnosis = new List<TestDiagnosisInfo>();
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                //опять DiagnosedDateSpecified, что с этим делать?
                //ещё есть какой-то idDiagnosisStatus и idLpu, так же idDiagnosisContainer - это не diagnosisType?
                string finddiagnosis = "SELECT * FROM public.diagnosis WHERE id_referral = '" + idReferral + "' AND id_diagnosis_container = '" + complication + "')";
                NpgsqlCommand person = new NpgsqlCommand(finddiagnosis, connection);
                using (NpgsqlDataReader diagnosisReader = person.ExecuteReader())
                {
                    while (diagnosisReader.Read())
                    {
                        DiagnosisInfo diag = new DiagnosisInfo();
                        if (diagnosisReader["comment"].ToString() != "")
                            diag.Comment = Convert.ToString(diagnosisReader["comment"]);
                        if (diagnosisReader["diagnosis_date"].ToString() != "")
                            diag.DiagnosedDate = Convert.ToDateTime(diagnosisReader["diagnosis_date"]);
                        TestDiagnosisInfo tDiag = new TestDiagnosisInfo(diag);
                        if (diagnosisReader["id_mkb_code"].ToString() != "")
                            tDiag.mkbCode = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(diagnosisReader["id_mkb_code"]));
                        diagnosis.Add(tDiag);
                    }
                }
            }
            if (diagnosis.Count != 0)
                return diagnosis;
            else
                return null;
        }
        private void FindMismatch(TestDiagnosisInfo r)
        {
            if (this.diagnosisInfo.Comment != r.diagnosisInfo.Comment)
                Global.errors3.Add("Несовпадение Comment TestDiagnosisInfo");
            if (this.diagnosisInfo.DiagnosedDate != r.diagnosisInfo.DiagnosedDate)
                Global.errors3.Add("Несовпадение DiagnosedDate TestDiagnosisInfo");
            if (Global.GetLength(this.diagnosisType) != Global.GetLength(r.diagnosisType))
                Global.errors3.Add("Несовпадение длины diagnosisInfo TestDiagnosisInfo");
            if (Global.GetLength(this.mkbCode) != Global.GetLength(r.mkbCode))
                Global.errors3.Add("Несовпадение длины ComplicationDiagnosis TestDiagnosisInfo");
        }
        public override bool Equals(Object obj)
        {
            TestDiagnosisInfo p = obj as TestDiagnosisInfo;
            if ((object)p == null)
            {
                return false;
            }
            if (this.diagnosisInfo == p.diagnosisInfo)
                return true;
            if ((this.diagnosisInfo == null) || (p.diagnosisInfo == null))
            {
                return false;
            }
            if ((this.diagnosisInfo.Comment == p.diagnosisInfo.Comment)&&
            (this.diagnosisInfo.DiagnosedDate == p.diagnosisInfo.DiagnosedDate)&&
            (Global.IsEqual(this.diagnosisType, p.diagnosisType)) &&
            (Global.IsEqual(this.mkbCode, p.mkbCode)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestDiagnosisInfo");
                return false;
            }
        }
        public static bool operator ==(TestDiagnosisInfo a, TestDiagnosisInfo b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestDiagnosisInfo a, TestDiagnosisInfo b)
        {
            return !(a.Equals(b));
        }
    }
}
