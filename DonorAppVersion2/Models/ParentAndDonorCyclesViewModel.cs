using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DonorAppVersion2.Models;

namespace DonorAppVersion2.Models
{
    public class ParentAndDonorCyclesViewModel
    {
        public Parent Parent { get; set; }
        public List<Models.DonorCycle> DonorCycle { get; set; }        
    }
}