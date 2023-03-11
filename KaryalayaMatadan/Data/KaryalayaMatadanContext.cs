using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace KaryalayaMatadan.Data
{
    public class KaryalayaMatadanContext : DbContext
    {
        public KaryalayaMatadanContext() : base("name=KaryalayaMatadanContext")
        {
        }

        public System.Data.Entity.DbSet<KaryalayaMatadan.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<KaryalayaMatadan.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<KaryalayaMatadan.Models.Address> Addresses { get; set; }

        public System.Data.Entity.DbSet<KaryalayaMatadan.Models.Role> Roles { get; set; }

        public System.Data.Entity.DbSet<KaryalayaMatadan.Models.JobHistory> JobHistories { get; set; }

        public System.Data.Entity.DbSet<KaryalayaMatadan.Models.EmployeeJobHistory> EmployeeJobHistories { get; set; }

        public System.Data.Entity.DbSet<KaryalayaMatadan.Models.VotingRecord> VotingRecords { get; set; }
    }
}