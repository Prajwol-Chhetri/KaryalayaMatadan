using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KaryalayaMatadan.Models;
using Oracle.ManagedDataAccess.Client;


namespace KaryalayaMatadan.Services
{
    public class JobHistoryService
    {
        // connection string for the database
        string constr = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=admin;PASSWORD=admin;";

        // method to Add JobHistory in database
        public void AddJobHistory(JobHistory JobHistory)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(constr))
                {
                    using (OracleCommand command = new OracleCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = String.Format("INSERT INTO JOB_HISTORY (salary, start_date, end_date, status, employee_id, department_id, role_id) " +
                            "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                            JobHistory.Salary,
                            JobHistory.StartDate.ToString("dd-MMM-yyyy"),
                            JobHistory.EndDate?.ToString("dd-MMM-yyyy"),
                            JobHistory.Status,
                            JobHistory.EmployeeID,
                            JobHistory.DepartmentID,
                            JobHistory.RoleID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // method to delete JobHistory from database
        public void DeleteJobHistory(int id)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(constr))
                {
                    using (OracleCommand command = new OracleCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = String.Format("DELETE from JOB_HISTORY WHERE job_history_id = " + id); ;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // function to update data in JobHistory table
        public void EditJobHistory(JobHistory JobHistory)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(constr))
                {
                    using (OracleCommand command = new OracleCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = String.Format("UPDATE JOB_HISTORY SET salary = '{1}', start_date = '{2}', end_date = '{3}', status = '{4}', employee_id = '{5}', department_id = '{6}', role_id = '{7}' WHERE job_history_id = {0}",
                            JobHistory.JobHistoryID,
                            JobHistory.Salary,
                            JobHistory.StartDate.ToString("dd-MMM-yyyy"),
                            JobHistory.EndDate?.ToString("dd-MMM-yyyy"),
                            JobHistory.Status,
                            JobHistory.EmployeeID,
                            JobHistory.DepartmentID,
                            JobHistory.RoleID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // function to get all data in JobHistory table
        public IEnumerable<JobHistory> GetAllJobHistory()
        {
            List<JobHistory> JobHistories = new List<JobHistory>();
            using (OracleConnection connection = new OracleConnection(constr))
            {
                using (OracleCommand command = new OracleCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.BindByName = true;
                    command.CommandText = "SELECT job_history_id, salary, start_date, end_date, status, employee_id, department_id, role_id FROM JOB_HISTORY";
                    OracleDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        JobHistory JobHistory = new JobHistory
                        {
                            JobHistoryID = Convert.ToInt32(dataReader["job_history_id"]),
                            Salary = Convert.ToDecimal(dataReader["salary"]),
                            StartDate = Convert.ToDateTime(dataReader["start_date"]),
                            Status = (Status)Enum.Parse(typeof(Status), dataReader["status"].ToString()),
                            EmployeeID = Convert.ToInt32(dataReader["employee_id"]),
                            DepartmentID = Convert.ToInt32(dataReader["department_id"]),
                            RoleID = Convert.ToInt32(dataReader["role_id"]),
                        };
                        var endDate = dataReader["end_date"];
                        if (!(endDate is DBNull))
                            JobHistory.EndDate = Convert.ToDateTime(endDate);
                        JobHistories.Add(JobHistory);
                    }
                }
            }
            return JobHistories;
        }

        public JobHistory GetJobHistoryById(int id)
        {
            JobHistory JobHistory = new JobHistory();
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.BindByName = true;
                    cmd.CommandText = "SELECT job_history_id, salary, start_date, end_date, status, employee_id, department_id, role_id FROM JOB_HISTORY WHERE job_history_id = " + id;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        JobHistory.JobHistoryID = Convert.ToInt32(rdr["job_history_id"]);
                        JobHistory.Salary = Convert.ToDecimal(rdr["salary"]);
                        JobHistory.StartDate = Convert.ToDateTime(rdr["start_date"]);
                        JobHistory.Status = (Status)Enum.Parse(typeof(Status), rdr["status"].ToString());
                        JobHistory.EmployeeID = Convert.ToInt32(rdr["employee_id"]);
                        JobHistory.DepartmentID = Convert.ToInt32(rdr["department_id"]);
                        JobHistory.RoleID = Convert.ToInt32(rdr["role_id"]);
                        var endDate = rdr["end_date"];
                        if (!(endDate is DBNull))
                            JobHistory.EndDate = Convert.ToDateTime(endDate);
                    }
                }
            }
            return JobHistory;
        }
    }
}