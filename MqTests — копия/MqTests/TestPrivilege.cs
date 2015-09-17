using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MqTests
{
    class TestPrivilege
    {
        public Privilege privilege;
        public TestCoding privilegeType;

        public TestPrivilege(Privilege p)
        {
            privilege = p;
            if (privilege.PrivilegeType != null)
                privilegeType = new TestCoding(privilege.PrivilegeType);
        }

        static public List<TestPrivilege> BuildPrivilegeFromDataBaseData(string idPerson)
        {
            List<TestPrivilege> priveleges = new List<TestPrivilege>();
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findPriviledge = "SELECT * FROM public.priviledge WHERE id_person = '" + idPerson + "'";
                NpgsqlCommand person = new NpgsqlCommand(findPriviledge, connection);
                using (NpgsqlDataReader contactReader = person.ExecuteReader())
                {
                    while (contactReader.Read())
                    {
                        //что делать с DateSpecified?
                        Privilege priv = new Privilege();
                        if (contactReader["date_start"] != DBNull.Value)
                            priv.StartDate = Convert.ToDateTime(contactReader["date_start"]);
                        if (contactReader["date_end"] != DBNull.Value)
                            priv.EndDate = Convert.ToDateTime(contactReader["date_end"]);
                        TestPrivilege privilege = new TestPrivilege(priv);
                        if (contactReader["id_priviledge_code"] != DBNull.Value)
                            privilege.privilegeType = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(contactReader["id_priviledge_code"]));
                        priveleges.Add(privilege);
                    }
                }
            }
            return (priveleges.Count != 0) ? priveleges : null;
        }

        public void FindMismatch(TestPrivilege b)
        {
            if (b.privilege != null)
            {
                if (this.privilege.EndDate != b.privilege.EndDate)
                    Global.errors3.Add("Несовпадение EndDate TestPrivilege");
                if (this.privilege.StartDate != b.privilege.StartDate)
                    Global.errors3.Add("Несовпадение StartDate TestPrivilege");
                if (Global.GetLength(this.privilegeType) != Global.GetLength(b.privilegeType))
                    Global.errors3.Add("Несовпадение IdPrivilageType TestPrivilege");
            }
        }

        public override bool Equals(Object obj)
        {
            TestPrivilege p = obj as TestPrivilege;
            if (p == null)
            {
                return false;
            }
            if ((this.privilege.EndDate != p.privilege.EndDate) &&
            (this.privilege.StartDate != p.privilege.StartDate) &&
            Global.IsEqual(this.privilegeType, p.privilegeType))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestPrivilege");
                return false;
            }
        }
        public static bool operator ==(TestPrivilege a, TestPrivilege b)
        {
            return Equals(a, b);
        }
        public static bool operator !=(TestPrivilege a, TestPrivilege b)
        {
            return !Equals(a, b);
        }
    }
}
