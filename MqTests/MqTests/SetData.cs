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
            //    referral.ReferralInfo.IdMq = null;
            referral.EventsInfo.Cancellation = null;
            return referral;
        }

        public Referral MinAgreedFromSourcedMo(string idMq)
        {
            Referral referral = new Referral();

            referral.ReferralInfo = new ReferralInfo { IdMq = idMq };

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

        public Referral FullAgreedFromSourcedMo(string idMq)
        {
            return ReferralData.referral = new Referral
            {
                ReferralInfo = new ReferralInfo
                {
                    Priority = ReferralData.referralInfo.Priority,
                    Date = ReferralData.referralInfo.Date,
                    Reason = ReferralData.referralInfo.Reason,
                    Comment = ReferralData.referralInfo.Comment,
                    IdMq = idMq
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

        public Referral MinPatientDocumentIssue(string idMq)
        {
            return ReferralData.referral = new Referral
            {
                ReferralInfo = new ReferralInfo
                {
                    Date = ReferralData.referralInfo.Date,
                    IdMq = idMq
                },
                EventsInfo = new EventsInfo
                {
                    Source = new EventSource
                    {
                        ReferralCreateDate = ReferralData.eventsInfo.Source.ReferralCreateDate,
                        ReferralOutDate = ReferralData.eventsInfo.Source.ReferralOutDate
                    }
                }
            };
        }

        public Referral FullPatientDocumentIssue(string idMq)
        {
            Referral referral = MinPatientDocumentIssue(idMq);
            referral.EventsInfo.Source.PlannedDate = ReferralData.eventsInfo.Source.PlannedDate;
            return referral;
        }

        public Referral MinCancellation(string idMq)
        {
            return ReferralData.referral = new Referral
            {
                ReferralInfo = new ReferralInfo { IdMq = idMq },
                EventsInfo = new EventsInfo
                {
                    Cancellation = new CancellationData
                    {
                        Date = ReferralData.eventsInfo.Cancellation.Date,
                        CancellationReason = SetCoding(ReferralData.eventsInfo.Cancellation.CancellationReason),
                        CancellationSource = SetCoding(ReferralData.eventsInfo.Cancellation.CancellationSource)
                    }
                },
            };
        }

        public Referral FullCancellation(string idMq)
        {
            return ReferralData.referral = new Referral
            {
                ReferralInfo = new ReferralInfo { IdMq = idMq },
                EventsInfo = new EventsInfo { Cancellation = ReferralData.eventsInfo.Cancellation },
            };
        }

        public Referral MinAgreedFromTargetMo(string idMq)
        {
            return ReferralData.referral = new Referral
            {
                ReferralInfo = new ReferralInfo { IdMq = idMq },
                EventsInfo = new EventsInfo
                {
                    Target = new EventTarget
                    {
                        IsReferralReviwed = ReferralData.eventsInfo.Target.IsReferralReviwed,
                        ReferralReviewDate = ReferralData.eventsInfo.Target.ReferralReviewDate

                    }
                }
            };
        }

        public Referral FullAgreedFromTargetMo(string idMq)
        {
            return ReferralData.referral = new Referral
            {
                ReferralInfo = new ReferralInfo { IdMq = idMq },
                EventsInfo = new EventsInfo { Target = ReferralData.eventsInfo.Target }
            };
        }

    }
}
