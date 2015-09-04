using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MqTests.WebReference;

namespace MqTests
{
    public class SetData
    {
        public Referral MinRegister()
        {
            Referral referral = new Referral();
            referral.ReferralInfo = new ReferralInfo
            {
                ReferralType = ReferralData.referralInfo.ReferralType,
                ProfileMedService = ReferralData.referralInfo.ProfileMedService,
            };

            referral.Source = new ReferralSource
            {
                IdReferralMis = ReferralData.referralSource.IdReferralMis,
                Lpu = ReferralData.referralSource.Lpu,
                Doctors = new Doctor[] 
                 {
                     new Doctor
                     {
                           Role = PersonData.doctor.Role,
                           Lpu =  PersonData.doctor.Lpu,
                           Speciality = PersonData.doctor.Speciality,
                           Position = PersonData.doctor.Position,
                           Person = new Person
                           {
                                BirthDate = PersonData.doctor.Person.BirthDate,
                                Sex = PersonData.doctor.Person.Sex,
                                IdPersonMis = PersonData.doctor.Person.IdPersonMis,
                                HumanName = new HumanName
                                {
                                     FamilyName = PersonData.doctor.Person.HumanName.FamilyName,
                                     GivenName = PersonData.doctor.Person.HumanName.GivenName
                                }
                           }
                     }
                 }
            };
            referral.Patient = new Patient
            {
                Person = new Person
                {
                    BirthDate = PersonData.patient.Person.BirthDate,
                    Sex = PersonData.patient.Person.Sex,
                    HumanName = PersonData.patient.Person.HumanName,
                }
            };

            return referral;
        }

        public Referral FullRegister()
        {
            Referral referral = ReferralData.referral;
            referral.ReferralInfo.IdMq = null;
            return referral;
        }

     /*   public ReferralData MinAgreedFromSourcedMo()
        {
            Referral referral = new Referral();
            




            return referral;
        }*/

    }
}
