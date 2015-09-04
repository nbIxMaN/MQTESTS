using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using MqTests.WebReference;

namespace QueueTest
{
    public static class ReferralData
    {
        public static Referral referral { get; set; }
        public static ReferralInfo referralInfo { get; set; }
        public static Survey survey { get; set; }
        public static ReferralSource referralSource { get; set; }
        public static MainDiagnosis mainDiagnosis { get; set; }
        public static ReferralTarget referralTarget { get; set; }
        public static EventsInfo eventsInfo { get; set; }
    }

    public static class CodingData
    {
        public static Coding coding { get; set; }
    }

    public static class PersonData
    {
        public static Doctor doctor { get; set; }
        public static Patient patient { get; set; }
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
        protected MqServiceClient mq { get; private set; }
        public string idLpu = "1.2.643.5.1.13.3.25.78.6";
        public string guid = "dda0e909-93cd-4549-b7ff-d1caaa1f0bc2";

        public static void SetDocument()
        {
            DocumentData.PatientPassport = new DocumentDto
            {
                DocumentType = SetCoding("14", "1.2.643.2.69.1.1.1.59", "1"),
                DocS = "2007",
                DocN = "395731",
                ProviderName = "УФМС",
                ExpiredDate = Convert.ToDateTime("19.02.2020"),
                IssuedDate = Convert.ToDateTime("03.09.2007"),
                RegionCode = SetCoding("11", "1.2.643.2.69.1.1.1.51", "1"),
                Provider = SetCoding("22003", "1.2.643.5.1.13.2.1.1.635", "1")
            };

            DocumentData.DoctorPassport = new DocumentDto
            {
                DocumentType = SetCoding("14", "1.2.643.2.69.1.1.1.59", "1"),
                DocS = "2005",
                DocN = "395712",
                ProviderName = "УФМС",
                ExpiredDate = Convert.ToDateTime("19.02.2020"),
                IssuedDate = Convert.ToDateTime("03.09.2007"),
                RegionCode = SetCoding("11", "1.2.643.2.69.1.1.1.51", "1"),
                Provider = SetCoding("22003", "1.2.643.5.1.13.2.1.1.635", "1")
            };

            DocumentData.SNILS = new DocumentDto
            {
                DocumentType = SetCoding("223", "1.2.643.2.69.1.1.1.59", "1"),
                DocN = "59165576238",
                ProviderName = "ПФР",
                ExpiredDate = Convert.ToDateTime("01.12.2010"),
                IssuedDate = Convert.ToDateTime("03.09.2006"),
                RegionCode = SetCoding("11", "1.2.643.2.69.1.1.1.51", "1"),
                Provider = SetCoding("22003", "1.2.643.5.1.13.2.1.1.635", "1")
            };

            DocumentData.OldOMS = new DocumentDto
            {
                DocumentType = SetCoding("226", "1.2.643.2.69.1.1.1.59", "1"),
                DocN = "225916",
                DocS = "AA",
                ProviderName = "Старый полис",
                ExpiredDate = Convert.ToDateTime("31.01.2040"),
                IssuedDate = Convert.ToDateTime("11.11.2000"),
                RegionCode = SetCoding("11", "1.2.643.2.69.1.1.1.51", "1"),
                Provider = SetCoding("22003", "1.2.643.5.1.13.2.1.1.635", "1")
            };

            DocumentData.SingleOMS = new DocumentDto
            {
                DocumentType = SetCoding("228", "1.2.643.2.69.1.1.1.59", "1"),
                DocN = "1234567",
                DocS = "1234",
                ProviderName = "Единый полис",
                ExpiredDate = Convert.ToDateTime("02.06.2000"),
                IssuedDate = Convert.ToDateTime("04.02.1994"),
                RegionCode = SetCoding("11", "1.2.643.2.69.1.1.1.51", "1"),
                Provider = SetCoding("22003", "1.2.643.5.1.13.2.1.1.635", "1")
            };

            DocumentData.OtherDoc = new DocumentDto
            {
                DocumentType = SetCoding("18", "1.2.643.2.69.1.1.1.59", "1"),
                DocN = "1234567",
                DocS = "1234",
                ProviderName = "Иной документ",
                ExpiredDate = Convert.ToDateTime("02.06.2000"),
                IssuedDate = Convert.ToDateTime("04.02.1994"),
                RegionCode = SetCoding("11", "1.2.643.2.69.1.1.1.51", "1"),
                Provider = SetCoding("22003", "1.2.643.5.1.13.2.1.1.635", "1")
            };
        }


        public static Coding SetCoding(string code, string system, string version)
        {
            return CodingData.coding = new Coding
            {
                Code = code,
                System = system,
                Version = version
            };
        }

