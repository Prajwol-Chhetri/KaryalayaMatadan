using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KaryalayaMatadan.Models
{
    public class MonthlyCandidate
    {
        public string CandidateName { get; set; }
        [Key]
        public string CandidateEmail { get; set; }
        public int Votes { get; set; }
        public int VoteYear { get; set; }
        public int VoteMonth { get; set; }
    }
}