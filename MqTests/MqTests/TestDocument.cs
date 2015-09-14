using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MqTests
{
    class TestDocument
    {
        public DocumentDto document;
        public TestCoding documentType;
        public TestCoding provider;
        public TestCoding regionCode;
        public TestDocument(DocumentDto d)
        {
            document = d;
            documentType = new TestCoding(document.DocumentType);
            provider = new TestCoding(document.Provider);
            regionCode = new TestCoding(document.RegionCode);
        }

        static public List<TestDocument> BuildDocumentsFromDataBaseData(string idPerson)
        {
            List<TestDocument> documents = new List<TestDocument>();
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findDocument = "SELECT * FROM public.documents WHERE id_person = '" + idPerson + "'";
                NpgsqlCommand person = new NpgsqlCommand(findDocument, connection);
                using (NpgsqlDataReader documentReader = person.ExecuteReader())
                {
                    while (documentReader.Read())
                    {
                        //Что делать с DateSpecified?
                        DocumentDto doc = new DocumentDto();
                        if (documentReader["docn"] != DBNull.Value)
                            doc.DocN = Convert.ToString(documentReader["docn"]);
                        if (documentReader["docs"] != DBNull.Value)
                            doc.DocS = Convert.ToString(documentReader["docs"]);
                        if (documentReader["expired_date"]!= DBNull.Value)
                            doc.ExpiredDate = Convert.ToDateTime(documentReader["expired_date"]);
                        if (documentReader["issued_date"] != DBNull.Value)
                            doc.IssuedDate = Convert.ToDateTime(documentReader["issued_date"]);
                        if (documentReader["provider_name"] != DBNull.Value)
                            doc.ProviderName = Convert.ToString(documentReader["provider_name"]);
                        TestDocument document = new TestDocument(doc);
                        if (documentReader["id_document_type"] != DBNull.Value)
                            document.documentType = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(documentReader["id_document_type"]));
                        if (documentReader["id_provider"] != DBNull.Value)
                            document.provider = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(documentReader["id_provider"]));                            
                        if (documentReader["id_region_code"] != DBNull.Value)
                            document.regionCode = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(documentReader["RegionCode"]));
                        documents.Add(document);
                    }
                }
            }
            return (documents.Count != 0) ? documents : null;
        }

        private void FindMismatch(TestDocument b)
        {
            if (this.document.DocN != b.document.DocN)
                Global.errors3.Add("Несовпадение DocN TestDocument");
            if (this.document.DocS != b.document.DocS)
                Global.errors3.Add("Несовпадение DocS TestDocument");
            if (this.document.ExpiredDate != b.document.ExpiredDate)
                Global.errors3.Add("Несовпадение ExpiredDate TestDocument");
            if (this.document.IssuedDate != b.document.IssuedDate)
                Global.errors3.Add("Несовпадение IssuedDate TestDocument");
            if (this.document.ProviderName != b.document.ProviderName)
                Global.errors3.Add("Несовпадение ProviderName TestDocument");
            if (Global.GetLength(this.documentType) != Global.GetLength(b.documentType))
                Global.errors3.Add("Несовпадение длинны documentType TestDocument");
            if (Global.GetLength(this.provider) != Global.GetLength(b.provider))
                Global.errors3.Add("Несовпадение длинны provider TestDocument");
            if (Global.GetLength(this.regionCode) != Global.GetLength(b.regionCode))
                Global.errors3.Add("Несовпадение длинны regionCode TestDocument");
        }

        public override bool Equals(Object obj)
        {
            TestDocument p = obj as TestDocument;
            if ((object)p == null)
            {
                return false;
            }
            if (this.document == p.document)
                return true;
            if ((this.document == null) || (p.document == null))
            {
                return false;
            }
            if ((this.document.DocN == p.document.DocN)&&
            (this.document.DocS == p.document.DocS)&&
            (this.document.ExpiredDate == p.document.ExpiredDate)&&
            (this.document.IssuedDate == p.document.IssuedDate)&&
            (this.document.ProviderName == p.document.ProviderName)&&
            (Global.IsEqual(this.documentType, p.documentType))&&
            (Global.IsEqual(this.provider, p.provider))&&
            (Global.IsEqual(this.regionCode, p.regionCode)))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestDocument");
                return false;
            }
        }
        public static bool operator ==(TestDocument a, TestDocument b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TestDocument a, TestDocument b)
        {
            return !(a.Equals(b));
        }
    }
}
