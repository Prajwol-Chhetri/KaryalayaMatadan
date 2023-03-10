using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaryalayaMatadan.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
    }
}