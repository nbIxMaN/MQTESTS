﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MqTests.WebReference;
using Npgsql;

namespace MqTests
{
    class TestJob
    {
        public Job job;

        public TestJob(Job j)
        {
            job = j;
        }
        static public List<TestJob> BuildJobFromDataBaseData(string idPerson)
        {
            List<TestJob> jobs = new List<TestJob>();
            using (NpgsqlConnection connection = Global.GetSqlConnection())
            {
                string findJob = "SELECT * FROM public.job WHERE id_person = '" + idPerson + "'";
                NpgsqlCommand person = new NpgsqlCommand(findJob, connection);
                using (NpgsqlDataReader jobReader = person.ExecuteReader())
                {
                    while (jobReader.Read())
                    {
                        Job job = new Job();
                        if (jobReader["company_name"]!= DBNull.Value)
                            job.CompanyName = Convert.ToString(jobReader["company_name"]);
                        if (jobReader["position"] != DBNull.Value)
                            job.Position = Convert.ToString(jobReader["position"]);
                        TestJob tJob = new TestJob(job);
                        jobs.Add(tJob);
                    }
                }
            }
            return (jobs.Count != 0) ? jobs : null;
        }

        private void FindMismatch(TestJob b)
        {
            if (b.job != null)
            {
                if (this.job.CompanyName != b.job.CompanyName)
                    Global.errors3.Add("Несовпадение имен компаний");
                if (this.job.Position != b.job.Position)
                    Global.errors3.Add("Несовпадение позиций");
            }
        }

        public override bool Equals(Object obj)
        {
            TestJob p = obj as TestJob;
            if (p == null)
            {
                return false;
            }
            if ((this.job.CompanyName == p.job.CompanyName) &&
               (this.job.Position == p.job.Position))
            {
                return true;
            }
            else
            {
                this.FindMismatch(p);
                Global.errors3.Add("несовпадение TestJob");
                return false;
            }
        }
        public static bool operator ==(TestJob a, TestJob b)
        {
            return Equals(a, b);
        }
        public static bool operator !=(TestJob a, TestJob b)
        {
            return !Equals(a, b);
        }
    }
}
