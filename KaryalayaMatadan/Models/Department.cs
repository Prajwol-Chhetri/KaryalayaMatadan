﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaryalayaMatadan.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int ManagerID { get; set; }
    }
}