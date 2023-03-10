using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaryalayaMatadan.Models
{
    public enum Status
    {
        Working,
        Resigned,
        Pending
    }
    public class JobHistory
    {
        public int JobHistoryID { get; set; }
        public decimal Salary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Status Status { get; set; }
        public int EmployeeID { get; set; }
        public int DepartmentID { get; set; }
        public int RoleID { get; set; }
    }
}