using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class DonorCycleEgg
    {
        [Key]
        public int DonorCycleId { get; set; }
        public int ParentId { get; set; }
        public string ChildType { get; set; }
        public string CycleType { get; set; }
        public int DonorId { get; set; }

        public string MonthAndYearOfRetrieval { get; set; }
        public string DonorEyeColor { get; set; }
        public string DonorHeight { get; set; }
        public string DonorAge { get; set; }

        public bool isApprovedByParent { get; set; }
        public bool isApprovedByDonor { get; set; }
        
        public virtual Parent Parent { get; set; }
        public virtual Donor Donor { get; set; }
    }
}