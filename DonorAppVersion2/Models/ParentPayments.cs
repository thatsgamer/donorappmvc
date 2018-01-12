using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class ParentPayments
    {
        [Key]
        public int PaymentId { get; set; }
        public int ParentId { get; set; }
        public string PaymentDescription { get; set; }
        public int Amount { get; set; }
        public DateTime CreationDate { get; set; }
        public Nullable<DateTime> TransactionDate { get; set; } 
        public bool TransactionStatus { get; set; } //for Deciding Pending Payments
        public bool PaymentStatus { get; set; } // For knowing Payment Status (Success or failed)
        public Nullable<bool> TransactionId { get; set; }
        public string Error { get; set; }

        public Parent Parnet { get; set; }
    }

}