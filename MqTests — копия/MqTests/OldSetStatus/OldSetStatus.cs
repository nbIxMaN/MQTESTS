using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MqTests.WebReference;

namespace MqTests.OldSetStatus
{
    //в этом классе хранятся старые версии сетов статусов
    class OldSetStatus
    {
        //Минимальные обязательные данные для задания статуса "Зарегистрировано в РЕГИЗ.УО"
        //С использованием метода Register()
        public Referral SetStatus_RegisterMin()
        {
            Referral referral = (new SetData()).MinRegister();
            referral.Source.Doctors[0].Role = new Coding { Code = "1", System = Dictionary.DOCTOR_ROLE, Version = "1" };

            referral.EventsInfo = new EventsInfo
            {
                Source = new EventSource { ReferralCreateDate = ReferralData.eventsInfo.Source.ReferralCreateDate, }
            };
            return referral;
        }

        //Задаём статус "Согласовано в направляющей МО"
        //с использованием минимального метода UpdateFromSourcedMo
        //document = либо DocumentData.SingleOMS, либо DocumentData.OldOMS
        public Referral SetStatus_AgreedInSourcedMO(string idMq, DocumentDto document)
        {
            Referral referral = (new SetData()).MinUpdateFromSourcedMo(idMq);
            referral.Patient = new Patient
            {
                Documents = new DocumentDto[] 
                { 
                    new DocumentDto 
                    {
                        ProviderName = document.ProviderName,
                        DocumentType = document.DocumentType
                    }
                }
            };
            referral.Source = new ReferralSource
            {
                Doctors = new Doctor[]
                { 
                    new Doctor { Role = new Coding { Code = "2", System = Dictionary.DOCTOR_ROLE, Version = "1" } }
                }
            };
            referral.EventsInfo = new EventsInfo
            {
                Source = new EventSource
                {
                    ReferralReviewDate = ReferralData.eventsInfo.Source.ReferralReviewDate,
                    IsReferralReviewed = true,
                    // ReferralCreatedate обязательный при указании Source
                    ReferralCreateDate = ReferralData.eventsInfo.Source.ReferralCreateDate
                }
            };
            return referral;
        }

        //Задаём статус "Выдано пациенту"
        //с использованием минимального метода UpdateFromSourcedMo
        public Referral SetStatus_PatientDocumentIssue(string idMq)
        {
            Referral referral = (new SetData()).MinUpdateFromSourcedMo(idMq);

            referral.ReferralInfo.Date = ReferralData.referralInfo.Date;
            referral.ReferralInfo.ReferralType = ReferralData.referralInfo.ReferralType;
            referral.ReferralInfo.Reason = ReferralData.referralInfo.Reason;

            referral.Patient = new Patient
            {
                Documents = new DocumentDto[] 
                { 
                    new DocumentDto 
                    {
                        ProviderName = DocumentData.PatientPassport.ProviderName,
                        DocumentType = DocumentData.PatientPassport.DocumentType
                    }
                },
                //тут типы (адреса, контакта) необязательны (но сейчас они задаются)
                ContactDtos = new ContactDto[] { PersonData.contact },
                Addresses = new AddressDto[] { PersonData.patient.Addresses[0] }
            };

            referral.ReferralSurvey = new Survey
            {
                SurveyOrgan = new Coding { Code = "3", System = Dictionary.SURVEY_ORGAN, Version = "1" },
                SurveyType = new Coding { Code = "3", System = Dictionary.SURVEY_TYPE, Version = "1" }
            };

            referral.Target = new ReferralTarget { Lpu = ReferralData.referralTarget.Lpu };

            referral.EventsInfo = new EventsInfo
            {
                Source = new EventSource
                {
                    ReferralOutDate = ReferralData.eventsInfo.Source.ReferralOutDate,
                    // ReferralCreatedate обязательный при указании Source
                    ReferralCreateDate = ReferralData.eventsInfo.Source.ReferralCreateDate
                }
            };

            return referral;
        }

        //document = либо DocumentData.SingleOMS, либо DocumentData.OldOMS
        public Referral SetStatus_HealthCareStart(string idMq, DocumentDto document)
        {
            Referral referral = (new SetData()).MinUpdateFromTargetMO(idMq);
            referral.Patient = new Patient
            {
                Documents = new DocumentDto[] 
                { 
                    new DocumentDto 
                    {
                        ProviderName = document.ProviderName,
                        DocumentType = document.DocumentType
                    }
                }
            };
            referral.EventsInfo = new EventsInfo
            {
                Target = new EventTarget
                {
                    CaseOpenDate = ReferralData.eventsInfo.Target.CaseOpenDate,
                    CaseAidForm = ReferralData.eventsInfo.Target.CaseAidForm,
                    CaseAidPlace = ReferralData.eventsInfo.Target.CaseAidPlace,
                    CaseAidType = ReferralData.eventsInfo.Target.CaseAidType
                }
            };

            return referral;
        }

        public Referral SetStatus_HealthCareEnd(string idMq)
        {
            Referral referral = (new SetData()).MinUpdateFromTargetMO(idMq);
            referral.EventsInfo = new EventsInfo
            {
                Target = new EventTarget
                {
                    CaseCloseDate = ReferralData.eventsInfo.Target.CaseCloseDate,
                    CaseAidForm = ReferralData.eventsInfo.Target.CaseAidForm,
                    CaseAidPlace = ReferralData.eventsInfo.Target.CaseAidPlace,
                    CaseAidType = ReferralData.eventsInfo.Target.CaseAidType
                }
            };

            return referral;
        }

    }
}
