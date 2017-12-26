using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class DonorLoginViewModel
    {
        [Required(ErrorMessage="Email cannot be blank!")]
        [DataType(DataType.EmailAddress, ErrorMessage="Invalid Email ID")]
        public string Email { get; set; }

        [Required(ErrorMessage="Password cannot be Blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}