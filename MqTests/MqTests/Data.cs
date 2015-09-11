﻿using System;
using NUnit.Framework;
using MqTests.WebReference;

namespace MqTests
{
    public static class ReferralData
    {
        public static Referral referral { get; set; }
        public static ReferralInfo referralInfo { get; set; }
        public static Survey survey { get; set; }
        public static ReferralSource referralSource { get; set; }
        public static ReferralTarget referralTarget { get; set; }
        public static EventsInfo eventsInfo { get; set; }
        public static ProfileMedService profileMedService { get; set; }
    }

    public static class CodingData
    {
        public static Coding coding { get; set; }
    }

    public static class OptionData
    {
        public static Options options { get; set; }
    }

    public static class InfoData
    {
        public static PatientMove_PlaceTypeInfoCell patientMove_PlaceTypeInfoCell { get; set; }
        public static PatientMove_SMOInfoCell patientMove_SMOInfoCell { get; set; }
    }

    public static class DiagnosisData
    {
        public static MainDiagnosis mainDiagnosis { get; set; }
        public static DiagnosisInfo diagnosisInfo { get; set; }
        public static DiagnosisInfo complicationDiagnosis { get; set; }
    }

    public static class PersonData
    {
        public static Doctor doctor { get; set; }
        public static Patient patient { get; set; }
        public static ContactDto contact { get; set; }
    }

    public static class DocumentData
    {
        public static DocumentDto PatientPassport { get; set; }
        public static DocumentDto DoctorPassport { get; set; }
        public static DocumentDto SNILS { get; set; }
        public static DocumentDto OldOMS { get; set; }
        public static DocumentDto SingleOMS { get; set; }
        public static DocumentDto OtherDoc { get; set; }
    }


    [TestFixture]
    public abstract class Data
    {
        public string idLpu = "1.2.643.5.1.13.3.25.78.6";
        public string guid = "dda0e909-93cd-4549-b7ff-d1caaa1f0bc2";

        private static void SetDocument()
        {
            DocumentData.PatientPassport = new DocumentDto
            {
                DocumentType = SetCoding("14", Dictionary.DOCUMENT_TYPE, "1"),
                DocS = "2007",
                DocN = "395731",
                ProviderName = "УФМС",
                ExpiredDate = Convert.ToDateTime("19.02.2020"),
                IssuedDate = Convert.ToDateTime("03.09.2007"),
                RegionCode = SetCoding("11", Dictionary.REGION_CODE, "1"),
                Provider = SetCoding("22003", Dictionary.PROVIDER, "1")
            };

            DocumentData.DoctorPassport = new DocumentDto
            {
                DocumentType = SetCoding("14", Dictionary.DOCUMENT_TYPE, "1"),
                DocS = "2005",
                DocN = "395712",
                ProviderName = "УФМС",
                ExpiredDate = Convert.ToDateTime("19.02.2020"),
                IssuedDate = Convert.ToDateTime("03.09.2007"),
                RegionCode = SetCoding("11", Dictionary.REGION_CODE, "1"),
                Provider = SetCoding("22003", Dictionary.PROVIDER, "1")
            };

            DocumentData.SNILS = new DocumentDto
            {
                DocumentType = SetCoding("223", Dictionary.DOCUMENT_TYPE, "1"),
                DocN = "59165576238",
                ProviderName = "ПФР",
                ExpiredDate = Convert.ToDateTime("01.12.2010"),
                IssuedDate = Convert.ToDateTime("03.09.2006"),
                RegionCode = SetCoding("11", Dictionary.REGION_CODE, "1"),
                Provider = SetCoding("22003", Dictionary.PROVIDER, "1")
            };

            DocumentData.OldOMS = new DocumentDto
            {
                DocumentType = SetCoding("226", Dictionary.DOCUMENT_TYPE, "1"),
                DocN = "225916",
                DocS = "AA",
                ProviderName = "Старый полис",
                ExpiredDate = Convert.ToDateTime("31.01.2040"),
                IssuedDate = Convert.ToDateTime("11.11.2000"),
                RegionCode = SetCoding("11", Dictionary.REGION_CODE, "1"),
                Provider = SetCoding("22003", Dictionary.PROVIDER, "1")
            };

            DocumentData.SingleOMS = new DocumentDto
            {
                DocumentType = SetCoding("228", Dictionary.DOCUMENT_TYPE, "1"),
                DocN = "1234567",
                DocS = "1234",
                ProviderName = "Единый полис",
                ExpiredDate = Convert.ToDateTime("02.06.2000"),
                IssuedDate = Convert.ToDateTime("04.02.1994"),
                RegionCode = SetCoding("11", Dictionary.REGION_CODE,  "1"),
                Provider = SetCoding("22003", Dictionary.PROVIDER, "1")
            };

            DocumentData.OtherDoc = new DocumentDto
            {
                DocumentType = SetCoding("18", Dictionary.DOCUMENT_TYPE, "1"),
                DocN = "1234567",
                DocS = "1234",
                ProviderName = "Иной документ",
                ExpiredDate = Convert.ToDateTime("02.06.2000"),
                IssuedDate = Convert.ToDateTime("04.02.1994"),
                RegionCode = SetCoding("11", Dictionary.REGION_CODE , "1"),
                Provider = SetCoding("22003", Dictionary.PROVIDER, "1")
            };
        }

