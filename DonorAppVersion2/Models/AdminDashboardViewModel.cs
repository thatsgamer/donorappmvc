using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class AdminDashboardViewModel
    {
        public List<DonorCycleEgg> DonorCycleEgg { get; set; }
        public List<Parent> Parent { get; set; }
        public List<Donor> Donor { get; set; }
        public List<Partner> Partner { get; set; }
        public List<ParentPayments> ParentPayments { get; set; }

    }
}