        public static Doctor SetDoctor()
        {
            return PersonData.doctor = new Doctor
            {
                Lpu = SetCoding("1.2.643.5.1.13.3.25.78.6", "1.2.643.2.69.1.1.1.64", "1"),
                Speciality = SetCoding("0", "1.2.643.5.1.13.2.1.1.181", "1"),
                Position = SetCoding("247", "1.2.643.5.1.13.2.1.1.607", "1"),
                Role = SetCoding("1", "1.2.643.2.69.1.1.1.66", "1"),
                Person = new Person
                {
                    IdPersonMis = "Идентификатор медицинского работника в МИС направляющей МО",
                    Sex = SetCoding("1", "1.2.643.5.1.13.2.1.1.156", "1"),
                    HumanName = new HumanName
                    {
                        FamilyName = "Иванов",
                        GivenName = "Пётр",
                        MiddleName = "Сергеевич"
                    }
                },
                ContactDtos = new ContactDto[]
                {
                     new ContactDto
                     {
                         ContactType = SetCoding("1","1.2.643.2.69.1.1.1.27","1"),
                          ContactValue = "89103456789"
                     }
                }
            };
        }

        public static Patient SetPatient()
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
                      AddressType = SetCoding("H","1.2.643.2.69.1.1.1.28","1"),
                      StringAddress = "г.Петергоф Ботаническая 66, корп 2"
                  }
                },
                ContactDtos = new ContactDto[]
                {
                     new ContactDto
                     {
                          ContactType = SetCoding("2","1.2.643.2.69.1.1.1.27","1"),
                          ContactValue = "4567809"
                     }
                },
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
                         PrivilegeType = SetCoding("901","1.2.643.2.69.1.1.1.7","1")
                    }
                },
                Documents = new DocumentDto[] 
                { 
                    DocumentData.PatientPassport
                }
            };
        }

        public static MainDiagnosis SetMainDiagnosis()
        {
            return ReferralData.mainDiagnosis = new MainDiagnosis
            {
                DiagnosisInfo = new DiagnosisInfo
                {
                    DiagnosedDate = Convert.ToDateTime("01.01.2012"),
                    Comment = "Комментарий к диагнозу",
                    DiagnosisType = SetCoding("1", "1.2.643.2.69.1.1.1.26", "1"),
                    MkbCode = SetCoding("A05.2", "1.2.643.2.69.1.1.1.2", "1")
                },
                ComplicationDiagnosis = new DiagnosisInfo[]
                {
                    new  DiagnosisInfo
                    {
                        DiagnosedDate = Convert.ToDateTime("01.01.2012"),
                        Comment = "Комментарий к диагнозу",
                        DiagnosisType = SetCoding("2", "1.2.643.2.69.1.1.1.26", "1"),
                        MkbCode = SetCoding("A05.2", "1.2.643.2.69.1.1.1.2", "1")
                    }
                }
            };
        }

        public static void SetRef()
        {
            ReferralData.referralInfo = new ReferralInfo
            {
                Priority = "Комментарий о приоритете и состоянии пациента",
                Date = Convert.ToDateTime("01.01.2012"),
                Reason = "Основание направления, цель направления",
                Comment = "Комментарий/дополнительные данные для направления",
                ReferralType = SetCoding("2", "1.2.643.2.69.1.1.1.55", "1"),
                ProfileMedService = SetCoding("1", "1.2.643.2.69.1.1.1.56", "1")
            };

            ReferralData.survey = new Survey
            {
                Comment = "Комментарий к области исследования",
                SurveyType = SetCoding("1", "1.2.643.2.69.1.1.1.57", "1"),
                SurveyOrgan = SetCoding("1", "1.2.643.2.69.1.1.1.58", "1"),
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
                Lpu = SetCoding("1.2.643.5.1.13.3.25.78.6", "1.2.643.2.69.1.1.1.64", "1"),
                Doctors = new Doctor[] { SetDoctor() },
                MainDiagnosis = new MainDiagnosis[] { SetMainDiagnosis() }
            };

            ReferralData.referralTarget = new ReferralTarget
            {
                IdCaseMis = "Идентификатор случая обслуживания в МИС целевой МО",
                Lpu = SetCoding("1.2.643.5.1.13.3.25.78.6", "1.2.643.2.69.1.1.1.64", "1"),
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
                },
            };

            ReferralData.referral = new Referral
            {
                ReferralInfo = ReferralData.referralInfo,
                EventsInfo = ReferralData.eventsInfo,
                ReferralSurvey = ReferralData.survey,
                Patient = SetPatient(),
                Source = ReferralData.referralSource,
                Target = ReferralData.referralTarget
            };
        }

        [SetUp]
        public void SetUp()
        {
            mq = new MqServiceClient();
            SetDocument();
            SetRef();
        }

        [TearDown]
        public void TearDown()
        {
            mq.Close();
        }
    }
}
