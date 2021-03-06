﻿using MqTests.WebReference;
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
        public List<TestDocument> documents;
        public Array docs
        {
            get
            {
                if (documents != null)
                    return documents.ToArray();
                else
                    return null;
            }
        }
        public List<TestAddress> addreses;
        public Array adds
        {
            get
            {
                if (addreses != null)
                    return addreses.ToArray();
                else
                    return null;
            }
        }
        public List<TestContact> contacts;
        public Array conts
        {
            get
            {
                if (contacts != null)
                    return contacts.ToArray();
                else
                    return null;
            }
        }
        public List<TestJob> jobs;
        public Array js
        {
            get
            {
                if (jobs != null)
                    return jobs.ToArray();
                else
                    return null;
            }
        }

        public List<TestPrivilege> privileges;
        public Array ps
        {
            get
            {
                if (privileges != null)
                    return privileges.ToArray();
                else
                    return null;
            }
        }

        public TestPatient(Patient p)
        {
            if ((p.Documents != null) && (p.Documents.Length != 0))
            {
                documents = new List<TestDocument>();
                foreach (DocumentDto i in p.Documents)
                    documents.Add(new TestDocument(i));
            }
            if ((p.Addresses != null) && (p.Addresses.Length != 0))
            {
                addreses = new List<TestAddress>();
                foreach (AddressDto i in p.Addresses)
                    addreses.Add(new TestAddress(i));
            }
            if ((p.ContactDtos != null) && (p.ContactDtos.Length != 0))
            {
                contacts = new List<TestContact>();
                foreach (ContactDto i in p.ContactDtos)
                    contacts.Add(new TestContact(i));
            }
            if ((p.Jobs != null) && (p.Jobs.Length != 0))
            {
                jobs = new List<TestJob>();
                foreach (Job i in p.Jobs)
                    jobs.Add(new TestJob(i));
            }
            if ((p.Privileges != null) && (p.Privileges.Length != 0))
            {
                privileges = new List<TestPrivilege>();
                foreach (Privilege i in p.Privileges)
                    privileges.Add(new TestPrivilege(i));
            }
            if (p.Person != null)
                person = new TestPerson(p.Person);
        }

        private TestPatient()
        {

        }

        internal void UpdateTestPatient(Patient p)
        {
            if ((p.Documents != null) && (p.Documents.Length != 0))
            {
                if (docs != null)
                    documents = new List<TestDocument>();
                foreach (DocumentDto i in p.Documents)
                    documents.Add(new TestDocument(i));
            }
            if ((p.Addresses != null) && (p.Addresses.Length != 0))
            {
                if (adds != null)
                    addreses = new List<TestAddress>();
                foreach (AddressDto i in p.Addresses)
                    addreses.Add(new TestAddress(i));
            }
            if ((p.ContactDtos != null) && (p.ContactDtos.Length != 0))
            {
                if (conts != null)
                    contacts = new List<TestContact>();
                foreach (ContactDto i in p.ContactDtos)
                    contacts.Add(new TestContact(i));
            }
            if ((p.Jobs != null) && (p.Jobs.Length != 0))
            {
                if (js != null)
                    jobs = new List<TestJob>();
                foreach (Job i in p.Jobs)
                    jobs.Add(new TestJob(i));
            }
            if ((p.Privileges != null) && (p.Privileges.Length != 0))
            {
                if (ps != null)
                    privileges = new List<TestPrivilege>();
                foreach (Privilege i in p.Privileges)
                    privileges.Add(new TestPrivilege(i));
            }
            if (p.Person != null)
                person.UpdateTestPerson(p.Person);
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
            if (p == null)
                return false;
            if ((Global.IsEqual(this.adds, p.adds)) &&
                (Global.IsEqual(this.conts, p.conts)) &&
                (Global.IsEqual(this.docs, p.docs)) &&
                (Global.IsEqual(this.js, p.js)) &&
                (Global.IsEqual(this.ps, p.ps)) &&
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
            return Equals(a, b);
        }
        public static bool operator !=(TestPatient a, TestPatient b)
        {
            return !Equals(a, b);
        }

    }
}
