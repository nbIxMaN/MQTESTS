using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqTests
{
    class TestProfileMedService
    {
        public ProfileMedService profileMedService;
        public TestCoding idProfileMedService;

        public TestProfileMedService(ProfileMedService j)
        {
            profileMedService = j;
        }
        static public TestProfileMedService BuildProfileMedServiceFromDataBaseData(string idLPU)
        {
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findProfileMedService = "SELECT * FROM public.lpu_profile WHERE id_lpu = '" + idLPU + "'";
                NpgsqlCommand person = new NpgsqlCommand(findProfileMedService, connection);
                using (NpgsqlDataReader ProfileMedServiceReader = person.ExecuteReader())
                {
                    while (ProfileMedServiceReader.Read())
                    {
                        ProfileMedService profileMedService = new ProfileMedService();
                        if (ProfileMedServiceReader["address"] != DBNull.Value)
                            profileMedService.Address = Convert.ToString(ProfileMedServiceReader["address"]);
                        if (ProfileMedServiceReader["comment"] != DBNull.Value)
                            profileMedService.Comment = Convert.ToString(ProfileMedServiceReader["comment"]);
                        if (ProfileMedServiceReader["contact_value"] != DBNull.Value)
                            profileMedService.ContactValue = Convert.ToString(ProfileMedServiceReader["contact_value"]);
                        if (ProfileMedServiceReader["date_end"] != DBNull.Value)
                            profileMedService.EndDate = Convert.ToDateTime(ProfileMedServiceReader["date_end"]);
                        if (ProfileMedServiceReader["site"] != DBNull.Value)
                            profileMedService.Site = Convert.ToString(ProfileMedServiceReader["site"]);
                        if (ProfileMedServiceReader["date_begin"] != DBNull.Value)
                            profileMedService.StartDate = Convert.ToDateTime(ProfileMedServiceReader["date_begin"]);
                        TestProfileMedService tProfileMedService = new TestProfileMedService(profileMedService);
                        if (ProfileMedServiceReader["id_profile_med_service"] != DBNull.Value)
                            tProfileMedService.idProfileMedService = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(ProfileMedServiceReader["id_profile_med_service"]));
                       return tProfileMedService;
                    }
                }
            }
            return null;
        }

        private void FindMismatch(TestProfileMedService b)
        {
            if (b.profileMedService != null)
            {
                if (this.profileMedService.Address != b.profileMedService.Address)
                    Global.errors3.Add("Несовпадение Address TestProfileMedService");
                if (this.profileMedService.Comment != b.profileMedService.Comment)
                    Global.errors3.Add("Несовпадение Comment TestProfileMedService");
                if (this.profileMedService.ContactValue != b.profileMedService.ContactValue)
                    Global.errors3.Add("Несовпадение ContactValue TestProfileMedService");
                if (this.profileMedService.EndDate != b.profileMedService.EndDate)
                    Global.errors3.Add("Несовпадение EndDate TestProfileMedService");
                if (this.profileMedService.Site != b.profileMedService.Site)
                    Global.errors3.Add("Несовпадение Site TestProfileMedService");
                if (this.profileMedService.StartDate != b.profileMedService.StartDate)
                    Global.errors3.Add("Несовпадение StartDate TestProfileMedService");
                if (Global.GetLength(this.idProfileMedService) != Global.GetLength(b.idProfileMedService))
                    Global.errors3.Add("Несовпадение длинны idProfileMedService TestProfileMedService");
            }
        }

        public override bool Equals(Object obj)
        {
            TestProfileMedService p = obj as TestProfileMedService;
            if (p == null)
            {
                return false;
            }
            if ((this.profileMedService.Address == p.profileMedService.Address)&&
            (this.profileMedService.Comment == p.profileMedService.Comment)&&
            (this.profileMedService.ContactValue == p.profileMedService.ContactValue)&&
            (this.profileMedService.EndDate == p.profileMedService.EndDate)&&
            (this.profileMedService.Site == p.profileMedService.Site)&&
            (this.profileMedService.StartDate == p.profileMedService.StartDate)&&
            (Global.IsEqual(this.idProfileMedService, p.idProfileMedService)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestProfileMedService");
                return false;
            }
        }
        public static bool operator ==(TestProfileMedService a, TestProfileMedService b)
        {
            return Equals(a, b);
        }
        public static bool operator !=(TestProfileMedService a, TestProfileMedService b)
        {
            return !Equals(a, b);
        }

    }
}
