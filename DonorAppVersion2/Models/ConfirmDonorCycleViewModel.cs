using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class ConfirmDonorCycleViewModel
    {
        public DonorCycleEgg DonorCycleEgg { get; set; }
        public List<ParentDonorCycleAgencies> ParentDonorCycleAgencies { get; set; }

    }
}