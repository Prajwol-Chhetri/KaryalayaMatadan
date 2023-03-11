using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using KaryalayaMatadan.Models;

namespace KaryalayaMatadan.Services
{
public class VotingRecordService
{
    // connection string for the database
    string constr = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=admin;PASSWORD=admin;";

    public IEnumerable<VotingRecord> GetVotingRecords()
    {
        List<VotingRecord> votingRecords = new List<VotingRecord>();
        using (OracleConnection connection = new OracleConnection(constr))
        {
            using (OracleCommand command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.BindByName = true;
                command.CommandText = @"SELECT v.vote_id, 
                    v.candidate_id, vc.employee_name ""candidate_name"", vc.email ""candidate_email"",
                    v.voter_id, ve.employee_name ""voter_name"", ve.email ""voter_email"",
                    v.voting_year, v.voting_month, v.voting_date
                    FROM VOTE v
                        INNER JOIN EMPLOYEE vc
                        ON v.candidate_id = vc.employee_id
                        INNER JOIN EMPLOYEE ve
                        ON v.voter_id = ve.employee_id";
                OracleDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    VotingRecord votingRecord = new VotingRecord
                    {
                        VoteID = Convert.ToInt32(dataReader["vote_id"]),
                        CandidateID = Convert.ToInt32(dataReader["candidate_id"]),
                        CandidateName = dataReader["candidate_name"].ToString(),
                        CandidateEmail = dataReader["candidate_email"].ToString(),
                        VoterID = Convert.ToInt32(dataReader["voter_id"]),
                        VoterName = dataReader["voter_name"].ToString(),
                        VoterEmail = dataReader["voter_email"].ToString(),
                        VoteYear = Convert.ToInt32(dataReader["voting_year"]),
                        VoteMonth = Convert.ToInt32(dataReader["voting_month"]),
                        VoteDate = Convert.ToInt32(dataReader["voting_date"])
                    };
                    votingRecords.Add(votingRecord);
                }
            }
        }
        return votingRecords;
    }

    public VotingRecord GetVotingRecordById(int id)
    {
        VotingRecord votingRecord = new VotingRecord();
        using (OracleConnection con = new OracleConnection(constr))
        {
            using (OracleCommand cmd = new OracleCommand())
            {
                con.Open();
                cmd.Connection = con;
                cmd.BindByName = true;
                cmd.CommandText = @"SELECT v.vote_id, 
                    v.candidate_id, vc.employee_name ""candidate_name"", vc.email ""candidate_email"",
                    v.voter_id, ve.employee_name ""voter_name"", ve.email ""voter_email"",
                    v.voting_year, v.voting_month, v.voting_date
                    FROM VOTE v
                        INNER JOIN EMPLOYEE vc
                        ON v.candidate_id = vc.employee_id
                        INNER JOIN EMPLOYEE ve
                        ON v.voter_id = ve.employee_id
                    WHERE v.vote_id = " + id;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    votingRecord.VoteID = Convert.ToInt32(rdr["vote_id"]);
                    votingRecord.CandidateID = Convert.ToInt32(rdr["candidate_id"]);
                    votingRecord.CandidateName = rdr["candidate_name"].ToString();
                    votingRecord.CandidateEmail = rdr["candidate_email"].ToString();
                    votingRecord.VoterID = Convert.ToInt32(rdr["voter_id"]);
                    votingRecord.VoterName = rdr["voter_name"].ToString();
                    votingRecord.VoterEmail = rdr["voter_email"].ToString();
                    votingRecord.VoteYear = Convert.ToInt32(rdr["voting_year"]);
                    votingRecord.VoteMonth = Convert.ToInt32(rdr["voting_month"]);
                    votingRecord.VoteDate = Convert.ToInt32(rdr["voting_date"]);
                }
            }
            return votingRecord;
        }
    }
}
}