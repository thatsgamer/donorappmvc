using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class MedicalReportsQuestions
    {
        [Key]
        public int RQID { get; set; }
        public int MedicalReportId { get; set; }
        public int MQID { get; set; }
        public string SelectedAnswers { get; set; }
        public string EnteredAnswer { get; set; }

        public virtual MedicalQuestions MedicalQuestions { get; set; }
    }
}