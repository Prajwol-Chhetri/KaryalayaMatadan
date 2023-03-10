using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using KaryalayaMatadan.Models;

namespace KaryalayaMatadan.Services
{
    public class EmployeeJobHistoryService
    {
        // connection string for the database
        string constr = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=admin;PASSWORD=admin;";

        public IEnumerable<EmployeeJobHistory> GetEmployeeJobHistories()
        {
            List<EmployeeJobHistory> employeeJobHistories = new List<EmployeeJobHistory>();
            using (OracleConnection connection = new OracleConnection(constr))
            {
                using (OracleCommand command = new OracleCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.BindByName = true;
                    command.CommandText = @"SELECT e.employee_id, e.employee_name, e.email,
                        d.department_name, 
                        r.role,
                        j.salary, j.start_date, j.end_date, j.status
                        FROM JOB_HISTORY j 
                            INNER JOIN ROLE r 
                            ON r.role_id = j.role_id 
                            INNER JOIN EMPLOYEE e 
                            ON e.employee_id = j.employee_id
                            INNER JOIN DEPARTMENT d
                            ON d.department_id = j.department_id
                        WHERE j.status != 'Working'";
                    OracleDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        EmployeeJobHistory employeeJobHistory = new EmployeeJobHistory
                        {
                            EmployeeID = Convert.ToInt32(dataReader["employee_id"]),
                            Name = dataReader["employee_name"].ToString(),
                            Email = dataReader["email"].ToString(),
                            Department = dataReader["department_name"].ToString(),
                            Role = dataReader["role"].ToString(),
                            Salary = Convert.ToDecimal(dataReader["salary"]),
                            StartDate = Convert.ToDateTime(dataReader["start_date"]),
                            EndDate = Convert.ToDateTime(dataReader["end_date"]),
                            Status = (Status)Enum.Parse(typeof(Status), dataReader["status"].ToString())
                        };
                        employeeJobHistories.Add(employeeJobHistory);
                    }
                }
            }
            return employeeJobHistories;
        }

        public EmployeeJobHistory GetEmployeeJobHistoryById(int id)
        {
            EmployeeJobHistory employeeJobHistory = new EmployeeJobHistory();
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.BindByName = true;
                    cmd.CommandText = @"SELECT e.employee_id, e.employee_name, e.email,
                        d.department_name, 
                        r.role,
                        j.salary, j.start_date, j.end_date, j.status
                        FROM JOB_HISTORY j 
                            INNER JOIN ROLE r 
                            ON r.role_id = j.role_id 
                            INNER JOIN EMPLOYEE e 
                            ON e.employee_id = j.employee_id
                            INNER JOIN DEPARTMENT d
                            ON d.department_id = j.department_id
                        WHERE j.status != 'Working'
                        AND j.employee_id = " + id;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        employeeJobHistory.EmployeeID = Convert.ToInt32(rdr["employee_id"]);
                        employeeJobHistory.Name = rdr["employee_name"].ToString();
                        employeeJobHistory.Email = rdr["email"].ToString();
                        employeeJobHistory.Department = rdr["department_name"].ToString();
                        employeeJobHistory.Role = rdr["role"].ToString();
                        employeeJobHistory.Salary = Convert.ToDecimal(rdr["salary"]);
                        employeeJobHistory.StartDate = Convert.ToDateTime(rdr["start_date"]);
                        employeeJobHistory.EndDate = Convert.ToDateTime(rdr["end_date"]);
                        employeeJobHistory.Status = (Status)Enum.Parse(typeof(Status), rdr["status"].ToString());
                    }
                }
                return employeeJobHistory;
            }
        }
    }
}