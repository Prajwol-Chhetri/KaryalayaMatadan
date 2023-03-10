using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KaryalayaMatadan.Models;
using Oracle.ManagedDataAccess.Client;


namespace KaryalayaMatadan.Services
{
    public class RoleService
{
    // connection string for the database
    string constr = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=admin;PASSWORD=admin;";

    // method to Add Role in database
    public void AddRole(Role role)
    {
        try
        {
            using (OracleConnection connection = new OracleConnection(constr))
            {
                using (OracleCommand command = new OracleCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = String.Format("INSERT INTO role (role, description) " +
                        "VALUES ('{0}','{1}')",
                        role.RoleName,
                        role.Description);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


    // method to delete Role from database
    public void DeleteRole(int id)
    {
        try
        {
            using (OracleConnection connection = new OracleConnection(constr))
            {
                using (OracleCommand command = new OracleCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = String.Format("DELETE from role WHERE role_id = " + id); ;
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


    // function to update data in role table
    public void EditRole(Role role)
    {
        try
        {
            using (OracleConnection connection = new OracleConnection(constr))
            {
                using (OracleCommand command = new OracleCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = String.Format("UPDATE role SET role = '{1}', description = '{2}' WHERE role_id = {0}",
                        role.RoleID,
                        role.RoleName,
                        role.Description);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


    // function to get all data in role table
    public IEnumerable<Role> GetAllRole()
    {
        List<Role> roles = new List<Role>();
        using (OracleConnection connection = new OracleConnection(constr))
        {
            using (OracleCommand command = new OracleCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.BindByName = true;
                command.CommandText = "SELECT role_id, role, description FROM role";
                OracleDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Role role = new Role
                    {
                        RoleID = Convert.ToInt32(dataReader["role_id"]),
                        RoleName = dataReader["role"].ToString(),
                        Description = dataReader["description"].ToString()
                    };
                    roles.Add(role);
                }
            }
        }
        return roles;
    }

    public Role GetRoleById(int id)
    {
        Role role = new Role();
        using (OracleConnection con = new OracleConnection(constr))
        {
            using (OracleCommand cmd = new OracleCommand())
            {
                con.Open();
                cmd.Connection = con;
                cmd.BindByName = true;
                cmd.CommandText = "SELECT role_id, role, description FROM role WHERE role_id = " + id;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    role.RoleID = Convert.ToInt32(rdr["role_id"]);
                    role.RoleName = rdr["role"].ToString();
                    role.Description = rdr["description"].ToString();
                }
            }
        }
        return role;
    }
}
}