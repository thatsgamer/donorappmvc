using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class DonorCycle
    {
        [Key]
        public int DonorCycleId { get; set; }
        public int ParentId { get; set; }
        public string ChildType { get; set; }
        public int DonorId { get; set; }
        public string MonthAndYearOfRetrieval { get; set; }
        public string FertilityAttorny { get; set; }
        public string DonorEyeColor { get; set; }
        public string DonorHeight { get; set; }
        public string DonorAge { get; set; }
        public bool isApprovedByParent { get; set; }
        public bool isApprovedByDonor { get; set; }
        public bool isApprovedByClinic { get; set; }
        public bool isApprovedByAgency { get; set; }


        
        public List<Models.Parent> Parent { get; set; }
        public List<Models.Donor> Donor { get; set; }
    }
}