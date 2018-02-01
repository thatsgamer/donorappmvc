using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class MedicalUpdateStepTwoViewModel
    {
        public MedicalReportUpdate MedicalReportUpdate { get; set; }
        public List<MedicalReportsQuestions> MedicalReportsQuestions { get; set; }
        public List<MedicalQuestions> MedicalQuestions { get; set; }
    }
}