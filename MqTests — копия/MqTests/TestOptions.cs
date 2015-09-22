using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqTests
{
    class TestOptions
    {
        static private List<string> GetCodindId(Coding c)
        {
            List<string> l = new List<string>();
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string p = "SELECT id_terminology_value FROM public.terminology_value WHERE code = '" + c.Code + "' AND (uri = '" + c.System + "'";
                if (c.Version != null)
                    p += " AND version = '" + c.Version + "'";
                p += ")";
                NpgsqlCommand person = new NpgsqlCommand(p, connection);
                using (NpgsqlDataReader documentReader = person.ExecuteReader())
                {
                    while (documentReader.Read())
                    {
                        l.Add(documentReader["id_terminology_value"].ToString());
                    }
                }
            }
            if (l.Count != 0)
                return l;
            else
                throw new Exception("Не надена запись");
        }

        static private List<string> GetReferralIdWithThisIqStatus(List<string> codingId, string idReferral)
        {
            string c = "";
            List<string> l = new List<string>();
            if (idReferral != null)
                c = "SELECT id_referral FROM public.referral_status WHERE id_referral = '" + idReferral + "' AND (id_mq_referral_status = '" + codingId[0] + "'";
            else
                c = "SELECT id_referral FROM public.referral_status WHERE (id_mq_referral_status = '" + codingId[0] + "'";
            for (int i = 1; i < codingId.Count; ++i)
            {
                c += " OR id_mq_referral_status = '" + codingId[i] + "'";
            }
            c += ")";
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                NpgsqlCommand person = new NpgsqlCommand(c, connection);
                using (NpgsqlDataReader documentReader = person.ExecuteReader())
                {
                    while (documentReader.Read())
                    {
                        l.Add(documentReader["id_referral"].ToString());
                    }
                }
            }
            if (l.Count != 0)
                return l;
            else
                throw new Exception("Не надена запись");
        } 
        static private List<string> GetPribiledgeId(Privilege p)
        {
            List<string> idCode = GetCodindId(p.PrivilegeType);
            List<string> l = new List<string>();
            if (idCode != null)
            {
                string c = "SELECT id_person FROM public.priviledge WHERE (id_priviledge_code = '" + l[0] + "'";
                for (int i = 1; i < l.Count; ++i)
                {
                    c += " OR id_priviledge_code = '" + l[i] + "'";
                }
                c += ")";
                using (NpgsqlConnection connection = Global.GetSqlConnection())
                {
                    NpgsqlCommand person = new NpgsqlCommand(c, connection);
                    using (NpgsqlDataReader documentReader = person.ExecuteReader())
                    {
                        while (documentReader.Read())
                        {
                            l.Add(documentReader["id_person"].ToString());
                        }
                    }
                }
            }
            if (l.Count != 0)
                return l;
            else
                throw new Exception("Не надена запись");
        }
        static private List<string> GetPatientId(Patient p)
        {
            List<string> l = new List<string>();
            string com = "";
            if (p.Privileges != null)
            {
                List<string> correctId = new List<string>();
                foreach (Privilege i in p.Privileges)
                    correctId.AddRange(GetPribiledgeId(i));
                com += "SELECT id_person FROM public.person, public.referral WHERE public.person.id_person = public.referral.id_patient_person AND (id_person = '" + correctId[0] + "'";
                for (int i = 1; i < correctId.Count; ++i)
                    com += " OR id_person = '" + correctId[i] + "'";
                com += ")";
            }
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                if (p.Person != null)
                    if (com == "")
                        com = "SELECT id_person FROM public.person, public.referral WHERE public.person.id_person = public.referral.id_patient_person AND public.person.birthday = '" + p.Person.BirthDate.Value.ToString("o") + "' AND id_patient_source_mo_mis = '" + p.Person.IdPatientMis + "'";
                    else
                        com +=  " AND birthday = '" + p.Person.BirthDate.Value.ToString("o") + "' AND id_patient_source_mo_mis = '" + p.Person.IdPatientMis + "'";
                if (p.Person.HumanName != null)
                {
                    com += " AND family_name = '" + p.Person.HumanName.FamilyName + "' AND given_name = '" + p.Person.HumanName.GivenName + "'";
                    if (p.Person.HumanName.MiddleName != null)
                        com += " AND middle_name = '" + p.Person.HumanName.MiddleName + "'";
                }
                NpgsqlCommand person = new NpgsqlCommand(com, connection);
                using (NpgsqlDataReader documentReader = person.ExecuteReader())
                {
                    while (documentReader.Read())
                    {
                        l.Add(documentReader["id_person"].ToString());
                    }
                }
            }
            if (l.Count != 0)
                return l;
            else
                throw new Exception("Не найдена запись");
        }
        static public List<string> GetReferralId(Options o)
        {
            string command = "";
            if (o.IdMq != null)
                command = "SELECT id_referral FROM public.referral WHERE id_referral = '" + o.IdMq + "'";
            if (o.ReferralInfo != null)
            {
                if (o.ReferralInfo.Date != null)
                    if (command == "")
                        command = "SELECT id_referral FROM public.referral WHERE referral_paper_date = '" + o.ReferralInfo.Date + "'";
                    else
                        command += " AND referral_paper_date = '" + o.ReferralInfo.Date + "'";
                if (o.ReferralInfo.ProfileMedService != null)
                {
                    List<string> l = GetCodindId(o.ReferralInfo.ProfileMedService);
                    if (command == "")
                        command = "SELECT id_referral FROM public.referral WHERE (id_profile_med_service = '" + l[0] + "'";
                    else
                        command += " AND (id_profile_med_service = '" + l[0] + "'";
                    for (int i = 1; i < l.Count; ++i)
                        command += " OR id_profile_med_service '" + l[i] + "'";
                    command += ")";
                }
                if (o.ReferralInfo.MqReferralStatus != null)
                {                    
                    List<string> l = GetReferralIdWithThisIqStatus(GetCodindId(o.ReferralInfo.MqReferralStatus), o.IdMq);
                    if (command == "")
                        command = "SELECT id_referral FROM public.referral WHERE (id_referral = '" + l[0] + "'";
                    else
                        command += " AND (id_referral = '" + l[0] + "'";
                    for (int i = 1; i < l.Count; ++i)
                        command += " OR id_referral '" + l[i] + "'";
                    command += ")";
                }
            }
            if (o.Target != null)
            {
                if (o.Target.Lpu != null)
                {
                    List<string> l = GetCodindId(o.Target.Lpu);
                    if (command == "")
                        command = "SELECT id_referral FROM public.referral WHERE (id_target_lpu = '" + l[0] + "'";
                    else
                        command += " AND (id_target_lpu = '" + l[0] + "'";
                    for (int i = 1; i < l.Count; ++i)
                        command += " OR id_target_lpu = '" + l[i] + "'";
                    command += ")";
                }
            }
            if (o.Source != null)
            {
                if (o.Source.Lpu != null)
                {
                    List<string> l = GetCodindId(o.Source.Lpu);
                    if (command == "")
                        command = "SELECT id_referral FROM public.referral WHERE (id_source_lpu = '" + l[0] + "'";
                    else
                        command += " AND (id_source_lpu = '" + l[0] + "'";
                    for (int i = 1; i < l.Count; ++i)
                        command += " OR id_source_lpu = '" + l[i] + "'";
                    command += ")";
                }
            }
            if (o.Survey != null)
            {
                if (o.Survey.SurveyType != null)
                {
                    List<string> l = GetCodindId(o.Survey.SurveyType);
                    if (command == "")
                        command = "SELECT id_referral FROM public.referral WHERE (id_survey_type = '" + l[0] + "'";
                    else
                        command += " AND (id_survey_type = '" + l[0] + "'";
                    for (int i = 1; i < l.Count; ++i)
                        command += " OR id_survey_type = '" + l[i] + "'";
                    command += ")";
                }
                if (o.Survey.SurveyOrgan != null)
                {
                    List<string> l = GetCodindId(o.Survey.SurveyOrgan);
                    if (command == "")
                        command = "SELECT id_referral FROM public.referral WHERE (id_survey_organ = '" + l[0] + "'";
                    else
                        command += " AND (id_survey_organ = '" + l[0] + "'";
                    for (int i = 1; i < l.Count; ++i)
                        command += " OR id_survey_organ = '" + l[i] + "'";
                    command += ")";
                }
            }
            if (o.Patient != null)
            {
                List<string> l = GetPatientId(o.Patient);
                if (command == "")
                    command = "SELECT id_referral FROM public.referral WHERE (id_patient_person = '" + l[0] + "'";
                else
                    command += " AND (id_patient_person = '" + l[0] + "'";
                for (int i = 1; i < l.Count; ++i)
                    command += " OR id_patient_person = '" + l[i] + "'";
                command += ")";
            }
            if (o.EventsInfo != null)
            {
                if (o.EventsInfo.Target != null)
                {
                    if (o.EventsInfo.Target.IsReferralReviwed != null)
                        if (command == "")
                            command = "SELECT id_referral FROM public.referral WHERE is_referral_review_target_mo = '" + o.EventsInfo.Target.IsReferralReviwed + "'";
                        else
                            command += " AND is_referral_review_target_mo = '" + o.EventsInfo.Target.IsReferralReviwed + "'";
                    if (o.EventsInfo.Target.ReceptionAppointDate != null)
                        if (command == "")
                            command = "SELECT id_referral FROM public.referral WHERE reception_appoint_time_date = '" + o.EventsInfo.Target.ReceptionAppointDate + "'";
                        else
                            command += " AND reception_appoint_time_date = '" + o.EventsInfo.Target.ReceptionAppointDate + "'";
                }
            }
            List<string> referralId = new List<string>();
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                try
                {
                    NpgsqlCommand person = new NpgsqlCommand(command, connection);
                    using (NpgsqlDataReader documentReader = person.ExecuteReader())
                    {
                            while (documentReader.Read())
                            {
                                referralId.Add(documentReader["id_referral"].ToString());
                            }
                            return referralId;
                    }
                }
                catch (Exception e)
                {
                    if (e.Message == "Не надена запись")
                        return null;
                    else 
                        throw e;
                }
            }
        }
    }
}
