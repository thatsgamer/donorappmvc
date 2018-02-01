using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class PotentialMatches
    {
        [Key]
        public int PMID { get; set; }
        public string ParentName { get; set; }
        public string ParentContactNumber { get; set; }
        public string ParentEmail { get; set; }
        public string DonorName { get; set; }
        public string DonorContactNumber { get; set; }
        public string DonorEmail { get; set; }
        public string DonorType { get; set; }
        public string DonorGender { get; set; }
        public string MatchStatus { get; set; }
        public int PartnerId { get; set; }

        public virtual Partner Partner { get; set; }
    }
}