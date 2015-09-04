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
        //подумать как сделать лучше!!
        public Coding SetCoding(Coding cod)
        {
            cod.Version = null;
            return cod;
        }

        public Referral MinRegister()
        {
            Referral referral = new Referral();
            referral.ReferralInfo = new ReferralInfo
            {
                ReferralType = SetCoding(ReferralData.referralInfo.ReferralType),
                ProfileMedService = SetCoding(ReferralData.referralInfo.ProfileMedService),
            };

            referral.Source = new ReferralSource
            {
                IdReferralMis = ReferralData.referralSource.IdReferralMis,
                Lpu = SetCoding(ReferralData.referralSource.Lpu),
                Doctors = new Doctor[] 
                 {
                     new Doctor
                     {
                           Role = SetCoding(PersonData.doctor.Role),
                           Lpu =  SetCoding(PersonData.doctor.Lpu),
                           Speciality = SetCoding(PersonData.doctor.Speciality),
                           Position = SetCoding(PersonData.doctor.Position),
                           Person = new Person
                           {
                                Sex = SetCoding(PersonData.doctor.Person.Sex),
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
                    Sex = SetCoding(PersonData.patient.Person.Sex),
                    HumanName = new HumanName
                    {
                        FamilyName = PersonData.patient.Person.HumanName.FamilyName,
                        GivenName = PersonData.patient.Person.HumanName.GivenName
                    }
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

        public Referral MinAgreedFromSourcedMo()
        {
            Referral referral = new Referral();

            referral.ReferralInfo = new ReferralInfo
            {
                IdMq = ReferralData.referralInfo.IdMq,
            };

            referral.Source = new ReferralSource
            {
                Doctors = new Doctor[] 
                 {
                     new Doctor
                     {
                           Lpu =  PersonData.doctor.Lpu,
                           Speciality = PersonData.doctor.Speciality,
                           Position = PersonData.doctor.Position,
                           Person = new Person
                           {
                                IdPersonMis = PersonData.doctor.Person.IdPersonMis,
                                HumanName = new HumanName
                                {
                                     FamilyName = PersonData.doctor.Person.HumanName.FamilyName,
                                     GivenName = PersonData.doctor.Person.HumanName.GivenName
                                }
                           },
                           ContactDtos = new ContactDto[]
                           {
                                new  ContactDto
                                {
                                     ContactType = SetCoding(PersonData.contact.ContactType),
                                     ContactValue = PersonData.contact.ContactValue
                                }
                           }
                     }
                 }
            };

            referral.EventsInfo = new EventsInfo
            {
                Source = new EventSource
                {
                    ReferralCreateDate = ReferralData.eventsInfo.Source.ReferralCreateDate,
                    IsReferralReviewed = ReferralData.eventsInfo.Source.IsReferralReviewed,
                    ReferralReviewDate = ReferralData.eventsInfo.Source.ReferralReviewDate
                }
            };

            return referral;
        }


        public Referral FullAgreedFromSourcedMo()
        {
            return ReferralData.referral = new Referral
            {
                ReferralInfo = new ReferralInfo
                {
                    Priority = ReferralData.referralInfo.Priority,
                    Date = ReferralData.referralInfo.Date,
                    Reason = ReferralData.referralInfo.Reason,
                    Comment = ReferralData.referralInfo.Comment,
                    IdMq = ReferralData.referralInfo.IdMq
                },
                EventsInfo = new EventsInfo
                {
                    Source = ReferralData.eventsInfo.Source
                },
                Source = new ReferralSource
                {
                    Doctors = new Doctor[] { PersonData.doctor }
                }
            };
        }



    }
}
