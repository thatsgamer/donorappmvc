using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class PartnerDashboardViewModel
    {
        public List<ParentDonorCycleAgencies> ParentDonorCycleAgencies { get; set; }
        public List<PartnerAndTheirContacts> PartnerAndTheirContacts { get; set; }
    }
}