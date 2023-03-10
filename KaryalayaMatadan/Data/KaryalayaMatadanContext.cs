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
    }
}