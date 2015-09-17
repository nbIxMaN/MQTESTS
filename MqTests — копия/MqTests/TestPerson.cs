using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqTests
{
    class TestPerson
    {
        public Person person;
        public TestCoding sex;
        public TestPerson(Person p)
        {
            person = p;
            if (person.Sex != null)
                sex = new TestCoding(person.Sex);
        }

        static public TestPerson BuildPersonFromDataBaseData(string idPerson, string MIS)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT * FROM public.person WHERE id_person = '" + idPerson + "'";
                NpgsqlCommand person = new NpgsqlCommand(findPatient, connection);
                using (NpgsqlDataReader personFromDataBase = person.ExecuteReader())
                {
                    Person p = new Person();
                    while (personFromDataBase.Read())
                    {
                        //что делать с DateSpecified и Мисами? 
                        if (personFromDataBase["birthday"]!= DBNull.Value)
                            p.BirthDate = Convert.ToDateTime(personFromDataBase["birthday"]);
                        if (MIS == "")
                            p.IdPatientMis = null;
                        if ((personFromDataBase["family_name"] != DBNull.Value) || (personFromDataBase["given_name"] != DBNull.Value) || (personFromDataBase["middle_name"] != DBNull.Value))
                        {
                            p.HumanName = new HumanName();
                            if (personFromDataBase["family_name"] != DBNull.Value)
                                p.HumanName.FamilyName = Convert.ToString(personFromDataBase["family_name"]);
                            if (personFromDataBase["given_name"] != DBNull.Value)
                                p.HumanName.GivenName = Convert.ToString(personFromDataBase["given_name"]);
                            if (personFromDataBase["middle_name"] != DBNull.Value)
                                p.HumanName.MiddleName = Convert.ToString(personFromDataBase["middle_name"]);
                        }
                        TestPerson pers = new TestPerson(p);
                        if (personFromDataBase["id_sex"] != DBNull.Value)
                            pers.sex = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_sex"]));
                        return pers;
                    }
                }
            }
            return null;
        }

        private void FindMismatch(TestPerson b)
        {
            if (this.person.BirthDate != b.person.BirthDate)
                Global.errors3.Add("Несовпадение BirthDate TestPerson");
            if (this.person.Sex.Code != b.person.Sex.Code)
                Global.errors3.Add("Несовпадение Sex TestPerson");
            if (this.person.IdPersonMis != b.person.IdPersonMis)
                Global.errors3.Add("Несовпадение IdPersonMis TestPerson");
            if (this.person.HumanName.FamilyName != b.person.HumanName.FamilyName)
                Global.errors3.Add("Несовпадение длины FamilyName TestPerson");
            if (this.person.HumanName.GivenName != b.person.HumanName.GivenName)
                Global.errors3.Add("Несовпадение длины GivenName TestPerson");
            if (this.person.HumanName.MiddleName != b.person.HumanName.MiddleName)
                Global.errors3.Add("Несовпадение длины MiddleName TestPerson");
        }

        public override bool Equals(Object obj)
        {
            TestPerson p = obj as TestPerson;
            if (p == null)
            {
                return false;
            }
            if ((this.person.BirthDate == p.person.BirthDate) &&
                (this.person.IdPersonMis == p.person.IdPersonMis) &&
                (Global.IsEqual(this.sex, p.sex)) &&
                (this.person.HumanName.FamilyName == p.person.HumanName.FamilyName) &&
                (this.person.HumanName.GivenName == p.person.HumanName.GivenName) &&
                (this.person.HumanName.MiddleName == p.person.HumanName.MiddleName))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestPerson");
                return false;
            }
        }
        public static bool operator ==(TestPerson a, TestPerson b)
        {
            return Equals(a, b);
        }
        public static bool operator !=(TestPerson a, TestPerson b)
        {
            return !Equals(a, b);
        }
    }
}
