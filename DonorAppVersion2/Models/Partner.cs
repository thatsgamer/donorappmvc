using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class Partner
    {
        [Key]
        public int PartnerId { get; set; }
        public string AssociationType { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string BusinessContact { get; set; }
        public string BusinessWebsite { get; set; }
        public string BusinessEmail { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonContact { get; set; }
        public string ContactPersonEmail { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public DateTime CreationDate { get; set; }
    }
}