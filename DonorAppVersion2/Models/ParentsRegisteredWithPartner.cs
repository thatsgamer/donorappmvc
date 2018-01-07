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
        public int PartnerContactsId { get; set; }
        public bool isApproved { get; set;}
        public DateTime DateOfApproval { get; set; }

        public string Status { get; set; }
        public string  ParentIdOnPartnersSystem { get; set; }


        public List<Parent> Parent { get; set; }
        public List<PartnerAndTheirContacts> PartnersAndTheirContacts { get; set; }
    }
}