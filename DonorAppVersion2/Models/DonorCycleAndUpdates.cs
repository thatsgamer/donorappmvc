using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorAppVersion2.Models
{
    public class DonorCycleAndUpdates
    {
        public DonorCycle DonorCycle { get; set; }
        public List<Models.DonorCycleUpdate> DonorCycleUpdate { get; set; }        
    }
}