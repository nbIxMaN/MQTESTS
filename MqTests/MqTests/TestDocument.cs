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
            if (d != null)
            {
                document = d;
                documentType = new TestCoding(d.DocumentType);
                provider = new TestCoding(d.Provider);
                regionCode = new TestCoding(d.RegionCode);
            }
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
                        if (documentReader["docn"].ToString() != "")
                            doc.DocN = Convert.ToString(documentReader["docn"]);
                        if (documentReader["docs"].ToString() != "")
                            doc.DocS = Convert.ToString(documentReader["docs"]);
                        if (documentReader["expired_date"].ToString() != "")
                            doc.ExpiredDate = Convert.ToDateTime(documentReader["expired_date"]);
                        if (documentReader["issued_date"].ToString() != "")
                            doc.IssuedDate = Convert.ToDateTime(documentReader["issued_date"]);
                        if (documentReader["provider_name"].ToString() != "")
                            doc.ProviderName = Convert.ToString(documentReader["provider_name"]);
                        TestDocument document = new TestDocument(doc);
                        if (documentReader["id_document_type"].ToString() != "")
                            document.documentType = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(documentReader["id_document_type"]));
                        if (documentReader["id_provider"].ToString() != "")
                            document.provider = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(documentReader["id_provider"]));                            
                        if (documentReader["id_region_code"].ToString() != "")
                            document.regionCode = TestCoding.BuildCodingFromDataBaseData(Convert.ToString(documentReader["RegionCode"]));
                        documents.Add(document);
                    }
                }
            }
            if (documents.Count != 0)
                return documents;
            else
                return null;
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
