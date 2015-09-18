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
        public List<TestJob> jobs = new List<TestJob>();
        public Array js
        {
            get
            {
                if (jobs.Count != 0)
                    return jobs.ToArray();
                else
                    return null;
            }
        }
        public List<TestPrivilege> privileges = new List<TestPrivilege>();
        public Array ps
        {
            get
            {
                if (privileges.Count != 0)
                    return privileges.ToArray();
                else
                    return null;
            }
        }

        public TestPatient(Patient p)
        {
            if ((p.Documents != null) && (p.Documents.Length != 0))
            {
                foreach (DocumentDto i in p.Documents)
                    documents.Add(new TestDocument(i));
            }
            if ((p.Addresses != null) && (p.Addresses.Length != 0))
            {
                foreach (AddressDto i in p.Addresses)
                    addreses.Add(new TestAddress(i));
            }
            if ((p.ContactDtos != null) && (p.ContactDtos.Length != 0))
            {
                foreach (ContactDto i in p.ContactDtos)
                    contacts.Add(new TestContact(i));
            }
            if ((p.Jobs != null) && (p.Jobs.Length != 0))
            {
                foreach (Job i in p.Jobs)
                    jobs.Add(new TestJob(i));
            }
            if ((p.Privileges != null) && (p.Privileges.Length != 0))
            {
                foreach (Privilege i in p.Privileges)
                    privileges.Add(new TestPrivilege(i));
            }
            if (p.Person != null)
                person = new TestPerson(p.Person);
        }

        private TestPatient()
        {

        }

        static public TestPatient BuildPatientFromDataBaseData(string idPerson, string MIS)
        {
            TestPatient patient = new TestPatient();
            patient.addreses = TestAddress.BuildAdressesFromDataBaseData(idPerson);
            patient.contacts = TestContact.BuildContactsFromDataBaseData(idPerson);
            patient.documents = TestDocument.BuildDocumentsFromDataBaseData(idPerson);
            patient.jobs = TestJob.BuildJobFromDataBaseData(idPerson);
            patient.privileges = TestPrivilege.BuildPrivilegeFromDataBaseData(idPerson);
            patient.person = TestPerson.BuildPersonFromDataBaseData(idPerson, MIS);
            return patient;
        }

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
        public override bool Equals(Object obj)
        {
            TestPatient p = obj as TestPatient;
            if ((Global.IsEqual(this.adds, p?.adds)) &&
                (Global.IsEqual(this.conts, p?.conts)) &&
                (Global.IsEqual(this.docs, p?.docs)) &&
                (Global.IsEqual(this.jobs, p?.jobs)) &&
                (Global.IsEqual(this.privileges, p?.privileges)) &&
                (Global.IsEqual(this.person, p?.person)))
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
