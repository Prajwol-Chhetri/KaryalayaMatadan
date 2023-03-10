using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KaryalayaMatadan.Models;
using Oracle.ManagedDataAccess.Client;


namespace KaryalayaMatadan.Services
{
    public class AddressService
    {
        // connection string for the database
        string constr = "DATA SOURCE=localhost:1521/xe;PERSIST SECURITY INFO=True;USER ID=admin;PASSWORD=admin;";

        // method to Add Address in database
        public void AddAddress(Address address)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(constr))
                {
                    using (OracleCommand command = new OracleCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = String.Format("INSERT INTO address (state, city, street) " +
                            "VALUES ('{0}','{1}', '{2}')",
                            address.State,
                            address.City,
                            address.Street);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // method to delete Address from database
        public void DeleteAddress(int id)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(constr))
                {
                    using (OracleCommand command = new OracleCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = String.Format("DELETE from address WHERE address_id = " + id); ;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // function to update data in address table
        public void EditAddress(Address address)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(constr))
                {
                    using (OracleCommand command = new OracleCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = String.Format("UPDATE address SET state = '{1}', city = '{2}', street = '{3}' WHERE address_id = {0}",
                            address.AddressID,
                            address.State,
                            address.City,
                            address.Street);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // function to get all data in address table
        public IEnumerable<Address> GetAllAddress()
        {
            List<Address> addresses = new List<Address>();
            using (OracleConnection connection = new OracleConnection(constr))
            {
                using (OracleCommand command = new OracleCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.BindByName = true;
                    command.CommandText = "SELECT address_id, state, city, street FROM address";
                    OracleDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Address address = new Address
                        {
                            AddressID = Convert.ToInt32(dataReader["address_id"]),
                            State = dataReader["state"].ToString(),
                            City = dataReader["city"].ToString(),
                            Street = dataReader["street"].ToString()
                        };
                        addresses.Add(address);
                    }
                }
            }
            return addresses;
        }

        public Address GetAddressById(int id)
        {
            Address address = new Address();
            using (OracleConnection con = new OracleConnection(constr))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.BindByName = true;
                    cmd.CommandText = "SELECT address_id, state, city, street FROM address WHERE address_id = " + id;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        address.AddressID = Convert.ToInt32(rdr["address_id"]);
                        address.State = rdr["state"].ToString();
                        address.City = rdr["city"].ToString();
                        address.Street = rdr["street"].ToString();
                    }
                }
            }
            return address;
        }
    }
}