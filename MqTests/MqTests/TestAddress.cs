using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MqTests
{
    class TestAddress
    {
        public AddressDto address;
        public TestCoding addressType;
        public TestAddress(AddressDto a)
        {
            if (a != null)
            {
                address = a;
                addressType = new TestCoding(a.AddressType);
            }
        }

        static public List<TestAddress> BuildAdressesFromDataBaseData(string idPerson)
        {
            List<TestAddress> addresses = new List<TestAddress>();
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findIdAdresses = "SELECT * FROM public.address WHERE id_person = '" + idPerson + "'";
                NpgsqlCommand IdAddresses = new NpgsqlCommand(findIdAdresses, connection);
                using (NpgsqlDataReader addressesReader = IdAddresses.ExecuteReader())
                {
                    while (addressesReader.Read())
                    {
                        AddressDto address = new AddressDto();
                        if (addressesReader["address"].ToString() != "")
                            address.StringAddress = Convert.ToString(addressesReader["address"]);
                        TestAddress a = new TestAddress(address);
                        if (addressesReader["id_address_type"].ToString() != "")
                            a.addressType = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(addressesReader["id_address_type"]));
                        addresses.Add(a);
                    }
                }
            }
            if (addresses.Count != 0)
                return addresses;
            else
                return null;
        }

        private void FindMismatch(TestAddress b)
        {
            if (this.address.StringAddress != b.address.StringAddress)
                Global.errors3.Add("Несовпадение StringAddress TestAddress");
            if (Global.GetLength(this.addressType) != Global.GetLength(b.addressType))
                Global.errors3.Add("Несовпадение длинны addressType TestAddress");
        }

        public override bool Equals(Object obj)
        {
            TestAddress p = obj as TestAddress;
            if ((object)p == null)
            {
                return false;
            }
            if (this.address == p.address)
                return true;
            if ((this.address == null) || (p.address == null))
            {
                return false;
            }
            if ((this.address.StringAddress == p.address.StringAddress)&&
            (Global.IsEqual (this.addressType, p.addressType)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestAddress");
                return false;
            }
        }
        public static bool operator ==(TestAddress a, TestAddress b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestAddress a, TestAddress b)
        {
            return !(a.Equals(b));
        }
    }
}
