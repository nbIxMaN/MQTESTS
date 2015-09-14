using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MqTests
{
    class TestContact
    {
        public ContactDto contact;
        public TestCoding contactType;

        public TestContact(ContactDto c)
        {
            contact = c;
            contactType = new TestCoding(contact.ContactType);
        }

        static public List<TestContact> BuildContactsFromDataBaseData(string idPerson)
        {
            List<TestContact> contacts = new List<TestContact>();
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findPatient = "SELECT * FROM public.Contact WHERE id_person = '" + idPerson + "'";
                NpgsqlCommand person = new NpgsqlCommand(findPatient, connection);
                using (NpgsqlDataReader contactReader = person.ExecuteReader())
                {
                    while (contactReader.Read())
                    {
                        ContactDto cont = new ContactDto();
                        if (contactReader["contact_value"] != DBNull.Value)
                            cont.ContactValue = Convert.ToString(contactReader["contact_value"]);
                        TestContact contact = new TestContact(cont);
                        if (contactReader["id_contact_type"] != DBNull.Value)
                            contact.contactType = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(contactReader["id_contact_type"]));
                        contacts.Add(contact);
                    }
                }
            }
            return (contacts.Count != 0) ? contacts : null;
        }

        private void FindMismatch(TestContact b)
        {
            if (this.contact.ContactValue != b.contact.ContactValue)
                Global.errors3.Add("Несовпадение ContactValue TestContact");
            if (Global.GetLength(this.contactType) != Global.GetLength(b.contactType))
                Global.errors3.Add("Несовпадение длинны ContactType TestContact");
        }

        public override bool Equals(Object obj)
        {
            TestContact p = obj as TestContact;
            if ((object)p == null)
            {
                return false;
            }
            if (this.contact == p.contact)
                return true;
            if ((this.contact == null) || (p.contact == null))
            {
                return false;
            }
            if ((this.contact.ContactValue == p.contact.ContactValue) &&
               (Global.IsEqual(this.contactType, p.contactType)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestContact");
                return false;
            }
        }
        public static bool operator ==(TestContact a, TestContact b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestContact a, TestContact b)
        {
            return !(a.Equals(b));
        }

    }
}
