using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KaryalayaMatadan.Models;
using Oracle.ManagedDataAccess.Client;


namespace KaryalayaMatadan.Services
{
    public class DepartmentService
    {
        // connection string for the database
        string constr = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=admin;PASSWORD=admin;";

        // method to Add Department in database
        public void AddDepartment(Department department)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(constr))
                {
                    using (OracleCommand command = new OracleCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = String.Format("INSERT INTO department (department_name, manager_id) " +
                            "VALUES ('{0}','{1}')",
                            department.DepartmentName,
                            department.ManagerID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // method to delete Department from database
        public void DeleteDepartment(int id)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(constr))
                {
                    using (OracleCommand command = new OracleCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = String.Format("DELETE from department WHERE department_id = " + id); ;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // function to update data in department table
        public void EditDepartment(Department department)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(constr))
                {
                    using (OracleCommand command = new OracleCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = String.Format("UPDATE department SET department_name = '{1}', manager_id = '{2}' WHERE department_id = {0}",
                            department.DepartmentID,
                            department.DepartmentName,
                            department.ManagerID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // function to get all data in department table
        public IEnumerable<Department> GetAllDepartment()
        {
            List<Department> departments = new List<Department>();
            using (OracleConnection connection = new OracleConnection(constr))
            {
                using (OracleCommand command = new OracleCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.BindByName = true;
                    command.CommandText = "SELECT department_id, department_name, manager_id FROM department";
                    OracleDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Department department = new Department
                        {
                            DepartmentID = Convert.ToInt32(dataReader["department_id"]),
                            DepartmentName = dataReader["department_name"].ToString(),
                            ManagerID = Convert.ToInt32(dataReader["manager_id"])
                        };
                        departments.Add(department);
                    }
                }
            }
            return departments;
        }

        public Department GetDepartmentById(int id)
        {
            Department department = new Department();
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.BindByName = true;
                    cmd.CommandText = "SELECT department_id, department_name, manager_id FROM department WHERE department_id = " + id;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        department.DepartmentID = Convert.ToInt32(rdr["department_id"]);
                        department.DepartmentName = rdr["department_name"].ToString();
                        department.ManagerID = Convert.ToInt32(rdr["manager_id"]);
                    }
                }
            }
            return department;
        }
    }
}