using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class ParentsRegisteredWithPartner
    {
        [Key]
        public int ParentPartnerId { get; set; }
        public int ParentId { get; set; }
        
        [Required(ErrorMessage="Plese Select Contact Person")]
        public int PartnerContactsId { get; set; }
        public bool isApproved { get; set;}
        public Nullable<DateTime> DateOfApproval { get; set; }

        public string Status { get; set; }
        public string  ParentIdOnPartnersSystem { get; set; }

        public virtual Parent Parent { get; set; }
        public virtual PartnerAndTheirContacts PartnerAndTheirContacts { get; set; }
    }
}