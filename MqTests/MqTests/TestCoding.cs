using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqTests
{
    class TestCoding
    {
        Coding coding;
        public TestCoding(Coding r)
        {
            coding = r;
        }
        public static TestCoding BuildCodingFromDataBaseData(string p)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findDocument = "SELECT * FROM public.terminology_value WHERE id_terminology_value = '" + p + "'";
                NpgsqlCommand person = new NpgsqlCommand(findDocument, connection);
                using (NpgsqlDataReader documentReader = person.ExecuteReader())
                {
                    while (documentReader.Read())
                    {
                        Coding x = new Coding();
                        if (documentReader["code"] != DBNull.Value)
                            x.Code = documentReader["code"].ToString();
                        if (documentReader["uri"] != DBNull.Value)
                            x.System = documentReader["uri"].ToString();
                        if (documentReader["version"] != DBNull.Value)
                            x.Version = documentReader["version"].ToString();
                        return new TestCoding(x);
                    }
                }
            }
            return null;
        }
        private void FindMismatch(TestCoding r)
        {
            if (this.coding.Code != r.coding.Code)
                Global.errors3.Add("Несовпадение Code TestCoding");
            if (this.coding.System!= r.coding.System)
                Global.errors3.Add("Несовпадение System TestCoding");
            if (this.coding.Version!= r.coding.Version)
                Global.errors3.Add("Несовпадение Version TestCoding");
        }
        public override bool Equals(Object obj)
        {
            TestCoding p = obj as TestCoding;
            if ((object)p == null)
            {
                return false;
            }
            if (this.coding == p.coding)
                return true;
            if ((this.coding == null) || (p.coding == null))
            {
                return false;
            }
            if ((this.coding.Code == p.coding.Code)&&
            (this.coding.System == p.coding.System)&&
            (this.coding.Version == p.coding.Version))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestCoding");
                return false;
            }
        }
        public static bool operator ==(TestCoding a, TestCoding b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestCoding a, TestCoding b)
        {
            return !(a.Equals(b));
        }
    }
}
