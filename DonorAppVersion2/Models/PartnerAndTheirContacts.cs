using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class PartnerAndTheirContacts
    {
        [Key]
        public int PartnerContactsId { get; set; }
        public int PartnerId { get; set; }
        public string ContactDesignation { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Partner Partner { get; set; }
    }
}