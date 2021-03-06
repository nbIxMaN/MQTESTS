﻿using MqTests.WebReference;
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
        public List<TestContact> cnts;
        public Array contacts
        {
            get
            {
                if (cnts != null)
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
            doctor = r;
            if ((doctor.ContactDtos != null) && (doctor.ContactDtos.Length != 0))
            {
                cnts = new List<TestContact>();
                foreach (ContactDto i in doctor.ContactDtos)
                    cnts.Add(new TestContact(i));
            }
            if (doctor.Lpu != null)
                lpu = new TestCoding(doctor.Lpu);
            if (doctor.Person != null)
                person = new TestPerson(doctor.Person);
            if (doctor.Position != null)
                position = new TestCoding(doctor.Position);
            if (doctor.Role != null)
                role = new TestCoding(doctor.Role);
            if (doctor.Speciality != null)
                speciality = new TestCoding(doctor.Speciality);
        }

        private TestDoctor() { }
        static public List<TestDoctor> BuildDoctorFromDataBaseData(string idReferral, string idLpu)
        {
            List<TestDoctor> docs = new List<TestDoctor>();
            if (idLpu != "")
            {
                using (NpgsqlConnection c = Global.GetSqlConnection())
                {
                    string findDoctorsIds = "SELECT * FROM public.referral_doctor WHERE id_referral = '" + idReferral +
                                            "'";
                    NpgsqlCommand fd = new NpgsqlCommand(findDoctorsIds, c);
                    using (NpgsqlDataReader idr = fd.ExecuteReader())
                    {
                        while (idr.Read())
                        {
                            if (idr["id_referral_doctor"] != DBNull.Value)
                            {
                                string idDoctor = idr["id_referral_doctor"].ToString();
                                using (NpgsqlConnection connection = Global.GetSqlConnection())
                                {
                                    string findDoctor = "SELECT * FROM public.doctor WHERE id_doctor = '" + idDoctor +
                                                        "' AND id_lpu = '" + idLpu +
                                                        "' ORDER BY id_doctor DESC LIMIT 1";
                                    NpgsqlCommand doc = new NpgsqlCommand(findDoctor, connection);
                                    using (NpgsqlDataReader doctorFromDataBase = doc.ExecuteReader())
                                    {
                                        while (doctorFromDataBase.Read())
                                        {
                                            TestDoctor doctor = new TestDoctor();
                                            if (doctorFromDataBase["id_doctor_role"] != DBNull.Value)
                                                doctor.role =
                                                    TestCoding.BuildCodingFromDataBaseData(
                                                        Convert.ToString(doctorFromDataBase["id_doctor_role"]));
                                            if (doctorFromDataBase["id_doctor_speciality"] != DBNull.Value)
                                                doctor.speciality =
                                                    TestCoding.BuildCodingFromDataBaseData(
                                                        Convert.ToString(doctorFromDataBase["id_doctor_speciality"]));
                                            if (doctorFromDataBase["id_lpu"] != DBNull.Value)
                                                doctor.lpu =
                                                    TestCoding.BuildCodingFromDataBaseData(
                                                        Convert.ToString(doctorFromDataBase["id_lpu"]));
                                            if (doctorFromDataBase["id_doctor_position"] != DBNull.Value)
                                                doctor.position =
                                                    TestCoding.BuildCodingFromDataBaseData(
                                                        Convert.ToString(doctorFromDataBase["id_doctor_position"]));
                                            if (doctorFromDataBase["id_person"] != DBNull.Value)
                                                doctor.person =
                                                    TestPerson.BuildPersonFromDataBaseData(
                                                        Convert.ToString(doctorFromDataBase["id_person"]),
                                                        Convert.ToString(doctorFromDataBase["id_doctor_mis"]));
                                            if (doctorFromDataBase["id_person"] != DBNull.Value)
                                                doctor.cnts =
                                                    TestContact.BuildContactsFromDataBaseData(
                                                        Convert.ToString(doctorFromDataBase["id_person"]));
                                            docs.Add(doctor);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return (docs.Count != 0) ? docs : null;
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
            if (p == null)
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
            return Equals(a, b);
        }
        public static bool operator !=(TestDoctor a, TestDoctor b)
        {
            return !Equals(a, b);
        }
    }
}
