using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class DonorVerificationViewModel
    {
        public int DonorId { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        
        [Display(Name = "Email Verification OTP")]
        public string EmailVerificationCode { get; set; }

        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Contact Number (123) 456 7890")]
        public string ContactNumber { get; set; }

        [Display(Name = "Contact Verification OTP")]
        public string ContactVerificationCode { get; set; }

        public bool isContactVerified { get; set; }

        public bool isEmailVerified { get; set; }
    }
}