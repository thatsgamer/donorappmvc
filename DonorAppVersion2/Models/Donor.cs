using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class Donor
    {
        [Key]
        public int DonorId { get; set; }

        public string Salutation { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Contact Number (123) 456 7890")]
        public string ContactNumber { get; set; }

        [Display(Name = "Contact Verification OTP")]
        public string ContactVerificationCode { get; set; }

        public bool isContactVerified { get; set; }



        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Email Verification OTP")]
        public string EmailVerificationCode { get; set; }

        public bool isEmailVerified { get; set; }


        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Eye Color")]
        public string EyeColor { get; set; }

        [Range(50, 500, ErrorMessage = "Invalid Height")]
        public int Height { get; set; }

        public string Race { get; set; }

        public string Photo { get; set; }

        public System.DateTime CreationDate { get; set; }

        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Must be at least 6 Characters long with Symbols & Numbers")]
        public string Password { get; set; }


        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password did not match")]
        public string ConfirmPassword { get; set; }

        public string Salt { get; set; }

        public string DonorType { get; set; }

        public string Gender { get; set; }

    }
}