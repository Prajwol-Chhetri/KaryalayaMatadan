using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using KaryalayaMatadan.Models;

namespace KaryalayaMatadan.Services
{
    public class MonthlyCandidateService
    {
        // connection string for the database
        string constr = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=admin;PASSWORD=admin;";

        public IEnumerable<MonthlyCandidate> GetMonthlyCandidates()
        {
            List<MonthlyCandidate> monthlyCandidates = new List<MonthlyCandidate>();
            using (OracleConnection connection = new OracleConnection(constr))
            {
                using (OracleCommand command = new OracleCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.BindByName = true;
                    command.CommandText = @"SELECT * FROM (
                            SELECT vc.employee_name ""candidate_name"", vc.email ""candidate_email"",
                            v.voting_year, v.voting_month,
                            COUNT(v.candidate_id) ""votes""
                            FROM VOTE v
                                INNER JOIN EMPLOYEE vc
                                ON v.candidate_id = vc.employee_id
                            WHERE v.voting_year = '2023' AND v.voting_month = '1'
                            GROUP BY vc.employee_name, vc.email, v.voting_year, v.voting_month
                            ORDER BY COUNT(v.candidate_id) DESC)
                        WHERE ROWNUM <= 3";
                    OracleDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        MonthlyCandidate monthlyCandidate = new MonthlyCandidate
                        {
                            CandidateName = dataReader["candidate_name"].ToString(),
                            CandidateEmail = dataReader["candidate_email"].ToString(),
                            Votes = Convert.ToInt32(dataReader["votes"]),
                            VoteYear = Convert.ToInt32(dataReader["voting_year"]),
                            VoteMonth = Convert.ToInt32(dataReader["voting_month"]),
                        };
                        monthlyCandidates.Add(monthlyCandidate);
                    }
                }
            }
            return monthlyCandidates;
        }
    }
}