        private static ContactDto SetContact()
        {
            return PersonData.contact = new ContactDto
            {
                ContactType = SetCoding("1", Dictionary.CONTACT_TYPES, "1"),
                ContactValue = "89103456789"
            };
        }

        private static Coding SetCoding(string code, string system, string version)
        {
            return CodingData.coding = new Coding
            {
                Code = code,
                System = system,
                Version = version
            };
        }

        private static Doctor SetDoctor()
        {
            return PersonData.doctor = new Doctor
            {
                Lpu = SetCoding("1.2.643.5.1.13.3.25.78.6", Dictionary.MO, "1"),
                Speciality = SetCoding("0", Dictionary.DOCTOR_SPECIALITY , "1"),
                Position = SetCoding("247", Dictionary.DOCTOR_POSITION, "1"),
                Role = SetCoding("1", Dictionary.DOCTOR_ROLE, "1"),
                Person = new Person
                {
                    IdPersonMis = "Идентификатор медицинского работника в МИС направляющей МО",
                    Sex = SetCoding("1", Dictionary.SEX, "1"),
                    HumanName = new HumanName
                    {
                        FamilyName = "Иванов",
                        GivenName = "Пётр",
                        MiddleName = "Сергеевич"
                    }
                },
                ContactDtos = new ContactDto[] { SetContact() }
            };
        }

        private static Patient SetPatient()
        {
            return PersonData.patient = new Patient
            {
                Person = new Person
                {
                    BirthDate = Convert.ToDateTime("01.01.1990"),
                    IdPatientMis = "Идентификатор пациента в МИС направляющей МО",
                    //IdPersonMis = "Идентификатор медицинского работника в МИС направляющей МО",
                    Sex = SetCoding("female", "1.2.643.5.1.13.2.1.1.156", "1"),
                    HumanName = new HumanName
                    {
                        FamilyName = "Петров",
                        GivenName = "Иван",
                        MiddleName = "Иванович"
                    }
                },
                Addresses = new AddressDto[] 
                { 
                    new AddressDto
                    {
                        AddressType = SetCoding("H", Dictionary.ADDRESS_TYPE, "1"),
                        StringAddress = "г.Петергоф Ботаническая 66, корп 2"
                    }
                },
                ContactDtos = new ContactDto[] { SetContact() },
                Jobs = new Job[] 
                { 
                    new  Job
                    {
                        CompanyName = "ОАО Солнышко",
                        Position = "Менеджер"
                    }
                },
                Privileges = new Privilege[]
                {
                    new Privilege
                    {
                        StartDate = Convert.ToDateTime("01.01.2012"),
                        EndDate = Convert.ToDateTime("01.02.2012"),
                        PrivilegeType = SetCoding("901", Dictionary.PRIVILEGE_TYPE, "1")
                    }
                },
                Documents = new DocumentDto[] { DocumentData.PatientPassport }
            };
        }
        private static DiagnosisInfo SetDiagnosisInfo()
        {
            return DiagnosisData.diagnosisInfo = new DiagnosisInfo
            {
                DiagnosedDate = Convert.ToDateTime("01.01.2012"),
                Comment = "Комментарий к диагнозу",
                DiagnosisType = SetCoding("1", Dictionary.DIAGNOSIS_TYPE, "1"),
                MkbCode = SetCoding("A05.2", Dictionary.MKB_CODE, "1")
            };
        }

