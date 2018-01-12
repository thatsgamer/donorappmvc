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

        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public string BusinessContact { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Invalid Website URL")]
        public string BusinessWebsite { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string BusinessEmail { get; set; }
        public string ContactPersonName { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public string ContactPersonContact { get; set; }


        [DataType(DataType.EmailAddress, ErrorMessage="Invalid Email Address")]
        public string ContactPersonEmail { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength=6, ErrorMessage="Password Must be Atleast 6 Char Long with atleast 1 Symbol and Number")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Password did not match")]

        public string ConfirmPassword { get; set; }
        public DateTime CreationDate { get; set; }

        public bool isActive { get; set; }
    }
}