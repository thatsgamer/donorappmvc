using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class AdminLoginViewModel
    {
        [Required (ErrorMessage="Email Cannot Be Blank!")]
        [DataType(DataType.EmailAddress, ErrorMessage="Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password!")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength=5, ErrorMessage="Password Must be atleast 6 char long with Symbols and Numbers")]
        public string Password { get; set; }
    }
}