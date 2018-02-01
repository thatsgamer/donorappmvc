using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class MedicalReportUpdate
    {
        [Key]
        public int MedicalReportId { get; set; }
        public int DonorCycleId { get; set; }
        public string UpdateHeading { get; set; }
        public string UpdateDescription { get; set; }
        public DateTime DateOfSubmission { get; set; }
        public string Status { get; set; }

        public virtual List<MedicalReportsQuestions> MedicalReportsQuestions { get; set; }
    }
}