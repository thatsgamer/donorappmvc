using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class ParentAgenciesViewModel
    {
        public int ParentPartnerId { get; set; }
        public int ParentId { get; set; }
        public int PartnerContactsId { get; set; }
        public bool isApproved { get; set; }
        public Nullable<DateTime> DateOfApproval { get; set; }
        public string Status { get; set; }
        public string ParentIdOnPartnersSystem { get; set; }


        public string ContactsName { get; set; }
        public string ContactsDesignation { get; set; }
        public string PartnerName { get; set; }
        public string PartnerType { get; set; }


    }
}