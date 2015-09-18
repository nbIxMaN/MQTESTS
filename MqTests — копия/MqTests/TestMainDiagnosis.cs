using MqTests.WebReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MqTests
{
    class TestMainDiagnosis
    {
        //Сам диагноз нам не нужен
        public MainDiagnosis diagnos;
        public TestDiagnosisInfo diagnosisInfo;
        public List<TestDiagnosisInfo> compDiagnosis;
        public Array ComplicationDiagnosis
        {
            get
            {
                if (compDiagnosis.Count != 0)
                    return compDiagnosis.ToArray();
                else
                    return null;
            }
        }
        public TestMainDiagnosis(MainDiagnosis r)
        {
            diagnos = r;
            diagnosisInfo = new TestDiagnosisInfo(diagnos.DiagnosisInfo);
            if ((diagnos.ComplicationDiagnosis != null) && (diagnos.ComplicationDiagnosis.Length != 0))
            {
                compDiagnosis = new List<TestDiagnosisInfo>();
                foreach (DiagnosisInfo i in diagnos.ComplicationDiagnosis)
                    compDiagnosis.Add(new TestDiagnosisInfo(i));
            }
        }
        static public List<TestMainDiagnosis> BuildTestMainDiagnosisInfoFromDataBaseData(string idReferral)
        {
            List<TestMainDiagnosis> tdi = new List<TestMainDiagnosis>();
            TestMainDiagnosis md = new TestMainDiagnosis(new MainDiagnosis());
            md.diagnosisInfo = TestDiagnosisInfo.BuildTestMainDiagnosisInfoFromDataBaseData(idReferral);
            md.compDiagnosis= TestDiagnosisInfo.BuildTestComplicationDiagnosisInfoFromDataBaseData(idReferral);
            tdi.Add(md);
            return (tdi.Count != 0) ? tdi : null;
        }
        private void FindMismatch(TestMainDiagnosis r)
        {
            if (Global.GetLength(this.diagnosisInfo) != Global.GetLength(r.diagnosisInfo))
                Global.errors3.Add("Несовпадение длины diagnosisInfo TestMainDiagnosis");
            if (Global.GetLength(this.ComplicationDiagnosis) != Global.GetLength(r.ComplicationDiagnosis))
                Global.errors3.Add("Несовпадение длины ComplicationDiagnosis TestMainDiagnosis");
        }
        public override bool Equals(Object obj)
        {
            TestMainDiagnosis p = obj as TestMainDiagnosis;
            if (p == null)
            {
                return false;
            }
            if ((Global.IsEqual(this.diagnosisInfo, p.diagnosisInfo))&&
            (Global.IsEqual(this.ComplicationDiagnosis, p.ComplicationDiagnosis)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestMainDiagnosis");
                return false;
            }
        }
        public static bool operator ==(TestMainDiagnosis a, TestMainDiagnosis b)
        {
            return Equals(a, b);
        }
        public static bool operator !=(TestMainDiagnosis a, TestMainDiagnosis b)
        {
            return !Equals(a, b);
        }
    }
}
