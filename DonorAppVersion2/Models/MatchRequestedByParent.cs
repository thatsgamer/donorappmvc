using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class MatchRequestedByParent
    {
        [Key]
        public int ParentMatchRequestId { get; set; }
        public int ParentId { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public bool isApproved { get; set; }
        public string Status { get; set; }
        public Nullable<bool> isPaidByParent { get; set; }
        public Nullable<int> ParentsPaymentId { get; set; }
        public string Note { get; set; }

        public List<Parent> Parent { get; set; }
    }
}