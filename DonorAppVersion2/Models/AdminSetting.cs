//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DonorAppVersion2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdminSetting
    {
        public int SettingId { get; set; }
        public double RegistrationChargesForParent { get; set; }
        public double AnnualRenewalChargesForParent { get; set; }
        public double NewDonorProfileChargesForParents { get; set; }
        public double ProfileCompletedChargesForDonor { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}
