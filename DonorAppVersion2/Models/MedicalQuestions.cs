using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class MedicalQuestions
    {
        [Key]
        public int MQID { get; set; }
        public string QuestionType { get; set; }
        public string Question { get; set; }
        public string PossibleAnswers { get; set; }
    }
}