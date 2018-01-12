using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class ParentDonorCycleAgencies
    {
        [Key]
        public int PDCAID { get; set; }
        public int DonorCycleId { get; set; }
        public int PartnerContactsId { get; set; }
        public bool isApprovedByPartner { get; set; }
        public string Reason { get; set; }

        public virtual DonorCycleEgg DonorCycleEgg { get; set; }
        public virtual PartnerAndTheirContacts PartnerAndTheirContacts { get; set; }
    }
}