using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MqTests
{
    class TestDoctor
    {
        // доктор тут не нужен
        public Doctor doctor;
        public List<TestContact> cnts = new List<TestContact>();
        public Array contacts
        {
            get
            {
                if (cnts.Count != 0)
                    return cnts.ToArray();
                else
                    return null;
            }
        }
        public TestCoding lpu;
        public TestPerson person;
        public TestCoding position;
        public TestCoding role;
        public TestCoding speciality;

        public TestDoctor(Doctor r)
        {
            if (r != null)
            {
                doctor = r;
                if (r.ContactDtos != null)
                    foreach (ContactDto i in r.ContactDtos)
                        cnts.Add(new TestContact(i));
                lpu = new TestCoding(r.Lpu);
                person = new TestPerson(r.Person);
                position = new TestCoding(r.Position);
                role = new TestCoding(r.Role);
                speciality = new TestCoding(r.Speciality);
            }
        }
        static public List<TestDoctor> BuildDoctorFromDataBaseData(string idReferral)
        {
            List<TestDoctor> docs = new List<TestDoctor>();
            using (NpgsqlConnection c = Global.GetSqlConnection())
            {
                string findDoctorsIds = "SELECT * FROM public.referral_doctor WHERE id_referral = '" + idReferral + "'";
                NpgsqlCommand fd = new NpgsqlCommand(findDoctorsIds, c);
                using (NpgsqlDataReader idr = fd.ExecuteReader())
                {
                    if (idr["id_doctor"].ToString() != "")
                    {
                        string idDoctor = idr["id_doctor"].ToString();
                        using (NpgsqlConnection connection = Global.GetSqlConnection())
                        {
                            string findDoctor = "SELECT * FROM public.doctor WHERE id_doctor = '" + idDoctor + "' ORDER BY id_doctor DESC LIMIT 1";
                            NpgsqlCommand doc = new NpgsqlCommand(findDoctor, connection);
                            using (NpgsqlDataReader doctorFromDataBase = doc.ExecuteReader())
                            {
                                Doctor p = new Doctor();
                                while (doctorFromDataBase.Read())
                                {
                                    //Закончил тут. 25.08.2015
                                    TestDoctor doctor = new TestDoctor(p);
                                    if (doctorFromDataBase["id_doctor_role"].ToString() != "")
                                        doctor.role = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(doctorFromDataBase["id_doctor_role"]));
                                    if (doctorFromDataBase["id_doctor_speciality"].ToString() != "")
                                        doctor.speciality = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(doctorFromDataBase["id_doctor_speciality"]));
                                    if (doctorFromDataBase["id_lpu"].ToString() != "")
                                        doctor.lpu = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(doctorFromDataBase["id_lpu"]));
                                    if (doctorFromDataBase["id_position"].ToString() != "")
                                        doctor.position = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(doctorFromDataBase["id_position"]));
                                    if (doctorFromDataBase["id_person"].ToString() != "")
                                        doctor.person = TestPerson.BuildPersonFromDataBaseData(Convert.ToString(doctorFromDataBase["id_person"]), Convert.ToString(doctorFromDataBase["id_doctor_mis"]));
                                    if (doctorFromDataBase["id_person"].ToString() != "")
                                        doctor.cnts = TestContact.BuildContactsFromDataBaseData(Convert.ToString(doctorFromDataBase["id_person"]));
                                    docs.Add(doctor);
                                }
                            }
                        }
                    }
                }
            }
            if (docs.Count != 0)
                return docs;
            else
                return null;
        }
        private void FindMismatch(TestDoctor r)
        {
            if (Global.GetLength(this.contacts) != Global.GetLength(this.contacts))
                Global.errors3.Add("Несовпадение длины contacts TestDoctor");
            if (Global.GetLength(this.lpu) != Global.GetLength(this.lpu))
                Global.errors3.Add("Несовпадение длины lpu TestDoctor");
            if (Global.GetLength(this.person) != Global.GetLength(this.person))
                Global.errors3.Add("Несовпадение длины person TestDoctor");
            if (Global.GetLength(this.position) != Global.GetLength(this.position))
                Global.errors3.Add("Несовпадение длины position TestDoctor");
            if (Global.GetLength(this.role) != Global.GetLength(this.role))
                Global.errors3.Add("Несовпадение длины role TestDoctor");
            if (Global.GetLength(this.speciality) != Global.GetLength(this.speciality))
                Global.errors3.Add("Несовпадение длины speciality TestDoctor");
        }
        public override bool Equals(Object obj)
        {
            TestDoctor p = obj as TestDoctor;
            if ((object)p == null)
            {
                return false;
            }
            if (this.doctor == p.doctor)
                return true;
            if ((this.doctor == null) || (p.doctor == null))
            {
                return false;
            }
            if ((Global.IsEqual(this.contacts, this.contacts))&&
            (Global.IsEqual(this.lpu, this.lpu))&&
            (Global.IsEqual(this.person, this.person))&&
            (Global.IsEqual(this.position, this.position))&&
            (Global.IsEqual(this.role, this.role))&&
            (Global.IsEqual(this.speciality, this.speciality)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestDoctor");
                return false;
            }
        }
        public static bool operator ==(TestDoctor a, TestDoctor b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestDoctor a, TestDoctor b)
        {
            return !(a.Equals(b));
        }
    }
}
