using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KaryalayaMatadan.Models;
using Oracle.ManagedDataAccess.Client;


namespace KaryalayaMatadan.Services
{
    public class EmployeeService
    {
        // connection string for the database
        string constr = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=admin;PASSWORD=admin;";

        // method to Add Employee in database
        public void AddEmployee(Employee employee)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(constr))
                {
                    using (OracleCommand command = new OracleCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = String.Format("INSERT INTO employee (employee_name, date_of_birth, phone, email) " +
                            "VALUES ('{0}','{1}','{2}',{3})",
                            employee.EmployeeName,
                            employee.DateOfBirth,
                            employee.Phone,
                            employee.Email);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // method to delete Employee from database
        public void DeleteEmployee(int id)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(constr))
                {
                    using (OracleCommand command = new OracleCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = String.Format("DELETE from employee WHERE employee_id = " + id); ;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // function to update data in employee table
        public void EditEmployee(Employee employee)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(constr))
                {
                    using (OracleCommand command = new OracleCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = String.Format("UPDATE employee SET employee_name = '{1}', date_of_birth = '{2}', phone = '{3}', email = {4} WHERE employee_id = {0}",
                            employee.EmployeeID,
                            employee.EmployeeName,
                            employee.DateOfBirth,
                            employee.Phone,
                            employee.Email);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // function to get all data in employee table
        public IEnumerable<Employee> GetAllEmployee()
        {
            List<Employee> employees = new List<Employee>();
            using (OracleConnection connection = new OracleConnection(constr))
            {
                using (OracleCommand command = new OracleCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.BindByName = true;
                    command.CommandText = "SELECT employee_id, employee_name, date_of_birth, phone, email FROM employee";
                    OracleDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Employee employee = new Employee
                        {
                            EmployeeID = Convert.ToInt32(dataReader["employee_id"]),
                            EmployeeName = dataReader["employee_name"].ToString(),
                            DateOfBirth = Convert.ToDateTime(dataReader["date_of_birth"]),
                            Phone = Int64.Parse(dataReader["phone"].ToString()),
                            Email = dataReader["email"].ToString()
                        };
                        employees.Add(employee);
                    }
                }
            }
            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.BindByName = true;
                    cmd.CommandText = "SELECT employee_id, employee_name, date_of_birth, phone, email FROM employee WHERE employee_id = " + id;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        employee.EmployeeID = Convert.ToInt32(rdr["employee_id"]);
                        employee.EmployeeName = rdr["employee_name"].ToString();
                        employee.DateOfBirth = Convert.ToDateTime(rdr["date_of_birth"]);
                        employee.Phone = Int64.Parse(rdr["phone"].ToString());
                        employee.Email = rdr["email"].ToString();
                    }
                }
            }
            return employee;
        }
    }
}