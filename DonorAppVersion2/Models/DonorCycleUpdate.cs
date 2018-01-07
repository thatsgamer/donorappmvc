using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class DonorCycleUpdate
    {
        [Key]
        public int DonorCycleUpdateId { get; set; }
        public int DonorCycleId { get; set; }
        public string UpdateHeading { get; set; }
        public string UpdateDescription { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool isSubmitted { get; set; }
        public bool isCompleted { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }

        public List<DonorCycle> DonorCycle { get; set; }
    }
}