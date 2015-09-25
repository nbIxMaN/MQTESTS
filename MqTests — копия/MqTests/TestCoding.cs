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
        public string Code;
        private string System;
        private string Version;
        public TestCoding(Coding r)
        {
            if (r.Code != null)
            {
                Code = r.Code;
                System = r.System;
                Version = r.Version;
            }
        }
        public static TestCoding BuildCodingFromDataBaseData(string p)
        {
            if (p != "")
            {
                using (NpgsqlConnection connection = Global.GetSqlConnection())
                {
                    string findDocument = "SELECT * FROM public.terminology_value WHERE id_terminology_value = '" + p +
                                          "'";
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
            }
            return null;
        }
        private void FindMismatch(TestCoding r)
        {
            if (this.Code != r.Code)
                Global.errors3.Add("Несовпадение Code TestCoding");
            if (this.System!= r.System)
                Global.errors3.Add("Несовпадение System TestCoding");
            if (this.Version!= r.Version)
                Global.errors3.Add("Несовпадение Version TestCoding");
        }
        public override bool Equals(Object obj)
        {
            TestCoding p = obj as TestCoding;
            if (p == null)
            {
                return false;
            }
            if ((this.Code == p.Code)&&
            (this.System == p.System)&&
            (this.Version == p.Version))
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
            return Equals(a, b);
        }
        public static bool operator !=(TestCoding a, TestCoding b)
        {
            return !Equals(a, b);
        }
    }
}
