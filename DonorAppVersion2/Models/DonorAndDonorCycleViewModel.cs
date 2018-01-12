using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DonorAppVersion2.Models;

namespace DonorAppVersion2.Models
{
    public class DonorAndDonorCycleViewModel
    {
        public Donor Donor { get; set; }
        public List<Models.DonorCycleEgg> DonorCycleEgg { get; set; }        
    }
}