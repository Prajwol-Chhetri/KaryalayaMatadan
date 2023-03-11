using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KaryalayaMatadan.Models
{
    public class VotingRecord
    {
        [Key]
        public int VoteID { get; set; }
        public int CandidateID { get; set; }
        public string CandidateName { get; set; }
        public string CandidateEmail { get; set; }
        public int VoterID { get; set; }
        public string VoterName { get; set; }
        public string VoterEmail { get; set; }
        public int VoteYear { get; set; }
        public int VoteMonth { get; set; }
        public int VoteDate { get; set; }
    }
}