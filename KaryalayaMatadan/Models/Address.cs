using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaryalayaMatadan.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}