using System;
using NUnit.Framework;
using MqTests.WebReference;
using System.Linq;

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

        private static string[] FamilyNames = { "Максимов", "Андреев", "Сергеев", "Сидоров", "Иванов", "Петров", "Абрамов", "Евгеньев", "Архипов", "Антонов", "Дмитриев", "Леонидов", "Денисов", "Тарасов", "Владимиров", "Константинов", "Николаев", "Романов", "Константинов", "Артемьев", "Филиппов", "Викторов", "Васильев", "Прохоров", "Алексеев", "Михайлов", "Афанасьев", "Харитонов" };
        private static string[] GivenNames = { "Максим", "Андрей", "Сергей", "Сидор", "Иван", "Пётр", "Абрам", "Евгений", "Архип", "Антон", "Дмитрий", "Леонид", "Денис", "Тарас", "Владимир", "Константин", "Николай", "Роман", "Константин", "Артём", "Филипп", "Виктор", "Василий", "Прохор", "Алексей", "Михаил", "Афанасий", "Харитон" };
        private static string[] MiddleNames = { "Максимович", "Андреевич", "Сергеевич", "Сидорович", "Иванович", "Петрович", "Абрамович", "Евгеньевич", "Архипович", "Антонович", "Дмитриевич", "Леонидович", "Денисович", "Тарасович", "Владимирович", "Константинович", "Николаевич", "Романович", "Константинович", "Артёмович", "Филиппович", "Викторович", "Васильевич", "Прохорович", "Алексеевич", "Михайлович", "Афанасьевич", "Харитонович" };
        private static string[] Streets = { "Невский пр.", "ул.Оптиков", "ул.Фрунзе", "ул.Дыбенко", "Пискарёвский пр.", "ул. Таллинская", "ул. Казанская", "наб. канала Грибоедова", "пл. Труда" };
        private static string[] CompanyNames = { "ОАО Солнышко", "ЧП Петров", "ЗАО Чайники", "ООО Пряники", "ОАО Лакомка", "ЧП Иванов", "ЗАО Газпром", "ОАО BestOfTheBest", "ОАО Программисты", "ЧП Тарасов", "ЗАО AllSafe", "Салон красоты Медея", "ЧП Булочная", "ЧП Зайки", "МедКлиника Доктор Хаус" };
        private static string[] Positions = { "Менеджер", "Директор", "Уборщица", "Старший разработчик", "Продавец", "Тестировщик", "Разнорабочий", "Парикмахер", "Повар", "Врач", "Аналитик", "Бухгалтер", "Просто хороший человек" };

        static Random R = new Random((int)DateTime.Now.Ticks);

        // Задание случайных номера(4 цифры) и серии документа(6 цифр)
        private static DocumentDto RandomDocument()
        {
            return new DocumentDto { DocS = R.Next(1000).ToString("D4"), DocN = R.Next(1000000).ToString("D6") };
        }

        //Задание случайного номера СНИЛС(11 цифр)
        private static DocumentDto RandomSnils()
        {
            return new DocumentDto { DocN = R.Next(1000000).ToString("D11") };
        }

        private static string[] RandomFIO()
        {
            int len = new[] { FamilyNames, GivenNames, MiddleNames }.Min(x => x.Length);
            return new string[] { FamilyNames[R.Next(len)], GivenNames[R.Next(len)], MiddleNames[R.Next(len)] };
        }

        private static string RandomBirthDate()
        {
            return new DateTime(1950, 1, 1).AddDays(R.Next((DateTime.Today.AddYears(-30) - new DateTime(1950, 1, 1)).Days)).ToString("dd.MM.yyyy");
        }

        private static string RandomAddress()
        {
            return Streets[R.Next(Streets.Length)] + ", д." + R.Next(30).ToString() + ", кв." + R.Next(150).ToString();
        }

        private static string RandomMobilePhone()
        {
            return "+7 9" + R.Next(35).ToString("D2") + R.Next(1000).ToString("D3") + R.Next(10000).ToString("D4");
        }

        private static string[] RandomJob()
        {
            return new string[] {CompanyNames[R.Next(CompanyNames.Length)], Positions[R.Next(Positions.Length)]};
        }

        private static void SetDocument()
        {
            DocumentData.PatientPassport = new DocumentDto
            {
                DocumentType = SetCoding("14", Dictionary.DOCUMENT_TYPE, "1"),
                DocS = RandomDocument().DocS,
                DocN = RandomDocument().DocN,
                ProviderName = "УФМС",
                ExpiredDate = Convert.ToDateTime("19.02.2020"),
                IssuedDate = Convert.ToDateTime("03.09.2007"),
                RegionCode = SetCoding("11", Dictionary.REGION_CODE, "1"),
                Provider = SetCoding("22003", Dictionary.PROVIDER, "1")
            };

            DocumentData.SNILS = new DocumentDto
            {
                DocumentType = SetCoding("223", Dictionary.DOCUMENT_TYPE, "1"),
                DocN = RandomSnils().DocN,
                ProviderName = "ПФР",
                ExpiredDate = Convert.ToDateTime("01.12.2010"),
                IssuedDate = Convert.ToDateTime("03.09.2006"),
                RegionCode = SetCoding("11", Dictionary.REGION_CODE, "1"),
                Provider = SetCoding("22003", Dictionary.PROVIDER, "1")
            };

            DocumentData.OldOMS = new DocumentDto
            {
                DocumentType = SetCoding("226", Dictionary.DOCUMENT_TYPE, "1"),
                DocN = RandomDocument().DocN,
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
                DocN = RandomDocument().DocN,
                DocS = RandomDocument().DocS,
                ProviderName = "Единый полис",
                ExpiredDate = Convert.ToDateTime("02.06.2000"),
                IssuedDate = Convert.ToDateTime("04.02.1994"),
                RegionCode = SetCoding("11", Dictionary.REGION_CODE, "1"),
                Provider = SetCoding("22003", Dictionary.PROVIDER, "1")
            };

            DocumentData.OtherDoc = new DocumentDto
            {
                DocumentType = SetCoding("18", Dictionary.DOCUMENT_TYPE, "1"),
                DocN = RandomDocument().DocN,
                DocS = RandomDocument().DocS,
                ProviderName = "Иной документ",
                ExpiredDate = Convert.ToDateTime("02.06.2000"),
                IssuedDate = Convert.ToDateTime("04.02.1994"),
                RegionCode = SetCoding("11", Dictionary.REGION_CODE, "1"),
                Provider = SetCoding("22003", Dictionary.PROVIDER, "1")
            };
        }

        private static ContactDto SetContact()
        {
            return PersonData.contact = new ContactDto
            {
                ContactType = SetCoding("1", Dictionary.CONTACT_TYPES, "1"),
                ContactValue = RandomMobilePhone()
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
                Speciality = SetCoding("0", Dictionary.DOCTOR_SPECIALITY, "1"),
                Position = SetCoding("247", Dictionary.DOCTOR_POSITION, "1"),
                Role = SetCoding("1", Dictionary.DOCTOR_ROLE, "1"),
                Person = new Person
                {
                    IdPersonMis = "IdPersonMis SourcedMO" + new Random().Next(1000),
                    Sex = SetCoding("1", Dictionary.SEX, "1"),
                    HumanName = new HumanName { FamilyName = RandomFIO()[0], GivenName = RandomFIO()[1], MiddleName = RandomFIO()[2] }
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
                    BirthDate = Convert.ToDateTime(RandomBirthDate()),
                    IdPatientMis = "IdPatientMis SoucedMO" + new Random().Next(1000),
                    Sex = SetCoding("1", Dictionary.SEX, "1"),
                    HumanName = new HumanName { FamilyName = RandomFIO()[0], GivenName = RandomFIO()[1], MiddleName = RandomFIO()[2] }
                },
                Addresses = new AddressDto[] 
                { 
                    new AddressDto
                    {
                        AddressType = SetCoding("H", Dictionary.ADDRESS_TYPE, "1"),
                        StringAddress = RandomAddress()
                    }
                },
                ContactDtos = new ContactDto[] { SetContact() },
                Jobs = new Job[] 
                { 
                    new  Job { CompanyName = RandomJob()[0], Position = RandomJob()[1] }
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
                Comment = "Комментарий к диагнозу" + new Random().Next(1000),
                DiagnosisType = SetCoding("1", Dictionary.DIAGNOSIS_TYPE, "1"),
                MkbCode = SetCoding("A05.2", Dictionary.MKB_CODE, "1")
            };
        }

        private static DiagnosisInfo SetComplicationDiagnosis()
        {
            return DiagnosisData.diagnosisInfo = new DiagnosisInfo
            {
                DiagnosedDate = Convert.ToDateTime("01.02.2012"),
                Comment = "Комментарий к диагнозу" + new Random().Next(1000),
                DiagnosisType = SetCoding("2", Dictionary.DIAGNOSIS_TYPE, "1"),
                MkbCode = SetCoding("B15", Dictionary.MKB_CODE, "1")
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
                DateReport = Convert.ToDateTime("01.01.2012"),
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

        // контейнер info?
        private static PatientMove_PlaceTypeInfoCell SetPatientMove_PlaceTypeInfoCell()
        {
            return InfoData.patientMove_PlaceTypeInfoCell = new PatientMove_PlaceTypeInfoCell
            {
                PlaceTypeCatalogId = SetCoding("1", Dictionary.PROFILE_MED_SERVICE, "1"),
                Info = new PatientMove_PlaceTypeInfo
                {
                    CurrentCount = new Random().Next(100),
                    IncomingCount = new Random().Next(100),
                    OutgoingCount = new Random().Next(100),
                    PlaningCount = new Random().Next(100),
                    ManPlaceAvailable = new Random().Next(100),
                    WomanPlaceAvailable = new Random().Next(100),
                    ChildrenPlaceAvailable = new Random().Next(100),
                    InfoList = new PatientMove_SMOInfoCell[] { SetPatientMove_SMOInfoCell() }
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
                    HospitalizationCount = new Random().Next(100),
                    PlaceDayCount = new Random().Next(365)
                }
            };
        }

        private static void SetRef()
        {

            ReferralData.referralInfo = new ReferralInfo
            {
                Priority = "Comment Priority" + new Random().Next(100),
                Date = Convert.ToDateTime("01.01.2012"),
                Reason = "Reason referral" + new Random().Next(100),
                Comment = "Additional Comment" + new Random().Next(100),
                ReferralType = SetCoding("2", Dictionary.REFERRAL_TYPE, "1"),
                ProfileMedService = SetCoding("1", Dictionary.PROFILE_MED_SERVICE, "1"),
                //     IdMq = "Идентификатор направления в РЕГИЗ.УО"
            };

            ReferralData.survey = new Survey
            {
                Comment = "Comment Survey" + new Random().Next(1000),
                SurveyType = SetCoding("1", Dictionary.SURVEY_TYPE, "1"),
                SurveyOrgan = SetCoding("1", Dictionary.SURVEY_ORGAN, "1"),
                Additional = new Additional
                {
                    Height = "140" + new Random().Next(50),
                    Weight = "40" + new Random().Next(40),
                    AllergyIodine = "AllergyIodine" + new Random().Next(100),
                }
            };

            ReferralData.referralSource = new ReferralSource
            {
                IdCaseMis = "IdCaseMis SourcedMO" + new Random().Next(1000),
                IdReferralMis = " IdReferralMis SourcedMO" + new Random().Next(1000),
                Lpu = SetCoding("1.2.643.5.1.13.3.25.78.6", Dictionary.MO, "1"),
                Doctors = new Doctor[] { SetDoctor() },
                MainDiagnosis = new MainDiagnosis[] { SetMainDiagnosis() }
            };

            ReferralData.referralTarget = new ReferralTarget
            {
                IdCaseMis = "IdCaseMis TargetMO" + new Random().Next(1000),
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
                    ReceptionAppointTime = "ReceptionAppointTime" + new Random().Next(100),
                    ReceptionAppointComment = "ReceptionAppointComment" + new Random().Next(100),
                    CaseOpenDate = Convert.ToDateTime("03.01.2012"),
                    CaseCloseDate = Convert.ToDateTime("03.01.2012"),
                    CaseAidType = SetCoding("1", Dictionary.CASE_AID_TYPE, "1"),
                    CaseAidForm = SetCoding("1", Dictionary.CASE_AID_FORM, "1"),
                    CaseAidPlace = SetCoding("2", Dictionary.CASE_AID_PLACE, "1")
                },
                Cancellation = new CancellationData
                {
                    Date = ReferralData.referralInfo.Date.Value.AddDays(1),
                    ReasonComment = "ReasonComment Cancellation" + new Random().Next(100),
                    CancellationSource = SetCoding("1", Dictionary.CANCELLATION_REASON, "1"),
                    CancellationReason = SetCoding("5", Dictionary.CANCELLATION_REASON, "1")
                }
            };

            ReferralData.profileMedService = new ProfileMedService
            {
                IdProfileMedService = SetCoding("1", Dictionary.PROFILE_MED_SERVICE, "1"),
                StartDate = Convert.ToDateTime("01.12.2011"),
                EndDate = Convert.ToDateTime("01.06.2012"),
                Address = RandomAddress(),
                ContactValue = RandomMobilePhone(),
                Comment = "Comment ProfileMedService" + new Random().Next(100),
                Site = "Сайт медицинской организации" + new Random().Next(100)
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
