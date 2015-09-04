using MqTests.WebReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MqTests
{
    class TestPatient
    {
        public Patient patient;
        public TestPerson person;
        public List<TestDocument> documents = new List<TestDocument>();
        public Array docs
        {
            get
            {
                if (documents.Count != 0)
                    return documents.ToArray();
                else
                    return null;
            }
        }
        public List<TestAddress> addreses = new List<TestAddress>();
        public Array adds
        {
            get
            {
                if (addreses.Count != 0)
                    return addreses.ToArray();
                else
                    return null;
            }
        }
        public List<TestContact> contacts = new List<TestContact>();
        public Array conts
        {
            get
            {
                if (contacts.Count != 0)
                    return contacts.ToArray();
                else
                    return null;
            }
        }
        public List<TestJob> js = new List<TestJob>();
        public Array jobs
        {
            get
            {
                if (js.Count != 0)
                    return js.ToArray();
                else
                    return null;
            }
        }
        public List<TestPrivilege> ps = new List<TestPrivilege>();
        public Array privileges
        {
            get
            {
                if (ps.Count != 0)
                    return ps.ToArray();
                else
                    return null;
            }
        }

        public TestPatient(Patient p)
        {
            if (p != null)
            {
                //сам пациент тут не нужен
                patient = p;
                if (p.Documents != null)
                    foreach (DocumentDto i in p.Documents)
                        documents.Add(new TestDocument(i));
                if (p.Addresses != null)
                    foreach (AddressDto i in p.Addresses)
                        addreses.Add(new TestAddress(i));
                if (p.ContactDtos != null)
                    foreach (ContactDto i in p.ContactDtos)
                        contacts.Add(new TestContact(i));
                if (p.Jobs != null)
                    foreach (Job i in p.Jobs)
                        js.Add(new TestJob(i));
                if (p.Privileges != null)
                    foreach (Privilege i in p.Privileges)
                        ps.Add(new TestPrivilege(i));
                person = new TestPerson(p.Person);
            }
        }

        static public TestPatient BuildPatientFromDataBaseData(string idPerson, string MIS)
        {
            TestPatient patient = new TestPatient(new Patient());
            patient.addreses = TestAddress.BuildAdressesFromDataBaseData(idPerson);
            patient.contacts = TestContact.BuildContactsFromDataBaseData(idPerson);
            patient.documents = TestDocument.BuildDocumentsFromDataBaseData(idPerson);
            patient.js = TestJob.BuildJobFromDataBaseData(idPerson);
            patient.ps = TestPrivilege.BuildPrivilegeFromDataBaseData(idPerson);
            patient.person = TestPerson.BuildPersonFromDataBaseData(idPerson, MIS);
            return patient;
        }

        //static private string GetPatientId(string guid, string idlpu, string mis)
        //{
        //    string patientId = "";
        //    string findIdPersonString = "";
        //    string findIdInstitutionalString =
        //        "SELECT TOP(1) IdInstitution FROM Institution WHERE IdFedNsi = '" + idlpu + "'";
        //    using (SqlConnection connection = Global.GetSqlConnection())
        //    {
        //        SqlCommand IdInstitution = new SqlCommand(findIdInstitutionalString, connection);
        //        using (SqlDataReader IdInstitutional = IdInstitution.ExecuteReader())
        //        {
        //            string InstId = "";
        //            while (IdInstitutional.Read())
        //            {
        //                InstId = IdInstitutional["IdInstitution"].ToString();
        //            }
        //            findIdPersonString =
        //                "SELECT TOP(1) * FROM ExternalId WHERE IdPersonMIS = '" + mis + "' AND IdLpu = '" + InstId + "' AND SystemGuid = '" + guid.ToLower() + "'";
        //        }
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

        //static private string[] GetGUIDandIDLPUandIDMIS(string idPerson)
        //{
        //    string guid = "";
        //    string idlpu = "";
        //    string mis = "";
        //    using (SqlConnection connection = Global.GetSqlConnection())
        //    {
        //        string findPatient = "SELECT TOP(1) * FROM ExternalId WHERE IdPerson = '" + idPerson + "'";
        //        SqlCommand person = new SqlCommand(findPatient, connection);
        //        using (SqlDataReader patientFromDataBase = person.ExecuteReader())
        //        {
        //            while (patientFromDataBase.Read())
        //            {
        //                guid = Convert.ToString(patientFromDataBase["SystemGuid"]);
        //                mis = Convert.ToString(patientFromDataBase["IdPersonMIS"]);
        //                string lpu = Convert.ToString(patientFromDataBase["IdLpu"]);
        //                string findIdInstitutionalString =
        //                    "SELECT TOP(1) IdFedNsi FROM Institution WHERE IdInstitution = '" + lpu + "'";
        //                using (SqlConnection connection2 = Global.GetSqlConnection())
        //                {
        //                    SqlCommand IdInstitution = new SqlCommand(findIdInstitutionalString, connection2);
        //                    using (SqlDataReader IdInstitutional = IdInstitution.ExecuteReader())
        //                    {
        //                        while (IdInstitutional.Read())
        //                        {
        //                            idlpu = Convert.ToString(IdInstitutional["IdFedNsi"]);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    string[] s = new string[] { guid, idlpu, mis };
        //    return s;
        //}

        //static public TestPatient BuildPatientFromDataBaseData(string guid = null, string idlpu = null, string mis = null, string patientId = null)
        //{
        //    if (patientId == null)
        //        patientId = GetPatientId(guid, idlpu, mis);
        //    else
        //    {
        //        string[] s = GetGUIDandIDLPUandIDMIS(patientId);
        //        guid = s[0];
        //        idlpu = s[1];
        //        mis = s[2];
        //    }
        //    if (patientId != null)
        //    {
        //        using (SqlConnection connection = Global.GetSqlConnection())
        //        {
        //            string findPatient = "SELECT TOP(1) * FROM Person, PatientAdditionalInfo WHERE Person.IdPerson = '" + patientId + "' AND Person.IdPerson = PatientAdditionalInfo.IdPerson";
        //            SqlCommand person = new SqlCommand(findPatient, connection);
        //            using (SqlDataReader patientFromDataBase = person.ExecuteReader())
        //            {
        //                while (patientFromDataBase.Read())
        //                {
        //                    PatientDto p = new PatientDto();
        //                    if (patientFromDataBase["FamilyName"].ToString() != "")
        //                        p.FamilyName = Convert.ToString(patientFromDataBase["FamilyName"]);
        //                    else
        //                        p.FamilyName = null;
        //                    if (patientFromDataBase["MiddleName"].ToString() != "")
        //                        p.MiddleName = Convert.ToString(patientFromDataBase["MiddleName"]);
        //                    else
        //                        p.MiddleName = null;
        //                    if (patientFromDataBase["GivenName"].ToString() != "")
        //                        p.GivenName = Convert.ToString(patientFromDataBase["GivenName"]);
        //                    else
        //                        p.GivenName = null;
        //                    p.BirthDate = Convert.ToDateTime(patientFromDataBase["BirthDate"]);
        //                    p.Sex = Convert.ToByte(patientFromDataBase["IdSex"]);
        //                    p.IsVip = Convert.ToBoolean(patientFromDataBase["IsVip"]);
        //                    if (patientFromDataBase["IdSocialStatus"].ToString() != "")
        //                        p.SocialStatus = Convert.ToString(patientFromDataBase["IdSocialStatus"]);
        //                    else
        //                        p.SocialStatus = null;
        //                    if (patientFromDataBase["IdSocialGroup"].ToString() != "")
        //                        p.SocialGroup = Convert.ToByte(patientFromDataBase["IdSocialGroup"]);
        //                    else
        //                        p.SocialGroup = null;
        //                    if (patientFromDataBase["IdLivingAreaType"].ToString() != "")
        //                        p.IdLivingAreaType = Convert.ToByte(patientFromDataBase["IdLivingAreaType"]);
        //                    else
        //                        p.IdLivingAreaType = null;
        //                    if (patientFromDataBase["IdBloodType"].ToString() != "")
        //                        p.IdBloodType = Convert.ToByte(patientFromDataBase["IdBloodType"]);
        //                    else
        //                        p.IdBloodType = null;
        //                    if (patientFromDataBase["DeathTime"].ToString() != "")
        //                        p.DeathTime = Convert.ToDateTime(patientFromDataBase["DeathTime"]);
        //                    else
        //                        p.DeathTime = null;
        //                    p.IdPatientMIS = mis;
        //                    p.IdGlobal = patientId;
        //                    TestPatient patient = new TestPatient(guid, idlpu, p);
        //                    patient.documents = TestDocument.BuildDocumentsFromDataBaseData(patientId);
        //                    patient.addreses = TestAddress.BuildAdressesFromDataBaseData(patientId);
        //                    patient.contacts = TestContact.BuildContactsFromDataBaseData(patientId);
        //                    patient.job = TestJob.BuildTestJobFromDataBase(patientId);
        //                    patient.contactPerson = TestContactPerson.BuildTestContactPersonFromDataBase(patientId);
        //                    patient.privilege = TestPrivilege.BuildTestPrivilegeFromDataBase(patientId);
        //                    patient.birthplace = TestBirthplace.BuildBirthplaceFromDataBaseData(patientId);
        //                    return patient;
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}

        private void FindMismatch(TestPatient b)
        {
            if (Global.GetLength(this.person) != Global.GetLength(b.person))
                Global.errors2.Add("несовпадение длины person TestPatient");
            if (Global.GetLength(this.docs) != Global.GetLength(b.docs))
                Global.errors2.Add("несовпадение длины documents TestPatient");
            if (Global.GetLength(this.conts) != Global.GetLength(b.conts))
                Global.errors2.Add("несовпадение длины contacts TestPatient");
            if (Global.GetLength(this.adds) != Global.GetLength(b.adds))
                Global.errors2.Add("несовпадение длины addreses TestPatient");
            if (Global.GetLength(this.jobs) != Global.GetLength(b.jobs))
                Global.errors2.Add("несовпадение длины job TestPatient");
            if (Global.GetLength(this.privileges) != Global.GetLength(b.privileges))
                Global.errors2.Add("несовпадение длины privilege TestPatient");
        }

        //public void ChangePatientField(PatientDto b)
        //{
        //    if ((b.FamilyName != null) && (this.patient.FamilyName != b.FamilyName))
        //        this.patient.FamilyName = b.FamilyName;
        //    if ((b.MiddleName != null) && (this.patient.MiddleName != b.MiddleName))
        //        this.patient.MiddleName = b.MiddleName;
        //    if ((b.GivenName != null) && (this.patient.GivenName != b.GivenName))
        //        this.patient.GivenName = b.GivenName;
        //    if ((b.BirthDate != DateTime.MinValue) && (this.patient.BirthDate != b.BirthDate))
        //        this.patient.BirthDate = b.BirthDate;
        //    if ((b.Sex != 0) && (this.patient.Sex != b.Sex))
        //        this.patient.Sex = b.Sex;
        //    this.patient.IsVip = b.IsVip;
        //    if ((b.SocialStatus != null) && (this.patient.SocialStatus != b.SocialStatus))
        //        this.patient.SocialStatus = b.SocialStatus;
        //    if ((b.IdLivingAreaType != null) && (this.patient.IdLivingAreaType != b.IdLivingAreaType))
        //        this.patient.IdLivingAreaType = b.IdLivingAreaType;
        //    if ((b.IdBloodType != null) && (this.patient.IdBloodType != b.IdBloodType))
        //        this.patient.IdBloodType = b.IdBloodType;
        //    if ((b.DeathTime != null) && (this.patient.DeathTime != b.DeathTime))
        //        this.patient.DeathTime = b.DeathTime;
        //    if (b.Documents != null)
        //    {
        //        foreach (DocumentDto d in b.Documents)
        //        {
        //            if (this.documents != null)
        //            {
        //                bool mark = false;
        //                foreach (TestDocument td in this.documents)
        //                {
        //                    if (d.IdDocumentType == td.document.IdDocumentType)
        //                    {
        //                        td.document = d;
        //                        mark = true;
        //                    }
        //                }
        //                if (!mark)
        //                    this.documents.Add(new TestDocument(d));
        //            }
        //            else
        //            {
        //                this.documents = new List<TestDocument>();
        //                documents.Add(new TestDocument(d));
        //            }
        //        }
        //    }
        //    if (b.Addresses != null)
        //    {
        //        foreach (AddressDto a in b.Addresses)
        //        {
        //            if (this.addreses != null)
        //            {
        //                bool mark = false;
        //                foreach (TestAddress ta in this.addreses)
        //                {
        //                    if (a.IdAddressType == ta.address.IdAddressType)
        //                    {
        //                        ta.address = a;
        //                        mark = true;
        //                    }
        //                }
        //                if (!mark)
        //                    this.addreses.Add(new TestAddress(a));
        //            }
        //            else
        //            {
        //                this.addreses = new List<TestAddress>();
        //                addreses.Add(new TestAddress(a));
        //            }
        //        }
        //    }
        //    if (b.Contacts != null)
        //        foreach (ContactDto c in b.Contacts)
        //        {
        //            TestContact tc = new TestContact(c);
        //            if (this.contacts != null)
        //            {
        //                bool mark = false;
        //                foreach (TestContact c1 in this.contacts)
        //                    if (tc == c1)
        //                        mark = true;
        //                if (!mark)
        //                    this.contacts.Add(tc);
        //            }
        //            else
        //                this.contacts.Add(tc);
        //        }
        //    if (b.ContactPerson != null)
        //        this.contactPerson = new TestContactPerson(b.ContactPerson);
        //    if (b.Job != null)
        //        this.job = new TestJob(b.Job);
        //    if (b.Privilege != null)
        //        this.privilege = new TestPrivilege(b.Privilege);
        //    if (b.BirthPlace != null)
        //        this.birthplace = new TestBirthplace(b.BirthPlace);
        //}

        //public bool CheckPatientInDataBase()
        //{
        //    TestPatient p = TestPatient.BuildPatientFromDataBaseData(GUID, IDLPU, patient.IdPatientMIS);
        //    this.Equals(p);
        //    return (this == p);
        //}

        //public void DeletePatient()
        //{
        //    using (SqlConnection connection = Global.GetSqlConnection())
        //    {
        //        string findIdPersonString =
        //            "SELECT TOP(1) * FROM ExternalId WHERE IdPersonMIS = '" + patient.IdPatientMIS + "'";
        //        SqlCommand command = new SqlCommand(findIdPersonString, connection);
        //        using (SqlDataReader MISreader = command.ExecuteReader())
        //        {
        //            string patientId = GetPatientId(GUID, IDLPU, patient.IdPatientMIS);
        //            while ((MISreader.Read()) && (patientId != null))
        //            {
        //                string command2 = "EXEC dbo.Delete_Patient @IdPatientMIS = '" + patient.IdPatientMIS + "'";
        //                using (SqlConnection connection2 = Global.GetSqlConnection())
        //                {
        //                    var SqlComm = new SqlCommand(command2, connection2);
        //                    SqlComm.ExecuteNonQuery();
        //                }
        //                patientId = GetPatientId(GUID, IDLPU, patient.IdPatientMIS);
        //            }
        //        }
        //    }
        //}

        public override bool Equals(Object obj)
        {
            TestPatient p = obj as TestPatient;
            if ((object)p == null)
            {
                return false;
            }
            if ((Global.IsEqual(this.adds, p.adds)) &&
                (Global.IsEqual(this.conts, p.conts)) &&
                (Global.IsEqual(this.docs, p.docs)) &&
                (Global.IsEqual(this.jobs, p.jobs)) &&
                (Global.IsEqual(this.privileges, p.privileges)) &&
                (Global.IsEqual(this.person, p.person)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestPatient");
                return false;
            }
        }
        public static bool operator ==(TestPatient a, TestPatient b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestPatient a, TestPatient b)
        {
            return !(a.Equals(b));
        }

    }
}
