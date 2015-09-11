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
            //cod.Version = null;
            return cod;
        }

        /// <summary>
        /// По заданному Referral'у строит Options для SeacrhOne.
        /// 
        /// Может можно сделать лучше?
        /// 
        /// </summary>
        public Options GetRefferalReturnOptions_SearchOne(Referral referral, Privilege[] privileges, string idMq, Coding mqReferralStatus)
        {
            Options opt = new Options();
            opt.IdMq = idMq;

            if (referral.ReferralInfo != null)
            {
                opt.ReferralInfo = new ReferralInfo
                {
                    ProfileMedService = referral.ReferralInfo.ProfileMedService,
                    ReferralType = referral.ReferralInfo.ReferralType,
                    MqReferralStatus = mqReferralStatus
                };
            }
            if (referral.Target != null)
            { opt.Target = new ReferralTarget { Lpu = referral.Target.Lpu }; }

            if (referral.Source != null)
            { opt.Source = new ReferralSource { Lpu = referral.Source.Lpu }; }

            if (referral.ReferralSurvey != null)
            {
                opt.Survey = new Survey
                {
                    SurveyType = referral.ReferralSurvey.SurveyType,
                    SurveyOrgan = referral.ReferralSurvey.SurveyOrgan
                };
            }

            if (referral.Patient != null)
            {
                opt.Patient.Person = new Person
                {
                    BirthDate = referral.Patient.Person.BirthDate,
                    IdPatientMis = referral.Patient.Person.IdPatientMis
                };

                if (referral.Patient.Person.HumanName != null)
                {
                    opt.Patient.Person.HumanName = new HumanName
                    {
                        FamilyName = referral.Patient.Person.HumanName.FamilyName,
                        GivenName = referral.Patient.Person.HumanName.GivenName,
                        MiddleName = referral.Patient.Person.HumanName.MiddleName
                    };
                }
            }

            if (privileges != null)
            { opt.Patient.Privileges = (Privilege[])privileges.Clone(); }

            if (referral.EventsInfo != null && referral.EventsInfo.Target != null)
            {
                opt.EventsInfo.Target = new EventTarget
                {
                    IsReferralReviwed = referral.EventsInfo.Target.IsReferralReviwed,
                    ReceptionAppointDate = referral.EventsInfo.Target.ReceptionAppointDate
                };
            }

            OptionData.options = opt;
            return opt;
        }

        /// <summary>
        /// По заданному Referral'у строит Options для SeacrhMany.
        /// </summary>
        public Options GetRefferalReturnOptions_SearchMany(Referral referral, Privilege[] privileges, Coding mqReferralStatus)
        {
            Options opt = GetRefferalReturnOptions_SearchOne(referral, privileges, null, mqReferralStatus);

            if (referral.EventsInfo != null && referral.EventsInfo.Source != null)
            {
                opt.EventsInfo.Source = new EventSource
                {
                    PlannedDate = referral.EventsInfo.Source.PlannedDate
                };
            }

            OptionData.options = opt;
            return opt;
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
            referral.Target = new ReferralTarget
            {
                IdCaseMis = ReferralData.referralTarget.IdCaseMis,
                Lpu = ReferralData.referralTarget.Lpu
            };
            referral.EventsInfo = new EventsInfo
            {
                Source = ReferralData.eventsInfo.Source,
                Target = new EventTarget
                {
                    IsReferralReviwed = ReferralData.eventsInfo.Target.IsReferralReviwed,
                    ReferralReviewDate = ReferralData.eventsInfo.Target.ReferralReviewDate,
                    ReceptionAppointComment = ReferralData.eventsInfo.Target.ReceptionAppointComment,
                    ReceptionAppointDate = ReferralData.eventsInfo.Target.ReceptionAppointDate,
                    ReceptionAppointTime = ReferralData.eventsInfo.Target.ReceptionAppointTime
                }
            };
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
            return new Referral
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
            return new Referral
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
            return new Referral
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
            return new Referral
            {
                ReferralInfo = new ReferralInfo { IdMq = idMq },
                EventsInfo = new EventsInfo { Cancellation = ReferralData.eventsInfo.Cancellation },
            };
        }

        public Referral MinAgreedFromTargetMo(string idMq)
        {
            return new Referral
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
            return new Referral
            {
                ReferralInfo = new ReferralInfo { IdMq = idMq },
                EventsInfo = new EventsInfo
                {
                    Target = new EventTarget
                    {
                        IsReferralReviwed = ReferralData.eventsInfo.Target.IsReferralReviwed,
                        ReferralReviewDate = ReferralData.eventsInfo.Target.ReferralReviewDate,
                        ReceptionAppointComment = ReferralData.eventsInfo.Target.ReceptionAppointComment,
                        ReceptionAppointDate = ReferralData.eventsInfo.Target.ReceptionAppointDate,
                        ReceptionAppointTime = ReferralData.eventsInfo.Target.ReceptionAppointTime
                    }
                }
            };
        }

        public ProfileMedService MinUpdateMedServiceProfile()
        {
            return new ProfileMedService
            {
                IdProfileMedService = SetCoding(ReferralData.profileMedService.IdProfileMedService)
            };
        }

        public ProfileMedService FullUpdateMedServiceProfile()
        {
            return ReferralData.profileMedService;
        }

        public Referral MinUpdateFromSourcedMo(string idMq)
        {
            return new Referral { ReferralInfo = new ReferralInfo { IdMq = idMq } };
        }

        public Referral FullUpdateFromSourcedMo(string idMq)
        {
            Referral referral = new Referral();
            referral.ReferralInfo = ReferralData.referralInfo;
            referral.ReferralInfo.IdMq = idMq;
            referral.ReferralSurvey = ReferralData.survey;
            referral.Source = ReferralData.referralSource;
            referral.Target = new ReferralTarget
            {
                Lpu = ReferralData.referralTarget.Lpu
            };
            referral.Patient = PersonData.patient;
            referral.EventsInfo = new EventsInfo
            {
                Source = ReferralData.eventsInfo.Source
            };
            return referral;
        }

        //следить за изменениями в  Data.options!
        public Options FullGetQueueInfo()
        {
            return OptionData.options;
        }

        public Referral MinUpdateFromTargetMO(string idMq)
        {
            return new Referral { ReferralInfo = new ReferralInfo { IdMq = idMq } };
        }

        public Referral FullUpdateFromTargetMO(string idMq)
        {
            return new Referral
            {
                ReferralInfo = new ReferralInfo
                {
                    IdMq = idMq,
                    ProfileMedService = ReferralData.referralInfo.ProfileMedService,
                },
                ReferralSurvey = ReferralData.survey,
                Target = new ReferralTarget
                {
                    IdCaseMis = ReferralData.referralTarget.IdCaseMis,
                    Lpu = ReferralData.referralTarget.Lpu,
                    Doctors = ReferralData.referralTarget.Doctors,
                    MainDiagnosis = ReferralData.referralTarget.MainDiagnosis
                },
                Patient = new Patient
                {
                    Addresses = PersonData.patient.Addresses,
                    ContactDtos = PersonData.patient.ContactDtos,
                    Jobs = PersonData.patient.Jobs,
                    Privileges = PersonData.patient.Privileges,
                    Documents = PersonData.patient.Documents
                },
                EventsInfo = new EventsInfo
                {
                    Target = new EventTarget
                    {
                        IsReferralReviwed = ReferralData.eventsInfo.Target.IsReferralReviwed,
                        ReferralReviewDate = ReferralData.eventsInfo.Target.ReferralReviewDate,
                        ReceptionAppointDate = ReferralData.eventsInfo.Target.ReceptionAppointDate,
                        ReceptionAppointTime = ReferralData.eventsInfo.Target.ReceptionAppointTime,
                        ReceptionAppointComment = ReferralData.eventsInfo.Target.ReceptionAppointComment,
                        CaseOpenDate = ReferralData.eventsInfo.Target.CaseOpenDate,
                        CaseCloseDate = ReferralData.eventsInfo.Target.CaseCloseDate,
                        CaseAidType = ReferralData.eventsInfo.Target.CaseAidType,
                        CaseAidForm = ReferralData.eventsInfo.Target.CaseAidForm,
                        CaseAidPlace = ReferralData.eventsInfo.Target.CaseAidPlace
                    }
                }

            };
        }

        public Referral MinChangePlannedResource(string idMq)
        {
            return new Referral
            {
                ReferralInfo = new ReferralInfo { IdMq = idMq },
                EventsInfo = new EventsInfo
                {
                    Target = new EventTarget
                    {
                        ReceptionAppointTime = ReferralData.eventsInfo.Target.ReceptionAppointTime,
                        ReceptionAppointDate = ReferralData.eventsInfo.Target.ReceptionAppointDate
                    }
                }
            };
        }

        public Referral FullChangePlannedResource(string idMq)
        {
            Referral referral = MinChangePlannedResource(idMq);
            referral.EventsInfo.Target.ReceptionAppointComment = ReferralData.eventsInfo.Target.ReceptionAppointComment;
            return referral;
        }

        public Referral MinHealthCareStart(string idMq)
        {
            return new Referral
            {
                ReferralInfo = new ReferralInfo { IdMq = idMq },
                Target = new ReferralTarget
                {
                    Lpu = SetCoding(ReferralData.referralTarget.Lpu),
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
                    },
                    MainDiagnosis = new MainDiagnosis[]
                    {
                        new   MainDiagnosis
                        {
                            // version задаётся
                            ComplicationDiagnosis = new DiagnosisInfo[] { DiagnosisData.complicationDiagnosis },
                            DiagnosisInfo = DiagnosisData.diagnosisInfo
                        }
                    }
                },
                EventsInfo = new EventsInfo
                {
                    Target = new EventTarget
                    {
                        CaseOpenDate = ReferralData.eventsInfo.Target.CaseOpenDate
                    }
                }
            };
        }

        public Referral FullHealthCareStart(string idMq)
        {
            return new Referral
            {
                ReferralInfo = new ReferralInfo { IdMq = idMq },
                Target = new ReferralTarget
                {
                    IdCaseMis = ReferralData.referralTarget.IdCaseMis,
                    Lpu = ReferralData.referralTarget.Lpu,
                    Doctors = ReferralData.referralTarget.Doctors,
                    MainDiagnosis = ReferralData.referralTarget.MainDiagnosis
                },
                Patient = new Patient
                {
                    Addresses = ReferralData.referral.Patient.Addresses,
                    ContactDtos = ReferralData.referral.Patient.ContactDtos,
                    Jobs = ReferralData.referral.Patient.Jobs,
                    Privileges = ReferralData.referral.Patient.Privileges,
                    Documents = ReferralData.referral.Patient.Documents,
                },
                EventsInfo = new EventsInfo
                {
                    Target = new EventTarget
                    {
                        CaseOpenDate = ReferralData.eventsInfo.Target.CaseOpenDate,
                        CaseAidForm = ReferralData.eventsInfo.Target.CaseAidForm,
                        CaseAidType = ReferralData.eventsInfo.Target.CaseAidType,
                        CaseAidPlace = ReferralData.eventsInfo.Target.CaseAidPlace
                    }

                }
            };
        }

        public Referral MinHealthCareEnd(string idMq)
        {
            return new Referral
            {
                ReferralInfo = new ReferralInfo { IdMq = idMq },
                Target = new ReferralTarget
                {
                    Lpu = SetCoding(ReferralData.referralTarget.Lpu),
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
                           },
                            ContactDtos = new ContactDto[] { PersonData.contact }
                        }
                    },
                    MainDiagnosis = new MainDiagnosis[]
                    {
                        new   MainDiagnosis
                        {
                            // version задаётся
                            ComplicationDiagnosis = new DiagnosisInfo[] { DiagnosisData.complicationDiagnosis },
                            DiagnosisInfo = DiagnosisData.diagnosisInfo
                        }
                    }
                },
                EventsInfo = new EventsInfo
                {
                    Target = new EventTarget
                    {
                        CaseOpenDate = ReferralData.eventsInfo.Target.CaseOpenDate,
                    }
                }
            };
        }

        public Referral FullHealthCareEnd(string idMq)
        {
            return new Referral
            {
                ReferralInfo = new ReferralInfo { IdMq = idMq },
                Target = new ReferralTarget
                {
                    IdCaseMis = ReferralData.referralTarget.IdCaseMis,
                    Lpu = ReferralData.referralTarget.Lpu,
                    Doctors = ReferralData.referralTarget.Doctors,
                    MainDiagnosis = ReferralData.referralTarget.MainDiagnosis
                },
                Patient = new Patient
                {
                    Addresses = ReferralData.referral.Patient.Addresses,
                    ContactDtos = ReferralData.referral.Patient.ContactDtos,
                    Jobs = ReferralData.referral.Patient.Jobs,
                    Privileges = ReferralData.referral.Patient.Privileges,
                    Documents = ReferralData.referral.Patient.Documents,
                },
                EventsInfo = new EventsInfo
                {
                    Target = new EventTarget
                    {
                        CaseOpenDate = ReferralData.eventsInfo.Target.CaseOpenDate,
                        CaseAidForm = ReferralData.eventsInfo.Target.CaseAidForm,
                        CaseAidType = ReferralData.eventsInfo.Target.CaseAidType,
                        CaseAidPlace = ReferralData.eventsInfo.Target.CaseAidPlace
                    }

                }
            };
        }
        public Referral MinSetOrChangeTargetMO(string idMq)
        {
            return new Referral
            {
                ReferralInfo = new ReferralInfo { IdMq = idMq },
                Target = new ReferralTarget { Lpu = SetCoding(ReferralData.referralTarget.Lpu) }
            };
        }

        public Referral FullSetOrChangeTargetMO(string idMq)
        {
            return new Referral
            {
                ReferralInfo = new ReferralInfo
                {
                    IdMq = idMq,
                    ReferralType = ReferralData.referralInfo.ReferralType,
                    ProfileMedService = ReferralData.referralInfo.ProfileMedService
                },
                ReferralSurvey = new Survey
                {
                    SurveyOrgan = ReferralData.survey.SurveyOrgan,
                    SurveyType = ReferralData.survey.SurveyType
                },
                Target = new ReferralTarget { Lpu = SetCoding(ReferralData.referralTarget.Lpu) },
            };
        }

        public Referral GetResultDocument(string idMq)
        {
            return new Referral { ReferralInfo = new ReferralInfo { IdMq = idMq } };
        }
    }
}