        private static DiagnosisInfo SetComplicationDiagnosis()
        {
            return DiagnosisData.diagnosisInfo = new DiagnosisInfo
            {
                DiagnosedDate = Convert.ToDateTime("01.02.2012"),
                Comment = "Комментарий к диагнозу2",
                DiagnosisType = SetCoding("2", Dictionary.DIAGNOSIS_TYPE, "1"),
                MkbCode = SetCoding("A05.2", Dictionary.MKB_CODE, "1")
            };
        }
        private static MainDiagnosis SetMainDiagnosis()
        {
            SetDiagnosisInfo();
            SetComplicationDiagnosis();
            return DiagnosisData.mainDiagnosis = new MainDiagnosis
            {
                DiagnosisInfo = DiagnosisData.diagnosisInfo,
                ComplicationDiagnosis = new DiagnosisInfo[] { DiagnosisData.complicationDiagnosis }
            };
        }

        //Если добавил/удалили поля в этом методе, внеси изменения в SetData().FullGetQueueInfo()
        private static void SetOptions()
        {
            OptionData.options = new Options
            {
                DateReport = Convert.ToDateTime("01.04.2012"),
                ReferralInfo = new ReferralInfo
                {
                    ProfileMedService = SetCoding("1", Dictionary.PROFILE_MED_SERVICE, "1"),
                },
                Target = new ReferralTarget
                {
                    Lpu = SetCoding("1.2.643.5.1.13.3.25.78.6", Dictionary.MO, "1")
                }
            };
        }

        // контейнер info? проверить тестовые данные
        private static PatientMove_PlaceTypeInfoCell SetPatientMove_PlaceTypeInfoCell()
        {
            return InfoData.patientMove_PlaceTypeInfoCell = new PatientMove_PlaceTypeInfoCell
            {
                PlaceTypeCatalogId = SetCoding("1", Dictionary.PROFILE_MED_SERVICE, "1"),
                Info = new PatientMove_PlaceTypeInfo
                {
                    CurrentCount = 1,
                    IncomingCount = 1,
                    OutgoingCount = 0,
                    PlaningCount = 1,
                    ManPlaceAvailable = 2,
                    WomanPlaceAvailable = 1,
                    ChildrenPlaceAvailable = 1,
                    InfoList = new PatientMove_SMOInfoCell[]
                    {
                         SetPatientMove_SMOInfoCell()
                    }
                }
            };
        }

        private static PatientMove_SMOInfoCell SetPatientMove_SMOInfoCell()
        {
            return InfoData.patientMove_SMOInfoCell = new PatientMove_SMOInfoCell
            {
                HicCatalogId = SetCoding("22003", Dictionary.HIC_CATALOGID, "1"),
                Info = new PatientMove_SMOInfo
                {
                    HospitalizationCount = 123,
                    PlaceDayCount = 34
                }
            };
        }

