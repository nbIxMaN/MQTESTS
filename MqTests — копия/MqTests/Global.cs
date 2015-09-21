using MqTests.WebReference;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqTests
{
    class Global
    {
        static public ArrayList errors3 = new ArrayList();
        static public ArrayList errors2 = new ArrayList();
        static public ArrayList errors1 = new ArrayList();
        static public int perem = 0;
        static public string errors
        {
            get
            {
                return ErrorsToString();
            }
        }
        static public int GetLength(object a)
        {
            if (a == null)
            {
                return 0;
            }
            else
            {
                Array arrayA = a as Array;
                if (arrayA != null)
                    return arrayA.Length;
                else
                    return 1;
            }
        }
        static private string _connectionPath =
            "PORT=5432;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20;COMPATIBLE=2.2.4.3;HOST=192.168.8.98;DATABASE=queues_test;USER ID=testers;PASSWORD=iloycQ45yVSPSMdyrtxQ";
        static public NpgsqlConnection GetSqlConnection()
        {
            NpgsqlConnection connection = new NpgsqlConnection(_connectionPath);
            connection.Open();
            perem += 1;
            connection.Disposed += disp;
            return connection;
        }

        static void disp(object sender, EventArgs e)
        {
            perem -= 1;
        }

        static public bool EqualsArrayLists(Array a, Array b)
        {
            if (a.Length == b.Length)
            {
                if (a.Length == 0)
                    return true;
                bool mark = true;
                foreach (var i in a)
                {
                    bool mark1 = false;
                    foreach (var i1 in b)
                    {
                        if (i.Equals(i1))
                            mark1 = true;
                    }
                    if (!mark1)
                        mark = false;
                }
                return mark;
            }
            else
                return false;
        }
        static public bool IsEqual(object a, object b)
        {
            //errors2.AddRange(errors3);
            //errors3.Clear();
            if ((a == null) && (b == null))
            {
                errors3.Clear();
                return true;
            }
            if ((a != null) && (b != null))
            {
                Array arrayA = a as Array;
                Array arrayB = b as Array;
                if ((arrayA != null) && (arrayB != null))
                    if (EqualsArrayLists(arrayA, arrayB))
                    {
                        errors3.Clear();
                        return true;
                    }
                    else
                    {
                        errors2.AddRange(errors3);
                        errors3.Clear();
                        return false;
                    }
                if ((arrayA == null) && (arrayB == null))
                    if (a.Equals(b))
                    {
                        errors3.Clear();
                        return true;
                    }
                    else
                    {
                        errors2.AddRange(errors3);
                        errors3.Clear();
                        return false;
                    }
            }
            errors2.AddRange(errors3);
            errors3.Clear();
            return false;
        }
        static private string ErrorsToString()
        {
            string errors = "";
            for (int i = 0; i < errors1.Count; i++)
            {
                errors += (i + 1).ToString() + ". " + errors1[i].ToString() + "\n";
            }
            return errors;
        }
        static public string GetIdInstitution(string idLpu)
        {
            if (idLpu != "")
            {
                string findIdInstitutionalString =
                    "SELECT TOP(1) IdInstitution FROM Institution WHERE IdFedNsi = '" + idLpu + "'";
                using (NpgsqlConnection connection = Global.GetSqlConnection())
                {
                    NpgsqlCommand IdInstitution = new NpgsqlCommand(findIdInstitutionalString, connection);
                    using (NpgsqlDataReader IdInstitutional = IdInstitution.ExecuteReader())
                    {
                        while (IdInstitutional.Read())
                        {
                            return (IdInstitutional["IdInstitution"].ToString());
                        }
                    }
                }
            }
            return "";
        }
        static public string GetIdIdLpu(string idInst)
        {
            if (idInst != "")
            {
                string findIdInstitutionalString =
                    "SELECT TOP(1) * FROM Institution WHERE IdInstitution = '" + idInst + "'";
                using (NpgsqlConnection connection = Global.GetSqlConnection())
                {
                    NpgsqlCommand IdInstitution = new NpgsqlCommand(findIdInstitutionalString, connection);
                    using (NpgsqlDataReader IdInstitutional = IdInstitution.ExecuteReader())
                    {
                        while (IdInstitutional.Read())
                        {
                            return (IdInstitutional["IdFedNsi"].ToString());
                        }
                    }
                }
            }
            return "";
        }
    }
}
