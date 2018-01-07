using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class Parent
    {
        [Key]
        public int ParentId { get; set; }

        public string Salutation { get; set; } 

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Contact Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Contact Number (123) 456 7890")]
        public string ContactNumber { get; set; }

        [DisplayName("Contact Verification OTP")]
        public string ContactVerificationCode { get; set; }

        public bool isContactVerified { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DisplayName("Email Verification OTP")]
        public string EmailVerificationCode { get; set; }

        public bool isEmailVerified { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public string DateOfBirth { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }


        public System.DateTime CreatinDate { get; set; }


        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Must be at least 6 Characters long with Symbols & Numbers")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password did not match")]
        public string ConfirmPassword { get; set; }


        public string Salt { get; set; }

        public bool isPaid { get; set; }

        public bool Status { get; set; }

        public string Note { get; set; }
    }
}