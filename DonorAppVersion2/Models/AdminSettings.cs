using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class AdminSettings
    {
        [Key]
        public int SettingId { get; set; }
        public int ParentRegistrationCharges { get; set; }
        public int ParentNewDonorCycleCharges { get; set; }
    }
}