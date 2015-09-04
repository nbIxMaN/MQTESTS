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
        //public TestHumanName humanName;
        public TestPerson(Person p)
        {
            if (p != null)
            {
                person = p;
                //humanName = new TestHumanName(p.HumanName);
                sex = new TestCoding(p.Sex);
            }
        }

        //static public string GetPersonId(string guid, string idlpu, string mis)
        //{
        //    string patientId = "";
        //    string findIdPersonString = "";
        //    using (SqlConnection connection = Global.GetSqlConnection())
        //    {
        //        string InstId = Global.GetIdInstitution(idlpu);
        //        findIdPersonString =
        //            "SELECT TOP(1) * FROM ExternalId WHERE IdPersonMIS = '" + mis + "' AND IdLpu = '" + InstId + "' AND SystemGuid = '" + guid.ToLower() + "'";
        //        SqlCommand command = new SqlCommand(findIdPersonString, connection);
        //        using (SqlDataReader IdPerson = command.ExecuteReader())
        //        {
        //            while (IdPerson.Read())
        //            {
        //                patientId = IdPerson["IdPerson"].ToString();
        //            }
        //        }
        //    }
        //    if (patientId != "")
        //        return patientId;
        //    else
        //        return null;
        //}

        //static private string GetIDMIS(string idPerson)
        //{
        //    string mis = "";
        //    using (SqlConnection connection = Global.GetSqlConnection())
        //    {
        //        string findPatient = "SELECT TOP(1) * FROM ExternalId WHERE IdPerson = '" + idPerson + "'";
        //        SqlCommand person = new SqlCommand(findPatient, connection);
        //        using (SqlDataReader patientFromDataBase = person.ExecuteReader())
        //        {
        //            while (patientFromDataBase.Read())
        //                mis = Convert.ToString(patientFromDataBase["IdPersonMIS"]);
        //        }
        //    }
        //    return mis;
        //}

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
                        if (personFromDataBase["birthday"].ToString() != "")
                            p.BirthDate = Convert.ToDateTime(personFromDataBase["birthday"]);
                        if (MIS == "")
                            p.IdPatientMis = null;
                        if ((personFromDataBase["family_name"].ToString() != "") || (personFromDataBase["given_name"].ToString() != "") || (personFromDataBase["middle_name"].ToString() != ""))
                        {
                            p.HumanName = new HumanName();
                            if (personFromDataBase["family_name"].ToString() != "")
                                p.HumanName.FamilyName = Convert.ToString(personFromDataBase["family_name"]);
                            if (personFromDataBase["given_name"].ToString() != "")
                                p.HumanName.GivenName = Convert.ToString(personFromDataBase["given_name"]);
                            if (personFromDataBase["middle_name"].ToString() != "")
                                p.HumanName.MiddleName = Convert.ToString(personFromDataBase["middle_name"]);
                        }
                        TestPerson pers = new TestPerson(p);
                        if (personFromDataBase["id_sex"].ToString() != "")
                            pers.sex = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(personFromDataBase["id_sex"]));
                        return pers;
                    }
                }
            }
            return null;
        }

        //static public TestPerson BuildPersonFromDataBaseData(string idPerson, string idAuthor)
        //{
        //    using (SqlConnection connection = Global.GetSqlConnection())
        //    {
        //        string findPatient = "SELECT TOP(1) * FROM Person WHERE id_person = '" + idPerson + "' AND id_author = '" + idAuthor + "'";
        //        SqlCommand person = new SqlCommand(findPatient, connection);
        //        using (SqlDataReader personFromDataBase = person.ExecuteReader())
        //        {
        //            Person p = new Person();
        //            if (personFromDataBase["id_person"].ToString() != "")
        //                p.IdPersonMis = personFromDataBase["id_person"].ToString();
        //            p.HumanName = new HumanName();
        //            if (personFromDataBase["given_name"].ToString() != "")
        //                p.HumanName.GivenName = personFromDataBase["given_name"].ToString();
        //            if (personFromDataBase["middle_name"].ToString() != "")
        //                p.HumanName.GivenName = personFromDataBase["middle_name"].ToString();
        //            if (personFromDataBase["family_name"].ToString() != "")
        //                p.HumanName.GivenName = personFromDataBase["family_name"].ToString();
        //            if (personFromDataBase["birthday"].ToString() != "")
        //                p.BirthDate = Convert.ToDateTime(personFromDataBase["birthday"]);
        //            TestPerson tp = new TestPerson(p);
        //            if (personFromDataBase["id_sex"].ToString() != "")
        //                tp.sex = new Coding
        //                {
        //                    Code = personFromDataBase["id_sex"].ToString();
        //                }
        //            return tp;
        //        }
        //    }
        //    return null;
        //}

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
            if ((object)p == null)
            {
                return false;
            }
            if ((this.person.BirthDate == p.person.BirthDate) &&
                (this.person.IdPersonMis == p.person.IdPersonMis) &&
                (this.person.Sex.Code == p.person.Sex.Code) &&
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
            return a.Equals(b);
        }
        public static bool operator !=(TestPerson a, TestPerson b)
        {
            return !(a.Equals(b));
        }
    }
}