        private static void SetRef()
        {

            ReferralData.referralInfo = new ReferralInfo
            {
                Priority = "Комментарий о приоритете и состоянии пациента",
                Date = Convert.ToDateTime("01.01.2012"),
                Reason = "Основание направления, цель направления",
                Comment = "Комментарий/дополнительные данные для направления",
                ReferralType = SetCoding("2", Dictionary.REFERRAL_TYPE, "1"),
                ProfileMedService = SetCoding("1", Dictionary.PROFILE_MED_SERVICE, "1"),
                //     IdMq = "Идентификатор направления в РЕГИЗ.УО"
            };

            ReferralData.survey = new Survey
            {
                Comment = "Комментарий к области исследования",
                SurveyType = SetCoding("1", Dictionary.SURVEY_TYPE, "1"),
                SurveyOrgan = SetCoding("1", Dictionary.SURVEY_ORGAN, "1"),
                Additional = new Additional
                {
                    Height = "170",
                    Weight = "56",
                    AllergyIodine = "Сведения о непереносимости пациентом йодосодержащих рентгенконтрастных препаратов",
                }
            };

            ReferralData.referralSource = new ReferralSource
            {
                IdCaseMis = "Идентификатор случая обслуживания в МИС направляющей МО",
                IdReferralMis = "Идентификатор направления в МИС направляющей МО",
                Lpu = SetCoding("1.2.643.5.1.13.3.25.78.6", Dictionary.MO, "1"),
                Doctors = new Doctor[] { SetDoctor() },
                MainDiagnosis = new MainDiagnosis[] { SetMainDiagnosis() }
            };

            ReferralData.referralTarget = new ReferralTarget
            {
                IdCaseMis = "Идентификатор случая обслуживания в МИС целевой МО",
                Lpu = SetCoding("1.2.643.5.1.13.3.25.78.6", Dictionary.MO, "1"),
                Doctors = new Doctor[] { SetDoctor() },
                MainDiagnosis = new MainDiagnosis[] { SetMainDiagnosis() }
            };

            ReferralData.eventsInfo = new EventsInfo
            {
                Source = new EventSource
                {
                    ReferralCreateDate = Convert.ToDateTime("01.01.2012"),
                    ReferralOutDate = Convert.ToDateTime("02.01.2012"),
                    IsReferralReviewed = true,
                    ReferralReviewDate = Convert.ToDateTime("01.01.2012"),
                    PlannedDate = Convert.ToDateTime("03.01.2012"),
                },
                Target = new EventTarget
                {
                    IsReferralReviwed = true,
                    ReferralReviewDate = Convert.ToDateTime("02.01.2012"),
                    ReceptionAppointDate = Convert.ToDateTime("04.01.2012"),
                    ReceptionAppointTime = "Сведения о времени и длительности приема в назначенную дату приема пациента по направлению",
                    ReceptionAppointComment = "Дополнительные сведения назначенном приеме пациента по направлению в целевой МО (например, кабинет или необходимость обращения в регистратуру)",
                    CaseOpenDate = Convert.ToDateTime("03.01.2012"),
                    CaseCloseDate = Convert.ToDateTime("03.01.2012"),
                    CaseAidType = SetCoding("1", Dictionary.CASE_AID_TYPE, "1"),
                    CaseAidForm = SetCoding("1", Dictionary.CASE_AID_FORM, "1"),
                    CaseAidPlace = SetCoding("2", Dictionary.CASE_AID_PLACE, "1")
                },
                Cancellation = new CancellationData
                {
                    Date = Convert.ToDateTime("04.01.2012"),
                    ReasonComment = "Описание причины аннулирования",
                    CancellationSource = SetCoding("1", Dictionary.CANCELLATION_REASON, "1"),
                    CancellationReason = SetCoding("5", Dictionary.CANCELLATION_REASON, "1")
                }
            };

            ReferralData.profileMedService = new ProfileMedService
            {
                IdProfileMedService = SetCoding("1", Dictionary.PROFILE_MED_SERVICE, "1"),
                StartDate = Convert.ToDateTime("01.12.2011"),
                EndDate = Convert.ToDateTime("01.06.2012"),
                Address = "г.Санкт-Петербург ул. Кирочная д.8",
                ContactValue = "12 345 67",
                Comment = "Комментарий о режиме работы с направленными пациентами",
                Site = "Сайт медицинской организации"
            };


            ReferralData.referral = new Referral
            {
                ReferralInfo = ReferralData.referralInfo,
                EventsInfo = ReferralData.eventsInfo,
                ReferralSurvey = ReferralData.survey,
                Patient = PersonData.patient,
                Source = ReferralData.referralSource,
                Target = ReferralData.referralTarget
            };
        }

        [SetUp]
        public void SetUp()
        {
            SetDocument();
            SetPatientMove_PlaceTypeInfoCell();
            SetOptions();
            SetPatient();
            SetRef();
        }

        [TearDown]
        public void TearDown()
        {
            Global.errors1.Clear();
            Global.errors2.Clear();
            Global.errors3.Clear();
        }
    }
}
